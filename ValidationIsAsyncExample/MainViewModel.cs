using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace ValidationIsAsyncExample
{
    public class MainViewModel : ViewModelBase
    {
        public virtual PersonModel Person { get; set; }

        public MainViewModel()
        {
            this.Location = "dgsdtgs";


            this.Person = new PersonModel()
            {
                Name = "fgnfrngierng"
            };
        }

        public virtual string Location { get; set; }

        public void Some()
        {
            this.Location = "New Value";
        }

    }
}
