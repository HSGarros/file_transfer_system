namespace Client
{
    partial class Chache
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
            txtClientIp = new TextBox();
            txtClientPoint = new TextBox();
            btnContent = new Button();
            txtChachePoint = new TextBox();
            label4 = new Label();
            btnListen = new Button();
            btnClean = new Button();
            label3 = new Label();
            txtMes2 = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(127, 89);
            label1.Name = "label1";
            label1.Size = new Size(31, 28);
            label1.TabIndex = 1;
            label1.Text = "IP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(576, 129);
            label2.Name = "label2";
            label2.Size = new Size(54, 28);
            label2.TabIndex = 2;
            label2.Text = "Port";
            // 
            // txtMes
            // 
            txtMes.Location = new Point(552, 181);
            txtMes.Multiline = true;
            txtMes.Name = "txtMes";
            txtMes.Size = new Size(467, 548);
            txtMes.TabIndex = 3;
            // 
            // txtClientIp
            // 
            txtClientIp.Location = new Point(218, 86);
            txtClientIp.Name = "txtClientIp";
            txtClientIp.Size = new Size(302, 34);
            txtClientIp.TabIndex = 6;
            // 
            // txtClientPoint
            // 
            txtClientPoint.Location = new Point(654, 126);
            txtClientPoint.Name = "txtClientPoint";
            txtClientPoint.Size = new Size(122, 34);
            txtClientPoint.TabIndex = 7;
            txtClientPoint.Text = "50000";
            // 
            // btnContent
            // 
            btnContent.Location = new Point(867, 117);
            btnContent.Name = "btnContent";
            btnContent.Size = new Size(131, 40);
            btnContent.TabIndex = 8;
            btnContent.Text = "Link";
            btnContent.UseVisualStyleBackColor = true;
            btnContent.Click += btnContent_Click;
            // 
            // txtChachePoint
            // 
            txtChachePoint.Location = new Point(654, 55);
            txtChachePoint.Name = "txtChachePoint";
            txtChachePoint.Size = new Size(122, 34);
            txtChachePoint.TabIndex = 12;
            txtChachePoint.Text = "40000";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(576, 61);
            label4.Name = "label4";
            label4.Size = new Size(54, 28);
            label4.TabIndex = 13;
            label4.Text = "Port";
            // 
            // btnListen
            // 
            btnListen.Location = new Point(867, 49);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(131, 40);
            btnListen.TabIndex = 14;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // btnClean
            // 
            btnClean.Location = new Point(582, 771);
            btnClean.Name = "btnClean";
            btnClean.Size = new Size(131, 40);
            btnClean.TabIndex = 15;
            btnClean.Text = "Clean";
            btnClean.UseVisualStyleBackColor = true;
            btnClean.Click += btnClean_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(291, 777);
            label3.Name = "label3";
            label3.Size = new Size(260, 28);
            label3.TabIndex = 16;
            label3.Text = "clean all cache fragment";
            // 
            // txtMes2
            // 
            txtMes2.Location = new Point(58, 181);
            txtMes2.Multiline = true;
            txtMes2.Name = "txtMes2";
            txtMes2.Size = new Size(462, 548);
            txtMes2.TabIndex = 17;
            // 
            // Chache
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1085, 845);
            Controls.Add(txtMes2);
            Controls.Add(label3);
            Controls.Add(btnClean);
            Controls.Add(btnListen);
            Controls.Add(label4);
            Controls.Add(txtChachePoint);
            Controls.Add(btnContent);
            Controls.Add(txtClientPoint);
            Controls.Add(txtClientIp);
            Controls.Add(txtMes);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Chache";
            Text = "Cache";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private TextBox txtMes;
        private TextBox txtClientIp;
        private TextBox txtClientPoint;
        private Button btnContent;
        private TextBox txtChachePoint;
        private Label label4;
        private Button btnListen;
        private Button btnClean;
        private Label label3;
        private TextBox txtMes2;
    }
}