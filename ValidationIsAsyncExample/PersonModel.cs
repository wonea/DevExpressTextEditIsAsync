using System.Diagnostics;

namespace ValidationIsAsyncExample
{
    public class PersonModel
    {
        private string _name;
        public virtual string Name
        {
            get => _name;
            set
            {
                Debug.WriteLine($"{nameof(Name)}: {value}");
                _name = value;
            }
        }
    }
}
