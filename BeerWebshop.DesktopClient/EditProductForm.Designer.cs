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
            txtIdEdit = new TextBox();
            lblShowId = new Label();
            lstProduct = new ListBox();
            btnCancel = new Button();
            btnEditProduct = new Button();
            lblImageUrl = new Label();
            lblCategory = new Label();
            lblABV = new Label();
            lblStock = new Label();
            lblDescription = new Label();
            lblPrice = new Label();
            lblBrewery = new Label();
            lblName = new Label();
            txtImageUrlEdit = new TextBox();
            txtCategoryEdit = new TextBox();
            txtABVEdit = new TextBox();
            txtStockEdit = new TextBox();
            txtDescriptionEdit = new TextBox();
            txtPriceEdit = new TextBox();
            txtBreweryEdit = new TextBox();
            txtNameEdit = new TextBox();
            lblRowVersionEdit = new Label();
            grpBoxEdit.SuspendLayout();
            SuspendLayout();
            // 
            // grpBoxEdit
            // 
            grpBoxEdit.Controls.Add(txtIdEdit);
            grpBoxEdit.Controls.Add(lblShowId);
            grpBoxEdit.Controls.Add(lstProduct);
            grpBoxEdit.Controls.Add(btnCancel);
            grpBoxEdit.Controls.Add(btnEditProduct);
            grpBoxEdit.Controls.Add(lblRowVersionEdit);
            grpBoxEdit.Controls.Add(lblImageUrl);
            grpBoxEdit.Controls.Add(lblCategory);
            grpBoxEdit.Controls.Add(lblABV);
            grpBoxEdit.Controls.Add(lblStock);
            grpBoxEdit.Controls.Add(lblDescription);
            grpBoxEdit.Controls.Add(lblPrice);
            grpBoxEdit.Controls.Add(lblBrewery);
            grpBoxEdit.Controls.Add(lblName);
            grpBoxEdit.Controls.Add(txtImageUrlEdit);
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
            grpBoxEdit.Size = new Size(726, 449);
            grpBoxEdit.TabIndex = 0;
            grpBoxEdit.TabStop = false;
            grpBoxEdit.Text = "Edit Product";
            // 
            // txtIdEdit
            // 
            txtIdEdit.Location = new Point(355, 53);
            txtIdEdit.Name = "txtIdEdit";
            txtIdEdit.Size = new Size(148, 23);
            txtIdEdit.TabIndex = 22;
            // 
            // lblShowId
            // 
            lblShowId.AutoSize = true;
            lblShowId.Location = new Point(230, 56);
            lblShowId.Name = "lblShowId";
            lblShowId.Size = new Size(20, 15);
            lblShowId.TabIndex = 21;
            lblShowId.Text = "Id:";
            // 
            // lstProduct
            // 
            lstProduct.Anchor = AnchorStyles.Left;
            lstProduct.FormattingEnabled = true;
            lstProduct.ItemHeight = 15;
            lstProduct.Location = new Point(6, 86);
            lstProduct.Name = "lstProduct";
            lstProduct.Size = new Size(192, 289);
            lstProduct.TabIndex = 20;
            lstProduct.SelectedIndexChanged += lstProduct_SelectedIndexChanged;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Right;
            btnCancel.Location = new Point(526, 330);
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
            btnEditProduct.Anchor = AnchorStyles.Right;
            btnEditProduct.Location = new Point(623, 330);
            btnEditProduct.Margin = new Padding(3, 2, 3, 2);
            btnEditProduct.Name = "btnEditProduct";
            btnEditProduct.Size = new Size(91, 23);
            btnEditProduct.TabIndex = 18;
            btnEditProduct.Text = "&Submit";
            btnEditProduct.UseVisualStyleBackColor = true;
            btnEditProduct.Click += btnEditProduct_Click;
            // 
            // lblImageUrl
            // 
            lblImageUrl.Anchor = AnchorStyles.Right;
            lblImageUrl.AutoSize = true;
            lblImageUrl.Location = new Point(230, 306);
            lblImageUrl.Name = "lblImageUrl";
            lblImageUrl.Size = new Size(45, 15);
            lblImageUrl.TabIndex = 11;
            lblImageUrl.Text = "Billede:";
            // 
            // lblCategory
            // 
            lblCategory.Anchor = AnchorStyles.Right;
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(230, 265);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(54, 15);
            lblCategory.TabIndex = 11;
            lblCategory.Text = "Kategori:";
            // 
            // lblABV
            // 
            lblABV.Anchor = AnchorStyles.Right;
            lblABV.AutoSize = true;
            lblABV.Location = new Point(230, 234);
            lblABV.Name = "lblABV";
            lblABV.Size = new Size(92, 15);
            lblABV.TabIndex = 12;
            lblABV.Text = "Alkoholprocent:";
            // 
            // lblStock
            // 
            lblStock.Anchor = AnchorStyles.Right;
            lblStock.AutoSize = true;
            lblStock.Location = new Point(230, 198);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(100, 15);
            lblStock.TabIndex = 13;
            lblStock.Text = "Lagerbeholdning:";
            // 
            // lblDescription
            // 
            lblDescription.Anchor = AnchorStyles.Right;
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(526, 156);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 14;
            lblDescription.Text = "Beskrivelse:";
            // 
            // lblPrice
            // 
            lblPrice.Anchor = AnchorStyles.Right;
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(230, 158);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(29, 15);
            lblPrice.TabIndex = 15;
            lblPrice.Text = "Pris:";
            // 
            // lblBrewery
            // 
            lblBrewery.Anchor = AnchorStyles.Right;
            lblBrewery.AutoSize = true;
            lblBrewery.Location = new Point(230, 122);
            lblBrewery.Name = "lblBrewery";
            lblBrewery.Size = new Size(54, 15);
            lblBrewery.TabIndex = 16;
            lblBrewery.Text = "Bryggeri:";
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Location = new Point(230, 86);
            lblName.Name = "lblName";
            lblName.Size = new Size(38, 15);
            lblName.TabIndex = 17;
            lblName.Text = "Navn:";
            // 
            // txtImageUrlEdit
            // 
            txtImageUrlEdit.Anchor = AnchorStyles.Right;
            txtImageUrlEdit.Location = new Point(355, 306);
            txtImageUrlEdit.Margin = new Padding(3, 2, 3, 2);
            txtImageUrlEdit.Name = "txtImageUrlEdit";
            txtImageUrlEdit.Size = new Size(148, 23);
            txtImageUrlEdit.TabIndex = 4;
            // 
            // txtCategoryEdit
            // 
            txtCategoryEdit.Anchor = AnchorStyles.Right;
            txtCategoryEdit.Location = new Point(355, 258);
            txtCategoryEdit.Margin = new Padding(3, 2, 3, 2);
            txtCategoryEdit.Name = "txtCategoryEdit";
            txtCategoryEdit.Size = new Size(148, 23);
            txtCategoryEdit.TabIndex = 4;
            // 
            // txtABVEdit
            // 
            txtABVEdit.Anchor = AnchorStyles.Right;
            txtABVEdit.Location = new Point(355, 225);
            txtABVEdit.Margin = new Padding(3, 2, 3, 2);
            txtABVEdit.Name = "txtABVEdit";
            txtABVEdit.Size = new Size(148, 23);
            txtABVEdit.TabIndex = 5;
            // 
            // txtStockEdit
            // 
            txtStockEdit.Anchor = AnchorStyles.Right;
            txtStockEdit.Location = new Point(355, 192);
            txtStockEdit.Margin = new Padding(3, 2, 3, 2);
            txtStockEdit.Name = "txtStockEdit";
            txtStockEdit.Size = new Size(148, 23);
            txtStockEdit.TabIndex = 6;
            // 
            // txtDescriptionEdit
            // 
            txtDescriptionEdit.Anchor = AnchorStyles.Right;
            txtDescriptionEdit.Location = new Point(526, 173);
            txtDescriptionEdit.Margin = new Padding(3, 2, 3, 2);
            txtDescriptionEdit.Multiline = true;
            txtDescriptionEdit.Name = "txtDescriptionEdit";
            txtDescriptionEdit.Size = new Size(188, 148);
            txtDescriptionEdit.TabIndex = 7;
            // 
            // txtPriceEdit
            // 
            txtPriceEdit.Anchor = AnchorStyles.Right;
            txtPriceEdit.Location = new Point(355, 154);
            txtPriceEdit.Margin = new Padding(3, 2, 3, 2);
            txtPriceEdit.Name = "txtPriceEdit";
            txtPriceEdit.Size = new Size(148, 23);
            txtPriceEdit.TabIndex = 8;
            // 
            // txtBreweryEdit
            // 
            txtBreweryEdit.Anchor = AnchorStyles.Right;
            txtBreweryEdit.Location = new Point(355, 117);
            txtBreweryEdit.Margin = new Padding(3, 2, 3, 2);
            txtBreweryEdit.Name = "txtBreweryEdit";
            txtBreweryEdit.Size = new Size(148, 23);
            txtBreweryEdit.TabIndex = 9;
            // 
            // txtNameEdit
            // 
            txtNameEdit.Anchor = AnchorStyles.Right;
            txtNameEdit.Location = new Point(355, 85);
            txtNameEdit.Margin = new Padding(3, 2, 3, 2);
            txtNameEdit.Name = "txtNameEdit";
            txtNameEdit.Size = new Size(148, 23);
            txtNameEdit.TabIndex = 10;
            // 
            // lblRowVersionEdit
            // 
            lblRowVersionEdit.Anchor = AnchorStyles.Right;
            lblRowVersionEdit.AutoSize = true;
            lblRowVersionEdit.Location = new Point(355, 377);
            lblRowVersionEdit.Name = "lblRowVersionEdit";
            lblRowVersionEdit.Size = new Size(45, 15);
            lblRowVersionEdit.TabIndex = 11;
            lblRowVersionEdit.Text = "Billede:";
            lblRowVersionEdit.Visible = false;
            // 
            // EditProductForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(726, 449);
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
        private ListBox lstProduct;
        private Label lblShowId;
        private TextBox txtIdEdit;
        private Label lblImageUrl;
        private TextBox txtImageUrlEdit;
        private Label lblRowVersionEdit;
    }
}