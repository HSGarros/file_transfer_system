namespace Client
{
    partial class Client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtMes = new TextBox();
            txtSendMes = new TextBox();
            txtClientIp = new TextBox();
            txtClientPoint = new TextBox();
            btnContent = new Button();
            btnSend = new Button();
            btnls = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(159, 39);
            label1.Name = "label1";
            label1.Size = new Size(31, 28);
            label1.TabIndex = 1;
            label1.Text = "IP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(578, 39);
            label2.Name = "label2";
            label2.Size = new Size(54, 28);
            label2.TabIndex = 2;
            label2.Text = "Port";
            // 
            // txtMes
            // 
            txtMes.Location = new Point(66, 91);
            txtMes.Multiline = true;
            txtMes.Name = "txtMes";
            txtMes.Size = new Size(955, 446);
            txtMes.TabIndex = 3;
            // 
            // txtSendMes
            // 
            txtSendMes.Location = new Point(228, 567);
            txtSendMes.Name = "txtSendMes";
            txtSendMes.Size = new Size(595, 34);
            txtSendMes.TabIndex = 4;
            // 
            // txtClientIp
            // 
            txtClientIp.Location = new Point(228, 36);
            txtClientIp.Name = "txtClientIp";
            txtClientIp.Size = new Size(302, 34);
            txtClientIp.TabIndex = 6;
            // 
            // txtClientPoint
            // 
            txtClientPoint.Location = new Point(656, 36);
            txtClientPoint.Name = "txtClientPoint";
            txtClientPoint.Size = new Size(122, 34);
            txtClientPoint.TabIndex = 7;
            txtClientPoint.Text = "40000";
            txtClientPoint.TextChanged += txtClientPoint_TextChanged;
            // 
            // btnContent
            // 
            btnContent.Location = new Point(869, 33);
            btnContent.Name = "btnContent";
            btnContent.Size = new Size(131, 40);
            btnContent.TabIndex = 8;
            btnContent.Text = "link";
            btnContent.UseVisualStyleBackColor = true;
            btnContent.Click += btnContent_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(869, 567);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(131, 40);
            btnSend.TabIndex = 9;
            btnSend.Text = "sent";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // btnls
            // 
            btnls.Location = new Point(66, 567);
            btnls.Name = "btnls";
            btnls.Size = new Size(131, 40);
            btnls.TabIndex = 10;
            btnls.Text = "ls";
            btnls.UseVisualStyleBackColor = true;
            btnls.Click += btnls_Click;
            // 
            // Client
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1085, 644);
            Controls.Add(btnls);
            Controls.Add(btnSend);
            Controls.Add(btnContent);
            Controls.Add(txtClientPoint);
            Controls.Add(txtClientIp);
            Controls.Add(txtSendMes);
            Controls.Add(txtMes);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Client";
            Text = "Client";
            Load += Client_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private TextBox txtMes;
        private TextBox txtSendMes;
        private TextBox txtClientIp;
        private TextBox txtClientPoint;
        private Button btnContent;
        private Button btnSend;
        private Button btnls;
    }
}