using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace WinFormClient
{
    public partial class Form1 : Form,ChatService.IServiceChaterCallback
    {
        ChatService.ServiceChaterClient _chatService;
        bool _isConnected = false;
        int _user_id;
        bool isAnonimus = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void UserNameBox_LostFocus(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                var textbox = sender as TextBox;
                if (string.IsNullOrEmpty(textbox.Text))
                {
                    PlaceHolder.Visible = true;
                }
            }
        }

        void ConnectDisConnect()
        {
            if (!_isConnected)
            {

                _user_id = _chatService.Connect(UserNameBox.Text == "" ? "Anonimus" : UserNameBox.Text);
                if (_user_id != -1)
                {
                    UserNameBox.Enabled = false;
                    ConDisConButton.Text = "Disconnect";
                    ChatMessageList.Items.Clear();
                    _chatService.GetHistory(_user_id);
                }
            }
            else
            {
                _chatService.Disconnect(_user_id);
                _user_id = -1;
                UserNameBox.Enabled = true;
                ConDisConButton.Text = "Connect";
            }
            _isConnected = !_isConnected;
            if ((UserNameBox.Text == "Anonimus" || UserNameBox.Text == ""))
                CheckAnonimus();
        }

        private void UserNameBox_GotFocus(object sender, EventArgs e)
        {
            PlaceHolder.Visible = false;
        }

        public void MessageCallBack(string mes)
        {
            ChatMessageList.Items.Add(mes);
            ChatMessageList.SelectedIndex=ChatMessageList.Items.Count-1;
            ChatMessageList.SelectedIndex = -1;
        }

        private void ConDisConButton_Click(object sender, EventArgs e)
        {
            ConnectDisConnect();
        }

        bool enterd = false;
        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _user_id > 0 && MessageText.Text != "")
            {
                _chatService.SendMessage(MessageText.Text, _user_id);
                enterd = true;
            }
        }

        private void Grid_Loaded(object sender, EventArgs e)
        {
            _chatService = new ChatService.ServiceChaterClient(
                new System.ServiceModel.InstanceContext(this));
        }

        void CheckAnonimus()
        {
            isAnonimus = !isAnonimus;
            if (isAnonimus)
            {
                this.BackColor = Color.DimGray;
                foreach (Control cont in this.Controls)
                {
                    foreach(Control c in cont.Controls)
                    if (!(c is Button))
                    {
                        c.BackColor = Color.DimGray;
                        if (c.Name != "PlaceHolder" )
                            c.ForeColor = Color.White;
                    }
                    if (!(cont is Button))
                    {
                        cont.BackColor = Color.DimGray;
                        if (cont.Name != "PlaceHolder")
                            cont.ForeColor = Color.White;
                    }
                }
            }
            else
            {
                this.BackColor = SystemColors.Control;
                foreach (Control cont in this.Controls)
                {
                    foreach (Control c in cont.Controls)
                    {
                        if (!(c is Button))
                        {
                            if (c is Panel)
                                c.BackColor = SystemColors.Control;
                            else
                                c.BackColor = Color.White;
                            if (c.Name != "PlaceHolder")
                                c.ForeColor = Color.Black;
                        }
                    }
                    if (!(cont is Button))
                    {
                        if (cont is Panel)
                            cont.BackColor = SystemColors.Control;
                        else
                            cont.BackColor = Color.White;
                        if (cont.Name != "PlaceHolder")
                            cont.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isConnected && _chatService.State != System.ServiceModel.CommunicationState.Faulted) 
                _chatService.Disconnect(_user_id);

        }

        private void MessageText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (enterd)
            {
                enterd = false;
                MessageText.Text = "";
                e.Handled = true;
            }
        }
    }
}

