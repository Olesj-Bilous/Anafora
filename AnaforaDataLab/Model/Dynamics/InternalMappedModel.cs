using AnaforaDataLab.Model.Dynamics.Base;

namespace AnaforaDataLab.Model.Dynamics
{
    public class InternalMappedType : DynamicType<int> { }
    public class InternalMappedComponent : DynamicComponent<int>
    {
        public ICollection<InternalMappedType> Types { get; set; }
    }
    public class InternalMappedComponentType : DynamicComponentType<InternalMappedComponent, InternalMappedType, int> { }
    public class InternalMappedProperty : DynamicProperty<InternalMappedComponent, int> { }
    public class InternalMappedValue : DynamicValue<InternalMappedComponent, InternalMappedProperty, int> { }
    public class InternalMappedModel : DynamicModel<int>
    {
        public ICollection<InternalMappedModelValue> Values { get; set; }
    }
    public class InternalMappedModelValue : DynamicModelValue<InternalMappedModel, InternalMappedValue, InternalMappedProperty, InternalMappedComponent, int> { }
}