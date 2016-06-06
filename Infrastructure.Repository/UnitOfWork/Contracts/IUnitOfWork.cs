using System;
using System.Data;

namespace Infrastructure.Repository
{
    /// <summary>
    /// The unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Runs the process in transaction.
        /// </summary>
        /// <param name="process">The process.</param>
        void RunProcessInTransaction(Action<IDbConnection, IDbTransaction> process);

        /// <summary>
        /// Runs the process with result in transaction.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        T RunProcessWithResultInTransaction<T>(Func<IDbConnection, IDbTransaction, T> process);
    }
}