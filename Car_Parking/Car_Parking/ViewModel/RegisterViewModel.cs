using Car_Parking.DB;
using DevExpress.Mvvm;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Car_Parking.ViewModel
{
    class RegisterViewModel : ViewModelBase, IDataErrorInfo
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

        private string repeatPassword;
        public string RepeatPassword
        {
            get { return repeatPassword; }
            set
            {
                this.repeatPassword = value;
                RaisePropertiesChanged(nameof(RepeatPassword));
            }
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
        private string PasswordFirst;

        public string Error { get { return null; } }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                if (flag)
                {
                    switch (columnName)
                    {
                        case "Login":
                            break;
                        default:
                            break;
                    }
                }
                return error;
            }
        }
        

        public Action CloseAction { get; set; }
        public ICommand register => new DelegateCommand(RegisterCommand);
        public void RegisterCommand()
        {
            ErrorMes = "";
            flag = true;
            Login += " ";
            int x1 = Login.Length - 1;
            Login = Login.Substring(0, x1);
            bool flagToRegistata = true;
            if(Login == null || Login == String.Empty)
            {
                flagToRegistata = false;
                ErrorMes = Properties.Resources.emptyfieldlogin;
            }
            if (PasswordFirst != PasswordSecond)
            {
                flagToRegistata = false;
                ErrorMes = Properties.Resources.eaqfield;
            }
            if (PasswordFirst.Length < 6)
            {
                flagToRegistata = false;
                ErrorMes = Properties.Resources.charac;
            }
            if (PasswordFirst == String.Empty || PasswordSecond == String.Empty || PasswordFirst == null || PasswordSecond == null)
            {
                flagToRegistata = false;
                ErrorMes = Properties.Resources.emptyfield;
            }
            bool IsDone = true;
            if (flagToRegistata)
            {
                SqlConnect spam = new SqlConnect();
                string Pass = firstHash(PasswordFirst).ToString();
                IsDone = spam.InsertUsersRecords(Login, Pass);
                if (IsDone)
                {
                    ViewLogin t = new ViewLogin();
                    t.Show();
                    CloseAction();
                }
            }

            if (!IsDone)
            {
                ErrorMes = Properties.Resources.existserr;
            }
            flag = false;
        }
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
        public ICommand PasswordChangedCommand
        {
            get
            {
                return new RelayCommand<object>(ExecChangePassword);
            }
        }

        private string PasswordSecond;

        private void ExecChangePassword(object obj)
        {
            PasswordFirst = ((System.Windows.Controls.PasswordBox)obj).Password;
        }

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



        public ICommand enter => new DelegateCommand(EnterCommand);
        public void EnterCommand()
        {
            ViewLogin q = new ViewLogin();
            q.Show();
            CloseAction();
        }
    }
}

