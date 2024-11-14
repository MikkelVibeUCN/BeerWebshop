namespace BeerWebshop.DesktopClient
{
    partial class ViewProductsForm
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
            lstProducts = new ListBox();
            panelEdit = new Panel();
            lblName = new Label();
            this.lblBrewery = new Label();
            lblPrice = new Label();
            this.lblStock = new Label();
            this.lblDescription = new Label();
            lblCategory = new Label();
            lblABV = new Label();
            btnDelete = new Button();
            btnEdit = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panelEdit.SuspendLayout();
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
            splitContainer1.Panel1.Controls.Add(lstProducts);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panelEdit);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 0;
            // 
            // lstProducts
            // 
            lstProducts.Dock = DockStyle.Fill;
            lstProducts.FormattingEnabled = true;
            lstProducts.Location = new Point(0, 0);
            lstProducts.Name = "lstProducts";
            lstProducts.Size = new Size(266, 450);
            lstProducts.TabIndex = 0;
            lstProducts.SelectedIndexChanged += this.lstProducts_SelectedIndexChanged;
            // 
            // panelEdit
            // 
            panelEdit.Controls.Add(btnEdit);
            panelEdit.Controls.Add(btnDelete);
            panelEdit.Controls.Add(lblCategory);
            panelEdit.Controls.Add(lblABV);
            panelEdit.Controls.Add(this.lblDescription);
            panelEdit.Controls.Add(this.lblStock);
            panelEdit.Controls.Add(lblPrice);
            panelEdit.Controls.Add(this.lblBrewery);
            panelEdit.Controls.Add(lblName);
            panelEdit.Dock = DockStyle.Fill;
            panelEdit.Location = new Point(0, 0);
            panelEdit.Name = "panelEdit";
            panelEdit.Size = new Size(530, 450);
            panelEdit.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 16F);
            lblName.Location = new Point(24, 23);
            lblName.Name = "lblName";
            lblName.Size = new Size(88, 37);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // lblBrewery
            // 
            this.lblBrewery.AutoSize = true;
            this.lblBrewery.Font = new Font("Segoe UI", 16F);
            this.lblBrewery.Location = new Point(24, 85);
            this.lblBrewery.Name = "lblBrewery";
            this.lblBrewery.Size = new Size(111, 37);
            this.lblBrewery.TabIndex = 0;
            this.lblBrewery.Text = "Brewery";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 16F);
            lblPrice.Location = new Point(24, 150);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(74, 37);
            lblPrice.TabIndex = 0;
            lblPrice.Text = "Price";
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Font = new Font("Segoe UI", 16F);
            this.lblStock.Location = new Point(24, 207);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new Size(80, 37);
            this.lblStock.TabIndex = 0;
            this.lblStock.Text = "Stock";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new Font("Segoe UI", 16F);
            this.lblDescription.Location = new Point(289, 23);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(152, 37);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 16F);
            lblCategory.Location = new Point(24, 332);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(125, 37);
            lblCategory.TabIndex = 0;
            lblCategory.Text = "Category";
            // 
            // lblABV
            // 
            lblABV.AutoSize = true;
            lblABV.Font = new Font("Segoe UI", 16F);
            lblABV.Location = new Point(24, 271);
            lblABV.Name = "lblABV";
            lblABV.Size = new Size(66, 37);
            lblABV.TabIndex = 0;
            lblABV.Text = "ABV";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(264, 394);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "&Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(128, 394);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(94, 29);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "&Edit";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // ViewProductsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "ViewProductsForm";
            Text = "Product manager";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panelEdit.ResumeLayout(false);
            panelEdit.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private ListBox lstProducts;
        private Panel panelEdit;
        private Label lblCategory;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label lblPrice;
        private Label label3;
        private Label label2;
        private Label lblName;
        private Label lblABV;
        private Button btnEdit;
        private Button btnDelete;
    }
}