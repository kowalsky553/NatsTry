using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxData.Net.InfluxDb.Models;

namespace Infrastructure.Influx.PointFactory
{
	class PointFactory : IPointFactory
	{
		public Point GetPoint(string measurmentName, Dictionary<string, object> dictionary)
		{
			var result = new Point()
			{
				Name = measurmentName,
				Fields = dictionary
					.Except(dictionary.Where(p => p.Key.EndsWith("id", StringComparison.OrdinalIgnoreCase)))
					.ToDictionary(x => x.Key, x => x.Value),
				Tags = dictionary
					.Where(p => p.Key.EndsWith("id", StringComparison.OrdinalIgnoreCase))
					.ToDictionary(x => x.Key, x => x.Value)
			};
			return result;
		}
	}
}
