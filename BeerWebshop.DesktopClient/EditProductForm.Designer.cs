namespace BeerWebshop.DesktopClient
{
    partial class EditProductForm
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
            grpBoxEdit = new GroupBox();
            btnCancel = new Button();
            btnEditProduct = new Button();
            lblCategory = new Label();
            lblABV = new Label();
            lblStock = new Label();
            lblDescription = new Label();
            lblPrice = new Label();
            lblBrewery = new Label();
            lblName = new Label();
            txtCategoryEdit = new TextBox();
            txtABVEdit = new TextBox();
            txtStockEdit = new TextBox();
            txtDescriptionEdit = new TextBox();
            txtPriceEdit = new TextBox();
            txtBreweryEdit = new TextBox();
            txtNameEdit = new TextBox();
            grpBoxEdit.SuspendLayout();
            SuspendLayout();
            // 
            // grpBoxEdit
            // 
            grpBoxEdit.Controls.Add(btnCancel);
            grpBoxEdit.Controls.Add(btnEditProduct);
            grpBoxEdit.Controls.Add(lblCategory);
            grpBoxEdit.Controls.Add(lblABV);
            grpBoxEdit.Controls.Add(lblStock);
            grpBoxEdit.Controls.Add(lblDescription);
            grpBoxEdit.Controls.Add(lblPrice);
            grpBoxEdit.Controls.Add(lblBrewery);
            grpBoxEdit.Controls.Add(lblName);
            grpBoxEdit.Controls.Add(txtCategoryEdit);
            grpBoxEdit.Controls.Add(txtABVEdit);
            grpBoxEdit.Controls.Add(txtStockEdit);
            grpBoxEdit.Controls.Add(txtDescriptionEdit);
            grpBoxEdit.Controls.Add(txtPriceEdit);
            grpBoxEdit.Controls.Add(txtBreweryEdit);
            grpBoxEdit.Controls.Add(txtNameEdit);
            grpBoxEdit.Dock = DockStyle.Fill;
            grpBoxEdit.Location = new Point(0, 0);
            grpBoxEdit.Name = "grpBoxEdit";
            grpBoxEdit.Size = new Size(726, 313);
            grpBoxEdit.TabIndex = 0;
            grpBoxEdit.TabStop = false;
            grpBoxEdit.Text = "Edit Product";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(509, 262);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(91, 23);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnEditProduct
            // 
            btnEditProduct.Location = new Point(606, 262);
            btnEditProduct.Margin = new Padding(3, 2, 3, 2);
            btnEditProduct.Name = "btnEditProduct";
            btnEditProduct.Size = new Size(91, 23);
            btnEditProduct.TabIndex = 18;
            btnEditProduct.Text = "&Submit";
            btnEditProduct.UseVisualStyleBackColor = true;
            btnEditProduct.Click += btnEditProduct_Click;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(28, 262);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(54, 15);
            lblCategory.TabIndex = 11;
            lblCategory.Text = "Kategori:";
            // 
            // lblABV
            // 
            lblABV.AutoSize = true;
            lblABV.Location = new Point(28, 222);
            lblABV.Name = "lblABV";
            lblABV.Size = new Size(92, 15);
            lblABV.TabIndex = 12;
            lblABV.Text = "Alkoholprocent:";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(28, 187);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(100, 15);
            lblStock.TabIndex = 13;
            lblStock.Text = "Lagerbeholdning:";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(28, 152);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 14;
            lblDescription.Text = "Beskrivelse:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(28, 119);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(29, 15);
            lblPrice.TabIndex = 15;
            lblPrice.Text = "Pris:";
            // 
            // lblBrewery
            // 
            lblBrewery.AutoSize = true;
            lblBrewery.Location = new Point(28, 87);
            lblBrewery.Name = "lblBrewery";
            lblBrewery.Size = new Size(54, 15);
            lblBrewery.TabIndex = 16;
            lblBrewery.Text = "Bryggeri:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(28, 52);
            lblName.Name = "lblName";
            lblName.Size = new Size(38, 15);
            lblName.TabIndex = 17;
            lblName.Text = "Navn:";
            // 
            // txtCategoryEdit
            // 
            txtCategoryEdit.Location = new Point(153, 260);
            txtCategoryEdit.Margin = new Padding(3, 2, 3, 2);
            txtCategoryEdit.Name = "txtCategoryEdit";
            txtCategoryEdit.Size = new Size(148, 23);
            txtCategoryEdit.TabIndex = 4;
            // 
            // txtABVEdit
            // 
            txtABVEdit.Location = new Point(153, 222);
            txtABVEdit.Margin = new Padding(3, 2, 3, 2);
            txtABVEdit.Name = "txtABVEdit";
            txtABVEdit.Size = new Size(148, 23);
            txtABVEdit.TabIndex = 5;
            // 
            // txtStockEdit
            // 
            txtStockEdit.Location = new Point(153, 187);
            txtStockEdit.Margin = new Padding(3, 2, 3, 2);
            txtStockEdit.Name = "txtStockEdit";
            txtStockEdit.Size = new Size(148, 23);
            txtStockEdit.TabIndex = 6;
            // 
            // txtDescriptionEdit
            // 
            txtDescriptionEdit.Location = new Point(153, 152);
            txtDescriptionEdit.Margin = new Padding(3, 2, 3, 2);
            txtDescriptionEdit.Name = "txtDescriptionEdit";
            txtDescriptionEdit.Size = new Size(148, 23);
            txtDescriptionEdit.TabIndex = 7;
            // 
            // txtPriceEdit
            // 
            txtPriceEdit.Location = new Point(153, 119);
            txtPriceEdit.Margin = new Padding(3, 2, 3, 2);
            txtPriceEdit.Name = "txtPriceEdit";
            txtPriceEdit.Size = new Size(148, 23);
            txtPriceEdit.TabIndex = 8;
            // 
            // txtBreweryEdit
            // 
            txtBreweryEdit.Location = new Point(153, 85);
            txtBreweryEdit.Margin = new Padding(3, 2, 3, 2);
            txtBreweryEdit.Name = "txtBreweryEdit";
            txtBreweryEdit.Size = new Size(148, 23);
            txtBreweryEdit.TabIndex = 9;
            // 
            // txtNameEdit
            // 
            txtNameEdit.Location = new Point(153, 52);
            txtNameEdit.Margin = new Padding(3, 2, 3, 2);
            txtNameEdit.Name = "txtNameEdit";
            txtNameEdit.Size = new Size(148, 23);
            txtNameEdit.TabIndex = 10;
            // 
            // EditProductForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(726, 313);
            Controls.Add(grpBoxEdit);
            Name = "EditProductForm";
            Text = "EditProductForm";
            grpBoxEdit.ResumeLayout(false);
            grpBoxEdit.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpBoxEdit;
        private Button btnCancel;
        private Button btnEditProduct;
        private Label lblCategory;
        private Label lblABV;
        private Label lblStock;
        private Label lblDescription;
        private Label lblPrice;
        private Label lblBrewery;
        private Label lblName;
        private TextBox txtCategoryEdit;
        private TextBox txtABVEdit;
        private TextBox txtStockEdit;
        private TextBox txtDescriptionEdit;
        private TextBox txtPriceEdit;
        private TextBox txtBreweryEdit;
        private TextBox txtNameEdit;
    }
}