using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SimpleInjector;

namespace Infrastructure.Repository
{
    /// <summary>
    /// The bootstrapper package.
    /// </summary>
    public class BootstrapperPackage
    {
        /// <summary>
        /// Initializes the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public static void Initialize(Container container)
        {
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<IDbConnection>(() => new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString), Lifestyle.Scoped);
            container.Register<IConnectionFactory>(() => new ConnectionFactory(container), Lifestyle.Scoped);
            container.Register<IRepository<BaseModel>, DapperRepository<BaseModel>>(Lifestyle.Scoped);
        }
    }
}