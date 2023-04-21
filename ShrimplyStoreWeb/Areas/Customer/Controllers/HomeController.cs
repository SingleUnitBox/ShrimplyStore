using Microsoft.AspNetCore.Mvc;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using Shrimply.Models.ViewModels;
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
            ShoppingCart shoppingCart = new ShoppingCart
            {
                Shrimp = _unitOfWork.Shrimps.Get(x => x.Id == shrimpId, includeProperties: "Species")
            };
            return View(shoppingCart);
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