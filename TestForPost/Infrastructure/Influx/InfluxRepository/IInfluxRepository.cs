using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxData.Net.InfluxDb.Models;
using InfluxData.Net.InfluxDb.Models.Responses;

namespace Infrastructure.Influx.InfluxRepository
{
	interface IInfluxRepository : IDisposable
	{
		void WritePoint(Point point);

		Task<IEnumerable<Serie>> GetSerie(string measurmentName, string query);
	}
}
