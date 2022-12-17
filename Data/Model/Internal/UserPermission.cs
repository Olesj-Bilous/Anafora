using AnaforaData.Utils.Enums;

namespace AnaforaData.Model
{
    public class UserPermission : InternalModel
    {
        public User User { get; set; }
        public ModelType ModelType { get; set; }
        public Guid ModelGuid { get; set; }
        public Permissions Permissions { get; set; }
    }
}