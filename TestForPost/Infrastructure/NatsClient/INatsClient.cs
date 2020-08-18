using System.Threading.Tasks;

namespace Infrastructure.NatsClient
{
	public interface INatsClient
	{
		Task Run(string subject);

		void Reconnect(string subject);

		bool IsConnected { get; }

		string SubjectName { get; set; }
	}
}
