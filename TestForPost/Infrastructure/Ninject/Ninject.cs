using System.Configuration;
using DomainModel.Enums;
using DomainModel.Message;
using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using Infrastructure.Helpers;
using Infrastructure.Influx.InfluxDatabaseManager;
using Infrastructure.Influx.InfluxRepository;
using Infrastructure.Influx.PointFactory;
using Infrastructure.MessageFactories;
using Infrastructure.NatsClient;
using Ninject;

namespace Infrastructure.Ninject
{
	public static class Ninject
	{
		private static IKernel _kernel;

		public static void InitKernel(NatsClientTypeEnum clientTypeEnum)
		{
			if (_kernel != null)
				return;

			_kernel = new StandardKernel();
			_kernel.Bind<IMessageFactory<IMessage>>().To<PublisherMessageFactory<IMessage>>();
			_kernel.Bind<IPointFactory>().To<PointFactory>();
			_kernel.Bind<IIteratorHelper>().To<IteratorHelper>();
			_kernel.Bind<IInfluxDatabaseManager>().To<InfluxDatabaseManager>();

			if (clientTypeEnum == NatsClientTypeEnum.Publisher)
			{
				_kernel.Bind<INatsClient>().ToConstant(new NatsPublisher(ConfigurationManager.AppSettings["SubjectName"]));
			}
			else
			{
				_kernel.Bind<IInfluxDbClient>()
					.ToConstant(new InfluxDbClient(ConfigurationManager.AppSettings["InfluxUri"], "username", "password", InfluxDbVersion.Latest));

				_kernel.Bind<IInfluxRepository>()
					.ToConstant(new InfluxRepository(ConfigurationManager.AppSettings["InfluxDatabaseName"]));

				_kernel.Bind<INatsClient>().ToConstant(new NatsSubscriber(ConfigurationManager.AppSettings["SubjectName"]));
			}
		}

		public static T Get<T>()
		{
			return _kernel.Get<T>();
		}
	}
}
