using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using SportsStore.Domain.Concrete;

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
            //var mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> {
            //        new Product { Name = "Kayak", Price = 275M },
            //        new Product { Name = "Lifejacket", Price = 48.95M },
            //        new Product { Name = "Soccer ball", Price = 19.50M },
            //        new Product { Name = "Corner flag", Price = 34.95M }
            //    }.AsQueryable());
            //_ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
            _ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}