using System.Collections.Generic;

namespace Assets.Game.Scripts.Core.Common
{
    public class CollectionExtension
    {
        public static bool RemoveFrom<T>(IList<T> list, T target)
        {
            int index = list.IndexOf(target);
            if (index < 0)
                return false;

            int last = list.Count - 1;
            list[index] = list[last];
            list.RemoveAt(last);
            return true;
        }
    }
}