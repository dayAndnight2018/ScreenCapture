namespace ScreenCapture
{
    partial class InputForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.fontCbx = new System.Windows.Forms.ComboBox();
            this.colorCbx = new System.Windows.Forms.ComboBox();
            this.drawBtn = new System.Windows.Forms.Button();
            this.cacelBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(165, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入文本后点击绘制";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 43);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(557, 238);
            this.panel2.TabIndex = 6;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(557, 238);
            this.textBox.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DimGray;
            this.panel3.Controls.Add(this.fontCbx);
            this.panel3.Controls.Add(this.colorCbx);
            this.panel3.Controls.Add(this.drawBtn);
            this.panel3.Controls.Add(this.cacelBtn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 281);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(557, 42);
            this.panel3.TabIndex = 7;
            // 
            // fontCbx
            // 
            this.fontCbx.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fontCbx.FormattingEnabled = true;
            this.fontCbx.Items.AddRange(new object[] {
            "小号",
            "中号",
            "大号"});
            this.fontCbx.Location = new System.Drawing.Point(102, 6);
            this.fontCbx.Name = "fontCbx";
            this.fontCbx.Size = new System.Drawing.Size(84, 32);
            this.fontCbx.TabIndex = 3;
            this.fontCbx.SelectedValueChanged += new System.EventHandler(this.fontCbx_SelectedValueChanged);
            // 
            // colorCbx
            // 
            this.colorCbx.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.colorCbx.FormattingEnabled = true;
            this.colorCbx.Items.AddRange(new object[] {
            "红色",
            "橙色",
            "黄色",
            "绿色",
            "蓝色",
            "紫色",
            "黑色",
            "白色"});
            this.colorCbx.Location = new System.Drawing.Point(3, 6);
            this.colorCbx.Name = "colorCbx";
            this.colorCbx.Size = new System.Drawing.Size(84, 32);
            this.colorCbx.TabIndex = 2;
            this.colorCbx.SelectedValueChanged += new System.EventHandler(this.colorCbx_SelectedValueChanged);
            // 
            // drawBtn
            // 
            this.drawBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.drawBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.drawBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.drawBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawBtn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.drawBtn.ForeColor = System.Drawing.Color.White;
            this.drawBtn.Location = new System.Drawing.Point(389, 0);
            this.drawBtn.Margin = new System.Windows.Forms.Padding(10, 10, 40, 10);
            this.drawBtn.Name = "drawBtn";
            this.drawBtn.Size = new System.Drawing.Size(84, 42);
            this.drawBtn.TabIndex = 1;
            this.drawBtn.Text = "绘制";
            this.drawBtn.UseVisualStyleBackColor = true;
            // 
            // cacelBtn
            // 
            this.cacelBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.cacelBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.cacelBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.cacelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cacelBtn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cacelBtn.ForeColor = System.Drawing.Color.White;
            this.cacelBtn.Location = new System.Drawing.Point(473, 0);
            this.cacelBtn.Margin = new System.Windows.Forms.Padding(40, 10, 10, 10);
            this.cacelBtn.Name = "cacelBtn";
            this.cacelBtn.Size = new System.Drawing.Size(84, 42);
            this.cacelBtn.TabIndex = 0;
            this.cacelBtn.Text = "取消";
            this.cacelBtn.UseVisualStyleBackColor = true;
            this.cacelBtn.Click += new System.EventHandler(this.cacelBtn_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 323);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InputForm";
            this.Text = "InputForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox fontCbx;
        private System.Windows.Forms.ComboBox colorCbx;
        private System.Windows.Forms.Button drawBtn;
        private System.Windows.Forms.Button cacelBtn;
    }
}