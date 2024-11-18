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
            grpOrders = new GroupBox();
            btnPreviousOrders = new Button();
            btnOpenOrders = new Button();
            grpCrudProducts = new GroupBox();
            btnAddProducts = new Button();
            btnViewProducts = new Button();
            button1 = new Button();
            grpFrontpage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            grpOrders.SuspendLayout();
            grpCrudProducts.SuspendLayout();
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
            grpFrontpage.Text = "BeerWebshop Admin Side";
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
            grpOrders.Text = "Ordrer";
            // 
            // btnPreviousOrders
            // 
            btnPreviousOrders.BackColor = Color.Gray;
            btnPreviousOrders.Location = new Point(43, 461);
            btnPreviousOrders.Name = "btnPreviousOrders";
            btnPreviousOrders.Size = new Size(160, 148);
            btnPreviousOrders.TabIndex = 0;
            btnPreviousOrders.Text = "&Tidligere ordrer";
            btnPreviousOrders.UseVisualStyleBackColor = false;
            // 
            // btnOpenOrders
            // 
            btnOpenOrders.BackColor = Color.LightPink;
            btnOpenOrders.Location = new Point(43, 119);
            btnOpenOrders.Name = "btnOpenOrders";
            btnOpenOrders.Size = new Size(160, 148);
            btnOpenOrders.TabIndex = 0;
            btnOpenOrders.Text = "&Åbne ordrer";
            btnOpenOrders.UseVisualStyleBackColor = false;
            btnOpenOrders.Click += btnOpenOrders_Click;
            // 
            // grpCrudProducts
            // 
            grpCrudProducts.Controls.Add(btnAddProducts);
            grpCrudProducts.Controls.Add(btnViewProducts);
            grpCrudProducts.Controls.Add(button1);
            grpCrudProducts.Dock = DockStyle.Top;
            grpCrudProducts.Location = new Point(0, 0);
            grpCrudProducts.Name = "grpCrudProducts";
            grpCrudProducts.Size = new Size(514, 712);
            grpCrudProducts.TabIndex = 0;
            grpCrudProducts.TabStop = false;
            grpCrudProducts.Text = "Produkter";
            // 
            // btnAddProducts
            // 
            btnAddProducts.BackColor = Color.LightGreen;
            btnAddProducts.Location = new Point(31, 119);
            btnAddProducts.Name = "btnAddProducts";
            btnAddProducts.Size = new Size(171, 148);
            btnAddProducts.TabIndex = 0;
            btnAddProducts.Text = "&Tilføj nyt produkt";
            btnAddProducts.UseVisualStyleBackColor = false;
            btnAddProducts.Click += btnAddProducts_Click;
            // 
            // btnViewProducts
            // 
            btnViewProducts.BackColor = SystemColors.Highlight;
            btnViewProducts.Location = new Point(315, 119);
            btnViewProducts.Name = "btnViewProducts";
            btnViewProducts.Size = new Size(166, 148);
            btnViewProducts.TabIndex = 0;
            btnViewProducts.Text = "&Administrer produkter";
            btnViewProducts.UseVisualStyleBackColor = false;
            btnViewProducts.Click += btnViewProducts_Click;
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 753);
            Controls.Add(grpFrontpage);
            Font = new Font("Segoe UI", 16F);
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "Forside";
            grpFrontpage.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            grpOrders.ResumeLayout(false);
            grpCrudProducts.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpFrontpage;
        private SplitContainer splitContainer1;
        private GroupBox grpCrudProducts;
        private GroupBox grpOrders;
        private Button btnAddProducts;
        private Button btnViewProducts;
        private Button button1;
        private Button btnPreviousOrders;
        private Button btnOpenOrders;
    }
}
