namespace BeerWebshop.DesktopClient
{
	partial class ManageOrdersForm
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
            splitContainer1 = new SplitContainer();
            lstOrders = new ListBox();
            txtBox_Total = new TextBox();
            lbl_Total = new Label();
            dgvOrderlines = new DataGridView();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            lbl_Status = new Label();
            lbl_Date = new Label();
            lbl_OrderId = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrderlines).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lstOrders);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(txtBox_Total);
            splitContainer1.Panel2.Controls.Add(lbl_Total);
            splitContainer1.Panel2.Controls.Add(dgvOrderlines);
            splitContainer1.Panel2.Controls.Add(textBox3);
            splitContainer1.Panel2.Controls.Add(textBox2);
            splitContainer1.Panel2.Controls.Add(textBox1);
            splitContainer1.Panel2.Controls.Add(lbl_Status);
            splitContainer1.Panel2.Controls.Add(lbl_Date);
            splitContainer1.Panel2.Controls.Add(lbl_OrderId);
            splitContainer1.Size = new Size(640, 360);
            splitContainer1.SplitterDistance = 210;
            splitContainer1.SplitterWidth = 3;
            splitContainer1.TabIndex = 0;
            // 
            // lstOrders
            // 
            lstOrders.Dock = DockStyle.Fill;
            lstOrders.FormattingEnabled = true;
            lstOrders.Location = new Point(0, 0);
            lstOrders.Margin = new Padding(2);
            lstOrders.Name = "lstOrders";
            lstOrders.Size = new Size(210, 360);
            lstOrders.TabIndex = 0;
            lstOrders.SelectedIndexChanged += lstOrders_SelectedIndexChanged;
            // 
            // txtBox_Total
            // 
            txtBox_Total.Location = new Point(133, 91);
            txtBox_Total.Name = "txtBox_Total";
            txtBox_Total.Size = new Size(125, 27);
            txtBox_Total.TabIndex = 10;
            // 
            // lbl_Total
            // 
            lbl_Total.AutoSize = true;
            lbl_Total.Location = new Point(17, 94);
            lbl_Total.Name = "lbl_Total";
            lbl_Total.Size = new Size(42, 20);
            lbl_Total.TabIndex = 9;
            lbl_Total.Text = "Total";
            // 
            // dgvOrderlines
            // 
            dgvOrderlines.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvOrderlines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderlines.Location = new Point(17, 145);
            dgvOrderlines.Name = "dgvOrderlines";
            dgvOrderlines.RowHeadersWidth = 51;
            dgvOrderlines.Size = new Size(399, 205);
            dgvOrderlines.TabIndex = 8;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(133, 66);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(133, 39);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(133, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 4;
            // 
            // lbl_Status
            // 
            lbl_Status.AutoSize = true;
            lbl_Status.Location = new Point(17, 69);
            lbl_Status.Name = "lbl_Status";
            lbl_Status.Size = new Size(112, 20);
            lbl_Status.TabIndex = 2;
            lbl_Status.Text = "Leveringsstatus:";
            // 
            // lbl_Date
            // 
            lbl_Date.AutoSize = true;
            lbl_Date.Location = new Point(17, 42);
            lbl_Date.Name = "lbl_Date";
            lbl_Date.Size = new Size(45, 20);
            lbl_Date.TabIndex = 1;
            lbl_Date.Text = "Dato:";
            // 
            // lbl_OrderId
            // 
            lbl_OrderId.AutoSize = true;
            lbl_OrderId.Location = new Point(17, 15);
            lbl_OrderId.Name = "lbl_OrderId";
            lbl_OrderId.Size = new Size(69, 20);
            lbl_OrderId.TabIndex = 0;
            lbl_OrderId.Text = "Ordre ID:";
            // 
            // ManageOrdersForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 360);
            Controls.Add(splitContainer1);
            Margin = new Padding(2);
            Name = "ManageOrdersForm";
            Text = "Åbne ordrer";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrderlines).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
		private ListBox lstOrders;
		private TextBox textBox3;
		private TextBox textBox2;
		private TextBox textBox1;
		private Label lbl_OrderId;
		private Label lbl_Date;
		private Label lbl_Status;
		private DataGridView dgvOrderlines;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private Label lbl_Total;
		private TextBox txtBox_Total;
	}
}