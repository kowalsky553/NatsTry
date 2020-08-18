using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
	public class IteratorHelper : IIteratorHelper
	{
		public int CurrentIterator
		{
			get
			{
				var key = "DatabaseIterator";
				try
				{
					return int.Parse(ConfigurationManager.AppSettings[key]);
				}
				catch (Exception e)
				{
					var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
					var settings = configFile.AppSettings.Settings;
					if (settings[key] == null)
					{
						settings.Add(key, "0");
					}
					else
					{
						settings[key].Value = "0";
					}
					configFile.Save(ConfigurationSaveMode.Modified);
					ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
					return 0;
				}
			}
			set
			{
				var key = "DatabaseIterator";
				var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var settings = configFile.AppSettings.Settings;
				if (settings[key] == null)
				{
					settings.Add(key, "0");
				}
				else
				{
					settings[key].Value = value.ToString();
				}
				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
			}
		}
	}
}
