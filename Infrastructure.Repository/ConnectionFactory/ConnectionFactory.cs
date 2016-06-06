using SimpleInjector;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Class ConnectionFactory
    /// </summary>
    public class ConnectionFactory : BaseConnectionFactory, IConnectionFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFactory" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ConnectionFactory(Container container) : base(container)
        {
        }
    }
}