using AnaforaDataLab.Model.Dynamics;
using AnaforaDataLab.Repository.Dynamics;
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