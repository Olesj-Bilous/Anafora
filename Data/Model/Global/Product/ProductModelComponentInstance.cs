namespace AnaforaData.Model.Global.Product
{
    public class ProductModelComponentInstance : IGlobalModel
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public ProductComponentInstance ComponentInstance { get; set; }
    }
}