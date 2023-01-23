namespace AnaforaData.Model.Global.Product
{
    public class ProductComponent : IGlobalModel
    {
        public Guid Id { get; set; }
        public ICollection<ProductComponentType> Types { get; set; }
    }
}