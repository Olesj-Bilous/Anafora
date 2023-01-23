namespace AnaforaData.Model.Global.Product
{
    public class ProductComponentType : IGlobalModel
    {
        public Guid Id { get; set; }
        public ProductComponent Component { get; set; }
        public ProductType Type { get; set; }
    }
}