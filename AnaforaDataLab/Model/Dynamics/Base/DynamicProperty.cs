using AnaforaData.Model.Base;
using AnaforaData.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicProperty<TKey> : IDataModel<TKey>,
        IDynamicProperty<DynamicComponent<TKey>> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
        public DynamicComponent<TKey> Component { get; set; }
    }
}