namespace BeerWebshop.DesktopClient.Controllers
{
    partial class AddProductForm
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
            grpAddProductPage = new GroupBox();
            cmbCategory = new ComboBox();
            btnCancel = new Button();
            btnAddProduct = new Button();
            lblCategory = new Label();
            lblABV = new Label();
            lblStock = new Label();
            lblDescription = new Label();
            lblPrice = new Label();
            lblBrewery = new Label();
            lblName = new Label();
            txtABV = new TextBox();
            txtStock = new TextBox();
            txtDescription = new TextBox();
            txtPrice = new TextBox();
            txtBrewery = new TextBox();
            txtName = new TextBox();
            grpAddProductPage.SuspendLayout();
            SuspendLayout();
            // 
            // grpAddProductPage
            // 
            grpAddProductPage.Controls.Add(cmbCategory);
            grpAddProductPage.Controls.Add(btnCancel);
            grpAddProductPage.Controls.Add(btnAddProduct);
            grpAddProductPage.Controls.Add(lblCategory);
            grpAddProductPage.Controls.Add(lblABV);
            grpAddProductPage.Controls.Add(lblStock);
            grpAddProductPage.Controls.Add(lblDescription);
            grpAddProductPage.Controls.Add(lblPrice);
            grpAddProductPage.Controls.Add(lblBrewery);
            grpAddProductPage.Controls.Add(lblName);
            grpAddProductPage.Controls.Add(txtABV);
            grpAddProductPage.Controls.Add(txtStock);
            grpAddProductPage.Controls.Add(txtDescription);
            grpAddProductPage.Controls.Add(txtPrice);
            grpAddProductPage.Controls.Add(txtBrewery);
            grpAddProductPage.Controls.Add(txtName);
            grpAddProductPage.Dock = DockStyle.Top;
            grpAddProductPage.Location = new Point(0, 0);
            grpAddProductPage.Name = "grpAddProductPage";
            grpAddProductPage.Size = new Size(800, 453);
            grpAddProductPage.TabIndex = 0;
            grpAddProductPage.TabStop = false;
            grpAddProductPage.Text = "Add product";
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(154, 341);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(169, 28);
            cmbCategory.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(561, 349);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(104, 31);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(672, 349);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(104, 31);
            btnAddProduct.TabIndex = 2;
            btnAddProduct.Text = "&Submit";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnSubmit_Click;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(11, 349);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(69, 20);
            lblCategory.TabIndex = 1;
            lblCategory.Text = "Kategori:";
            // 
            // lblABV
            // 
            lblABV.AutoSize = true;
            lblABV.Location = new Point(11, 296);
            lblABV.Name = "lblABV";
            lblABV.Size = new Size(114, 20);
            lblABV.TabIndex = 1;
            lblABV.Text = "Alkoholprocent:";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(11, 249);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(125, 20);
            lblStock.TabIndex = 1;
            lblStock.Text = "Lagerbeholdning:";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(11, 203);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(84, 20);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "Beskrivelse:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(11, 159);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(35, 20);
            lblPrice.TabIndex = 1;
            lblPrice.Text = "Pris:";
            // 
            // lblBrewery
            // 
            lblBrewery.AutoSize = true;
            lblBrewery.Location = new Point(11, 116);
            lblBrewery.Name = "lblBrewery";
            lblBrewery.Size = new Size(68, 20);
            lblBrewery.TabIndex = 1;
            lblBrewery.Text = "Bryggeri:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(11, 69);
            lblName.Name = "lblName";
            lblName.Size = new Size(46, 20);
            lblName.TabIndex = 1;
            lblName.Text = "Navn:";
            // 
            // txtABV
            // 
            txtABV.Location = new Point(154, 296);
            txtABV.Name = "txtABV";
            txtABV.Size = new Size(169, 27);
            txtABV.TabIndex = 0;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(154, 249);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(169, 27);
            txtStock.TabIndex = 0;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(154, 203);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(169, 27);
            txtDescription.TabIndex = 0;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(154, 159);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(169, 27);
            txtPrice.TabIndex = 0;
            // 
            // txtBrewery
            // 
            txtBrewery.Location = new Point(154, 113);
            txtBrewery.Name = "txtBrewery";
            txtBrewery.Size = new Size(169, 27);
            txtBrewery.TabIndex = 0;
            // 
            // txtName
            // 
            txtName.Location = new Point(154, 69);
            txtName.Name = "txtName";
            txtName.Size = new Size(169, 27);
            txtName.TabIndex = 0;
            // 
            // AddProductForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(grpAddProductPage);
            Name = "AddProductForm";
            Text = "AddProductForm";
            grpAddProductPage.ResumeLayout(false);
            grpAddProductPage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpAddProductPage;
        private Label lblName;
        private TextBox txtName;
        private Label lblPrice;
        private Label lblBrewery;
        private Label lblCategory;
        private Label lblABV;
        private Label lblStock;
        private Label lblDescription;
        private TextBox txtDescription;
        private TextBox txtPrice;
        private TextBox txtBrewery;
        private TextBox txtABV;
        private TextBox txtStock;
        private Button btnAddProduct;
        private Button btnCancel;
        private ComboBox cmbCategory;
    }
}