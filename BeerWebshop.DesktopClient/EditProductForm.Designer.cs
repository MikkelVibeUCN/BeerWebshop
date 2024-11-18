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
            lblRowVersionEdit = new Label();
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
            grpBoxEdit.Margin = new Padding(3, 4, 3, 4);
            grpBoxEdit.Name = "grpBoxEdit";
            grpBoxEdit.Padding = new Padding(3, 4, 3, 4);
            grpBoxEdit.Size = new Size(830, 599);
            grpBoxEdit.TabIndex = 0;
            grpBoxEdit.TabStop = false;
            grpBoxEdit.Text = "Rediger produkt";
            // 
            // txtIdEdit
            // 
            txtIdEdit.Location = new Point(406, 71);
            txtIdEdit.Margin = new Padding(3, 4, 3, 4);
            txtIdEdit.Name = "txtIdEdit";
            txtIdEdit.Size = new Size(169, 27);
            txtIdEdit.TabIndex = 22;
            // 
            // lblShowId
            // 
            lblShowId.AutoSize = true;
            lblShowId.Location = new Point(263, 75);
            lblShowId.Name = "lblShowId";
            lblShowId.Size = new Size(25, 20);
            lblShowId.TabIndex = 21;
            lblShowId.Text = "Id:";
            // 
            // lstProduct
            // 
            lstProduct.Anchor = AnchorStyles.Left;
            lstProduct.FormattingEnabled = true;
            lstProduct.Location = new Point(7, 115);
            lstProduct.Margin = new Padding(3, 4, 3, 4);
            lstProduct.Name = "lstProduct";
            lstProduct.Size = new Size(219, 384);
            lstProduct.TabIndex = 20;
            lstProduct.SelectedIndexChanged += lstProduct_SelectedIndexChanged;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Right;
            btnCancel.Location = new Point(601, 440);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(104, 31);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "&Annullér";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnEditProduct
            // 
            btnEditProduct.Anchor = AnchorStyles.Right;
            btnEditProduct.Location = new Point(712, 440);
            btnEditProduct.Name = "btnEditProduct";
            btnEditProduct.Size = new Size(104, 31);
            btnEditProduct.TabIndex = 18;
            btnEditProduct.Text = "&Bekræft";
            btnEditProduct.UseVisualStyleBackColor = true;
            btnEditProduct.Click += btnEditProduct_Click;
            // 
            // lblRowVersionEdit
            // 
            lblRowVersionEdit.Anchor = AnchorStyles.Right;
            lblRowVersionEdit.AutoSize = true;
            lblRowVersionEdit.Location = new Point(406, 503);
            lblRowVersionEdit.Name = "lblRowVersionEdit";
            lblRowVersionEdit.Size = new Size(58, 20);
            lblRowVersionEdit.TabIndex = 11;
            lblRowVersionEdit.Text = "Billede:";
            lblRowVersionEdit.Visible = false;
            // 
            // lblImageUrl
            // 
            lblImageUrl.Anchor = AnchorStyles.Right;
            lblImageUrl.AutoSize = true;
            lblImageUrl.Location = new Point(263, 408);
            lblImageUrl.Name = "lblImageUrl";
            lblImageUrl.Size = new Size(58, 20);
            lblImageUrl.TabIndex = 11;
            lblImageUrl.Text = "Billede:";
            // 
            // lblCategory
            // 
            lblCategory.Anchor = AnchorStyles.Right;
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(263, 353);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(69, 20);
            lblCategory.TabIndex = 11;
            lblCategory.Text = "Kategori:";
            // 
            // lblABV
            // 
            lblABV.Anchor = AnchorStyles.Right;
            lblABV.AutoSize = true;
            lblABV.Location = new Point(263, 312);
            lblABV.Name = "lblABV";
            lblABV.Size = new Size(114, 20);
            lblABV.TabIndex = 12;
            lblABV.Text = "Alkoholprocent:";
            // 
            // lblStock
            // 
            lblStock.Anchor = AnchorStyles.Right;
            lblStock.AutoSize = true;
            lblStock.Location = new Point(263, 264);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(125, 20);
            lblStock.TabIndex = 13;
            lblStock.Text = "Lagerbeholdning:";
            // 
            // lblDescription
            // 
            lblDescription.Anchor = AnchorStyles.Right;
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(601, 208);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(84, 20);
            lblDescription.TabIndex = 14;
            lblDescription.Text = "Beskrivelse:";
            // 
            // lblPrice
            // 
            lblPrice.Anchor = AnchorStyles.Right;
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(263, 211);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(35, 20);
            lblPrice.TabIndex = 15;
            lblPrice.Text = "Pris:";
            // 
            // lblBrewery
            // 
            lblBrewery.Anchor = AnchorStyles.Right;
            lblBrewery.AutoSize = true;
            lblBrewery.Location = new Point(263, 163);
            lblBrewery.Name = "lblBrewery";
            lblBrewery.Size = new Size(68, 20);
            lblBrewery.TabIndex = 16;
            lblBrewery.Text = "Bryggeri:";
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Location = new Point(263, 115);
            lblName.Name = "lblName";
            lblName.Size = new Size(46, 20);
            lblName.TabIndex = 17;
            lblName.Text = "Navn:";
            // 
            // txtImageUrlEdit
            // 
            txtImageUrlEdit.Anchor = AnchorStyles.Right;
            txtImageUrlEdit.Location = new Point(406, 408);
            txtImageUrlEdit.Name = "txtImageUrlEdit";
            txtImageUrlEdit.Size = new Size(169, 27);
            txtImageUrlEdit.TabIndex = 4;
            // 
            // txtCategoryEdit
            // 
            txtCategoryEdit.Anchor = AnchorStyles.Right;
            txtCategoryEdit.Location = new Point(406, 344);
            txtCategoryEdit.Name = "txtCategoryEdit";
            txtCategoryEdit.Size = new Size(169, 27);
            txtCategoryEdit.TabIndex = 4;
            // 
            // txtABVEdit
            // 
            txtABVEdit.Anchor = AnchorStyles.Right;
            txtABVEdit.Location = new Point(406, 300);
            txtABVEdit.Name = "txtABVEdit";
            txtABVEdit.Size = new Size(169, 27);
            txtABVEdit.TabIndex = 5;
            // 
            // txtStockEdit
            // 
            txtStockEdit.Anchor = AnchorStyles.Right;
            txtStockEdit.Location = new Point(406, 256);
            txtStockEdit.Name = "txtStockEdit";
            txtStockEdit.Size = new Size(169, 27);
            txtStockEdit.TabIndex = 6;
            // 
            // txtDescriptionEdit
            // 
            txtDescriptionEdit.Anchor = AnchorStyles.Right;
            txtDescriptionEdit.Location = new Point(601, 231);
            txtDescriptionEdit.Multiline = true;
            txtDescriptionEdit.Name = "txtDescriptionEdit";
            txtDescriptionEdit.Size = new Size(214, 196);
            txtDescriptionEdit.TabIndex = 7;
            // 
            // txtPriceEdit
            // 
            txtPriceEdit.Anchor = AnchorStyles.Right;
            txtPriceEdit.Location = new Point(406, 205);
            txtPriceEdit.Name = "txtPriceEdit";
            txtPriceEdit.Size = new Size(169, 27);
            txtPriceEdit.TabIndex = 8;
            // 
            // txtBreweryEdit
            // 
            txtBreweryEdit.Anchor = AnchorStyles.Right;
            txtBreweryEdit.Location = new Point(406, 156);
            txtBreweryEdit.Name = "txtBreweryEdit";
            txtBreweryEdit.Size = new Size(169, 27);
            txtBreweryEdit.TabIndex = 9;
            // 
            // txtNameEdit
            // 
            txtNameEdit.Anchor = AnchorStyles.Right;
            txtNameEdit.Location = new Point(406, 113);
            txtNameEdit.Name = "txtNameEdit";
            txtNameEdit.Size = new Size(169, 27);
            txtNameEdit.TabIndex = 10;
            // 
            // EditProductForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(830, 599);
            Controls.Add(grpBoxEdit);
            Margin = new Padding(3, 4, 3, 4);
            Name = "EditProductForm";
            Text = "Redigering af produkter";
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