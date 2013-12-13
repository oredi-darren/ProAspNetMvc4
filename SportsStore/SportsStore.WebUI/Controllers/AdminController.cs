using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;
        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: /Admin/

        public ViewResult Index()
        {
            return View(_repository.Products);
        }

    }
}
