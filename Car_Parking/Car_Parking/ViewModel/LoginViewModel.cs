using Car_Parking.DB;
using DevExpress.Mvvm;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.RegularExpressions;


namespace Car_Parking.ViewModel
{
    class LoginViewModel : ViewModelBase , IDataErrorInfo
    {
        private string phoneNumberLog;
        public string PhoneNumberLog
        {
            get { return phoneNumberLog; }
            set
            {
                if (value == this.phoneNumberLog) return;
                this.phoneNumberLog = value;
                PhoneMask(); RaisePropertiesChanged(nameof(PhoneNumberLog));
            }
        }

        public int PhoneLength { get; set; }

        public async Task PhoneMask()
        {
            var newVal = Regex.Replace(PhoneNumberLog, @"[^0-9]", "");
            if (PhoneLength != newVal.Length && !string.IsNullOrEmpty(newVal))
            {
                PhoneLength = newVal.Length;
                PhoneNumberLog = string.Empty;

                if (newVal.Length <= 3)
                {
                    PhoneNumberLog = Regex.Replace(newVal, @"(375)", "+$1");
                }
                else if (newVal.Length <= 5)
                {
                    PhoneNumberLog = Regex.Replace(newVal, @"(375)(\d{0,2})", "+$1($2)");
                }
                else if (newVal.Length <= 8)
                {
                    PhoneNumberLog = Regex.Replace(newVal, @"(375)(\d{2})(\d{0,3})", "+$1($2)$3");
                }
                else if (newVal.Length <= 10)
                {
                    PhoneNumberLog = Regex.Replace(newVal, @"(375)(\d{2})(\d{0,3})(\d{0,2})", "+$1($2)$3-$4");
                }
                else if (newVal.Length > 10)
                {
                    PhoneNumberLog = Regex.Replace(newVal, @"(375)(\d{2})(\d{0,3})(\d{0,2})(\d{0,2})", "+$1($2)$3-$4-$5");
                }
                PhoneLength = 0;
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                this.password = value;
                RaisePropertiesChanged(nameof(Password));
            }
        }

        private string PasswordSecond;
        public ICommand PasswordChangedCommand2
        {
            get
            {
                return new RelayCommand<object>(ExecChangePassword2);
            }
        }

        private void ExecChangePassword2(object obj)
        {
            PasswordSecond = ((System.Windows.Controls.PasswordBox)obj).Password;
        }


        public ICommand loginfunc => new DelegateCommand(LoginCommand);

        public void LoginCommand()
        {
            bool flagToLogin = true;
            bool IsDone = true;
            flag = true;
            ErrorMes = "";

            if (PasswordSecond == String.Empty || PasswordSecond == null)
            {
                flagToLogin = false;
                ErrorMes = Properties.Resources.emptyfield;
            }
            if (flagToLogin && canreg)
            {
                SqlConnect spam = new SqlConnect();
                string Pass = firstHash(PasswordSecond).ToString();
                IsDone = spam.GiveUsersRecords(PhoneNumberLog, Pass);
                if (IsDone)
                {
                    Properties.Settings.Default.User = PhoneNumberLog;
                    Properties.Settings.Default.UserId = spam.GetIdUserByName(PhoneNumberLog);
                    Properties.Settings.Default.Save();
                    MainWindow sp = new MainWindow();
                    sp.Show();
                    CloseAction2();
                }
            }
            if (!IsDone)
            {
                ErrorMes = Properties.Resources.nosuchuser;
            }
            flag = false;
            canreg = true;


        }
        public Action CloseAction2 { get; set; }
        private StringBuilder firstHash(string password)
        {
            // создаем объект этого класса. Отмечу, что он создается не через new, а вызовом метода Create
            MD5 md5Hasher = MD5.Create();

            // Преобразуем входную строку в массив байт и вычисляем хэш
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(password));

            // Создаем новый Stringbuilder (Изменяемую строку) для набора байт
            StringBuilder sBuilder = new StringBuilder();

            // Преобразуем каждый байт хэша в шестнадцатеричную строку
            for (int i = 0; i < data.Length; i++)
            {
                //указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder;
        }

        private string errorMes;
        public string ErrorMes
        {
            get { return errorMes; }
            set
            {
                this.errorMes = value;
                RaisePropertiesChanged(nameof(ErrorMes));
            }
        }

        bool flag = false;
        bool canreg = true;

        public string Error { get { return null; } }

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;
                if(flag)
                {
                    switch (columnName)
                    {
                        case "Login":
                            break;
                    }                    
                }
                return result;
            }
        }

        public ICommand regfunc => new DelegateCommand(RegCommand);

        public void RegCommand()
        {
            ViewRegister q = new ViewRegister();
            q.Show();
            CloseAction2();
        }
    }
}

