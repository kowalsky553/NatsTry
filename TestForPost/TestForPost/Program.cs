using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DomainModel.Enums;
using Infrastructure.NatsClient;
using Infrastructure.Ninject;

namespace TestForPost
{
	class Program
	{
		static void Main()
		{
			Ninject.InitKernel(NatsClientTypeEnum.Publisher);
			var publisher = Ninject.Get<INatsClient>();
			var task = publisher.Run("foo");
			task.Start();

		}
	}
}
