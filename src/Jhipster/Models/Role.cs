using System.Collections.Generic;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;

namespace MyCompany.Models {
    public class Role : MongoIdentityRole<string> {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
