using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using SimpleInjector;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace Infrastructure.Repository
{
    /// <summary>
    /// The base connection factory.
    /// </summary>
    public abstract class BaseConnectionFactory : IBaseConnectionFactory, IDisposable
    {
        #region Container

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        private readonly Container Container;

        #endregion

        #region Properties

        /// <summary>
        /// The database connection.
        /// </summary>
        private IDbConnection dbConnection;

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        public IDbConnection DbConnection
        {
            get
            {
                if (dbConnection != null)
                    return dbConnection;

                dbConnection = Container.GetInstance<IDbConnection>();

                if (ConfigurationManager.AppSettings["IsMiniProfilerEnabled"] == Boolean.TrueString)
                    dbConnection = new ProfiledDbConnection(dbConnection as DbConnection, MiniProfiler.Current);

                dbConnection.Open();

                return dbConnection;
            }
        }

        /// <summary>
        /// The database transaction.
        /// </summary>
        private IDbTransaction dbTransaction;

        /// <summary>
        /// Gets the database transaction.
        /// </summary>
        /// <value>
        /// The database transaction.
        /// </value>
        public IDbTransaction DbTransaction
        {
            get { return dbTransaction ?? (dbTransaction = DbConnection.BeginTransaction()); }
        }

        #endregion

        #region BaseConnectionFactory

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConnectionFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        protected BaseConnectionFactory(Container container)
        {
            Container = container;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="BaseConnectionFactory"/> is reclaimed by garbage collection.
        /// </summary>
        ~BaseConnectionFactory()
        {
            Dispose(false);
        }

        #endregion

        #region IBaseConnectionFactory

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            try
            {
                if (dbConnection == null || dbTransaction == null)
                   return;

                var profiledConnection = dbConnection as ProfiledDbConnection;
                if (profiledConnection != null && profiledConnection.WrappedConnection == null)
                    return;

                var profiledTransaction = dbTransaction as ProfiledDbTransaction;
                if (profiledTransaction != null && profiledTransaction.WrappedTransaction.Connection == null)
                    return;

                if (dbConnection.State == ConnectionState.Closed)
                    return;

                if (dbTransaction.Connection != null)
                    dbTransaction.Commit();

                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Close();
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException || e is InvalidOperationException)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                throw;
            }
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            try
            {
                if (dbConnection == null || dbTransaction == null)
                    return;

                var profiledConnection = dbConnection as ProfiledDbConnection;
                if (profiledConnection != null && profiledConnection.WrappedConnection == null)
                    return;

                var profiledTransaction = dbTransaction as ProfiledDbTransaction;
                if (profiledTransaction != null && profiledTransaction.WrappedTransaction.Connection == null)
                    return;

                if (dbTransaction.Connection != null)
                    dbTransaction.Rollback();
                if ( dbConnection.State != ConnectionState.Closed)
                    dbConnection.Close();
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Commit();
                if (dbTransaction != null)
                    dbTransaction.Dispose();

                if (dbConnection != null)
                    dbConnection.Dispose();
            }

            dbConnection = null;
            dbTransaction = null;

            disposed = true;
        }

        #endregion
    }
}