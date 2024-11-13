namespace BeerWebshop.DesktopClient
{
    partial class MainForm
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
            grpFrontpage = new GroupBox();
            splitContainer1 = new SplitContainer();
            grpCrudProducts = new GroupBox();
            button1 = new Button();
            btnViewProducts = new Button();
            button3 = new Button();
            btnAddProducts = new Button();
            btnDeleteProducts = new Button();
            btnEditProducts = new Button();
            grpOrders = new GroupBox();
            btnOpenOrders = new Button();
            btnPreviousOrders = new Button();
            grpFrontpage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            grpCrudProducts.SuspendLayout();
            grpOrders.SuspendLayout();
            SuspendLayout();
            // 
            // grpFrontpage
            // 
            grpFrontpage.Controls.Add(splitContainer1);
            grpFrontpage.Dock = DockStyle.Top;
            grpFrontpage.Location = new Point(0, 0);
            grpFrontpage.Name = "grpFrontpage";
            grpFrontpage.Size = new Size(782, 754);
            grpFrontpage.TabIndex = 0;
            grpFrontpage.TabStop = false;
            grpFrontpage.Text = "BeerWebshop Admin Page";
            grpFrontpage.Enter += grpFrontpage_Enter;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 39);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(grpOrders);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(grpCrudProducts);
            splitContainer1.Size = new Size(776, 712);
            splitContainer1.SplitterDistance = 258;
            splitContainer1.TabIndex = 0;
            // 
            // grpCrudProducts
            // 
            grpCrudProducts.Controls.Add(btnDeleteProducts);
            grpCrudProducts.Controls.Add(button3);
            grpCrudProducts.Controls.Add(btnEditProducts);
            grpCrudProducts.Controls.Add(btnAddProducts);
            grpCrudProducts.Controls.Add(btnViewProducts);
            grpCrudProducts.Controls.Add(button1);
            grpCrudProducts.Dock = DockStyle.Top;
            grpCrudProducts.Location = new Point(0, 0);
            grpCrudProducts.Name = "grpCrudProducts";
            grpCrudProducts.Size = new Size(514, 712);
            grpCrudProducts.TabIndex = 0;
            grpCrudProducts.TabStop = false;
            grpCrudProducts.Text = "Product Manager";
            // 
            // button1
            // 
            button1.Location = new Point(31, 119);
            button1.Name = "button1";
            button1.Size = new Size(160, 148);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnViewProducts
            // 
            btnViewProducts.BackColor = SystemColors.Highlight;
            btnViewProducts.Location = new Point(31, 461);
            btnViewProducts.Name = "btnViewProducts";
            btnViewProducts.Size = new Size(160, 148);
            btnViewProducts.TabIndex = 0;
            btnViewProducts.Text = "&View products";
            btnViewProducts.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.Location = new Point(300, 461);
            button3.Name = "button3";
            button3.Size = new Size(160, 148);
            button3.TabIndex = 0;
            button3.Text = "button1";
            button3.UseVisualStyleBackColor = true;
            // 
            // btnAddProducts
            // 
            btnAddProducts.BackColor = Color.LightGreen;
            btnAddProducts.Location = new Point(31, 119);
            btnAddProducts.Name = "btnAddProducts";
            btnAddProducts.Size = new Size(160, 148);
            btnAddProducts.TabIndex = 0;
            btnAddProducts.Text = "&Add new product";
            btnAddProducts.UseVisualStyleBackColor = false;
            btnAddProducts.Click += btnAddProducts_Click;
            // 
            // btnDeleteProducts
            // 
            btnDeleteProducts.BackColor = Color.Red;
            btnDeleteProducts.Location = new Point(300, 461);
            btnDeleteProducts.Name = "btnDeleteProducts";
            btnDeleteProducts.Size = new Size(160, 148);
            btnDeleteProducts.TabIndex = 0;
            btnDeleteProducts.Text = "&Delete products";
            btnDeleteProducts.UseVisualStyleBackColor = false;
            // 
            // btnEditProducts
            // 
            btnEditProducts.BackColor = Color.Yellow;
            btnEditProducts.Location = new Point(300, 119);
            btnEditProducts.Name = "btnEditProducts";
            btnEditProducts.Size = new Size(160, 148);
            btnEditProducts.TabIndex = 0;
            btnEditProducts.Text = "&Edit products";
            btnEditProducts.UseVisualStyleBackColor = false;
            // 
            // grpOrders
            // 
            grpOrders.Controls.Add(btnPreviousOrders);
            grpOrders.Controls.Add(btnOpenOrders);
            grpOrders.Dock = DockStyle.Top;
            grpOrders.Location = new Point(0, 0);
            grpOrders.Name = "grpOrders";
            grpOrders.Size = new Size(258, 712);
            grpOrders.TabIndex = 0;
            grpOrders.TabStop = false;
            grpOrders.Text = "Orders";
            // 
            // btnOpenOrders
            // 
            btnOpenOrders.BackColor = Color.LightPink;
            btnOpenOrders.Location = new Point(43, 119);
            btnOpenOrders.Name = "btnOpenOrders";
            btnOpenOrders.Size = new Size(160, 148);
            btnOpenOrders.TabIndex = 0;
            btnOpenOrders.Text = "&Open orders";
            btnOpenOrders.UseVisualStyleBackColor = false;
            // 
            // btnPreviousOrders
            // 
            btnPreviousOrders.BackColor = Color.Gray;
            btnPreviousOrders.Location = new Point(43, 461);
            btnPreviousOrders.Name = "btnPreviousOrders";
            btnPreviousOrders.Size = new Size(160, 148);
            btnPreviousOrders.TabIndex = 0;
            btnPreviousOrders.Text = "&Previous orders";
            btnPreviousOrders.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 753);
            Controls.Add(grpFrontpage);
            Font = new Font("Segoe UI", 16F);
            Margin = new Padding(4, 4, 4, 4);
            Name = "MainForm";
            Text = "Frontpage";
            Load += Form1_Load;
            grpFrontpage.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            grpCrudProducts.ResumeLayout(false);
            grpOrders.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpFrontpage;
        private SplitContainer splitContainer1;
        private GroupBox grpCrudProducts;
        private GroupBox grpOrders;
        private Button btnDeleteProducts;
        private Button button3;
        private Button btnEditProducts;
        private Button btnAddProducts;
        private Button btnViewProducts;
        private Button button1;
        private Button btnPreviousOrders;
        private Button btnOpenOrders;
    }
}
