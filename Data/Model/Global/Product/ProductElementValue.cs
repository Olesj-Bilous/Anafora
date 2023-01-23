namespace AnaforaData.Model.Global.Product
{
    public class ProductElementValue : IGlobalModel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public ProductElementProperty ElementProperty { get; set; }
    }
}