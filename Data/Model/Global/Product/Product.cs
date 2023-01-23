namespace AnaforaData.Model.Global.Product
{
    public class Product : IGlobalModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductModelElementValue> ElementValues { get; set; }
    }
}
