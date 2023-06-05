using AnaforaData.Utils.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Model.Global.Product
{
    public class ProductModelStringValue : IDynamicModelValue<Guid, ProductStringProperty, string, Product, ProductStringValue>
    {
        public Guid Id { get; set; }
        public Product Model { get; set; }
        public ProductStringValue Value { get; set; }
    }
}
