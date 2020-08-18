using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Message;

namespace DomainModel
{
	[Table("Message")]
    public class PublisherMessage : IMessage
    {
	    public int Id { get; set; }
	    public string Content { get; set; }
		[NotMapped]
	    public DateTime SendTime { get; set; }
		[NotMapped]
	    public int DatabaseHash { get; set; }

	    public Dictionary<string, object> ToDictionary()
	    {
		    var result = new Dictionary<string, object>()
		    {
			    {"Id", Id},
			    {"Content", Content},
			    {"SendTime", SendTime},
			    {"DatabaseHash", DatabaseHash}
		    };
		    return result;
	    }
    }
}
