using AnaforaData.Model;
using AnaforaDataLab.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicModel<TKey> : IDynamicModel, IDataModel<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}