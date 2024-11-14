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
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.SuspendLayout();
			SuspendLayout();
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = DockStyle.Fill;
			splitContainer1.Location = new Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(lstOrders);
			splitContainer1.Size = new Size(800, 450);
			splitContainer1.SplitterDistance = 266;
			splitContainer1.TabIndex = 0;
			// 
			// lstOrders
			// 
			lstOrders.Dock = DockStyle.Fill;
			lstOrders.FormattingEnabled = true;
			lstOrders.ItemHeight = 25;
			lstOrders.Location = new Point(0, 0);
			lstOrders.Name = "lstOrders";
			lstOrders.Size = new Size(266, 450);
			lstOrders.TabIndex = 0;
			lstOrders.SelectedIndexChanged += lstOrders_SelectedIndexChanged;
			// 
			// ManageOrdersForm
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(splitContainer1);
			Name = "ManageOrdersForm";
			Text = "ManageOrdersForm";
			splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private SplitContainer splitContainer1;
		private ListBox lstOrders;
	}
}