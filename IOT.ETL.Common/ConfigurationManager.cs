using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace IOT.ETL.Common
{
    public class ConfigurationManager
    {
        public readonly static IConfiguration Configuration;
        static ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true).Build();
        }
        public static string Conn
        {
            get { return Configuration.GetConnectionString("con"); }
        }
        public static string ConnMySql
        {
            get { return Configuration.GetConnectionString("MySql"); }
        }
        public static string ConnSql
        {
            get { return Configuration.GetConnectionString("Sql"); }
        }
        public static string ConnName
        {
            get { return Configuration.GetConnectionString("conname"); }
        }
        public static string ConnNameSql
        {
            get { return Configuration.GetConnectionString("connamesql"); }
        }

        public static object AppSettings { get; internal set; }
    }
}
