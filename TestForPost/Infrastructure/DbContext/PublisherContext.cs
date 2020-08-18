using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Message;

namespace Infrastructure.DbContext
{
	public class PublisherContext : BaseContext
	{
		public PublisherContext(string subject) : base(subject)
		{

		}

		public DbSet<PublisherMessage> Messages { get; set; }

		public override async Task<bool> WriteMessageAsync(IMessage item)
		{
			Messages.Add((PublisherMessage)item);
			await base.SaveChangesAsync();
			return true;
		}
	}
}
