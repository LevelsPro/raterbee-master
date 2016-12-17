using System.Linq;
using System.Web.Mvc;
using ApplicationGeneration.DAL;
using ApplicaitonGeneration;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace ApplicationGeneration
{
    [Authorize]
    public class ClientController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public ClientController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationEntities());
        }

        public ActionResult Index()
        {
            var clients = new List<db_RealtorClients>();
            var userId = User.Identity.GetUserId<int>();
            if (userId > 0)
                clients = _unitOfWork.RealtorClients.Find(rc => rc.UserId == userId).ToList();

            return View(clients);
        }
    }
}