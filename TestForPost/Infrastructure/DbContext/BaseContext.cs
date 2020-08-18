using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Message;

namespace Infrastructure.DbContext
{
	public abstract class BaseContext : System.Data.Entity.DbContext
	{
		public BaseContext(string subject) : base(subject)
		{

		}
		
		public abstract Task<bool> WriteMessageAsync(IMessage item);
	}
}
