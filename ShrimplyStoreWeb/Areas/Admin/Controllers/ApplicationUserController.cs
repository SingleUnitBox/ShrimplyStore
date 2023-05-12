using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shrimply.DataAccess.Data;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using Shrimply.Models.ViewModels;
using Shrimply.Utility;

namespace ShrimplyStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationUserController(IUnitOfWork unitOfWork,
            ShrimplyStoreDbContext shrimplyStoreDbContext,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RoleManagement(string userId)
        {
            RoleManagementViewModel roleManagementViewModel = new()
            {
                ApplicationUser = _unitOfWork.ApplicationUsers.Get(x => x.Id == userId, includeProperties: "Company"),
                RoleList = _shrimplyStoreDbContext.Roles.ToList()
                    .Select(x => new SelectListItem 
                    { 
                        Text = x.Name,
                        Value = x.Name
                    }),
                CompanyList = _unitOfWork.Companies.GetAll().Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
            };
            var roles = _shrimplyStoreDbContext.Roles.ToList();
            var userRoles = _shrimplyStoreDbContext.UserRoles.ToList();
            var userRoleId = userRoles.FirstOrDefault(x => x.UserId == userId).RoleId;
            roleManagementViewModel.ApplicationUser.Role = roles.FirstOrDefault(x => x.Id == userRoleId).Name;
            return View(roleManagementViewModel);
        }
        [HttpPost]
        public IActionResult RoleManagement(RoleManagementViewModel roleManagementViewModel)
        {
            var userFromDb = _unitOfWork.ApplicationUsers.Get(x => x.Id == roleManagementViewModel.ApplicationUser.Id,
                includeProperties: "Company");
            if (roleManagementViewModel.ApplicationUser.CompanyId != null)
            {
                userFromDb.CompanyId = roleManagementViewModel.ApplicationUser.CompanyId;
            }
            var roles = _shrimplyStoreDbContext.Roles.ToList();
            var userRoles = _shrimplyStoreDbContext.UserRoles.ToList();
            var userFromDbRoleId = userRoles.FirstOrDefault(x => x.UserId == userFromDb.Id).RoleId;
            userFromDb.Role = roles.FirstOrDefault(x => x.Id == userFromDbRoleId).Name;
            if (roleManagementViewModel.ApplicationUser.Role != userFromDb.Role)
            {
                _userManager.RemoveFromRoleAsync(roleManagementViewModel.ApplicationUser, userFromDb.Role).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(roleManagementViewModel.ApplicationUser, roleManagementViewModel.ApplicationUser.Role).GetAwaiter().GetType();
            }
            _shrimplyStoreDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> users = _unitOfWork.ApplicationUsers.GetAll(includeProperties: "Company").ToList();
            var userRoles = _shrimplyStoreDbContext.UserRoles.ToList();
            var roles = _shrimplyStoreDbContext.Roles.ToList();
            foreach (var user in users)
            {
                var roleId = userRoles.FirstOrDefault(x => x.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(x => x.Id == roleId).Name;

                if (user.Company == null)
                {
                    user.Company = new() { Name = "" };
                }
            }
            return Json(new { data = users });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var user = _shrimplyStoreDbContext.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking" });
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                //user is locked, unlock required
                user.LockoutEnd = DateTime.Now;
                _shrimplyStoreDbContext.SaveChanges();
                return Json(new { success = true, message = "Unlocking successful" });
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
                _shrimplyStoreDbContext.SaveChanges();
                return Json(new { success = true, message = "Locking successful" });
            }


        }
        #endregion
    }
}
