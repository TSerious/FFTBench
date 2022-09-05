namespace FFTBench
{
    partial class FormChoose
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
            this.components = new System.ComponentModel.Container();
            this.tests = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListDummy = new System.Windows.Forms.ImageList(this.components);
            this.deselectAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.tests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tests.CheckBoxes = true;
            this.tests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.tests.FullRowSelect = true;
            this.tests.GridLines = true;
            this.tests.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.tests.HideSelection = false;
            this.tests.Location = new System.Drawing.Point(0, 0);
            this.tests.Name = "listView1";
            this.tests.Size = new System.Drawing.Size(293, 449);
            this.tests.SmallImageList = this.imageListDummy;
            this.tests.TabIndex = 0;
            this.tests.UseCompatibleStateImageBehavior = false;
            this.tests.View = System.Windows.Forms.View.Details;
            this.tests.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.Tests_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 220;
            // 
            // imageListDummy
            // 
            this.imageListDummy.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListDummy.ImageSize = new System.Drawing.Size(20, 20);
            this.imageListDummy.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // deselectAll
            // 
            this.deselectAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.deselectAll.Location = new System.Drawing.Point(0, 455);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(293, 23);
            this.deselectAll.TabIndex = 1;
            this.deselectAll.Text = "deselect all";
            this.deselectAll.UseVisualStyleBackColor = true;
            this.deselectAll.Click += new System.EventHandler(this.DeselectAll_Click);
            // 
            // FormChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 478);
            this.Controls.Add(this.deselectAll);
            this.Controls.Add(this.tests);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChoose";
            this.Text = "Choose Tests";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView tests;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageListDummy;
        private System.Windows.Forms.Button deselectAll;
    }
}