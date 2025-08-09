namespace Embers.Tests.Classes
{
    public delegate int MyEvent(string n);

    public delegate int MyIntEvent();

    public class Person
    {
        public Person()
        {
        }

        public Person(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public event MyEvent NameEvent;

        public event MyIntEvent IntEvent;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GetName()
        {
            NameEvent?.Invoke(FirstName);

            IntEvent?.Invoke();

            return LastName + ", " + FirstName;
        }
    }
}
