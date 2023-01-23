using AnaforaData.Model.Base;
using AnaforaData.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicModelValue<TKey> : IDataModel<TKey>,
        IDynamicModelValue<DynamicModel<TKey>, DynamicValue<TKey>, DynamicProperty<TKey>, DynamicComponent<TKey>>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public DynamicModel<TKey> Model { get; set; }
        public DynamicValue<TKey> Value { get; set; }
    }
}