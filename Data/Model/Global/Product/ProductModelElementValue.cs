namespace AnaforaData.Model.Global.Product
{
    public class ProductModelElementValue : IGlobalModel
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public ProductElementValue Value { get; set; }
    }
}