using Fusion;
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
using System.Windows.Shapes;

namespace Zelu.Views
{

    public partial class AuthView : Window
    {
        private static FusionApp App = new FusionApp("62146009083");
        public AuthView()
        {
            InitializeComponent();
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            panelLogin.Visibility = Visibility.Hidden;
            panelRegister.Visibility = Visibility.Visible;
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            panelRegister.Visibility = Visibility.Hidden;
            panelLogin.Visibility = Visibility.Visible;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            var loginResponse = await App.Login(tbUsername.Text, tbPassword.Password, "", true, false);
            if (loginResponse.Error == false)
            {
                this.Hide();
                MainView f2 = new MainView();
                f2.Show();


            }
            else
            {
               
            }

        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerReponse = await App.Register(tbRegisterUser.Text, tbRegisterPass.Password, tbRegisterKey.Password);
            if(registerReponse.Error == false)
            {
                
            }
            else
            {
                
            }
        }
    }
}
