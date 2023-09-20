namespace Servers
{
    partial class Server
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
            txtHostIp = new TextBox();
            IP = new Label();
            btnListen = new Button();
            Port = new Label();
            txtHostPoint = new TextBox();
            txtShowMes = new TextBox();
            btnUpload = new Button();
            txtFilePath = new TextBox();
            SuspendLayout();
            // 
            // txtHostIp
            // 
            txtHostIp.Location = new Point(184, 37);
            txtHostIp.Name = "txtHostIp";
            txtHostIp.Size = new Size(299, 34);
            txtHostIp.TabIndex = 0;
            // 
            // IP
            // 
            IP.AutoSize = true;
            IP.Location = new Point(123, 37);
            IP.Name = "IP";
            IP.Size = new Size(31, 28);
            IP.TabIndex = 1;
            IP.Text = "IP";
            IP.Click += label1_Click;
            // 
            // btnListen
            // 
            btnListen.Location = new Point(837, 31);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(92, 40);
            btnListen.TabIndex = 4;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // Port
            // 
            Port.AutoSize = true;
            Port.Location = new Point(542, 37);
            Port.Name = "Port";
            Port.Size = new Size(54, 28);
            Port.TabIndex = 6;
            Port.Text = "Port";
            // 
            // txtHostPoint
            // 
            txtHostPoint.Location = new Point(635, 37);
            txtHostPoint.Name = "txtHostPoint";
            txtHostPoint.Size = new Size(129, 34);
            txtHostPoint.TabIndex = 5;
            txtHostPoint.Text = "50000";
            // 
            // txtShowMes
            // 
            txtShowMes.Location = new Point(54, 97);
            txtShowMes.Multiline = true;
            txtShowMes.Name = "txtShowMes";
            txtShowMes.Size = new Size(959, 455);
            txtShowMes.TabIndex = 7;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(635, 587);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(358, 40);
            btnUpload.TabIndex = 8;
            btnUpload.Text = "upload file";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(123, 587);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(434, 34);
            txtFilePath.TabIndex = 9;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 671);
            Controls.Add(txtFilePath);
            Controls.Add(btnUpload);
            Controls.Add(txtShowMes);
            Controls.Add(Port);
            Controls.Add(txtHostPoint);
            Controls.Add(btnListen);
            Controls.Add(IP);
            Controls.Add(txtHostIp);
            Name = "Server";
            Text = "Servers";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtHostIp;
        private Label IP;
        private Button btnListen;
        private Label Port;
        private TextBox txtHostPoint;
        private TextBox txtShowMes;
        private Button btnUpload;
        private TextBox txtFilePath;
    }
}