using System;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DomainModel.Message;
using Infrastructure.Helpers;
using Infrastructure.MessageFactories;
using NATS.Client;
using Newtonsoft.Json;

namespace Infrastructure.NatsClient
{
	public class NatsPublisher : INatsClient
	{
		public bool IsConnected
		{
			get
			{
				return _connection != null && _connection.State == ConnState.CONNECTED;
			}
		}

		#region Private Properties

		private IConnection _connection;

		private IMessageFactory<IMessage> _messageFactory;

		private IIteratorHelper _iteratorHelper;

		#endregion

		public NatsPublisher(string subjectName)
		{
			SubjectName = subjectName;
			_messageFactory = Ninject.Ninject.Get<IMessageFactory<IMessage>>();
			_iteratorHelper = Ninject.Ninject.Get<IIteratorHelper>();
		}

		public string SubjectName { get; set; }

		public async Task Run(string subject)
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
					var newMessage = _messageFactory.CreateMessage(_iteratorHelper.CurrentIterator);

					_connection.Publish(subject, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newMessage)));
					_iteratorHelper.CurrentIterator++;

					Console.WriteLine("Message has been sent");
					Thread.Sleep(1000);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
			_connection.Close();
		}

		public void Reconnect(string subject)
		{
			var cf = new ConnectionFactory();

			while (!IsConnected)
			{
				try
				{
					_connection = cf.CreateConnection();
				}
				catch (Exception ex)
				{
					Console.WriteLine("Connecting Failed");
					Thread.Sleep(1000);
				}
			}
		}

	}
}
