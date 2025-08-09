namespace Embers.Tests.Classes
{
    public class Calculator
    {
        public event Action<int, int> MyEvent;

        public static int Value { get { return 3; } }

        public int Add(int x, int y)
        {
            if (MyEvent != null)
                MyEvent(x, y);

            return x + y;
        }

        public int Subtract(int x, int y)
        {
            if (MyEvent != null)
                MyEvent(x, y);

            return x - y;
        }
    }
}
