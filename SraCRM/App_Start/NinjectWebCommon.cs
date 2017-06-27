[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SraCRM.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SraCRM.App_Start.NinjectWebCommon), "Stop")]

namespace SraCRM.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using System.Reflection;
    using Ninject;
    using Ninject.Web.Common;
    using LinhNguyen.Repository;
    using System.Data.Entity.Infrastructure;
    using LinhNguyen.Repository.Factories;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                //kernel.Bind<IExpenseTrackerRepository>().To<ExpenseTrackerEFRepository>();
                //kernel.Bind<IObjectContextAdapter>().To<LinhNguyen.Repository.DAL.SraContext>();
                RegisterServices(kernel);
                //System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
                //System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(Kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IObjectContextAdapter>().To<LinhNguyen.Repository.DAL.SraContext>();
            kernel.Bind<IExpenseTrackerRepository>().To<ExpenseTrackerEFRepository>();
            kernel.Bind<ExpenseGroupFactory>().ToSelf();
            kernel.Bind<ExpenseMasterDataFactory>().ToSelf();
        }        
    }
}
