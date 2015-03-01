using Microsoft.Practices.Unity;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Infraestructure.Data;

namespace SpaUserControl.Startup
{
    public class DependencyResolver
    {
        public static void Resolver(UnityContainer container)
        {
            container.RegisterType<AppDataContext, AppDataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, IUserRepository>(new HierarchicalLifetimeManager());
        }
    }
}
