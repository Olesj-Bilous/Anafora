namespace AnaforaDataLab.Utils.Dynamics
{
    public interface IDynamicComponentType<TComponent, TType> where TComponent : IDynamicComponent where TType : IDynamicType
    {
        public TComponent Component { get; set; }
        public TType Type { get; set; }
    }
}