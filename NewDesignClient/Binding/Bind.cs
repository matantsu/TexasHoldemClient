using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDesignClient.Binding
{

    public class Bind<T> : Changing
    {
        public Bind(T val)
        {
            data = val;
        }
        public Bind()
        {
         
        }

        private T data;
        public T Data
        {
            get { return data; }
            set
            {
                if (!EqualityComparer<T>.Default.Equals(data, value))
                {
                    data = value;
                    OnPropertyChanged("Data");
                }
            }
        }

        public override string ToString()
        {
            return String.Format(Data.ToString());
        }

    }
}
