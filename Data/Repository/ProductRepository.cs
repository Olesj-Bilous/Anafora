using AnaforaData.Context;
using AnaforaData.Model.Global.Product;

namespace AnaforaData.Repository
{
    public class ProductRepository
    {
        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        private DataContext _context;

        public List<Product> GetByType(Guid typeId)
        {
            return _context.Products.Where(product => product.ElementValues
                .Any(productValue => productValue.Value.ElementProperty.Component.Types
                    .Any(componentType => componentType.Type.Id == typeId))).ToList();
        }
    }
}