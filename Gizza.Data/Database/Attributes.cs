using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Gizza.Data.Database
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PocoDatabaseOptionsAttribute : Attribute
    {
        public PocoDatabaseOptions DatabaseOptions { get; set; }
        public PocoDatabaseOptionsAttribute(PocoDatabaseOptions databaseOptions)
        {
            this.DatabaseOptions = databaseOptions;
        }
        public PocoDatabaseOptionsAttribute(DatabaseEngine databaseEngine, string connectionString)
        {
            this.DatabaseOptions = new PocoDatabaseOptions(databaseEngine, connectionString);
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SchemaNameAttribute : Attribute
    {
        public string SchemaName { get; set; }
        public SchemaNameAttribute(string schemaName)
        {
            this.SchemaName = schemaName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; set; }
        public TableNameAttribute(string tableName)
        {
            this.TableName = tableName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PrimaryKeyAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public PrimaryKeyAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class StoreSettingsAttribute : Attribute
    {
        public StoreAs DateTime { get; set; }
        public StoreSettingsAttribute(StoreAs dateTime = StoreAs.Default)
        {
            this.DateTime = dateTime;
        }
    }

    public enum StoreAs
    {
        Default,
        EpochSeconds,
        EpochMilliSeconds,
    }

}
