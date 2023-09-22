using AnaforaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicProperty<TKey, TValue> : IDataModel<TKey> // TValue acts as a type constraint for DynamicValues that refer here
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; }
    }
}
