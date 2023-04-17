using Microsoft.AspNetCore.Mvc;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using System.Diagnostics;

namespace ShrimplyStoreWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Shrimp> shrimpList = _unitOfWork.Shrimps.GetAll(includeProperties: "Species").ToList();
            return View(shrimpList);
        }
        public IActionResult Details(int shrimpId)
        {
            var shrimp = _unitOfWork.Shrimps.Get(x => x.Id == shrimpId, includeProperties: "Species");
            return View(shrimp);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}