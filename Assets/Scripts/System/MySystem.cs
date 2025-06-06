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
}
