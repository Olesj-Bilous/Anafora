using AnaforaData.Model.Base;
using AnaforaData.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicComponentType<TComponent, TType, TKey> : IDataModel<TKey>, IDynamicComponentType<TComponent, TType>
        where TComponent : DynamicComponent<TKey>
        where TType : DynamicType<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public TComponent Component { get; set; }
        public TType Type { get; set; }
    }
}