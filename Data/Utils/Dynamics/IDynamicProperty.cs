using AnaforaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicProperty<TKey, TValueType> : IDataModel<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; }
    }
}
