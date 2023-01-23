using AnaforaData.Model.Base;
using AnaforaData.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicComponent<TKey> : IDataModel<TKey>, IDynamicComponent where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}