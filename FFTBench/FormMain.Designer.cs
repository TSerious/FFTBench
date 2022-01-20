namespace FFTBench
{
    partial class FormMain
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnChoose = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.comboSize1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.comboSize2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.comboRepeat = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExplore = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExplore,
            this.toolStripSeparator4,
            this.btnChoose,
            this.toolStripLabel1,
            this.comboSize1,
            this.toolStripLabel2,
            this.comboSize2,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.comboRepeat,
            this.toolStripSeparator1,
            this.btnRun,
            this.toolStripSeparator3,
            this.btnCopy,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnChoose
            // 
            this.btnChoose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnChoose.Image = global::FFTBench.Properties.Resources.properties;
            this.btnChoose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(23, 22);
            this.btnChoose.Text = "Choose Tests";
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(30, 22);
            this.toolStripLabel1.Text = "Size:";
            // 
            // comboSize1
            // 
            this.comboSize1.AutoCompleteCustomSource.AddRange(new string[] {
            "128",
            "200",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192",
            "16384"});
            this.comboSize1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSize1.Items.AddRange(new object[] {
            "128",
            "200",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192",
            "16384"});
            this.comboSize1.Name = "comboSize1";
            this.comboSize1.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(17, 22);
            this.toolStripLabel2.Text = "to";
            // 
            // comboSize2
            // 
            this.comboSize2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSize2.Items.AddRange(new object[] {
            "128",
            "200",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192",
            "16384"});
            this.comboSize2.Name = "comboSize2";
            this.comboSize2.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel3.Text = "Repeat:";
            // 
            // comboRepeat
            // 
            this.comboRepeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRepeat.Items.AddRange(new object[] {
            "50",
            "100",
            "200",
            "500"});
            this.comboRepeat.Name = "comboRepeat";
            this.comboRepeat.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRun
            // 
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRun.Image = global::FFTBench.Properties.Resources.play;
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(23, 22);
            this.btnRun.Text = "Run Benchmark";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExplore
            // 
            this.btnExplore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExplore.Image = global::FFTBench.Properties.Resources.graph;
            this.btnExplore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExplore.Name = "btnExplore";
            this.btnExplore.Size = new System.Drawing.Size(23, 22);
            this.btnExplore.Text = "Open FFT Explorer";
            this.btnExplore.Click += new System.EventHandler(this.btnExplore_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::FFTBench.Properties.Resources.log;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Text = "Copy results to clipboard";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::FFTBench.Properties.Resources.save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save image";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 451);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(675, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 426);
            this.panel1.TabIndex = 7;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 473);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormMain";
            this.Text = "FFT Benchmark";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox comboSize1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox comboSize2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox comboRepeat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripButton btnExplore;
        private System.Windows.Forms.ToolStripButton btnChoose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

