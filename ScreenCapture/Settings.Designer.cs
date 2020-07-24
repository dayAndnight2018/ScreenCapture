namespace ScreenCapture
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.screenTbx = new System.Windows.Forms.TextBox();
            this.sniffTbx = new System.Windows.Forms.TextBox();
            this.sceenCbx = new System.Windows.Forms.CheckBox();
            this.sniffCbx = new System.Windows.Forms.CheckBox();
            this.screenBtn = new System.Windows.Forms.Button();
            this.sniffBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "截图快捷键";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "取色快捷键";
            // 
            // screenTbx
            // 
            this.screenTbx.Enabled = false;
            this.screenTbx.Location = new System.Drawing.Point(128, 57);
            this.screenTbx.Name = "screenTbx";
            this.screenTbx.Size = new System.Drawing.Size(188, 21);
            this.screenTbx.TabIndex = 2;
            this.screenTbx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.screenTbx_KeyDown);
            // 
            // sniffTbx
            // 
            this.sniffTbx.Enabled = false;
            this.sniffTbx.Location = new System.Drawing.Point(128, 120);
            this.sniffTbx.Name = "sniffTbx";
            this.sniffTbx.Size = new System.Drawing.Size(188, 21);
            this.sniffTbx.TabIndex = 3;
            this.sniffTbx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sniffTbx_KeyDown);
            // 
            // sceenCbx
            // 
            this.sceenCbx.AutoSize = true;
            this.sceenCbx.Location = new System.Drawing.Point(333, 61);
            this.sceenCbx.Name = "sceenCbx";
            this.sceenCbx.Size = new System.Drawing.Size(48, 16);
            this.sceenCbx.TabIndex = 4;
            this.sceenCbx.Text = "变更";
            this.sceenCbx.UseVisualStyleBackColor = true;
            this.sceenCbx.CheckedChanged += new System.EventHandler(this.sceenCbx_CheckedChanged);
            // 
            // sniffCbx
            // 
            this.sniffCbx.AutoSize = true;
            this.sniffCbx.Location = new System.Drawing.Point(333, 120);
            this.sniffCbx.Name = "sniffCbx";
            this.sniffCbx.Size = new System.Drawing.Size(48, 16);
            this.sniffCbx.TabIndex = 5;
            this.sniffCbx.Text = "变更";
            this.sniffCbx.UseVisualStyleBackColor = true;
            this.sniffCbx.CheckedChanged += new System.EventHandler(this.sniffCbx_CheckedChanged);
            // 
            // screenBtn
            // 
            this.screenBtn.Location = new System.Drawing.Point(397, 57);
            this.screenBtn.Name = "screenBtn";
            this.screenBtn.Size = new System.Drawing.Size(58, 23);
            this.screenBtn.TabIndex = 6;
            this.screenBtn.Text = "确认";
            this.screenBtn.UseVisualStyleBackColor = true;
            this.screenBtn.Click += new System.EventHandler(this.screenBtn_Click);
            // 
            // sniffBtn
            // 
            this.sniffBtn.Location = new System.Drawing.Point(397, 118);
            this.sniffBtn.Name = "sniffBtn";
            this.sniffBtn.Size = new System.Drawing.Size(58, 23);
            this.sniffBtn.TabIndex = 7;
            this.sniffBtn.Text = "确认";
            this.sniffBtn.UseVisualStyleBackColor = true;
            this.sniffBtn.Click += new System.EventHandler(this.sniffBtn_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 234);
            this.Controls.Add(this.sniffBtn);
            this.Controls.Add(this.screenBtn);
            this.Controls.Add(this.sniffCbx);
            this.Controls.Add(this.sceenCbx);
            this.Controls.Add(this.sniffTbx);
            this.Controls.Add(this.screenTbx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox screenTbx;
        private System.Windows.Forms.TextBox sniffTbx;
        private System.Windows.Forms.CheckBox sceenCbx;
        private System.Windows.Forms.CheckBox sniffCbx;
        private System.Windows.Forms.Button screenBtn;
        private System.Windows.Forms.Button sniffBtn;
    }
}