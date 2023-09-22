using Microsoft.AspNetCore.Identity;

namespace AnaforaData.Model
{
    public class Role : IdentityRole<Guid>, IGlobalModel
    {
        //public ICollection<RolePermission> Permissions { get; set; }
    }
}