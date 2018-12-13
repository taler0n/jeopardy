using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal static class CollectionManager
    {
        internal static bool IsReady(IEnumerable<object> collection)
        {
            foreach (var item in collection)
            {
                if (item == null)
                {
                    return false;
                }
            }

            return true;
        }

        internal static void Swap(object[] collection, int index1, int index2)
        {
            var tmp = collection[index1];
            collection[index1] = collection[index2];
            collection[index2] = tmp;
        }

        internal static void Swap<T>(List<T> collection, int index1, int index2)
        {
            var tmp = collection[index1];
            collection[index1] = collection[index2];
            collection[index2] = tmp;
        }
    }
}
