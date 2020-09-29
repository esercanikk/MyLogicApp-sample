using Microsoft.AspNet.Identity;
using MyLogicApp_sample.BLL.Account;
using MyLogicApp_sample.ENTITY.Enums;
using MyLogicApp_sample.ENTITY.IdentityModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyLogicApp_sample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var roller = Enum.GetNames(typeof(Roller));

            var roleManager = MembershipTools.NewRoleManager();
            foreach (var rol in roller)
            {
                if (!roleManager.RoleExists(rol))
                    roleManager.Create(new Role()
                    {
                        Name = rol
                    });
            }

            var userStore = MembershipTools.NewUserStore();
            var userManager = MembershipTools.NewUserManager();

             if (!userStore.Users.Any())
            {
                var adminUser = new User()
                {
                    UserName = "admin",
                    Name = "admin",
                    Surname = "user"
                };
                userManager.Create(adminUser, "admin1234");
                userManager.AddToRole(adminUser.Id, "Admin");

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        var otherUser = new User()
                        {
                            UserName = $"user_{i + 1}_{j + 1}",
                            Name = $"user_{i + 1}_{j + 1}",
                            Surname = "user"
                        };
                        userManager.Create(otherUser, $"user_{i + 1}_{j + 1}");
                        userManager.AddToRole(otherUser.Id, $"Role{i + 1}");
                    }
                }
            }

        }
    }
}
