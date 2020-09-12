using LMIS.Infrastructure.Interfaces.Managers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LMIS.Portal.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LMIS.Portal.App_Start.NinjectWebCommon), "Stop")]

namespace LMIS.Portal.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using LMIS.Infrastructure.Interfaces;
    using LMIS.Bll.Managers;

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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
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
            kernel.Bind<IGeneralCodeManager>().To<GeneraCodeManager>();
            kernel.Bind<IAspNetUsersManager>().To<AspNetUsersManager>();
            kernel.Bind<IDimThemesManager>().To<DimThemesManager>();
            kernel.Bind<ISubCodeManager>().To<SubCodeManager>();
            kernel.Bind<IThemesVariablesManager>().To<ThemesVariablesManager>();
            kernel.Bind<IReportsManager>().To<ReportsManager>();
            kernel.Bind<IFactStatisticalDataManager>().To<FactStatisticalDataManager>();
        }        
    }
}
