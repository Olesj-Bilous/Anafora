using AnaforaData.Model;
using AnaforaDataLab.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicValue<TComponent, TProperty, TKey> : IDataModel<TKey>, IDynamicValue<TProperty, TComponent>
        where TComponent : DynamicComponent<TKey>
        where TProperty : DynamicProperty<TComponent, TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public string Value { get; set; }
        public TProperty Property { get; set; }
    }
}