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
            pictureBox = new PictureBox();
            txtIdEdit = new TextBox();
            lblShowId = new Label();
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
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // grpBoxEdit
            // 
            grpBoxEdit.Controls.Add(pictureBox);
            grpBoxEdit.Controls.Add(txtIdEdit);
            grpBoxEdit.Controls.Add(lblShowId);
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
            grpBoxEdit.Margin = new Padding(4, 5, 4, 5);
            grpBoxEdit.Name = "grpBoxEdit";
            grpBoxEdit.Padding = new Padding(4, 5, 4, 5);
            grpBoxEdit.Size = new Size(1038, 749);
            grpBoxEdit.TabIndex = 0;
            grpBoxEdit.TabStop = false;
            grpBoxEdit.Text = "Rediger produkt";
            // 
            // pictureBox
            // 
            pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox.Location = new Point(578, 95);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(334, 454);
            pictureBox.TabIndex = 23;
            pictureBox.TabStop = false;
            // 
            // txtIdEdit
            // 
            txtIdEdit.Location = new Point(239, 95);
            txtIdEdit.Margin = new Padding(4, 5, 4, 5);
            txtIdEdit.Name = "txtIdEdit";
            txtIdEdit.Size = new Size(210, 31);
            txtIdEdit.TabIndex = 22;
            // 
            // lblShowId
            // 
            lblShowId.AutoSize = true;
            lblShowId.Location = new Point(67, 95);
            lblShowId.Margin = new Padding(4, 0, 4, 0);
            lblShowId.Name = "lblShowId";
            lblShowId.Size = new Size(32, 25);
            lblShowId.TabIndex = 21;
            lblShowId.Text = "Id:";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Right;
            btnCancel.Location = new Point(644, 666);
            btnCancel.Margin = new Padding(4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 39);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "&Annullér";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnEditProduct
            // 
            btnEditProduct.Anchor = AnchorStyles.Right;
            btnEditProduct.Location = new Point(782, 666);
            btnEditProduct.Margin = new Padding(4);
            btnEditProduct.Name = "btnEditProduct";
            btnEditProduct.Size = new Size(130, 39);
            btnEditProduct.TabIndex = 18;
            btnEditProduct.Text = "&Bekræft";
            btnEditProduct.UseVisualStyleBackColor = true;
            btnEditProduct.Click += btnEditProduct_Click;
            // 
            // lblRowVersionEdit
            // 
            lblRowVersionEdit.Anchor = AnchorStyles.Right;
            lblRowVersionEdit.AutoSize = true;
            lblRowVersionEdit.Location = new Point(508, 629);
            lblRowVersionEdit.Margin = new Padding(4, 0, 4, 0);
            lblRowVersionEdit.Name = "lblRowVersionEdit";
            lblRowVersionEdit.Size = new Size(67, 25);
            lblRowVersionEdit.TabIndex = 11;
            lblRowVersionEdit.Text = "Billede:";
            lblRowVersionEdit.Visible = false;
            // 
            // lblImageUrl
            // 
            lblImageUrl.Anchor = AnchorStyles.Right;
            lblImageUrl.AutoSize = true;
            lblImageUrl.Location = new Point(67, 375);
            lblImageUrl.Margin = new Padding(4, 0, 4, 0);
            lblImageUrl.Name = "lblImageUrl";
            lblImageUrl.Size = new Size(67, 25);
            lblImageUrl.TabIndex = 11;
            lblImageUrl.Text = "Billede:";
            // 
            // lblCategory
            // 
            lblCategory.Anchor = AnchorStyles.Right;
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(67, 336);
            lblCategory.Margin = new Padding(4, 0, 4, 0);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(82, 25);
            lblCategory.TabIndex = 11;
            lblCategory.Text = "Kategori:";
            // 
            // lblABV
            // 
            lblABV.Anchor = AnchorStyles.Right;
            lblABV.AutoSize = true;
            lblABV.Location = new Point(67, 297);
            lblABV.Margin = new Padding(4, 0, 4, 0);
            lblABV.Name = "lblABV";
            lblABV.Size = new Size(138, 25);
            lblABV.TabIndex = 12;
            lblABV.Text = "Alkoholprocent:";
            // 
            // lblStock
            // 
            lblStock.Anchor = AnchorStyles.Right;
            lblStock.AutoSize = true;
            lblStock.Location = new Point(67, 260);
            lblStock.Margin = new Padding(4, 0, 4, 0);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(150, 25);
            lblStock.TabIndex = 13;
            lblStock.Text = "Lagerbeholdning:";
            // 
            // lblDescription
            // 
            lblDescription.Anchor = AnchorStyles.Right;
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(67, 461);
            lblDescription.Margin = new Padding(4, 0, 4, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(101, 25);
            lblDescription.TabIndex = 14;
            lblDescription.Text = "Beskrivelse:";
            // 
            // lblPrice
            // 
            lblPrice.Anchor = AnchorStyles.Right;
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(67, 219);
            lblPrice.Margin = new Padding(4, 0, 4, 0);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(44, 25);
            lblPrice.TabIndex = 15;
            lblPrice.Text = "Pris:";
            // 
            // lblBrewery
            // 
            lblBrewery.Anchor = AnchorStyles.Right;
            lblBrewery.AutoSize = true;
            lblBrewery.Location = new Point(67, 180);
            lblBrewery.Margin = new Padding(4, 0, 4, 0);
            lblBrewery.Name = "lblBrewery";
            lblBrewery.Size = new Size(82, 25);
            lblBrewery.TabIndex = 16;
            lblBrewery.Text = "Bryggeri:";
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Location = new Point(67, 138);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(57, 25);
            lblName.TabIndex = 17;
            lblName.Text = "Navn:";
            lblName.Click += lblName_Click;
            // 
            // txtImageUrlEdit
            // 
            txtImageUrlEdit.Anchor = AnchorStyles.Right;
            txtImageUrlEdit.Location = new Point(239, 369);
            txtImageUrlEdit.Margin = new Padding(4);
            txtImageUrlEdit.Name = "txtImageUrlEdit";
            txtImageUrlEdit.Size = new Size(210, 31);
            txtImageUrlEdit.TabIndex = 4;
            // 
            // txtCategoryEdit
            // 
            txtCategoryEdit.Anchor = AnchorStyles.Right;
            txtCategoryEdit.Location = new Point(239, 330);
            txtCategoryEdit.Margin = new Padding(4);
            txtCategoryEdit.Name = "txtCategoryEdit";
            txtCategoryEdit.Size = new Size(210, 31);
            txtCategoryEdit.TabIndex = 4;
            // 
            // txtABVEdit
            // 
            txtABVEdit.Anchor = AnchorStyles.Right;
            txtABVEdit.Location = new Point(239, 291);
            txtABVEdit.Margin = new Padding(4);
            txtABVEdit.Name = "txtABVEdit";
            txtABVEdit.Size = new Size(210, 31);
            txtABVEdit.TabIndex = 5;
            // 
            // txtStockEdit
            // 
            txtStockEdit.Anchor = AnchorStyles.Right;
            txtStockEdit.Location = new Point(239, 252);
            txtStockEdit.Margin = new Padding(4);
            txtStockEdit.Name = "txtStockEdit";
            txtStockEdit.Size = new Size(210, 31);
            txtStockEdit.TabIndex = 6;
            // 
            // txtDescriptionEdit
            // 
            txtDescriptionEdit.Anchor = AnchorStyles.Right;
            txtDescriptionEdit.Location = new Point(234, 461);
            txtDescriptionEdit.Margin = new Padding(4);
            txtDescriptionEdit.Multiline = true;
            txtDescriptionEdit.Name = "txtDescriptionEdit";
            txtDescriptionEdit.Size = new Size(266, 244);
            txtDescriptionEdit.TabIndex = 7;
            // 
            // txtPriceEdit
            // 
            txtPriceEdit.Anchor = AnchorStyles.Right;
            txtPriceEdit.Location = new Point(239, 213);
            txtPriceEdit.Margin = new Padding(4);
            txtPriceEdit.Name = "txtPriceEdit";
            txtPriceEdit.Size = new Size(210, 31);
            txtPriceEdit.TabIndex = 8;
            // 
            // txtBreweryEdit
            // 
            txtBreweryEdit.Anchor = AnchorStyles.Right;
            txtBreweryEdit.Location = new Point(239, 174);
            txtBreweryEdit.Margin = new Padding(4);
            txtBreweryEdit.Name = "txtBreweryEdit";
            txtBreweryEdit.Size = new Size(210, 31);
            txtBreweryEdit.TabIndex = 9;
            // 
            // txtNameEdit
            // 
            txtNameEdit.Anchor = AnchorStyles.Right;
            txtNameEdit.Location = new Point(239, 135);
            txtNameEdit.Margin = new Padding(4);
            txtNameEdit.Name = "txtNameEdit";
            txtNameEdit.Size = new Size(210, 31);
            txtNameEdit.TabIndex = 10;
            // 
            // EditProductForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1038, 749);
            Controls.Add(grpBoxEdit);
            Margin = new Padding(4, 5, 4, 5);
            Name = "EditProductForm";
            Text = "Redigering af produkter";
            grpBoxEdit.ResumeLayout(false);
            grpBoxEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
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
        private Label lblShowId;
        private TextBox txtIdEdit;
        private Label lblImageUrl;
        private TextBox txtImageUrlEdit;
        private Label lblRowVersionEdit;
        private PictureBox pictureBox;
    }
}