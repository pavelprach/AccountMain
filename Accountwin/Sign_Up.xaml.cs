using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PasswordCheck2;
using System.IO;

namespace Accountwin
{
    /// <summary>
    /// Логика взаимодействия для Sign_Up.xaml
    /// </summary>
    public partial class Sign_Up : Window
    {
        public Sign_Up()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите выйти?", "выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            input_login.Clear();
            input_password.Clear();
            input_passwordrepeat.Clear();
            labelPasswordCheck_1.Content = "";
            labelPasswordCheck_2.Content = "";
            labelPasswordCheck_3.Content = "";
            labelPasswordCheck_4.Content = "";
        }
        private bool CheckSameLogin(string login)
        {
            
            string[] line_inFile;
            string msg = "";



            using (StreamReader sr = new StreamReader("reg.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line_inFile = line.Split(new char[] { ' ' });
                    string login_inFile = line_inFile[0];
                    if(login==login_inFile)
                    {
                        return false;
                    }
                   
                  
                }
            }
            return true;
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string password = input_password.Password;
            Password check = new Password();
            check.Error += ErrorToString;
        
           // check.Success += SuccessMessage;

            bool minlenght = check.MinLenght(password);
            bool checksymbol = check.CheckSymbols(password);
            bool checklatin = check.CheckAphabet(password);
            bool check_unic_login = CheckSameLogin(input_login.Text);
          if(minlenght && checksymbol && checklatin &&(input_password.Password==input_passwordrepeat.Password) && input_login.Text != string.Empty && check_unic_login)
            {
                using StreamWriter file = new StreamWriter("reg.txt",true);
                string log_pas;
                log_pas = input_login.Text;
                log_pas += " ";
                log_pas += input_password.Password;
                SuccessMessage("Регистрация прошла успешна");

                file.WriteLine(log_pas);
            }
            else
            {
                if (input_passwordrepeat.Password != input_password.Password)
                {
                    ErrorToString("пароли не совпадают");
                }
                if (input_login.Text == string.Empty)
                {
                    ErrorToString("поле для логина пусты");
                }
                if(check_unic_login==false)
                {
                    ErrorToString("логин не уникален");
                }
                ErrorMessage(msg);
               
                msg = "";
            }

        }
        private string msg;
        private void ErrorToString(string message)
        {
            msg += message;
            msg += "\n";
        }
        private void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void SuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void input_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password pass = new Password();
            string password = input_password.Password;
            
            if(pass.MinLenght(password))
                {
                labelPasswordCheck_1.Content = "Оптимальная длина";
            }else
            {
                labelPasswordCheck_1.Content = "Пароль должен содержать больше 8 символов";
            }
            if(pass.CheckSymbols(password))
            {
                labelPasswordCheck_2.Content = "Пароль содержит хотя бы одну строчную и заглавную букву, цифру, и спец символ";
            }
            else
            {
                labelPasswordCheck_2.Content = "Пароль должен содержать хотя бы одну строчную и заглавную букву, цифру, и спец символ";
            }
            if(!pass.CheckAphabet(password))
            {
                labelPasswordCheck_3.Content = "Пароль должен содержать только латинские буквы";
            }
            else
            {
                labelPasswordCheck_3.Content = "Пароль содержит только латинские буквы";
            }
            if(input_password.Password==input_passwordrepeat.Password)
            {
                labelPasswordCheck_4.Content = "Пароли совпадают";
            }
            else
            {
                labelPasswordCheck_4.Content = "Пароли не совпадают";
            }
        }

        private void input_passwordrepeat_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (input_password.Password == input_passwordrepeat.Password)
            {
                labelPasswordCheck_4.Content = "Пароли совпадают";
            }
            else
            {
                labelPasswordCheck_4.Content = "Пароли не совпадают";
            }
        }
    }
}
