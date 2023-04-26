
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shrimply.DataAccess.Data;
using Shrimply.Models;
using Shrimply.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shrimply.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ShrimplyStoreDbContext shrimplyStoreDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
        }
        public void Initialize()
        {
            //migrations
            try
            {
                if (_shrimplyStoreDbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _shrimplyStoreDbContext.Database.Migrate();
                }
            }
            catch (Exception ex) { }
            //roles
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

                //admin user
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Admin",
                    PhoneNumber = "1234567890",
                    StreetAddress = "Main St",
                    City = "Big City",
                    State = "IL",
                    PostalCode = "1715",

                }, "Czcz123$").GetAwaiter().GetResult();

                ApplicationUser user = _shrimplyStoreDbContext.ApplicationUsers
                    .FirstOrDefault(u => u.Email == "admin@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
