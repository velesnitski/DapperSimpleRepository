using System;
using JetBrains.Annotations;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Incapsulate SQL and Parameters for Dapper methods
    /// </summary>
    /// <remarks>
    /// http://www.martin fowler.com/eaaCatalog/queryObject.html
    /// </remarks>
    public class QueryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryObject"/> class.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <exception cref="System.ArgumentNullException">sql</exception>
        public QueryObject([NotNull] string sql)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            Sql = sql;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryObject"/> class.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="queryParams">The query params.</param>
        public QueryObject([NotNull] string sql, [CanBeNull] object queryParams) : this(sql)
        {
            QueryParams = queryParams;
        }

        /// <summary>
        /// SQL string
        /// </summary>
        /// <value>The SQL.</value>
        [NotNull]
        public string Sql { get; private set; }

        /// <summary>
        /// Parameter list
        /// </summary>
        /// <value>The query params.</value>
        [CanBeNull]
        public object QueryParams { get; private set; }
    }
}