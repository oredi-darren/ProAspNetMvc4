using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory 
        : DefaultControllerFactory
    {
        private IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : _ninjectKernel.Get(controllerType) as IController;
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            var emailSettings = new EmailSettings { WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "true") };
            _ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
        }
    }
}