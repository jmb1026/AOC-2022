namespace day01
{
    public class Bag
    {
        int _total = 0;

        public int Total { get => _total; }

        public void Add(int value)
        {
            _total += value;
        }
    }
}