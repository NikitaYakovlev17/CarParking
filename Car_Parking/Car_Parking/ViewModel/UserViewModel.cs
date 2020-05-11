using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;
using Car_Parking.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using WPFLocalizeExtension.Engine;
using System.Globalization;



namespace Car_Parking.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        public ICommand Language => new DelegateCommand(Set_Language);
        private int q = 0;

        private void Set_Language()
        {
            if (q == 0)
            {
                q++;
                LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
                LocalizeDictionary.Instance.Culture = new CultureInfo("en");
            }
            else
            {
                q--;
                LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
                LocalizeDictionary.Instance.Culture = new CultureInfo("");
            }
        }
    }
}
