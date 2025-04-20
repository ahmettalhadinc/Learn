using Autofac;
using Learn.Core.Repositories;
using Learn.Core.Services;
using Learn.Core.UnitOfWorks;
using Learn.Repository;
using Learn.Repository.Repositories;
using Learn.Repository.UnitOfWorks;
using Learn.Service.Mappings;
using Learn.Service.Services;
using System.Reflection;
using Module=Autofac.Module;

namespace Learn.API.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>))
                .As(typeof(IService<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWorks>().As<IUnitOfWorks>().InstancePerLifetimeScope();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();
            var apiAssembly= Assembly.GetExecutingAssembly();
            var repoAssembly= Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly= Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
               .Where(x => x.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerLifetimeScope();
        }


    }
}
