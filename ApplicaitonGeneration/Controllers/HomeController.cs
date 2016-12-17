using System.Linq;
using System.Web.Mvc;
using ApplicationGeneration.DAL;
using ApplicaitonGeneration;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace ApplicationGeneration
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public HomeController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationEntities());
        }

        public ActionResult Index()
        {

            ViewBag.ShowBackground = true;
            return View();
        }
    }
}