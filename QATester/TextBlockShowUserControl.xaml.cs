using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QATester
{
    /// <summary>
    /// Interaction logic for TextBlockShowUserControl.xaml
    /// </summary>
    public partial class TextBlockShowUserControl : UserControl,INotifyPropertyChanged
    {
        public TextBlockShowUserControl()
        {
            InitializeComponent();
        }

        private string _currentText;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentText
        {
            get { return _currentText; }
            set { _currentText = value;  OnPropertyChanged("CurrentText"); }
        }

        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private bool _radioButtonIsChecked =false;

        public bool RadioButtonIsChecked
        {
            get { return _radioButtonIsChecked; }
            set { SetProperty(ref _radioButtonIsChecked, value); }
        }

        protected bool SetProperty<T>(ref T storage, T value,
        [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private string _radioButtonGroupName;

        public string RadioButtonGroupName
        {
            get { return _radioButtonGroupName; }
            set { _radioButtonGroupName = value; OnPropertyChanged("RadioButtonGroupName"); }
        }


    }
}
