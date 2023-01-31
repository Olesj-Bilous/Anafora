using AnaforaDataLab.Model.Dynamics.Base;

namespace AnaforaDataLab.Model.Dynamics
{
    public class InternalDynamicType : DynamicType<int> { }
    public class InternalDynamicComponent : DynamicComponent<int> { }
    public class InternalDynamicComponentType : DynamicComponentType<InternalDynamicComponent, InternalDynamicType, int> { }
    public class InternalDynamicProperty : DynamicProperty<InternalDynamicComponent, int> { }
    public class InternalDynamicValue : DynamicValue<InternalDynamicComponent, InternalDynamicProperty, int> { }
    public class InternalDynamicModel : DynamicModel<int> { }
    public class InternalDynamicModelValue : DynamicModelValue<InternalDynamicModel, InternalDynamicValue, InternalDynamicProperty, InternalDynamicComponent, int> { }
}