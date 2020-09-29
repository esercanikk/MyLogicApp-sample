
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyLogicApp_sample.DAL;
using MyLogicApp_sample.ENTITY.IdentityModels;

namespace MyLogicApp_sample.BLL.Account
{
    public static class MembershipTools
    {
        public static UserStore<User> NewUserStore() => new UserStore<User>(new MyContext());
        public static UserManager<User> NewUserManager() => new UserManager<User>(NewUserStore());
        public static RoleStore<Role> NewRoleStore() => new RoleStore<Role>(new MyContext());
        public static RoleManager<Role> NewRoleManager() => new RoleManager<Role>(NewRoleStore());
    }
}
