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
            btnAddProduct = new Button();
            lblCategory = new Label();
            lblABV = new Label();
            lblStock = new Label();
            lblDescription = new Label();
            lblPrice = new Label();
            lblBrewery = new Label();
            lblName = new Label();
            txtCategory = new TextBox();
            txtABV = new TextBox();
            txtStock = new TextBox();
            txtDescription = new TextBox();
            txtPrice = new TextBox();
            txtBrewery = new TextBox();
            txtName = new TextBox();
            btnCancel = new Button();
            grpAddProductPage.SuspendLayout();
            SuspendLayout();
            // 
            // grpAddProductPage
            // 
            grpAddProductPage.Controls.Add(btnCancel);
            grpAddProductPage.Controls.Add(btnAddProduct);
            grpAddProductPage.Controls.Add(lblCategory);
            grpAddProductPage.Controls.Add(lblABV);
            grpAddProductPage.Controls.Add(lblStock);
            grpAddProductPage.Controls.Add(lblDescription);
            grpAddProductPage.Controls.Add(lblPrice);
            grpAddProductPage.Controls.Add(lblBrewery);
            grpAddProductPage.Controls.Add(lblName);
            grpAddProductPage.Controls.Add(txtCategory);
            grpAddProductPage.Controls.Add(txtABV);
            grpAddProductPage.Controls.Add(txtStock);
            grpAddProductPage.Controls.Add(txtDescription);
            grpAddProductPage.Controls.Add(txtPrice);
            grpAddProductPage.Controls.Add(txtBrewery);
            grpAddProductPage.Controls.Add(txtName);
            grpAddProductPage.Dock = DockStyle.Top;
            grpAddProductPage.Location = new Point(0, 0);
            grpAddProductPage.Margin = new Padding(3, 2, 3, 2);
            grpAddProductPage.Name = "grpAddProductPage";
            grpAddProductPage.Padding = new Padding(3, 2, 3, 2);
            grpAddProductPage.Size = new Size(700, 340);
            grpAddProductPage.TabIndex = 0;
            grpAddProductPage.TabStop = false;
            grpAddProductPage.Text = "Add product";
            grpAddProductPage.Enter += groupBox1_Enter;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(588, 262);
            btnAddProduct.Margin = new Padding(3, 2, 3, 2);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(91, 23);
            btnAddProduct.TabIndex = 2;
            btnAddProduct.Text = "&Submit";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnSubmit_Click;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(10, 262);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(54, 15);
            lblCategory.TabIndex = 1;
            lblCategory.Text = "Kategori:";
            lblCategory.Click += label1_Click;
            // 
            // lblABV
            // 
            lblABV.AutoSize = true;
            lblABV.Location = new Point(10, 222);
            lblABV.Name = "lblABV";
            lblABV.Size = new Size(92, 15);
            lblABV.TabIndex = 1;
            lblABV.Text = "Alkoholprocent:";
            lblABV.Click += label1_Click;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(10, 187);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(100, 15);
            lblStock.TabIndex = 1;
            lblStock.Text = "Lagerbeholdning:";
            lblStock.Click += label1_Click;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(10, 152);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "Beskrivelse:";
            lblDescription.Click += label1_Click;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(10, 119);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(29, 15);
            lblPrice.TabIndex = 1;
            lblPrice.Text = "Pris:";
            lblPrice.Click += label1_Click;
            // 
            // lblBrewery
            // 
            lblBrewery.AutoSize = true;
            lblBrewery.Location = new Point(10, 87);
            lblBrewery.Name = "lblBrewery";
            lblBrewery.Size = new Size(54, 15);
            lblBrewery.TabIndex = 1;
            lblBrewery.Text = "Bryggeri:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 52);
            lblName.Name = "lblName";
            lblName.Size = new Size(38, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Navn:";
            // 
            // txtCategory
            // 
            txtCategory.Location = new Point(135, 260);
            txtCategory.Margin = new Padding(3, 2, 3, 2);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(148, 23);
            txtCategory.TabIndex = 0;
            txtCategory.TextChanged += textBox3_TextChanged;
            // 
            // txtABV
            // 
            txtABV.Location = new Point(135, 222);
            txtABV.Margin = new Padding(3, 2, 3, 2);
            txtABV.Name = "txtABV";
            txtABV.Size = new Size(148, 23);
            txtABV.TabIndex = 0;
            txtABV.TextChanged += textBox3_TextChanged;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(135, 187);
            txtStock.Margin = new Padding(3, 2, 3, 2);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(148, 23);
            txtStock.TabIndex = 0;
            txtStock.TextChanged += textBox3_TextChanged;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(135, 152);
            txtDescription.Margin = new Padding(3, 2, 3, 2);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(148, 23);
            txtDescription.TabIndex = 0;
            txtDescription.TextChanged += textBox3_TextChanged;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(135, 119);
            txtPrice.Margin = new Padding(3, 2, 3, 2);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(148, 23);
            txtPrice.TabIndex = 0;
            // 
            // txtBrewery
            // 
            txtBrewery.Location = new Point(135, 85);
            txtBrewery.Margin = new Padding(3, 2, 3, 2);
            txtBrewery.Name = "txtBrewery";
            txtBrewery.Size = new Size(148, 23);
            txtBrewery.TabIndex = 0;
            // 
            // txtName
            // 
            txtName.Location = new Point(135, 52);
            txtName.Margin = new Padding(3, 2, 3, 2);
            txtName.Name = "txtName";
            txtName.Size = new Size(148, 23);
            txtName.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(491, 262);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(91, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // AddProductForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(grpAddProductPage);
            Margin = new Padding(3, 2, 3, 2);
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
        private TextBox txtCategory;
        private TextBox txtABV;
        private TextBox txtStock;
        private Button btnAddProduct;
        private Button btnCancel;
    }
}