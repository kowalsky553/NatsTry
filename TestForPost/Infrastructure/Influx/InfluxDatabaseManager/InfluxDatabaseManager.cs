using System.Linq;
using System.Net;
using InfluxData.Net.Common.Infrastructure;
using InfluxData.Net.InfluxDb;

namespace Infrastructure.Influx.InfluxDatabaseManager
{
	public class InfluxDatabaseManager  : IInfluxDatabaseManager
	{
		private IInfluxDbClient _client = Ninject.Ninject.Get<IInfluxDbClient>();

		public void InitDatabase(string dbName)
		{
			var database = _client.Database.GetDatabasesAsync().Result.FirstOrDefault(db => db.Name == dbName);
			if (database == null)
			{
				var responce = _client.Database.CreateDatabaseAsync(dbName).Result;
				if (!responce.Success)
				{
					throw new InfluxDataApiException(HttpStatusCode.BadRequest, "Не удалось создать базу данных");
				}
			}
		}
	}
}