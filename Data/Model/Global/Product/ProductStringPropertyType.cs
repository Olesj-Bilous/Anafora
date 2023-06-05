using AnaforaData.Utils.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Model.Global.Product
{
    public class ProductStringPropertyType : IDynamicPropertyType<Guid, string, ProductStringProperty, ProductType>
    {
        public Guid Id { get; set; }
        public ProductStringProperty Property { get; set; }
        public ProductType Type { get; set; }
    }
}
