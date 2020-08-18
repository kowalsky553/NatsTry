using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using DomainModel;
using DomainModel.Message;
using Infrastructure.DbContext;
using IMessage = DomainModel.Message.IMessage;

namespace Infrastructure.MessageFactories
{
	class PublisherMessageFactory<T> : IMessageFactory<T> where T : IMessage
	{
		public IMessage CreateMessage(int currentLineNumber)
		{
			try
			{
				using (var ctx = new PublisherContext("DefaultConnection"))
				{
					var dbMessage = ctx.Messages.FirstOrDefault(m => m.Id == currentLineNumber % ctx.Messages.Count());
					var result = new PublisherMessage()
					{
						Id = dbMessage.Id,
						Content = dbMessage.Content,
						SendTime = DateTime.Now,
						DatabaseHash = currentLineNumber
					};
					return result;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		}
	}
}
