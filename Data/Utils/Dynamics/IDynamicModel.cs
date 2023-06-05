using AnaforaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicModel<TKey> : IDataModel<TKey> where TKey : IEquatable<TKey>
    {
    }
}
