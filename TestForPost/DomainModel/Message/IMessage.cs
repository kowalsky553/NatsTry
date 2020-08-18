using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Message
{

	public interface IMessage
	{
		[Key]
		int Id { get; set; }

		string Content { get; set; }

		DateTime SendTime { get; set; }

		int DatabaseHash { get; set; }

		Dictionary<string, object> ToDictionary();
	}
}
