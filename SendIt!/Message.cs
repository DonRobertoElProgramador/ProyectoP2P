using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendIt_
{    
    /*Clase que representa un mensaje*/
    public class Message : INotifyPropertyChanged
    {

        private string content;

        public string message
        {
            get {
                return content; }
            set { content = value;
                RaisePropertyChanged("content");
            }
        }

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


        public event PropertyChangedEventHandler PropertyChanged;

    }
}
