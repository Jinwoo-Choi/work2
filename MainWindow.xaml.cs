using System;
using System.IO;
using System.Windows;
using System.Windows.Input;


namespace work1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        mysql manager = new mysql();
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // DB Connection
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);

            manager.Initialize();
        }

        #region Login

        public class LoginEventArgs : EventArgs
        {
            public bool isSuccess;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            LoginEventArgs args = new LoginEventArgs();
            if(tbpw.Text.Length > 0)
            { 
                manager.Select("users", 2, tbemail.Text, tbpw.Text);
            }
            if (App.DataSearchResult == true)
            {
                args.isSuccess = true;
            }

            if (args.isSuccess == true)
            {
                //MessageBox.Show("로그인에 성공하셨습니다!");
                
                work1.MainPage mainPage = new work1.MainPage();
                mainPage.Show();
                this.Close();
                //gdInfo.Visibility = Visibility.Collapsed;
                //tbMsg.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("로그인에 실패하셨습니다.");
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnLogin_Click_1(sender, e);
            }
        }
        private void CenterWindowOnScreen()

        {

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;

            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            double windowWidth = this.Width;

            double windowHeight = this.Height;

            this.Left = (screenWidth / 2) - (windowWidth / 2);

            this.Top = (screenHeight / 2) - (windowHeight / 2);

        }
    }
}
