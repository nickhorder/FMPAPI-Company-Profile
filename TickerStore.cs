public class TickerStore
{
    string[] items;
    public TickerStore()
    {
        items = new string[] {
            "GSK.L",
            "AZN.L",
            "NXT.L",
            "EXPN.L",
            "IDEA.L" };
    }
    public TickerEnumerator GetEnumerator()
    {
        return new TickerEnumerator(this);
    }

// Declare the enumerator class:  
    public class TickerEnumerator
    {
        int nIndex;
        TickerStore collection;
        public TickerEnumerator(TickerStore coll)
        {
            collection = coll;
            nIndex = -1;
        }
        public bool MoveNext()
        {
            nIndex++;
            return (nIndex < collection.items.Length);
        }

        public string Current => collection.items[nIndex];
    }
}

  