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

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,ChatService.IServiceChaterCallback
    {
        ChatService.ServiceChaterClient _chatService;
        bool _isConnected = false;
        int _user_id;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                var textbox = sender as TextBox;
                if (string.IsNullOrEmpty(textbox.Text))
                {
                    UsernamePlaceholder.Visibility = Visibility.Visible;
                }
            }
        }

        void ConnectDisConnect()
        {
            if (!_isConnected)
            {
                if(_chatService==null)
                    _chatService = new ChatService.ServiceChaterClient(
                        new System.ServiceModel.InstanceContext(this));
                _user_id = _chatService.Connect(UserNameBox.Text==""?"Anonimus":UserNameBox.Text);
                UserNameBox.IsEnabled = false;
                ConDisConButton.Content = "Disconnect";
            }
            else 
            {
                _chatService.Disconnect(_user_id);
                _user_id = -1;
                UserNameBox.IsEnabled = true;
                ConDisConButton.Content = "Connect";
            }
            _isConnected = !_isConnected;
        }

        private void UserNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UsernamePlaceholder.Visibility = Visibility.Collapsed;
        }

        public void MessageCallBack(string mes)
        {
            ChatMessageList.Items.Add(mes);
        }

        private void ConDisConButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectDisConnect();
        }


        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && _user_id > 0 && MessageBox.Text != "")
            {
                _chatService.SendMessage(MessageBox.Text, _user_id);
                MessageBox.Text = "";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isConnected)
                _chatService.Disconnect(_user_id);
        }
    }
}
