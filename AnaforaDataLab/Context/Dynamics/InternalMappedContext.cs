using AnaforaDataLab.Context.Dynamics.Base;
using AnaforaDataLab.Model.Dynamics;
using Microsoft.EntityFrameworkCore;

namespace AnaforaDataLab.Context.Dynamics
{
    public class InternalMappedContext : DynamicContext<InternalMappedContext, InternalMappedType, InternalMappedComponent, InternalMappedComponentType, InternalMappedProperty, InternalMappedValue, InternalMappedModel, InternalMappedModelValue, int>
    {
        public InternalMappedContext(DbContextOptions<InternalMappedContext> options) : base(options)
        {
        }
    }
}