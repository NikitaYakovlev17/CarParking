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

namespace Car_Parking.ViewModel
{
    class LoginViewModel : ViewModelBase , IDataErrorInfo
    {
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                this.login = value;
                RaisePropertiesChanged(nameof(Login));
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
            Login += " ";
            int x1 = Login.Length - 1;
            Login = Login.Substring(0, x1);

            if (PasswordSecond == String.Empty || PasswordSecond == null)
            {
                flagToLogin = false;
                ErrorMes = Properties.Resources.emptyfield;
            }
            if (flagToLogin)
            {
                SqlConnect spam = new SqlConnect();
                string Pass = firstHash(PasswordSecond).ToString();
                IsDone = spam.GiveUsersRecords(Login, Pass);
                if (IsDone)
                {
                    Properties.Settings.Default.User = Login;
                    Properties.Settings.Default.UserId = spam.GetIdUserByName(Login);
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

