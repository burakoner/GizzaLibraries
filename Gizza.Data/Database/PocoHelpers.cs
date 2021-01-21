using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Reflection;

namespace Gizza.Data.Database
{
    public  class PocoHelpers
    {
        /* Properties */
        public DatabaseEngine DatabaseEngine { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Administrator, 04/06/2019. </remarks>
        ///
        /// <param name="databaseEngine">   The database engine. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public PocoHelpers(DatabaseEngine databaseEngine)
        {
            this.DatabaseEngine = databaseEngine;
        }

        public string LOWER(string data, bool addQuotes = false)
        {
            if (this.DatabaseEngine == DatabaseEngine.Oracle)
            {
                return " LOWER(" + (addQuotes ? "'" : "") + data + (addQuotes ? "'" : "") + ") ";
            }

            if (this.DatabaseEngine == DatabaseEngine.SqlServer)
            {
                return " LOWER(" + (addQuotes ? "'" : "") + data + (addQuotes ? "'" : "") + ") ";
            }

            // Return
            return string.Empty;
        }

        public string UPPER(string data, bool addQuotes = false)
        {
            if (this.DatabaseEngine == DatabaseEngine.Oracle)
            {
                return " UPPER(" + (addQuotes ? "'" : "") + data + (addQuotes ? "'" : "") + ") ";
            }

            if (this.DatabaseEngine == DatabaseEngine.SqlServer)
            {
                return " UPPER(" + (addQuotes ? "'" : "") + data + (addQuotes ? "'" : "") + ") ";
            }

            // Return
            return string.Empty;
        }

        public string NOW()
        {
            if (this.DatabaseEngine == DatabaseEngine.Oracle)
            {
                return " SYSDATE ";
            }

            if (this.DatabaseEngine == DatabaseEngine.SqlServer)
            {
                return " GETDATE() ";
            }

            // Return
            return string.Empty;
        }

        public string LAST_INSERTED_ROWID()
        {
            if (this.DatabaseEngine == DatabaseEngine.Oracle)
            {
                return string.Empty;
            }

            if (this.DatabaseEngine == DatabaseEngine.Sqlite)
            {
                return " SELECT LAST_INSERT_ROWID() ";
            }

            if (this.DatabaseEngine == DatabaseEngine.SqlServer)
            {
                return " SELECT CAST(SCOPE_IDENTITY() AS INT) ";
            }

            // Return
            return string.Empty;
        }

        public string TOP(string sql)
        {
            return sql;
        }
    }

}



