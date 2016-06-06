using System;
using System.Data;

namespace Infrastructure.Repository
{
    /// <summary>
    /// The unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties

        /// <summary>
        /// Gets or sets the connection factory.
        /// </summary>
        /// <value>
        /// The connection factory.
        /// </value>
        private IConnectionFactory ConnectionFactory { get; set; }

        #endregion

        #region UnitOfWork
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="connectionFactory">The connection factory.</param>
        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }
        #endregion

        #region IUnitOfWork

        /// <summary>
        /// Runs the process in transaction.
        /// </summary>
        /// <param name="process">The process.</param>
        public void RunProcessInTransaction(Action<IDbConnection, IDbTransaction> process)
        {
            try
            {
                process(ConnectionFactory.DbConnection, ConnectionFactory.DbTransaction);
                ConnectionFactory.Commit();
            }
            catch (Exception)
            {
                ConnectionFactory.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Runs the process with result in transaction.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        public T RunProcessWithResultInTransaction<T>(Func<IDbConnection, IDbTransaction, T> process)
        {
            try
            {
                var result = process(ConnectionFactory.DbConnection, ConnectionFactory.DbTransaction);
                ConnectionFactory.Commit();
                return result;
            }
            catch (Exception)
            {
                ConnectionFactory.Rollback();
                throw;
            }
        }

        #endregion
    }
}