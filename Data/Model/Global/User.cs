using Microsoft.AspNetCore.Identity;

namespace AnaforaData.Model
{
    public class User : IdentityUser<Guid>, IGlobalModel
    {
        //public ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        //public ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
        //public ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
        //public ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
    }
}
