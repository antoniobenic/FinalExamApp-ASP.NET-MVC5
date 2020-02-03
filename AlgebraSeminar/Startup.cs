using AlgebraSeminar.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlgebraSeminar.Startup))]
namespace AlgebraSeminar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            User();
            ConfigureAuth(app);
        }



        private void User()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (!roleManager.RoleExists("Admin"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name= "Admin"
                };
                roleManager.Create(role);

                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Antonio",
                    Email="antonio.benic1234@gmai.com",
                    PhoneNumber="0955021686"
                };

                string userPWD = "Benic1234";

                IdentityResult chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var resault = UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
