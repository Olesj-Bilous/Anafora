namespace AnaforaData.Model.Base
{
    public interface IDataModel<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}