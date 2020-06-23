using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Interop;
using PasswordCheck2;

namespace Accountwin
{
    /// <summary>
    /// Логика взаимодействия для ForgotYourPassword.xaml
    /// </summary>
    /// /////
    public partial class ForgotYourPassword : Window
    {
        public ForgotYourPassword()
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
        private void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void SuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        string msg1;
        private void ErrorToString(string message)
        {
            msg1 += message;
            msg1 += "\n";
        }

        private void btn_accept_Click(object sender, RoutedEventArgs e)
        {
            /////
            string password = input_newpassword.Password;
            Password check = new Password();
            check.Error += ErrorToString;

        

            bool minlenght = check.MinLenght(password);
            bool checksymbol = check.CheckSymbols(password);
            bool checklatin = check.CheckAphabet(password);
            bool checloginavailability=false;
            if (minlenght && checksymbol && checklatin  && input_login.Text != string.Empty )
            {
                string login = input_login.Text;
                string[] line_inFile;
                string msg = "";



                using (StreamReader sr = new StreamReader("reg.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line_inFile = line.Split(new char[] { ' ' });
                        string login_inFile = line_inFile[0];

                        if (login == login_inFile)
                        {
                            checloginavailability = true;
                            msg += login_inFile + " " + input_newpassword.Password + "\n";
                        }
                        else
                        {
                            msg += line + "\n";
                        }
                    }
                }
                using StreamWriter file = new StreamWriter("reg.txt");
                file.WriteLine(msg);
                
                if (checloginavailability == false)
                {
                    ErrorMessage("такого логина не существует");
                }else
                {
                    SuccessMessage("пароль успешно изменен");
                }

            }
            else
            {
                if (checloginavailability == false)
                {
                    ErrorToString("такого логина не существует");
                }
                if (input_login.Text == string.Empty)
                {
                    ErrorToString("поле для логина пусты");
                }
              
               
                
                ErrorMessage(msg1);

                msg1 = "";
            }
            
            ////////
            ///////////
            //////////

        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            input_login.Clear();
            input_newpassword.Clear();
            //////////////
            ////////////
        }
    }
}
