using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxData.Net.InfluxDb.Models;

namespace Infrastructure.Influx.PointFactory
{
	interface IPointFactory
	{
		Point GetPoint(string measurmentName, Dictionary<string, object> dictionary);
	}
}
