using AnaforaData.Model.Base;
using AnaforaData.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicModelValue<TModel, TValue, TProperty, TComponent, TKey>
        : IDataModel<TKey>, IDynamicModelValue<TModel, TValue, TProperty, TComponent>
        where TModel : DynamicModel<TKey>
        where TValue : DynamicValue<TComponent, TProperty, TKey>
        where TProperty : DynamicProperty<TComponent, TKey>
        where TComponent : DynamicComponent<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public TModel Model { get; set; }
        public TValue Value { get; set; }
    }
}