using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JetBrains.Annotations;
using SQLinq;
using SQLinq.Dapper;

namespace Infrastructure.Repository
{
    /// <summary>
    /// The dapper repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DapperRepository<T> : IRepository<T> where T : BaseModel, new()
    {
        #region Properties

        /// <summary>
        /// Gets the connection factory.
        /// </summary>
        /// <value>
        /// The connection factory.
        /// </value>
        private IConnectionFactory ConnectionFactory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DapperRepository{T}"/> class.
        /// </summary>
        /// <param name="connectionFactory">The connection factory.</param>
        public DapperRepository(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        #endregion

        #region IRepository

        #region Get

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T Get(object id)
        {
            return ConnectionFactory.DbConnection.Get<T>(id, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<T> GetAsync(object id)
        {
            return ConnectionFactory.DbConnection.GetAsync<T>(id, ConnectionFactory.DbTransaction);
        }

        #endregion

        #region Save

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        public int Save(BaseModel entity)
        {
            var key = entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            var objectId = key.GetValue(entity, null);

            if (objectId.ToInt() == 0)
                objectId = Insert(entity).ToInt(0);
            else
                Update(entity);

            return objectId.ToInt(0);
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        public Task<int?> SaveAsync(BaseModel entity)
        {
            var key = entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            var objectId = key.GetValue(entity, null);

            return objectId.ToInt() == 0 ? InsertAsync(entity) : UpdateAsync(entity);
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserts the specified entity to insert.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public int? Insert(object entityToInsert)
        {
            return ConnectionFactory.DbConnection.Insert(entityToInsert, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Inserts the specified entity to insert.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public TKey Insert<TKey>(object entityToInsert)
        {
            return ConnectionFactory.DbConnection.Insert<TKey>(entityToInsert, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public async Task<int?> InsertAsync(object entityToInsert)
        {
            return await ConnectionFactory.DbConnection.InsertAsync<int?>(entityToInsert, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public async Task<TKey> InsertAsync<TKey>(object entityToInsert)
        {
            return await ConnectionFactory.DbConnection.InsertAsync<TKey>(entityToInsert, ConnectionFactory.DbTransaction);
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        public void Update(object entityToUpdate)
        {
            ConnectionFactory.DbConnection.Update(entityToUpdate, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <returns></returns>
        public async Task<int?> UpdateAsync(object entityToUpdate)
        {
            return await ConnectionFactory.DbConnection.UpdateAsync(entityToUpdate, ConnectionFactory.DbTransaction);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        public int Delete(T entityToDelete)
        {
            if (entityToDelete == null)
                throw new ArgumentNullException("entityToDelete");

            return ConnectionFactory.DbConnection.Delete(entityToDelete, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int Delete(object id)
        {
            if (id.ToInt(0) <= 0)
                throw new ArgumentOutOfRangeException(id.ToString());

            return ConnectionFactory.DbConnection.Delete(id, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        public int DeleteList(object whereConditions)
        {
            if (whereConditions == null)
                throw new ArgumentNullException("whereConditions");
            
            return ConnectionFactory.DbConnection.DeleteList<T>(whereConditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public int DeleteList(string conditions)
        {
            if (String.IsNullOrEmpty((conditions ?? String.Empty).Trim()))
                throw new ArgumentException("conditions");
            
            return ConnectionFactory.DbConnection.DeleteList<T>(conditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        public Task<int> DeleteListAsync(object whereConditions)
        {
            if (whereConditions == null)
                throw new ArgumentNullException("whereConditions");

            return ConnectionFactory.DbConnection.DeleteListAsync<T>(whereConditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        public Task<int> DeleteAsync(T entityToDelete)
        {
            if (entityToDelete == null)
                throw new ArgumentNullException("entityToDelete");

            return ConnectionFactory.DbConnection.DeleteAsync(entityToDelete, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<int> DeleteAsync(object id)
        {
            if (id.ToInt(0) <= 0)
                throw new ArgumentOutOfRangeException(id.ToString());

            return ConnectionFactory.DbConnection.DeleteAsync<T>(id, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entitiesToDelete">The entities to delete.</param>
        /// <returns></returns>
        public IEnumerable<Task<int>> DeleteListAsync(List<T> entitiesToDelete)
        {
            if (entitiesToDelete.Any())
                throw new ArgumentException("Empty collection " + entitiesToDelete);

            return entitiesToDelete.Select(DeleteAsync);
        }

        #endregion

        #region Query

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(SQLinq<T> query)
        {
            return ConnectionFactory.DbConnection.Query(query, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public int? Query(SQLinqCount<T> query)
        {
            var result = (IDictionary<string, object>)ConnectionFactory.DbConnection.Query(query, ConnectionFactory.DbTransaction).Single();
            foreach (var r in result)
            {
                return (int)r.Value;
            }

            return null;
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(string query, object param = null)
        {
            return ConnectionFactory.DbConnection.Query<T>(query, param, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public IEnumerable<K> Query<K>(string query, object param = null)
        {
            return ConnectionFactory.DbConnection.Query<K>(query, param, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <returns></returns>
        public IEnumerable<K> Query<K>(string query,  [NotNull] int? commandTimeout, object param = null)
        {
            if (commandTimeout == null) throw new ArgumentNullException("commandTimeout");
            return ConnectionFactory.DbConnection.Query<K>(query, param, ConnectionFactory.DbTransaction, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(string sql, Func<T, T, T> map, object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction).AsQueryable();
        }

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public IEnumerable<dynamic> QueryDynamic(string query, object param)
        {
            return ConnectionFactory.DbConnection.Query(query, param, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public IEnumerable<dynamic> QueryDynamic(string sql, Func<T, T, T> map, object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction).AsQueryable();
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetList()
        {
            return ConnectionFactory.DbConnection.GetList<T>(null, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(object whereConditions)
        {
            return ConnectionFactory.DbConnection.GetList<T>(whereConditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(string conditions)
        {
            return ConnectionFactory.DbConnection.GetList<T>(conditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list paged.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowsPerPage">The rows per page.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="orderby">The orderby.</param>
        /// <returns></returns>
        public IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string @orderby)
        {
            return ConnectionFactory.DbConnection.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync()
        {
            return ConnectionFactory.DbConnection.GetListAsync<T>(null, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync(object whereConditions)
        {
            return ConnectionFactory.DbConnection.GetListAsync<T>(whereConditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync(string conditions)
        {
            return ConnectionFactory.DbConnection.GetListAsync<T>(conditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list paged asynchronous.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowsPerPage">The rows per page.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="orderby">The orderby.</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby)
        {
            return ConnectionFactory.DbConnection.GetListPagedAsync<T>(pageNumber, rowsPerPage, conditions, orderby);
        }

        /// <summary>
        /// Records the count.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public int RecordCount(string conditions = "")
        {
            return ConnectionFactory.DbConnection.RecordCount<T>(conditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Records the count asynchronous.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public async Task<int> RecordCountAsync(string conditions)
        {
            return await ConnectionFactory.DbConnection.RecordCountAsync<T>(conditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Executes the sp.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        public dynamic ExecuteSp(string spName, object whereConditions = null)
        {
            return ConnectionFactory.DbConnection.Query(spName, whereConditions, ConnectionFactory.DbTransaction, commandType: CommandType.StoredProcedure).First();
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public K ExecuteScalar<K>(string sql, object param = null)
        {
            return ConnectionFactory.DbConnection.ExecuteScalar<K>(sql, param, ConnectionFactory.DbTransaction);
        }

        #endregion

        #endregion
    }
}