using AnaforaDataLab.Model.Dynamics;
using AnaforaDataLab.Repository.Dynamics;
using Microsoft.EntityFrameworkCore;

namespace AnaforaDataLab.Context.Dynamics
{
    public class InternalDynamicContext : DynamicContext<InternalDynamicContext, InternalDynamicType, InternalDynamicComponent, InternalDynamicComponentType, InternalDynamicProperty, InternalDynamicValue, InternalDynamicModel, InternalDynamicModelValue, int>
    {
        public InternalDynamicContext(DbContextOptions<InternalDynamicContext> options) : base(options)
        {
        }
    }
}