using Microsoft.AspNet.Identity.EntityFramework;

namespace MyLogicApp_sample.ENTITY.IdentityModels
{
    public class Role : IdentityRole
    {
        public Role(string name)
        {
            this.Name = name;
        }

        public Role()
        {

        }
    }
}
