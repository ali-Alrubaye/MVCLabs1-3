using Datalager.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Datalager
{
    public class IdentityContext: IdentityDbContext<AppUser>
    {
       public IdentityContext() : base("IdentityDB"){ }

    //    static IdentityContext()
    //    {
    //       Database.SetInitializer<IdentityContext>(new IdentityDbInit());
    //    }
    //    public static IdentityContext Create() { return new IdentityContext();}
    //}

    //public class IdentityDbInit : DropCreateDatabaseIfModelChanges<IdentityContext>
    //{
    //    protected override void Seed(IdentityContext context)
    //    {
    //        PerformInitialSetup(context); base.Seed(context);
    //    }

    //    private void PerformInitialSetup(IdentityContext context)
    //    {
    //        // Initial configuration will go gere
    //    }
    }
}