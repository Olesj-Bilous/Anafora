using AnaforaData.Utils.Dynamics;

namespace AnaforaData.Model.Global.Product
{
    public class ProductType : IDynamicType<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}