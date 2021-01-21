#define OracleSupport_
#define SqliteSupport_
#define SqlServerSupport

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dapper;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
#if OracleSupport
using Oracle.ManagedDataAccess.Client;
#endif
#if SqliteSupport
using System.Data.SQLite;
#endif
#if SqlServerSupport
using System.Data.SqlClient;
#endif

namespace Gizza.Data.Database
{
    public class PocoDatabase : IDisposable
    {
        #region Oracle Variables
#if OracleSupport
        private OracleConnection OraConn { get; set; }
        private OracleCommand OraCommand { get; set; }
        private OracleTransaction OraTransaction { get; set; }
#endif
        #endregion

        #region Sqlite Variables
#if SqliteSupport
        private SQLiteConnection SqliteConn { get; set; }
        private SQLiteCommand SqliteCommand { get; set; }
        private SQLiteTransaction SqliteTransaction { get; set; }
#endif
        #endregion

        #region SqlServer Variables
#if SqlServerSupport
        private SqlConnection SqlServerConn { get; set; }
        private SqlCommand SqlServerCommand { get; set; }
        private SqlTransaction SqlServerTransaction { get; set; }
#endif
        #endregion

        #region Common Properties
        public DatabaseEngine DatabaseEngine { get; set; }
        public int CommandTimeout
        {
            get
            {
#if OracleSupport
                if (this.DatabaseEngine == DatabaseEngine.Oracle)
                    return this.OraCommand.CommandTimeout;
#endif
#if SqliteSupport
                if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    return this.SqliteCommand.CommandTimeout;
#endif
#if SqlServerSupport
                if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    return this.SqlServerCommand.CommandTimeout;
#endif
                return 0;
            }
            set
            {
#if OracleSupport
                if (this.DatabaseEngine == DatabaseEngine.Oracle)
                    this.OraCommand.CommandTimeout = value;
#endif
#if SqliteSupport
                if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    this.SqliteCommand.CommandTimeout = value;
#endif
#if SqlServerSupport
                if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    this.SqlServerCommand.CommandTimeout = value;
#endif
            }
        }
        public ConnectionState ConnectionState
        {
            get
            {
#if OracleSupport
                if (this.DatabaseEngine == DatabaseEngine.Oracle)
                    return this.OraConn.State;
#endif
#if SqliteSupport
                if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    return this.SqliteConn.State;
#endif
#if SqlServerSupport
                if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    return this.SqlServerConn.State;
#endif

                return ConnectionState.Closed;
            }
        }
        public string ConnectionString { get; set; }
        public int ConnectionTimeout
        {
            get
            {
#if OracleSupport
                if (this.DatabaseEngine == DatabaseEngine.Oracle)
                    return this.OraConn.ConnectionTimeout;
#endif
#if SqliteSupport
                if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    return this.SqliteConn.ConnectionTimeout;
#endif
#if SqlServerSupport
                if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    return this.SqlServerConn.ConnectionTimeout;
#endif

                return 0;
            }
        }
        public string Database
        {
            get
            {
#if OracleSupport
                if (this.DatabaseEngine == DatabaseEngine.Oracle)
                {
                    return this.OraConn.Database;
                }
#endif
#if SqliteSupport
                if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    return this.SqliteConn.Database;
#endif
#if SqlServerSupport
                if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    return this.SqlServerConn.Database;
#endif

                return string.Empty;
            }
            set
            {
#if OracleSupport
                if (this.DatabaseEngine == DatabaseEngine.Oracle)
                    this.OraConn.ChangeDatabase(value);
#endif
#if SqliteSupport
                if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    this.SqliteConn.ChangeDatabase(value);
#endif
#if SqlServerSupport
                if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    this.SqlServerConn.ChangeDatabase(value);
#endif
            }
        }
        public string ServerVersion
        {
            get
            {
#if OracleSupport
                if (this.DatabaseEngine == DatabaseEngine.Oracle)
                    return this.OraConn.ServerVersion;
#endif
#if SqliteSupport
                if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    return this.SqliteConn.ServerVersion;
#endif
#if SqlServerSupport
                if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    return this.SqlServerConn.ServerVersion;
#endif

                return string.Empty;
            }
        }
        public Stopwatch StopWatch { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        #endregion

        #region Common Properties
        public bool DebugOutput { get; set; } = true;
        #endregion

        #region Multi Database Objects
        public PocoHelpers Helpers { get; private set; }
        #endregion

        public PocoDatabase()
        {
            // Connection Parameters
            this.DatabaseEngine = PocoStatic.DefaultOptions.DatabaseEngine;
            this.ConnectionString = PocoStatic.DefaultOptions.ConnectionString;

            // Helpers
            this.Helpers = new PocoHelpers(this.DatabaseEngine);

            // Construct
            this.Construct();
        }

        public PocoDatabase(DatabaseEngine databaseEngine, string connectionString)
        {
            // Connection Parameters
            this.DatabaseEngine = databaseEngine;
            this.ConnectionString = connectionString;

            // Helpers
            this.Helpers = new PocoHelpers(this.DatabaseEngine);

            // Construct
            this.Construct();
        }

        private void Construct()
        {

#if OracleSupport
            if (this.DatabaseEngine == DatabaseEngine.Oracle)
            {
                this.OraConn = new OracleConnection(this.ConnectionString);
                this.OraCommand = new OracleCommand();
            }
#endif
#if SqliteSupport
            if (this.DatabaseEngine == DatabaseEngine.Sqlite)
            {
                this.SqliteConn = new SQLiteConnection (this.ConnectionString);
                this.SqliteCommand = new SQLiteCommand ();
            }
#endif
#if SqlServerSupport
            if (this.DatabaseEngine == DatabaseEngine.SqlServer)
            {
                this.SqlServerConn = new SqlConnection(this.ConnectionString);
                this.SqlServerCommand = new SqlCommand();
            }
#endif
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            // Close Connection
            this.CloseConnection();

            // Dispose Variables
            this.ConnectionString = null;
            this.StopWatch = null;
#if OracleSupport
            if (this.OraConn != null) this.OraConn.Dispose();
            this.OraConn = null;
            this.OraCommand = null;
            this.OraTransaction = null;
#endif
#if SqliteSupport
            if (this.SqliteConn != null) this.SqliteConn.Dispose();
            this.SqliteConn = null;
            this.SqliteCommand = null;
            this.SqliteTransaction = null;
#endif
#if SqlServerSupport
            if (this.SqlServerConn != null) this.SqlServerConn.Dispose();
            this.SqlServerConn = null;
            this.SqlServerCommand = null;
            this.SqlServerTransaction = null;
#endif

            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // NOTE: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // NOTE: Leave out the finalizer altogether if this class doesn't own unmanaged resources itself, but leave the other methods exactly as they are.   
        ~PocoDatabase()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            // Finalizer calls Dispose(false)  
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // NOTE: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Open/Close Connection
        public bool OpenConnection()
        {
            try
            {
                if (this.ConnectionState == ConnectionState.Closed)
                {
#if OracleSupport
                    if (this.DatabaseEngine == DatabaseEngine.Oracle)
                    {
                        this.OraConn.Open();
                    }
#endif
#if SqliteSupport
                    if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    {
                        this.SqliteConn.Open();
                    }
#endif
#if SqlServerSupport
                    if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    {
                        this.SqlServerConn.Open();
                    }
#endif
                }

                if (this.ConnectionState == ConnectionState.Broken)
                {
#if OracleSupport
                    if (this.DatabaseEngine == DatabaseEngine.Oracle)
                    {
                        this.OraConn.Close();
                        this.OraConn.Open();
                    }
#endif
#if SqliteSupport
                    if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    {
                        this.SqliteConn.Close();
                        this.SqliteConn.Open();
                    }
#endif
#if SqlServerSupport
                    if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    {
                        this.SqlServerConn.Close();
                        this.SqlServerConn.Open();
                    }
#endif
                }

                return true;
            }
            catch { }

            // Return Dummy
            return false;
        }

        public bool CloseConnection()
        {
            try
            {
                if (this.ConnectionState == ConnectionState.Connecting || this.ConnectionState == ConnectionState.Executing || this.ConnectionState == ConnectionState.Fetching)
                    return false;

                else if (this.ConnectionState == ConnectionState.Broken || this.ConnectionState == ConnectionState.Open)
                {
#if OracleSupport
                    if (this.DatabaseEngine == DatabaseEngine.Oracle)
                    {
                        this.OraConn.Close();
                        this.OraConn.Dispose();
                    }
#endif
#if SqliteSupport
                    if (this.DatabaseEngine == DatabaseEngine.Sqlite)
                    {
                        this.SqliteConn.Close();
                        this.SqliteConn.Dispose();
                    }
#endif
#if SqlServerSupport
                    if (this.DatabaseEngine == DatabaseEngine.SqlServer)
                    {
                        this.SqlServerConn.Close();
                        this.SqlServerConn.Dispose();
                    }
#endif
                    return true;
                }

                else if (this.ConnectionState == ConnectionState.Closed)
                    return true;
            }
            catch { }

            // Return Dummy
            return false;
        }
        #endregion

        public IDbConnection GetConnection(bool openConnection = true)
        {
            if (openConnection)
            {
                this.OpenConnection();
            }

#if OracleSupport
            if (this.DatabaseEngine == DatabaseEngine.Oracle)
            {
                return this.OraConn;
            }
#endif
#if SqliteSupport
            if (this.DatabaseEngine == DatabaseEngine.Sqlite)
            {
                return this.SqliteConn;
            }
#endif
#if SqlServerSupport
            if (this.DatabaseEngine == DatabaseEngine.SqlServer)
            {
                return this.SqlServerConn;
            }
#endif

            // Return Dummy
            return null;
        }

    }
}
