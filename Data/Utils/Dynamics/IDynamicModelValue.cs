using AnaforaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicModelValue<TKey, TProperty, TValueType, TModel, TValue> : IDataModel<TKey>
        where TKey : IEquatable<TKey>
        where TProperty : IDynamicProperty<TKey, TValueType>
        where TModel : IDynamicModel<TKey>
        where TValue : IDynamicValue<TKey, TValueType, TProperty>
    {
        TModel Model { get; set; }
        TValue Value { get; set; }
    }
}
