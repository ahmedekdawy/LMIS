using LMIS.Bll.Managers;
using LMIS.Infrastructure.Interfaces;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMIS.Infrastructure.Interfaces.Managers;

namespace LMIS.Portal.Infrastructure
{
    public class NinjectControllerFactory : NinjectHttpApplication
    {
        // A Ninject "kernel" is the thing that can supply object instances
      
        protected override IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel(new DataBankModules());
            return kernel;
        }
        private class DataBankModules : NinjectModule
        {
            public override void Load()
            {
                Bind<IGeneralCodeManager>().To<GeneraCodeManager>();
            }
        }
    }
    
}