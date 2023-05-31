using AnaforaData.Utils.Dynamics;

namespace AnaforaData.Model.Global.Product
{
    public class Product : IDynamicModel<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
