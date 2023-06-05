using AnaforaData.Utils.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Model.Global.Product
{
    public class ProductStringProperty : IDynamicProperty<Guid, string>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
