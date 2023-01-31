using AnaforaDataLab.Context.Dynamics.Base;
using AnaforaDataLab.Model.Dynamics;
using Microsoft.EntityFrameworkCore;

namespace AnaforaDataLab.Context.Dynamics
{
    public class GlobalMappedContext : DynamicContext<GlobalMappedContext, GlobalMappedType, GlobalMappedComponent, GlobalMappedComponentType, GlobalMappedProperty, GlobalMappedValue, GlobalMappedModel, GlobalMappedModelValue, Guid>
    {
        public GlobalMappedContext(DbContextOptions<GlobalMappedContext> options) : base(options)
        {
        }
    }
}