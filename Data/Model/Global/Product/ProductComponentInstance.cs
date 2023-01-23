namespace AnaforaData.Model.Global.Product
{
    public class ProductComponentInstance : IGlobalModel
    {
        public Guid Id { get; set; }
        public ProductComponent Component { get; set; }
    }
}