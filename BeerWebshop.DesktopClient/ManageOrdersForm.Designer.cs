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
			dgvOrderlines = new DataGridView();
			textBox3 = new TextBox();
			textBox2 = new TextBox();
			textBox1 = new TextBox();
			lbl_Orderlines = new Label();
			lbl_Status = new Label();
			lbl_Date = new Label();
			lbl_OrderId = new Label();
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
			splitContainer1.Panel2.Controls.Add(dgvOrderlines);
			splitContainer1.Panel2.Controls.Add(textBox3);
			splitContainer1.Panel2.Controls.Add(textBox2);
			splitContainer1.Panel2.Controls.Add(textBox1);
			splitContainer1.Panel2.Controls.Add(lbl_Orderlines);
			splitContainer1.Panel2.Controls.Add(lbl_Status);
			splitContainer1.Panel2.Controls.Add(lbl_Date);
			splitContainer1.Panel2.Controls.Add(lbl_OrderId);
			splitContainer1.Size = new Size(640, 360);
			splitContainer1.SplitterDistance = 211;
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
			lstOrders.Size = new Size(211, 360);
			lstOrders.TabIndex = 0;
			lstOrders.SelectedIndexChanged += lstOrders_SelectedIndexChanged;
			// 
			// dgvOrderlines
			// 
			dgvOrderlines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvOrderlines.Location = new Point(17, 145);
			dgvOrderlines.Name = "dgvOrderlines";
			dgvOrderlines.RowHeadersWidth = 51;
			dgvOrderlines.Size = new Size(396, 203);
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
			// lbl_Orderlines
			// 
			lbl_Orderlines.AutoSize = true;
			lbl_Orderlines.Location = new Point(17, 122);
			lbl_Orderlines.Name = "lbl_Orderlines";
			lbl_Orderlines.Size = new Size(77, 20);
			lbl_Orderlines.TabIndex = 3;
			lbl_Orderlines.Text = "Orderlines";
			// 
			// lbl_Status
			// 
			lbl_Status.AutoSize = true;
			lbl_Status.Location = new Point(17, 69);
			lbl_Status.Name = "lbl_Status";
			lbl_Status.Size = new Size(110, 20);
			lbl_Status.TabIndex = 2;
			lbl_Status.Text = "Delivery Status:";
			// 
			// lbl_Date
			// 
			lbl_Date.AutoSize = true;
			lbl_Date.Location = new Point(17, 42);
			lbl_Date.Name = "lbl_Date";
			lbl_Date.Size = new Size(44, 20);
			lbl_Date.TabIndex = 1;
			lbl_Date.Text = "Date:";
			// 
			// lbl_OrderId
			// 
			lbl_OrderId.AutoSize = true;
			lbl_OrderId.Location = new Point(17, 15);
			lbl_OrderId.Name = "lbl_OrderId";
			lbl_OrderId.Size = new Size(69, 20);
			lbl_OrderId.TabIndex = 0;
			lbl_OrderId.Text = "Order ID:";
			// 
			// ManageOrdersForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(640, 360);
			Controls.Add(splitContainer1);
			Margin = new Padding(2);
			Name = "ManageOrdersForm";
			Text = "ManageOrdersForm";
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
		private Label lbl_Orderlines;
		private Label lbl_OrderId;
		private Label lbl_Date;
		private Label lbl_Status;
		private DataGridView dgvOrderlines;
	}
}