using System;
using System.Threading;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Message;
using Infrastructure.Influx.InfluxRepository;
using Infrastructure.Influx.PointFactory;
using NATS.Client;
using Newtonsoft.Json;

namespace Infrastructure.NatsClient
{
	public class NatsSubscriber : INatsClient
	{

		#region PrivateProperties

		private IConnection _connection;

		private readonly IPointFactory _factory;

		private ISyncSubscription _subscription;

		#endregion

		public NatsSubscriber(string subjectName)
		{
			SubjectName = subjectName;
			_factory = Ninject.Ninject.Get<IPointFactory>();
		}

		public bool IsConnected
		{
			get
			{
				return _connection != null && _connection.State == ConnState.CONNECTED;
			}
		}

		public string SubjectName { get; set; }

		public async Task Run(string subject)
		{
			try
			{
				var cancelationTokenSource = new CancellationTokenSource();
				while (!cancelationTokenSource.Token.IsCancellationRequested)
				{
					try
					{
						if (!IsConnected)
						{
							Reconnect(subject);
						}

						var responseMessage = JsonConvert.DeserializeObject<PublisherMessage>(System.Text.Encoding.Default.GetString(_subscription.NextMessage(1200).Data));
						Console.WriteLine("Message has been received");
						WriteMessageToDatabaseAsync(responseMessage);
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}
				_connection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public void Reconnect(string subject)
		{
			ConnectionFactory cf = new ConnectionFactory();

			while (!IsConnected)
			{
				try
				{
					_connection = cf.CreateConnection();
					_subscription = _connection.SubscribeSync(subject);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Connecting Failed");
					Thread.Sleep(1000);
				}
			}
		}

		private async void WriteMessageToDatabaseAsync(IMessage baseMessage)
		{
			using (var repository = Ninject.Ninject.Get<IInfluxRepository>())
			{
				repository.WritePoint(_factory.GetPoint("Messages", baseMessage.ToDictionary()));
			}
		}
	}
}
