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
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }
    }

    public class PocoTable<TEntity/*, TKey*/> /*: IDisposable*/ where TEntity : class
    {
        // Private Variables
        private string SchemaName;
        private string TableName;
        private string PrimaryKey;
        private PocoDatabaseOptions DatabaseOptions;

        // Database Success Flag
        [JsonIgnore]
        public bool DbSucces;

        // PocoTable as Array
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        public PocoTable()
        {
            // Construct
            this.Construct();
        }

        public PocoTable(object primaryKeyValue, PocoDatabase db)
        {
            // Construct
            this.Construct();

            // Flag
            this.DbSucces = false;

            // Check Point
            if (primaryKeyValue == null) return;

            // Check Point
            if (string.IsNullOrEmpty(this.TableName) || string.IsNullOrEmpty(this.PrimaryKey)) return;

            // Check Point
            if (db == null && this.DatabaseOptions == null) return;

            // Check PocoDatabase
            if (db == null && this.DatabaseOptions != null)
            {
                db = new PocoDatabase(this.DatabaseOptions.DatabaseEngine, this.DatabaseOptions.ConnectionString);
            }

            // Create Entity
            TEntity data = null;

            // Get Data
            // Bunu ekleyince sadece 1 defa güncelleme yapıyor ve sonra objeyi yokediyor.
            // using (db)
            {
                data = db.GetConnection().Query<TEntity>("SELECT * FROM " + this.TableName + " WHERE " + this.PrimaryKey + "='" + primaryKeyValue + "'", this).FirstOrDefault();
            }

            // Check Data
            if (data != null)
            {
                PropertyInfo[] dataProperties = data.GetType().GetProperties();
                PropertyInfo[] pocoProperties = this.GetType().GetProperties();
                foreach (PropertyInfo pocoProperty in pocoProperties)
                {
                    // Check Point
                    if (pocoProperty.Name == "Item")
                        continue;

                    // Find Correct Property and Set
                    foreach (PropertyInfo dataProperty in dataProperties)
                    {
                        if (dataProperty.Name == pocoProperty.Name)
                        {
                            // Set Value
                            pocoProperty.SetValue(this, dataProperty.GetValue(data));

                            // Break
                            break;
                        }
                    }
                }

                // Flag
                this.DbSucces = true;
            }
        }

        private void Construct()
        {
            this.SchemaName = typeof(TEntity).GetAttributeValue((SchemaNameAttribute attr) => attr.SchemaName);
            this.TableName = typeof(TEntity).GetAttributeValue((TableNameAttribute attr) => attr.TableName);
            this.PrimaryKey = typeof(TEntity).GetAttributeValue((PrimaryKeyAttribute attr) => attr.ColumnName);
            if (this.DatabaseOptions == null)
            {
                var databaseOptions = typeof(TEntity).GetAttributeValue((PocoDatabaseOptionsAttribute pdo) => pdo.DatabaseOptions);
                if (databaseOptions != null) this.DatabaseOptions = databaseOptions;
            }

            if (this.DatabaseOptions == null)
            {
                if (PocoStatic.DefaultOptions != null) this.DatabaseOptions = PocoStatic.DefaultOptions;
            }
        }

        // PocoDatabase db=null'ı aktif ettiğim anda eğer db değeri null gelirse sistem yeni bir bağlantı açıyor.
        // Bu durumda arkada kapanmamış bağlantılar birikiyor.
        // Bu da sistemde hata üretiyor ve artık yeni bağlantı açılamıyor.
        public bool SubmitChanges(PocoDatabase db/*=null*/)
        {
            // Flag
            this.DbSucces = false;

            // Check Point
            if (string.IsNullOrEmpty(this.TableName) || string.IsNullOrEmpty(this.PrimaryKey)) return this.DbSucces;

            // Check Point
            if (db == null && this.DatabaseOptions == null) return this.DbSucces;

            // Check PocoDatabase
            if (db == null && this.DatabaseOptions != null)
            {
                db = new PocoDatabase(this.DatabaseOptions.DatabaseEngine, this.DatabaseOptions.ConnectionString);
            }

            // Check Point
            if (db == null && this.DatabaseOptions == null) return this.DbSucces;

            // Primary Key Value
            Type primaryKeyType = null;
            object primaryKeyValue = null;
            PropertyInfo primaryPoperty = null;
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == this.PrimaryKey)
                {
                    primaryPoperty = property;
                    primaryKeyType = property.PropertyType;
                    primaryKeyValue = property.GetValue(this);
                    break;
                }
            }

            bool intNull =
            (primaryKeyType == typeof(int) && ((int)primaryKeyValue) == 0)
            || (primaryKeyType == typeof(int?) && ((int?)primaryKeyValue).HasValue == false)

            || (primaryKeyType == typeof(long) && ((long)primaryKeyValue) == 0)
            || (primaryKeyType == typeof(long?) && ((long?)primaryKeyValue).HasValue == false)

            || (primaryKeyType == typeof(Int16) && ((Int16)primaryKeyValue) == 0)
            || (primaryKeyType == typeof(Int16?) && ((Int16?)primaryKeyValue).HasValue == false)

            || (primaryKeyType == typeof(Int32) && ((Int32)primaryKeyValue) == 0)
            || (primaryKeyType == typeof(Int32?) && ((Int32?)primaryKeyValue).HasValue == false)

            || (primaryKeyType == typeof(Int64) && ((Int64)primaryKeyValue) == 0)
            || (primaryKeyType == typeof(Int64?) && ((Int64?)primaryKeyValue).HasValue == false);

            bool stringNull = (primaryKeyType == typeof(string) && string.IsNullOrEmpty((string)primaryKeyValue));

            // INSERT
            if (stringNull || intNull)
            {
                if (stringNull)
                {
                    primaryKeyValue = Guid.NewGuid().ToString();
                }
                else if (intNull)
                {

                }

                // Construct Sql
                List<string> Columns = new List<string>();
                List<string> Values = new List<string>();
                string sql = "INSERT INTO {TableName} ({PrimaryKey} {Columns}) VALUES ({PrimaryKeyValue} {Values})";
                if (stringNull)
                {
                }
                else if (intNull)
                {
                    sql = sql.Replace("{PrimaryKey}", "").Replace("{PrimaryKeyValue}", "");
                }

                foreach (PropertyInfo property in properties)
                {
                    // Check Point
                    if (property.Name == "Item")
                        continue;

                    if (property.Name == this.PrimaryKey)
                    {
                    }
                    else
                    {
                        Columns.Add(property.Name);
                        Values.Add("@" + property.Name);
                    }
                }

                // Replace Sql
                sql = sql.Replace("{TableName}", this.TableName);
                sql = sql.Replace("{PrimaryKey}", Columns.Count > 0 ? this.PrimaryKey + "," : this.PrimaryKey);
                sql = sql.Replace("{Columns}", string.Join(", ", Columns));
                sql = sql.Replace("{PrimaryKeyValue}", Columns.Count > 0 ? "'" + primaryKeyValue + "'," : "'" + primaryKeyValue + "',");
                sql = sql.Replace("{Values}", string.Join(", ", Values));

                // Execute
                if (intNull && !string.IsNullOrEmpty(db.Helpers.LAST_INSERTED_ROWID()))
                {
                    sql = sql + "; " + db.Helpers.LAST_INSERTED_ROWID();
                    var id = db.GetConnection().Query<int>(sql, this).Single();
                    primaryPoperty.SetValue(this, id);
                }
                else
                {
                    db.GetConnection().Query(sql, this);
                    primaryPoperty.SetValue(this, primaryKeyValue);
                }

                // Flag
                this.DbSucces = true;
            }

            // UPDATE
            else
            {
                List<string> Columns = new List<string>();
                string sql = "UPDATE {TableName} SET {Columns} WHERE {PrimaryKey}='{PrimaryKeyValue}'";
                foreach (PropertyInfo property in properties)
                {
                    // Check Point
                    if (property.Name == "Item")
                        continue;

                    if (property.Name == this.PrimaryKey)
                    {
                    }
                    else
                    {
                        Columns.Add(" " + property.Name + "=@" + property.Name + " ");
                    }
                }

                // Replace Sql
                sql = sql.Replace("{TableName}", this.TableName);
                sql = sql.Replace("{Columns}", string.Join(", ", Columns));
                sql = sql.Replace("{PrimaryKey}", this.PrimaryKey);
                sql = sql.Replace("{PrimaryKeyValue}", primaryKeyValue.ToString());

                // Execute
                db.GetConnection().Query(sql, this);

                // Flag
                this.DbSucces = true;
            }

            // Return
            return this.DbSucces;
        }

        public bool DeleteRecord(PocoDatabase db = null)
        {
            // Flag
            this.DbSucces = false;

            // Check Point
            if (string.IsNullOrEmpty(this.TableName) || string.IsNullOrEmpty(this.PrimaryKey)) return this.DbSucces;

            // Check Point
            if (db == null && this.DatabaseOptions == null) return this.DbSucces;

            // Check PocoDatabase
            if (db == null && this.DatabaseOptions != null)
            {
                db = new PocoDatabase(this.DatabaseOptions.DatabaseEngine, this.DatabaseOptions.ConnectionString);
            }

            // Check Point
            if (db == null && this.DatabaseOptions == null) return this.DbSucces;

            // Primary Key Value
            Type primaryKeyType = null;
            object primaryKeyValue = null;
            PropertyInfo primaryPoperty = null;
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == this.PrimaryKey)
                {
                    primaryPoperty = property;
                    primaryKeyType = property.PropertyType;
                    primaryKeyValue = property.GetValue(this);
                    break;
                }
            }

            // Check Point
            if (db == null && this.DatabaseOptions == null) return this.DbSucces;

            bool intNull =
            (primaryKeyType == typeof(int) && ((int)primaryKeyValue) == 0)
            || (primaryKeyType == typeof(int?) && ((int?)primaryKeyValue).HasValue == false)

            || (primaryKeyType == typeof(Int16) && ((Int16)primaryKeyValue) == 0)
            || (primaryKeyType == typeof(Int16?) && ((Int16?)primaryKeyValue).HasValue == false)

            || (primaryKeyType == typeof(Int32) && ((Int32)primaryKeyValue) == 0)
            || (primaryKeyType == typeof(Int32?) && ((Int32?)primaryKeyValue).HasValue == false)

            || (primaryKeyType == typeof(Int64) && ((Int64)primaryKeyValue) == 0)
            || (primaryKeyType == typeof(Int64?) && ((Int64?)primaryKeyValue).HasValue == false);

            bool stringNull = (primaryKeyType == typeof(string) && string.IsNullOrEmpty((string)primaryKeyValue));

            // Check Point
            if (intNull || stringNull) return this.DbSucces;

            // Construct Sql
            string sql = "DELETE FROM " + this.TableName + " WHERE " + this.PrimaryKey + "=@" + this.PrimaryKey + "";

            // Execute
            db.GetConnection().Query(sql, this);

            // Flag
            this.DbSucces = true;

            // Return
            return this.DbSucces;
        }

        public void JsonImport(string jsonObject)
        {
            var json = JsonConvert.DeserializeObject<TEntity>(jsonObject);

            PropertyInfo[] jsonProperties = json.GetType().GetProperties();
            PropertyInfo[] pocoProperties = this.GetType().GetProperties();
            foreach (PropertyInfo pocoProperty in pocoProperties)
            {
                // Check Point
                if (pocoProperty.Name == "Item")
                    continue;

                // Find Correct Property and Set
                foreach (PropertyInfo jsonProperty in jsonProperties)
                {
                    if (pocoProperty.Name == jsonProperty.Name)
                    {
                        // Set Value
                        pocoProperty.SetValue(this, jsonProperty.GetValue(json));

                        // Break
                        break;
                    }
                }
            }
        }

        public string JsonExport()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string GetSchemaName()
        {
            return this.SchemaName;
        }

        public string GetTableName()
        {
            return this.TableName;
        }

        public string GetPrimaryKeyColumn()
        {
            return this.PrimaryKey;
        }
    }

}
