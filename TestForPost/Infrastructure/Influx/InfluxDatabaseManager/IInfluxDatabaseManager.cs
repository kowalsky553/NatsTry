using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Influx.InfluxDatabaseManager
{
	interface IInfluxDatabaseManager
	{
		void InitDatabase(string dbName);
	}
}
