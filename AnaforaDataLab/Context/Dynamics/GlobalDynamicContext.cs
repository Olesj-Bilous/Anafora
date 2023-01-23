using AnaforaDataLab.Model.Dynamics;
using AnaforaDataLab.Repository.Dynamics;
using Microsoft.EntityFrameworkCore;

namespace AnaforaDataLab.Context.Dynamics
{
    public class GlobalDynamicContext : DynamicContext<GlobalDynamicType, GlobalDynamicComponent, GlobalDynamicComponentType, GlobalDynamicProperty, GlobalDynamicValue, GlobalDynamicModel, GlobalDynamicModelValue, Guid>
    {
    }
}