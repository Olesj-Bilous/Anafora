using AnaforaData.Utils.Enums;
using AnaforaData.Utils.Helpers;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AnaforaData.Model
{
    public class RolePermission
    {
        public Role Role { get; set; }
        public ModelType ModelType { get; set; }
        public Permissions Permissions { get; set; }
    }
}