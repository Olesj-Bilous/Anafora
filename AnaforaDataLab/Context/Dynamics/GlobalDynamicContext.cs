using AnaforaDataLab.Context.Dynamics.Base;
using AnaforaDataLab.Model.Dynamics;
using Microsoft.EntityFrameworkCore;

namespace AnaforaDataLab.Context.Dynamics
{
    public class GlobalDynamicContext : DynamicContext<GlobalDynamicContext, GlobalDynamicType, GlobalDynamicComponent, GlobalDynamicComponentType, GlobalDynamicProperty, GlobalDynamicValue, GlobalDynamicModel, GlobalDynamicModelValue, Guid>
    {
        public GlobalDynamicContext(DbContextOptions<GlobalDynamicContext> options) : base(options)
        {
        }
    }
}