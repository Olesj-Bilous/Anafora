using AnaforaData.Model;
using AnaforaDataLab.Utils.Dynamics;

namespace AnaforaDataLab.Model.Dynamics.Base
{
    public abstract class DynamicProperty<TComponent, TKey> : IDataModel<TKey>, IDynamicProperty<TComponent>
        where TComponent : IDynamicComponent
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
        public TComponent Component { get; set; }
    }
}