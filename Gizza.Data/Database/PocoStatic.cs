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
    public static class PocoStatic
    {
        public static PocoDatabaseOptions DefaultOptions { get; set; }
        
        public static void SetDefaultOptions(PocoDatabaseOptions databaseOptions)
        {
            DefaultOptions = databaseOptions;
        }

        public static void SetDefaultOptions(DatabaseEngine databaseEngine, string connectionString)
        {
            DefaultOptions = new PocoDatabaseOptions(databaseEngine, connectionString);
        }
    }

}
