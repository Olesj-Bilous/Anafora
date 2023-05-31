namespace AnaforaDataLab.Utils.Dynamics
{
    public interface IDynamicValue<TProperty, TComponent> where TProperty : IDynamicProperty<TComponent> where TComponent : IDynamicComponent
    {
        public string Value { get; set; }
        public TProperty Property { get; set; }
    }
}