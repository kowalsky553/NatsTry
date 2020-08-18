using System.Runtime.Remoting.Messaging;
using DomainModel;
using DomainModel.Message;
using IMessage = DomainModel.Message.IMessage;

namespace Infrastructure.MessageFactories
{
	interface IMessageFactory<in  T> where T : IMessage
	{
		IMessage CreateMessage(int currentLineNumber);
	}
}
