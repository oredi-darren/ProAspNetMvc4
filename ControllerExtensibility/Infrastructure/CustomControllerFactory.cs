﻿using ControllerExtensibility.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace ControllerExtensibility.Infrastructure
{
    public class CustomControllerFactory
        : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            var targetType = default(Type);
            switch (controllerName)
            {
                case "Product":
                    targetType = typeof(ProductController);
                    break;
                case "Customer":
                    targetType = typeof(CustomerController);
                    break;
                case "ActionInvoker":
                    targetType = typeof(ActionInvokerController);
                    break;
                default:
                    requestContext.RouteData.Values["controller"] = "Product";
                    targetType = typeof(ProductController);
                    break;
            }

            return targetType == null ? null : DependencyResolver.Current.GetService(targetType) as IController;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}