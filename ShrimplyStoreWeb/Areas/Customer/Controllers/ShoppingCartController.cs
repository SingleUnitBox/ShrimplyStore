using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using Shrimply.Models.ViewModels;
using System.Security.Claims;

namespace ShrimplyStoreWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCartsList = _unitOfWork.ShoppingCarts.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Shrimp")
            };

            foreach (var cart in ShoppingCartViewModel.ShoppingCartsList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartViewModel.OrderTotal += (cart.Price*cart.Count);

            }

            return View(ShoppingCartViewModel);
        }
        public IActionResult Summary()
        {
            return View();
        }
        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCarts.Get(x => x.Id == cartId, tracked: true);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCarts.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCarts.Get(x => x.Id == cartId, tracked: true);
            if (cartFromDb.Count <= 1)
            {
                _unitOfWork.ShoppingCarts.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCarts.Update(cartFromDb);
            }            
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCarts.Get(x => x.Id == cartId, tracked: true);
            _unitOfWork.ShoppingCarts.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Shrimp.Price;
            }
            else
            {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Shrimp.Price50;
                }
                else
                {
                    return shoppingCart.Shrimp.Price100;
                }

            }

        }
    }
}
