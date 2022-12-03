using System.Collections.Generic;
using System.Linq;

namespace day01
{

    public class Bag
    {
        private readonly List<int> _values = new();

        public int Total { get => _values.Sum(); }

        public void Add(int value)
        {
            _values.Add(value);
        }
    }
}