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
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lstProduct);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pnlEdit);
            splitContainer1.Size = new Size(823, 504);
            splitContainer1.SplitterDistance = 233;
            splitContainer1.TabIndex = 0;
            // 
            // lstProduct
            // 
            lstProduct.Dock = DockStyle.Fill;
            lstProduct.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstProduct.FormattingEnabled = true;
            lstProduct.ItemHeight = 21;
            lstProduct.Location = new Point(0, 0);
            lstProduct.Name = "lstProduct";
            lstProduct.Size = new Size(233, 504);
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
            pnlEdit.Name = "pnlEdit";
            pnlEdit.Size = new Size(586, 504);
            pnlEdit.TabIndex = 0;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F);
            label7.Location = new Point(276, 100);
            label7.Name = "label7";
            label7.Size = new Size(151, 25);
            label7.TabIndex = 16;
            label7.Text = "Beer Description";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(39, 341);
            label6.Name = "label6";
            label6.Size = new Size(76, 21);
            label6.TabIndex = 15;
            label6.Text = "Category:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(39, 291);
            label5.Name = "label5";
            label5.Size = new Size(42, 21);
            label5.TabIndex = 14;
            label5.Text = "ABV:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(39, 239);
            label4.Name = "label4";
            label4.Size = new Size(50, 21);
            label4.TabIndex = 13;
            label4.Text = "Stock:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(39, 178);
            label3.Name = "label3";
            label3.Size = new Size(47, 21);
            label3.TabIndex = 12;
            label3.Text = "Price:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(39, 113);
            label2.Name = "label2";
            label2.Size = new Size(70, 21);
            label2.TabIndex = 11;
            label2.Text = "Brewery:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(39, 53);
            label1.Name = "label1";
            label1.Size = new Size(55, 21);
            label1.TabIndex = 10;
            label1.Text = "Name:";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(499, 442);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 9;
            btnBack.Text = "&Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(140, 442);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(85, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "&Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(40, 442);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(94, 23);
            btnEdit.TabIndex = 7;
            btnEdit.Text = "&Edit";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 12F);
            lblDescription.Location = new Point(276, 125);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(298, 303);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "Description";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 12F);
            lblCategory.Location = new Point(152, 341);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(73, 21);
            lblCategory.TabIndex = 5;
            lblCategory.Text = "Category";
            // 
            // lblABV
            // 
            lblABV.AutoSize = true;
            lblABV.Font = new Font("Segoe UI", 12F);
            lblABV.Location = new Point(152, 291);
            lblABV.Name = "lblABV";
            lblABV.Size = new Size(39, 21);
            lblABV.TabIndex = 4;
            lblABV.Text = "ABV";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Font = new Font("Segoe UI", 12F);
            lblStock.Location = new Point(152, 239);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(47, 21);
            lblStock.TabIndex = 3;
            lblStock.Text = "Stock";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 12F);
            lblPrice.Location = new Point(152, 178);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(44, 21);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "Price";
            // 
            // lblBrewery
            // 
            lblBrewery.AutoSize = true;
            lblBrewery.Font = new Font("Segoe UI", 12F);
            lblBrewery.Location = new Point(152, 113);
            lblBrewery.Name = "lblBrewery";
            lblBrewery.Size = new Size(67, 21);
            lblBrewery.TabIndex = 1;
            lblBrewery.Text = "Brewery";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 12F);
            lblName.Location = new Point(152, 53);
            lblName.Name = "lblName";
            lblName.Size = new Size(52, 21);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // ViewEditDeleteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 504);
            Controls.Add(splitContainer1);
            Name = "ViewEditDeleteForm";
            Text = "ViewEditDeleteForm";
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