using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyCompany.Models.Interfaces;
using Newtonsoft.Json;
using AspNetCore.Identity.MongoDbCore.Models;

namespace MyCompany.Models {
    public class User : MongoIdentityUser<string>, IAuditedEntityBase {
        public string Login {
            get => UserName;
            set => UserName = value;
        }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Required] public bool Activated { get; set; }

        [StringLength(6, MinimumLength = 2)]
        public string LangKey { get; set; }

        [Url]
        [StringLength(256)]
        public string ImageUrl { get; set; }

        [StringLength(20)]
        public string ActivationKey { get; set; }

        [StringLength(20)]
        public string ResetKey { get; set; }

        public DateTime? ResetDate { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            if (obj == null || GetType() != obj.GetType()) return false;

            var user = obj as User;
            if (user?.Id == null || Id == null) return false;

            return EqualityComparer<string>.Default.Equals(Id, user.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return "User{" +
                   $"ID='{Id}'" +
                   $", Login='{Login}'" +
                   $", FirstName='{FirstName}'" +
                   $", LastName='{LastName}'" +
                   $", Email='{Email}'" +
                   $", ImageUrl='{ImageUrl}'" +
                   $", Activated='{Activated}'" +
                   $", LangKey='{LangKey}'" +
                   $", ActivationKey='{ActivationKey}'" +
                   "}";
        }
    }
}
