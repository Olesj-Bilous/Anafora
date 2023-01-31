using AnaforaDataLab.Model.Dynamics.Base;

namespace AnaforaDataLab.Model.Dynamics
{
    public class GlobalMappedType : DynamicType<Guid> { }
    public class GlobalMappedComponent : DynamicComponent<Guid>
    {
        public ICollection<GlobalMappedType> Types { get; set; }
    }
    public class GlobalMappedComponentType : DynamicComponentType<GlobalMappedComponent, GlobalMappedType, Guid> { }
    public class GlobalMappedProperty : DynamicProperty<GlobalMappedComponent, Guid> { }
    public class GlobalMappedValue : DynamicValue<GlobalMappedComponent, GlobalMappedProperty, Guid> { }
    public class GlobalMappedModel : DynamicModel<Guid>
    {
        public ICollection<GlobalMappedModelValue> Values { get; set; }
    }
    public class GlobalMappedModelValue : DynamicModelValue<GlobalMappedModel, GlobalMappedValue, GlobalMappedProperty, GlobalMappedComponent, Guid> { }
}