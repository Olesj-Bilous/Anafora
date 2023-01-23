using AnaforaData.Model.Base;
using AnaforaData.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicModel<TKey> : IDynamicModel, IDataModel<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}