namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicModelValue<TModel, TValue, TProperty, TComponent>
        where TModel : IDynamicModel
        where TValue : IDynamicValue<TProperty, TComponent>
        where TProperty: IDynamicProperty<TComponent>
        where TComponent : IDynamicComponent
    {
        public TModel Model { get; set; }
        public TValue Value { get; set; }
    }
}