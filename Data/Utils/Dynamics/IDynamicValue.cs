using AnaforaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicValue<TKey, TProperty, TValueType> : IDataModel<TKey>
        where TKey : IEquatable<TKey>
        where TProperty : IDynamicProperty<TKey, TValueType>
    {
        TValueType Value { get; set; }
    }
}
