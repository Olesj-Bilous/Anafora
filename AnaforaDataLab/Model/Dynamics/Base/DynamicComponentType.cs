using AnaforaData.Model.Base;
using AnaforaData.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicComponentType<TKey> : IDataModel<TKey>, IDynamicComponentType<DynamicComponent<TKey>, DynamicType<TKey>> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public DynamicComponent<TKey> Component { get; set; }
        public DynamicType<TKey> Type { get; set; }
    }
}