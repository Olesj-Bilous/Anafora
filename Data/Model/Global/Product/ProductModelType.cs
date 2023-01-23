namespace AnaforaData.Model.Global.Product
{
    public class ProductModelType : IGlobalModel
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public ProductType Type { get; set; }
    }
}