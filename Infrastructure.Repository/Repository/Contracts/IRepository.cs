using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLinq;

namespace Infrastructure.Repository
{
    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseModel
    {
        #region Get

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T Get(object id);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetAsync(object id);

        #endregion

        #region Save

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        int Save(BaseModel entity);

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<int?> SaveAsync(BaseModel entity);

        #endregion

        #region Insert

        /// <summary>
        /// Inserts the specified entity to insert.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        int? Insert(object entityToInsert);

        /// <summary>
        /// Inserts the specified entity to insert.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        TKey Insert<TKey>(object entityToInsert);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        Task<int?> InsertAsync(object entityToInsert);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        Task<TKey> InsertAsync<TKey>(object entityToInsert);

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        void Update(object entityToUpdate);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <returns></returns>
        Task<int?> UpdateAsync(object entityToUpdate);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        int Delete(T entityToDelete);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        int Delete(object id);

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        int DeleteList(object whereConditions);

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        int DeleteList(string conditions);

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        Task<int> DeleteListAsync(object whereConditions);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        Task<int> DeleteAsync(T entityToDelete);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> DeleteAsync(object id);

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <param name="entitiesToDelete">The entities to delete.</param>
        /// <returns></returns>
        IEnumerable<Task<int>> DeleteListAsync(List<T> entitiesToDelete);

        #endregion

        #region Query

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<T> Query(SQLinq<T> query);

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        int? Query(SQLinqCount<T> query);

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query(string query, object param);

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<K> Query<K>(string query, object param);

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<K> Query<K>(string query, int? commandTimeout, object param);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query(string sql, Func<T, T, T> map, object param = null);

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        IEnumerable<dynamic> QueryDynamic(string query, object param);

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        IEnumerable<dynamic> QueryDynamic(string sql, Func<T, T, T> map, object param = null);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        IEnumerable<T> GetList(object whereConditions);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        IEnumerable<T> GetList(string conditions);

        /// <summary>
        /// Gets the list paged.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowsPerPage">The rows per page.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="orderby">The orderby.</param>
        /// <returns></returns>
        IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync();

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(object whereConditions);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(string conditions);

        /// <summary>
        /// Gets the list paged asynchronous.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowsPerPage">The rows per page.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="orderby">The orderby.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby);

        /// <summary>
        /// Records the count.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        int RecordCount(string conditions = "");

        /// <summary>
        /// Records the count asynchronous.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        Task<int> RecordCountAsync(string conditions);

        /// <summary>
        /// Executes the sp.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        dynamic ExecuteSp(string spName, object whereConditions = null);

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        K ExecuteScalar<K>(string sql, object param = null);

        #endregion
    }
}