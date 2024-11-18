
namespace BeerWebshop.DesktopClient
{
    partial class ViewEditDeleteForm
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
            lstProduct = new ListBox();
            pnlEdit = new Panel();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnBack = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            lblDescription = new Label();
            lblCategory = new Label();
            lblABV = new Label();
            lblStock = new Label();
            lblPrice = new Label();
            lblBrewery = new Label();
            lblName = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlEdit.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lstProduct);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pnlEdit);
            splitContainer1.Size = new Size(941, 672);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // lstProduct
            // 
            lstProduct.Dock = DockStyle.Fill;
            lstProduct.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstProduct.FormattingEnabled = true;
            lstProduct.ItemHeight = 28;
            lstProduct.Location = new Point(0, 0);
            lstProduct.Margin = new Padding(3, 4, 3, 4);
            lstProduct.Name = "lstProduct";
            lstProduct.Size = new Size(266, 672);
            lstProduct.TabIndex = 0;
            lstProduct.SelectedIndexChanged += lstProduct_SelectedIndexChanged;
            // 
            // pnlEdit
            // 
            pnlEdit.Controls.Add(label7);
            pnlEdit.Controls.Add(label6);
            pnlEdit.Controls.Add(label5);
            pnlEdit.Controls.Add(label4);
            pnlEdit.Controls.Add(label3);
            pnlEdit.Controls.Add(label2);
            pnlEdit.Controls.Add(label1);
            pnlEdit.Controls.Add(btnBack);
            pnlEdit.Controls.Add(btnDelete);
            pnlEdit.Controls.Add(btnEdit);
            pnlEdit.Controls.Add(lblDescription);
            pnlEdit.Controls.Add(lblCategory);
            pnlEdit.Controls.Add(lblABV);
            pnlEdit.Controls.Add(lblStock);
            pnlEdit.Controls.Add(lblPrice);
            pnlEdit.Controls.Add(lblBrewery);
            pnlEdit.Controls.Add(lblName);
            pnlEdit.Dock = DockStyle.Fill;
            pnlEdit.Location = new Point(0, 0);
            pnlEdit.Margin = new Padding(3, 4, 3, 4);
            pnlEdit.Name = "pnlEdit";
            pnlEdit.Size = new Size(670, 672);
            pnlEdit.TabIndex = 0;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F);
            label7.Location = new Point(315, 133);
            label7.Name = "label7";
            label7.Size = new Size(136, 32);
            label7.TabIndex = 16;
            label7.Text = "Beskrivelse:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(45, 455);
            label6.Name = "label6";
            label6.Size = new Size(91, 28);
            label6.TabIndex = 15;
            label6.Text = "Kategori:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(45, 388);
            label5.Name = "label5";
            label5.Size = new Size(52, 28);
            label5.TabIndex = 14;
            label5.Text = "ABV:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(45, 319);
            label4.Name = "label4";
            label4.Size = new Size(165, 28);
            label4.TabIndex = 13;
            label4.Text = "Lagerbeholdning:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(45, 237);
            label3.Name = "label3";
            label3.Size = new Size(47, 28);
            label3.TabIndex = 12;
            label3.Text = "Pris:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(45, 151);
            label2.Name = "label2";
            label2.Size = new Size(90, 28);
            label2.TabIndex = 11;
            label2.Text = "Bryggeri:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(45, 71);
            label1.Name = "label1";
            label1.Size = new Size(62, 28);
            label1.TabIndex = 10;
            label1.Text = "Navn:";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(570, 589);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(86, 31);
            btnBack.TabIndex = 9;
            btnBack.Text = "&Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(160, 589);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(97, 31);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "&Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(46, 589);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(107, 31);
            btnEdit.TabIndex = 7;
            btnEdit.Text = "&Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 12F);
            lblDescription.Location = new Point(315, 167);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(341, 404);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "Description";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 12F);
            lblCategory.Location = new Point(174, 455);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(92, 28);
            lblCategory.TabIndex = 5;
            lblCategory.Text = "Category";
            // 
            // lblABV
            // 
            lblABV.AutoSize = true;
            lblABV.Font = new Font("Segoe UI", 12F);
            lblABV.Location = new Point(174, 388);
            lblABV.Name = "lblABV";
            lblABV.Size = new Size(48, 28);
            lblABV.TabIndex = 4;
            lblABV.Text = "ABV";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Font = new Font("Segoe UI", 12F);
            lblStock.Location = new Point(174, 319);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(60, 28);
            lblStock.TabIndex = 3;
            lblStock.Text = "Stock";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 12F);
            lblPrice.Location = new Point(174, 237);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(54, 28);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "Price";
            // 
            // lblBrewery
            // 
            lblBrewery.AutoSize = true;
            lblBrewery.Font = new Font("Segoe UI", 12F);
            lblBrewery.Location = new Point(174, 151);
            lblBrewery.Name = "lblBrewery";
            lblBrewery.Size = new Size(81, 28);
            lblBrewery.TabIndex = 1;
            lblBrewery.Text = "Brewery";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 12F);
            lblName.Location = new Point(174, 71);
            lblName.Name = "lblName";
            lblName.Size = new Size(64, 28);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // ViewEditDeleteForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(941, 672);
            Controls.Add(splitContainer1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ViewEditDeleteForm";
            Text = "Produktoversigt";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlEdit.ResumeLayout(false);
            pnlEdit.PerformLayout();
            ResumeLayout(false);
        }



        #endregion

        private SplitContainer splitContainer1;
        private ListBox lstProduct;
        private Panel pnlEdit;
        private Label lblName;
        private Button btnBack;
        private Button btnDelete;
        private Button btnEdit;
        private Label lblCategory;
        private Label lblABV;
        private Label lblStock;
        private Label lblPrice;
        private Label lblBrewery;
        private Label label1;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label7;
        private Label lblDescription;
    }
}