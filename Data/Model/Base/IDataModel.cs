using System.ComponentModel.DataAnnotations;

namespace AnaforaData.Model
{
    public interface IDataModel<TKey> where TKey : IEquatable<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}