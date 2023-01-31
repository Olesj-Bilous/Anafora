using AnaforaDataLab.Model.Dynamics.Base;
using Microsoft.EntityFrameworkCore.Storage;

namespace AnaforaDataLab.Model.Dynamics
{
    public class GlobalDynamicType : DynamicType<Guid> { }
    public class GlobalDynamicComponent : DynamicComponent<Guid> { }
    public class GlobalDynamicComponentType : DynamicComponentType<GlobalDynamicComponent, GlobalDynamicType, Guid> { }
    public class GlobalDynamicProperty : DynamicProperty<GlobalDynamicComponent, Guid> { }
    public class GlobalDynamicValue : DynamicValue<GlobalDynamicComponent, GlobalDynamicProperty, Guid> { }
    public class GlobalDynamicModel : DynamicModel<Guid> { }
    public class GlobalDynamicModelValue : DynamicModelValue<GlobalDynamicModel, GlobalDynamicValue, GlobalDynamicProperty, GlobalDynamicComponent, Guid> { }
}