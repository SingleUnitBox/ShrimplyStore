using Microsoft.AspNetCore.Authorization;
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

        public ApplicationUserController(IUnitOfWork unitOfWork,
            ShrimplyStoreDbContext shrimplyStoreDbContext)
        {
            _unitOfWork = unitOfWork;
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RoleManagement(string? userId)
        {
            RoleManagementViewModel roleManagementViewModel = new()
            {
                ApplicationUser = _unitOfWork.ApplicationUsers.Get(x => x.Id == userId, includeProperties: "Company"),
                
                CompanyList = _unitOfWork.Companies.GetAll().Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                    }),
                RoleList = _shrimplyStoreDbContext.Roles.ToList().Select(x =>
                   new SelectListItem
                   {
                       Text = x.Name,
                       Value = x.Name,

                   }
                )
            };
            var userRoles = _shrimplyStoreDbContext.UserRoles.ToList(); //UserId, RoleID
            var roles = _shrimplyStoreDbContext.Roles.ToList(); //Id, Name - admin, employee, customer, company

            var roleId = userRoles.FirstOrDefault(x => x.UserId == userId).RoleId; 
            roleManagementViewModel.ApplicationUser.Role = roles.FirstOrDefault(x => x.Id == roleId).Name;

            return View(roleManagementViewModel);
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
