using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utility
{
    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public delegate void UpdateEventHandler(TKey key, TValue value);

        public event UpdateEventHandler Updated;

        public void Update(TKey key, TValue value)
        {
            if (Keys.Contains(key))
            {
                this[key] = value;

                if (Updated != null)
                    Updated.Invoke(key, value);
            }
        }

    }
}
