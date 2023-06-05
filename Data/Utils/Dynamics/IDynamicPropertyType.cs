using AnaforaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicPropertyType<TKey, TValueType, TProperty, TType> : IDataModel<TKey>
        where TKey : IEquatable<TKey>
        where TProperty : IDynamicProperty<TKey, TValueType>
        where TType : IDynamicType<TKey>
    {
        TProperty Property { get; set; }
        TType Type { get; set; }
    }
}
