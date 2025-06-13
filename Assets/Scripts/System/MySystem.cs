using UnityEngine;
using System.Collections.Generic;


namespace MySystem
{

    [System.Serializable]
    public class pair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;


        public pair(TKey first, TValue second)
        {
            this.Key = first;
            this.Value = second;
        }
    }

    [System.Serializable]
    public class MyDictionary<TKey, TValue>
    {
        [SerializeField]
        pair<TKey, TValue>[] pairs;
        Dictionary<TKey, TValue> dict;

        public Dictionary<TKey,TValue> Dict
        {
            get => dict;

            private set
            {
                dict = value;
            }
        }

        public void Initialize()
        {
            foreach(var pair in pairs)
            {
                if (dict == null) dict = new Dictionary<TKey, TValue>();
                dict.Add(pair.Key, pair.Value);
            }
        }

        public void Reset()
        {
            dict.Clear();
            pairs = null;
        }
    }
}
