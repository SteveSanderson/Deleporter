using System;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;
using WhatTimeIsIt.Services;

namespace WhatTimeIsIt
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        public readonly IKernel Kernel = new StandardKernel(new ServiceModule());

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType != null ? (IController) Kernel.Get(controllerType) : null;
        }

        private class ServiceModule : NinjectModule
        {
            public override void Load()
            {
                Bind<IDateProvider>().To<RealDateProvider>();
            }
        }
    }
}