﻿using System.Collections;

namespace Embers.Language
{
    /// <summary>
    /// Represents a range of integers from <c>from</c> to <c>to</c>.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable&lt;System.Int32&gt;" />
    public class Range(int from, int to) : IEnumerable<int>
    {
        private readonly int from = from;
        private readonly int to = to;

        public IEnumerator<int> GetEnumerator()
        {
            return new RangeEnumerator(from, to);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RangeEnumerator(from, to);
        }

        public override string ToString()
        {
            return string.Format("{0}..{1}", from, to);
        }

        private class RangeEnumerator(int from, int to) : IEnumerator<int>
        {
            private readonly int from = from;
            private readonly int to = to;
            private int current = from - 1;

            int IEnumerator<int>.Current
            {
                get { return current; }
            }

            public object Current
            {
                get { return current; }
            }

            public bool MoveNext()
            {
                current++;

                return current >= from && current <= to;
            }

            public void Reset()
            {
                current = from - 1;
            }

            public void Dispose()
            {
            }
        }
    }
}
