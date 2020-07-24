namespace ScreenCapture
{
    partial class ColorSniffPan
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.HexTbx = new System.Windows.Forms.TextBox();
            this.hexBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dexTbx = new System.Windows.Forms.TextBox();
            this.DexBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 60);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.HexTbx);
            this.panel3.Controls.Add(this.hexBtn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(358, 30);
            this.panel3.TabIndex = 1;
            // 
            // HexTbx
            // 
            this.HexTbx.BackColor = System.Drawing.Color.DimGray;
            this.HexTbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HexTbx.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HexTbx.ForeColor = System.Drawing.Color.White;
            this.HexTbx.Location = new System.Drawing.Point(0, 0);
            this.HexTbx.Name = "HexTbx";
            this.HexTbx.ReadOnly = true;
            this.HexTbx.Size = new System.Drawing.Size(236, 29);
            this.HexTbx.TabIndex = 1;
            this.HexTbx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // hexBtn
            // 
            this.hexBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.hexBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hexBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hexBtn.ForeColor = System.Drawing.Color.White;
            this.hexBtn.Location = new System.Drawing.Point(236, 0);
            this.hexBtn.Name = "hexBtn";
            this.hexBtn.Size = new System.Drawing.Size(122, 30);
            this.hexBtn.TabIndex = 0;
            this.hexBtn.Text = "复制十六进制";
            this.hexBtn.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dexTbx);
            this.panel2.Controls.Add(this.DexBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(358, 30);
            this.panel2.TabIndex = 0;
            // 
            // dexTbx
            // 
            this.dexTbx.BackColor = System.Drawing.Color.DimGray;
            this.dexTbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dexTbx.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dexTbx.ForeColor = System.Drawing.Color.White;
            this.dexTbx.Location = new System.Drawing.Point(0, 0);
            this.dexTbx.Name = "dexTbx";
            this.dexTbx.ReadOnly = true;
            this.dexTbx.Size = new System.Drawing.Size(236, 29);
            this.dexTbx.TabIndex = 2;
            this.dexTbx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DexBtn
            // 
            this.DexBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.DexBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DexBtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DexBtn.ForeColor = System.Drawing.Color.White;
            this.DexBtn.Location = new System.Drawing.Point(236, 0);
            this.DexBtn.Name = "DexBtn";
            this.DexBtn.Size = new System.Drawing.Size(122, 30);
            this.DexBtn.TabIndex = 1;
            this.DexBtn.Text = "复制RGB";
            this.DexBtn.UseVisualStyleBackColor = true;
            // 
            // ColorSniffPan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(358, 60);
            this.MinimumSize = new System.Drawing.Size(358, 60);
            this.Name = "ColorSniffPan";
            this.Size = new System.Drawing.Size(358, 60);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox HexTbx;
        private System.Windows.Forms.Button hexBtn;
        private System.Windows.Forms.TextBox dexTbx;
        private System.Windows.Forms.Button DexBtn;
    }
}
