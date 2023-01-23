using AnaforaData.Model.Base;
using AnaforaData.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicValue<TKey> : IDataModel<TKey>,
        IDynamicValue<DynamicProperty<TKey>, DynamicComponent<TKey>> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public string Value { get; set; }
        public DynamicProperty<TKey> Property { get; set; }
    }
}