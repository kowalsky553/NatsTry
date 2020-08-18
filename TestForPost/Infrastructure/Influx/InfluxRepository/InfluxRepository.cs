using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models;
using InfluxData.Net.InfluxDb.Models.Responses;
using Infrastructure.Influx.InfluxDatabaseManager;

namespace Infrastructure.Influx.InfluxRepository
{
	class InfluxRepository : IInfluxRepository
	{
		private IInfluxDbClient _client = Ninject.Ninject.Get<IInfluxDbClient>();

		private IInfluxDatabaseManager _databaseManager;

		private string _dbName;

		public InfluxRepository(string dbName)
		{
			_databaseManager = Ninject.Ninject.Get<IInfluxDatabaseManager>();
			_databaseManager.InitDatabase(dbName);
			_dbName = dbName;
		}

		public async void WritePoint(Point point)
		{
			await _client.Client.WriteAsync(point, _dbName);
		}

		public async Task<IEnumerable<Serie>> GetSerie(string measurmentName, string query)
		{
			if (query == null)
			{
				return await _client.Client.QueryAsync($"select * from {measurmentName}");
			}
			return await _client.Client.QueryAsync(query);
		}

		public void Dispose()
		{
		}
	}
}
