using AnaforaData.Model.Base;
using AnaforaData.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicType<TKey> : IDataModel<TKey>, IDynamicType where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}