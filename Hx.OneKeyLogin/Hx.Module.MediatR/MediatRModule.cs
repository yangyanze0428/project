using Autofac;
using Hx.Components;
using Hx.Modules;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Module.MediatR
{
     public class MediatRModule: ModuleBase
    {
        public override void PreInitialize()
        {
            RegisterInstance<ServiceFactory>(type => Resolve(type));
            Register<IMediator, Mediator>();
        }
    }
}
