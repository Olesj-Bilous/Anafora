namespace AnaforaData.Model.Global.Product
{
    public class ProductComponentInstanceElementValue : IGlobalModel
    {
        public Guid Id { get; set; }
        public ProductComponentInstance ComponentInstance { get; set; }
        public ProductElementValue Value { get; set; }
    }
}