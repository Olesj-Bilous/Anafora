namespace AnaforaData.Model.Global.Product
{
    public class ProductElementProperty : IGlobalModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductComponent Component { get; set; }
    }
}