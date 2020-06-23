using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;


namespace Accountwin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hyper_Signin_Click(object sender, RoutedEventArgs e)
        {
            Sign_Up sign_Up = new Sign_Up();
            sign_Up.Show();
        }

        private void Hyper_password_Click(object sender, RoutedEventArgs e)
        {
            ForgotYourPassword forgotYourPassword = new ForgotYourPassword();
            forgotYourPassword.Show();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string msg;
            msg = input_login.Text;
            msg += " ";
            msg += input_password.Password;
            using (StreamReader sr = new StreamReader("reg.txt"))
            {
                string line;
                int i = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    if (msg == line)
                    {
                        i = 1;
                        break;
                    }

                }
                if (i == 0)
                    MessageBox.Show("введите корректный пароль или зарегистрируйтесь", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show("вход успешно осуществлен", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            input_login.Clear();
            input_password.Clear();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите выйти?", "выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(res == MessageBoxResult.Yes)
            { 
                Close();
            }
           
        }
    }
}
