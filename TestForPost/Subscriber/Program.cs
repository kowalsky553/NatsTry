﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DomainModel.Enums;
using Infrastructure.NatsClient;
using Infrastructure.Ninject;

namespace Subscriber
{
	class Program
	{
		static void Main(string[] args)
		{
			Ninject.InitKernel(NatsClientTypeEnum.Subscriber);
			var publisher = Ninject.Get<INatsClient>();
			var task = publisher.Run("foo");
			task.Start();
		}
	}
}
