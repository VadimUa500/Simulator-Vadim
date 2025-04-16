namespace тест
{
    partial class AdminPanelForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminPanelForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabProducts = new System.Windows.Forms.TabPage();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnEditProduct = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.txtProductDiscount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProductCategory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProductDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProductPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.txtProductImagePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.tabOrders = new System.Windows.Forms.TabPage();
            this.btnChangeOrderStatus = new System.Windows.Forms.Button();
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.btnUnblockUser = new System.Windows.Forms.Button();
            this.btnBlockUser = new System.Windows.Forms.Button();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.lblPopularProduct = new System.Windows.Forms.Label();
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            this.tabOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.tabReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabProducts);
            this.tabControl.Controls.Add(this.tabOrders);
            this.tabControl.Controls.Add(this.tabUsers);
            this.tabControl.Controls.Add(this.tabReports);
            this.tabControl.Location = new System.Drawing.Point(16, 15);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 492);
            this.tabControl.TabIndex = 0;
            // 
            // tabProducts
            // 
            this.tabProducts.Controls.Add(this.btnDeleteProduct);
            this.tabProducts.Controls.Add(this.btnEditProduct);
            this.tabProducts.Controls.Add(this.btnAddProduct);
            this.tabProducts.Controls.Add(this.txtProductDiscount);
            this.tabProducts.Controls.Add(this.label5);
            this.tabProducts.Controls.Add(this.txtProductCategory);
            this.tabProducts.Controls.Add(this.label4);
            this.tabProducts.Controls.Add(this.txtProductDescription);
            this.tabProducts.Controls.Add(this.label3);
            this.tabProducts.Controls.Add(this.txtProductPrice);
            this.tabProducts.Controls.Add(this.label2);
            this.tabProducts.Controls.Add(this.txtProductName);
            this.tabProducts.Controls.Add(this.label1);
            this.tabProducts.Controls.Add(this.dataGridViewProducts);
            this.tabProducts.Controls.Add(this.txtProductImagePath);
            this.tabProducts.Controls.Add(this.label6);
            this.tabProducts.Controls.Add(this.btnSelectImage);
            this.tabProducts.Location = new System.Drawing.Point(4, 25);
            this.tabProducts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabProducts.Size = new System.Drawing.Size(992, 463);
            this.tabProducts.TabIndex = 0;
            this.tabProducts.Text = "Товари";
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.Location = new System.Drawing.Point(827, 418);
            this.btnDeleteProduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(133, 37);
            this.btnDeleteProduct.TabIndex = 9;
            this.btnDeleteProduct.Text = "Видалити";
            this.btnDeleteProduct.UseVisualStyleBackColor = true;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // btnEditProduct
            // 
            this.btnEditProduct.Location = new System.Drawing.Point(680, 418);
            this.btnEditProduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditProduct.Name = "btnEditProduct";
            this.btnEditProduct.Size = new System.Drawing.Size(133, 37);
            this.btnEditProduct.TabIndex = 8;
            this.btnEditProduct.Text = "Редагувати";
            this.btnEditProduct.UseVisualStyleBackColor = true;
            this.btnEditProduct.Click += new System.EventHandler(this.btnEditProduct_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(533, 418);
            this.btnAddProduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(133, 37);
            this.btnAddProduct.TabIndex = 7;
            this.btnAddProduct.Text = "Додати";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // txtProductDiscount
            // 
            this.txtProductDiscount.Location = new System.Drawing.Point(400, 382);
            this.txtProductDiscount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProductDiscount.Name = "txtProductDiscount";
            this.txtProductDiscount.Size = new System.Drawing.Size(105, 22);
            this.txtProductDiscount.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(400, 357);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Знижка (%):";
            // 
            // txtProductCategory
            // 
            this.txtProductCategory.Location = new System.Drawing.Point(267, 382);
            this.txtProductCategory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProductCategory.Name = "txtProductCategory";
            this.txtProductCategory.Size = new System.Drawing.Size(105, 22);
            this.txtProductCategory.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(267, 357);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "ID категорії:";
            // 
            // txtProductDescription
            // 
            this.txtProductDescription.Location = new System.Drawing.Point(133, 382);
            this.txtProductDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProductDescription.Name = "txtProductDescription";
            this.txtProductDescription.Size = new System.Drawing.Size(105, 22);
            this.txtProductDescription.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 357);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Опис:";
            // 
            // txtProductPrice
            // 
            this.txtProductPrice.Location = new System.Drawing.Point(13, 382);
            this.txtProductPrice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProductPrice.Name = "txtProductPrice";
            this.txtProductPrice.Size = new System.Drawing.Size(105, 22);
            this.txtProductPrice.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 357);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ціна:";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(13, 320);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(265, 22);
            this.txtProductName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 295);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Назва:";
            // 
            // dataGridViewProducts
            // 
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Location = new System.Drawing.Point(13, 12);
            this.dataGridViewProducts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.RowHeadersWidth = 51;
            this.dataGridViewProducts.Size = new System.Drawing.Size(960, 271);
            this.dataGridViewProducts.TabIndex = 0;
            this.dataGridViewProducts.SelectionChanged += new System.EventHandler(this.dataGridViewProducts_SelectionChanged);
            // 
            // txtProductImagePath
            // 
            this.txtProductImagePath.Location = new System.Drawing.Point(319, 320);
            this.txtProductImagePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProductImagePath.Name = "txtProductImagePath";
            this.txtProductImagePath.ReadOnly = true;
            this.txtProductImagePath.Size = new System.Drawing.Size(265, 22);
            this.txtProductImagePath.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(352, 300);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Шлях до зображення:";
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(593, 319);
            this.btnSelectImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(133, 25);
            this.btnSelectImage.TabIndex = 12;
            this.btnSelectImage.Text = "Вибрати зображення";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // tabOrders
            // 
            this.tabOrders.Controls.Add(this.btnChangeOrderStatus);
            this.tabOrders.Controls.Add(this.dataGridViewOrders);
            this.tabOrders.Location = new System.Drawing.Point(4, 25);
            this.tabOrders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabOrders.Name = "tabOrders";
            this.tabOrders.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabOrders.Size = new System.Drawing.Size(992, 463);
            this.tabOrders.TabIndex = 1;
            this.tabOrders.Text = "Замовлення";
            // 
            // btnChangeOrderStatus
            // 
            this.btnChangeOrderStatus.Location = new System.Drawing.Point(827, 418);
            this.btnChangeOrderStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChangeOrderStatus.Name = "btnChangeOrderStatus";
            this.btnChangeOrderStatus.Size = new System.Drawing.Size(133, 37);
            this.btnChangeOrderStatus.TabIndex = 1;
            this.btnChangeOrderStatus.Text = "Змінити статус";
            this.btnChangeOrderStatus.UseVisualStyleBackColor = true;
            this.btnChangeOrderStatus.Click += new System.EventHandler(this.btnChangeOrderStatus_Click);
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(13, 12);
            this.dataGridViewOrders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.RowHeadersWidth = 51;
            this.dataGridViewOrders.Size = new System.Drawing.Size(960, 394);
            this.dataGridViewOrders.TabIndex = 0;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.btnUnblockUser);
            this.tabUsers.Controls.Add(this.btnBlockUser);
            this.tabUsers.Controls.Add(this.dataGridViewUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 25);
            this.tabUsers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Size = new System.Drawing.Size(992, 463);
            this.tabUsers.TabIndex = 2;
            this.tabUsers.Text = "Користувачі";
            // 
            // btnUnblockUser
            // 
            this.btnUnblockUser.Location = new System.Drawing.Point(827, 418);
            this.btnUnblockUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUnblockUser.Name = "btnUnblockUser";
            this.btnUnblockUser.Size = new System.Drawing.Size(133, 37);
            this.btnUnblockUser.TabIndex = 2;
            this.btnUnblockUser.Text = "Розблокувати";
            this.btnUnblockUser.UseVisualStyleBackColor = true;
            this.btnUnblockUser.Click += new System.EventHandler(this.btnUnblockUser_Click);
            // 
            // btnBlockUser
            // 
            this.btnBlockUser.Location = new System.Drawing.Point(680, 418);
            this.btnBlockUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBlockUser.Name = "btnBlockUser";
            this.btnBlockUser.Size = new System.Drawing.Size(133, 37);
            this.btnBlockUser.TabIndex = 1;
            this.btnBlockUser.Text = "Заблокувати";
            this.btnBlockUser.UseVisualStyleBackColor = true;
            this.btnBlockUser.Click += new System.EventHandler(this.btnBlockUser_Click);
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(13, 12);
            this.dataGridViewUsers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.RowHeadersWidth = 51;
            this.dataGridViewUsers.Size = new System.Drawing.Size(960, 394);
            this.dataGridViewUsers.TabIndex = 0;
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.lblPopularProduct);
            this.tabReports.Controls.Add(this.lblTotalProfit);
            this.tabReports.Location = new System.Drawing.Point(4, 25);
            this.tabReports.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabReports.Name = "tabReports";
            this.tabReports.Size = new System.Drawing.Size(992, 463);
            this.tabReports.TabIndex = 3;
            this.tabReports.Text = "Звіти";
            // 
            // lblPopularProduct
            // 
            this.lblPopularProduct.AutoSize = true;
            this.lblPopularProduct.Location = new System.Drawing.Point(13, 49);
            this.lblPopularProduct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPopularProduct.Name = "lblPopularProduct";
            this.lblPopularProduct.Size = new System.Drawing.Size(218, 16);
            this.lblPopularProduct.TabIndex = 1;
            this.lblPopularProduct.Text = "Популярний товар: немає даних";
            // 
            // lblTotalProfit
            // 
            this.lblTotalProfit.AutoSize = true;
            this.lblTotalProfit.Location = new System.Drawing.Point(13, 25);
            this.lblTotalProfit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(180, 16);
            this.lblTotalProfit.TabIndex = 0;
            this.lblTotalProfit.Text = "Загальний прибуток: 0 грн";
            // 
            // AdminPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 554);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AdminPanelForm";
            this.Text = "Панель адміністратора";
            this.tabControl.ResumeLayout(false);
            this.tabProducts.ResumeLayout(false);
            this.tabProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.tabOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.tabUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.tabReports.ResumeLayout(false);
            this.tabReports.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabProducts;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnEditProduct;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.TextBox txtProductDiscount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProductCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProductDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProductPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.TextBox txtProductImagePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.TabPage tabOrders;
        private System.Windows.Forms.Button btnChangeOrderStatus;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.Button btnUnblockUser;
        private System.Windows.Forms.Button btnBlockUser;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.Label lblPopularProduct;
        private System.Windows.Forms.Label lblTotalProfit;
    }
}