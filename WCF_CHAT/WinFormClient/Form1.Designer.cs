
namespace WinFormClient
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserNameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConDisConButton = new System.Windows.Forms.Button();
            this.ChatMessageList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PlaceHolder = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MessageText = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserNameBox
            // 
            this.UserNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserNameBox.Location = new System.Drawing.Point(95, 8);
            this.UserNameBox.Name = "UserNameBox";
            this.UserNameBox.Size = new System.Drawing.Size(238, 22);
            this.UserNameBox.TabIndex = 0;
            this.UserNameBox.Enter += new System.EventHandler(this.UserNameBox_GotFocus);
            this.UserNameBox.Leave += new System.EventHandler(this.UserNameBox_LostFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "User name";
            // 
            // ConDisConButton
            // 
            this.ConDisConButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConDisConButton.Location = new System.Drawing.Point(339, 5);
            this.ConDisConButton.Name = "ConDisConButton";
            this.ConDisConButton.Size = new System.Drawing.Size(105, 28);
            this.ConDisConButton.TabIndex = 2;
            this.ConDisConButton.Text = "Connect";
            this.ConDisConButton.UseVisualStyleBackColor = true;
            this.ConDisConButton.Click += new System.EventHandler(this.ConDisConButton_Click);
            // 
            // ChatMessageList
            // 
            this.ChatMessageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatMessageList.FormattingEnabled = true;
            this.ChatMessageList.ItemHeight = 16;
            this.ChatMessageList.Location = new System.Drawing.Point(5, 47);
            this.ChatMessageList.Name = "ChatMessageList";
            this.ChatMessageList.Size = new System.Drawing.Size(454, 260);
            this.ChatMessageList.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PlaceHolder);
            this.panel1.Controls.Add(this.ConDisConButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.UserNameBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 42);
            this.panel1.TabIndex = 4;
            // 
            // PlaceHolder
            // 
            this.PlaceHolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaceHolder.BackColor = System.Drawing.SystemColors.Window;
            this.PlaceHolder.Enabled = false;
            this.PlaceHolder.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.PlaceHolder.Location = new System.Drawing.Point(109, 11);
            this.PlaceHolder.Name = "PlaceHolder";
            this.PlaceHolder.Size = new System.Drawing.Size(208, 14);
            this.PlaceHolder.TabIndex = 3;
            this.PlaceHolder.Text = "User name";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.MessageText);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(5, 307);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel2.Size = new System.Drawing.Size(454, 135);
            this.panel2.TabIndex = 5;
            // 
            // MessageText
            // 
            this.MessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageText.Location = new System.Drawing.Point(0, 5);
            this.MessageText.Multiline = true;
            this.MessageText.Name = "MessageText";
            this.MessageText.Size = new System.Drawing.Size(454, 130);
            this.MessageText.TabIndex = 2;
            this.MessageText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageBox_KeyDown);
            this.MessageText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MessageText_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 447);
            this.Controls.Add(this.ChatMessageList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Grid_Loaded);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox UserNameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ConDisConButton;
        private System.Windows.Forms.ListBox ChatMessageList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label PlaceHolder;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox MessageText;
    }
}

