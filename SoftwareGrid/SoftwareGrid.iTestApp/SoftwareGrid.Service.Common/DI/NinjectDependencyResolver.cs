﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;

namespace SoftwareGrid.Service.Common.DI
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType, new IParameter[0]);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType, new IParameter[0]);
        }
    }
}
