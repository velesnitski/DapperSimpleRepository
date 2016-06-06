using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Infrastructure.Repository
{
    /// <summary>
    /// The dapper extensions.
    /// </summary>
    public static class DapperExtensions
    {
        /// <summary>
        /// Queries the specified CNN.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn">The CNN.</param>
        /// <param name="queryObject">The query object.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="buffered">if set to <c>true</c> [buffered].</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>IEnumerable{``0}.</returns>
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, QueryObject queryObject, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Query<T>(queryObject.Sql, queryObject.QueryParams, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes the specified CNN.
        /// </summary>
        /// <param name="cnn">The CNN.</param>
        /// <param name="queryObject">The query object.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>System.Int32.</returns>
        public static int Execute(this IDbConnection cnn, QueryObject queryObject, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Execute(queryObject.Sql, queryObject.QueryParams, transaction, commandTimeout, commandType);
        }
    }
}