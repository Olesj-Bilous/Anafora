using AnaforaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicType<TKey> : IDataModel<TKey> where TKey : IEquatable<TKey>
    {
        public string Name { get; set; }
    }
}
