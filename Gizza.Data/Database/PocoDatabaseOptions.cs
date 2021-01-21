using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;

namespace Gizza.Data.Database
{
    public class PocoDatabaseOptions
    {
        public DatabaseEngine DatabaseEngine { get; set; }
        public string ConnectionString { get; set; }

        public PocoDatabaseOptions()
        {

        }

        public PocoDatabaseOptions(DatabaseEngine databaseEngine, string connectionString)
        {
            this.DatabaseEngine = databaseEngine;
            this.ConnectionString = connectionString;
        }
    }

}
