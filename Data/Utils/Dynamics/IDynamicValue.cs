using AnaforaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicValue<TKey, TValue, TProperty> : IDataModel<TKey>
        where TKey : IEquatable<TKey>
        where TProperty : IDynamicProperty<TKey, TValue>
    {
        TValue Value { get; set; }
        TProperty Property { get; set; }
    }
}
