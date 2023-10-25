using AnaforaData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaforaData.Utils.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsDataModel(this Type type)
        {
            return type.IsClass
                && type.HasGenericInterface(typeof(IDataModel<>));
        }

        public static bool IsDataRelation(this Type type)
        {
            return type.IsIEnumerable(typeof(IDataModel<>));
        }

        public static bool IsIEnumerable(this Type type, Type? item)
        {
            return type.HasGenericInterface(typeof(IEnumerable<>))
                && (
                    item == null
                    || (
                        type.GenericTypeArguments.FirstOrDefault()?.IsAssignableTo(item)
                        ?? false
                    )
                );
        }

        public static bool HasGenericInterface(this Type type, Type iface)
        {
            return type.GetInterfaces()
                .Where(iface => iface.IsGenericType)
                .Select(iface => iface.GetGenericTypeDefinition())
                .Contains(iface);
        }
    }
}
