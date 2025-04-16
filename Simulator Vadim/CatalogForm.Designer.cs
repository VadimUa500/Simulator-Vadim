using System.Drawing;
using System.Windows.Forms;

namespace тест
{
    partial class CatalogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogForm));
            this.flowLayoutPanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.labelSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.labelCategory = new System.Windows.Forms.Label();
            this.comboBoxCategories = new System.Windows.Forms.ComboBox();
            this.labelSort = new System.Windows.Forms.Label();
            this.comboBoxSort = new System.Windows.Forms.ComboBox();
            this.labelMinPrice = new System.Windows.Forms.Label();
            this.txtMinPrice = new System.Windows.Forms.TextBox();
            this.labelMaxPrice = new System.Windows.Forms.Label();
            this.txtMaxPrice = new System.Windows.Forms.TextBox();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.btnViewCart = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.panelFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelProducts
            // 
            this.flowLayoutPanelProducts.AutoScroll = true;
            this.flowLayoutPanelProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.flowLayoutPanelProducts.Location = new System.Drawing.Point(16, 15);
            this.flowLayoutPanelProducts.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            this.flowLayoutPanelProducts.Size = new System.Drawing.Size(1013, 492);
            this.flowLayoutPanelProducts.TabIndex = 0;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.panelFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilters.Controls.Add(this.labelSearch);
            this.panelFilters.Controls.Add(this.txtSearch);
            this.panelFilters.Controls.Add(this.btnSearch);
            this.panelFilters.Controls.Add(this.labelCategory);
            this.panelFilters.Controls.Add(this.comboBoxCategories);
            this.panelFilters.Controls.Add(this.labelSort);
            this.panelFilters.Controls.Add(this.comboBoxSort);
            this.panelFilters.Controls.Add(this.labelMinPrice);
            this.panelFilters.Controls.Add(this.txtMinPrice);
            this.panelFilters.Controls.Add(this.labelMaxPrice);
            this.panelFilters.Controls.Add(this.txtMaxPrice);
            this.panelFilters.Controls.Add(this.btnApplyFilters);
            this.panelFilters.Controls.Add(this.btnViewCart);
            this.panelFilters.Controls.Add(this.btnProfile);
            this.panelFilters.Location = new System.Drawing.Point(16, 517);
            this.panelFilters.Margin = new System.Windows.Forms.Padding(4);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1013, 123);
            this.panelFilters.TabIndex = 1;
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(13, 12);
            this.labelSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(52, 16);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "Пошук:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(93, 9);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(399, 22);
            this.txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(507, 9);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(133, 31);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Пошук";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(13, 49);
            this.labelCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(73, 16);
            this.labelCategory.TabIndex = 3;
            this.labelCategory.Text = "Категорія:";
            // 
            // comboBoxCategories
            // 
            this.comboBoxCategories.Location = new System.Drawing.Point(93, 46);
            this.comboBoxCategories.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxCategories.Name = "comboBoxCategories";
            this.comboBoxCategories.Size = new System.Drawing.Size(199, 24);
            this.comboBoxCategories.TabIndex = 4;
            this.comboBoxCategories.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategories_SelectedIndexChanged);
            // 
            // labelSort
            // 
            this.labelSort.AutoSize = true;
            this.labelSort.Location = new System.Drawing.Point(307, 49);
            this.labelSort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSort.Name = "labelSort";
            this.labelSort.Size = new System.Drawing.Size(81, 16);
            this.labelSort.TabIndex = 5;
            this.labelSort.Text = "Сортувати:";
            // 
            // comboBoxSort
            // 
            this.comboBoxSort.Items.AddRange(new object[] {
            "За назвою (А-Я)",
            "За назвою (Я-А)",
            "За ціною (зростання)",
            "За ціною (спадання)",
            "За знижкою"});
            this.comboBoxSort.Location = new System.Drawing.Point(387, 46);
            this.comboBoxSort.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSort.Name = "comboBoxSort";
            this.comboBoxSort.Size = new System.Drawing.Size(199, 24);
            this.comboBoxSort.TabIndex = 6;
            // 
            // labelMinPrice
            // 
            this.labelMinPrice.AutoSize = true;
            this.labelMinPrice.Location = new System.Drawing.Point(600, 49);
            this.labelMinPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMinPrice.Name = "labelMinPrice";
            this.labelMinPrice.Size = new System.Drawing.Size(65, 16);
            this.labelMinPrice.TabIndex = 7;
            this.labelMinPrice.Text = "Мін. ціна:";
            // 
            // txtMinPrice
            // 
            this.txtMinPrice.Location = new System.Drawing.Point(680, 46);
            this.txtMinPrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinPrice.Name = "txtMinPrice";
            this.txtMinPrice.Size = new System.Drawing.Size(79, 22);
            this.txtMinPrice.TabIndex = 8;
            // 
            // labelMaxPrice
            // 
            this.labelMaxPrice.AutoSize = true;
            this.labelMaxPrice.Location = new System.Drawing.Point(773, 49);
            this.labelMaxPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMaxPrice.Name = "labelMaxPrice";
            this.labelMaxPrice.Size = new System.Drawing.Size(76, 16);
            this.labelMaxPrice.TabIndex = 9;
            this.labelMaxPrice.Text = "Макс. ціна:";
            // 
            // txtMaxPrice
            // 
            this.txtMaxPrice.Location = new System.Drawing.Point(853, 46);
            this.txtMaxPrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxPrice.Name = "txtMaxPrice";
            this.txtMaxPrice.Size = new System.Drawing.Size(79, 22);
            this.txtMaxPrice.TabIndex = 10;
            // 
            // btnApplyFilters
            // 
            this.btnApplyFilters.Location = new System.Drawing.Point(867, 80);
            this.btnApplyFilters.Margin = new System.Windows.Forms.Padding(4);
            this.btnApplyFilters.Name = "btnApplyFilters";
            this.btnApplyFilters.Size = new System.Drawing.Size(133, 31);
            this.btnApplyFilters.TabIndex = 11;
            this.btnApplyFilters.Text = "Застосувати";
            this.btnApplyFilters.UseVisualStyleBackColor = true;
            this.btnApplyFilters.Click += new System.EventHandler(this.btnApplyFilters_Click);
            // 
            // btnViewCart
            // 
            this.btnViewCart.Location = new System.Drawing.Point(720, 80);
            this.btnViewCart.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewCart.Name = "btnViewCart";
            this.btnViewCart.Size = new System.Drawing.Size(133, 31);
            this.btnViewCart.TabIndex = 12;
            this.btnViewCart.Text = "Кошик";
            this.btnViewCart.UseVisualStyleBackColor = true;
            this.btnViewCart.Click += new System.EventHandler(this.btnViewCart_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.Location = new System.Drawing.Point(573, 80);
            this.btnProfile.Margin = new System.Windows.Forms.Padding(4);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(133, 31);
            this.btnProfile.TabIndex = 13;
            this.btnProfile.Text = "Мій профіль";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // CatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 654);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.flowLayoutPanelProducts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CatalogForm";
            this.Text = "Каталог товарів";
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProducts;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.ComboBox comboBoxCategories;
        private System.Windows.Forms.Label labelSort;
        private System.Windows.Forms.ComboBox comboBoxSort;
        private System.Windows.Forms.Label labelMinPrice;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.Label labelMaxPrice;
        private System.Windows.Forms.TextBox txtMaxPrice;
        private System.Windows.Forms.Button btnApplyFilters;
        private System.Windows.Forms.Button btnViewCart;
        private System.Windows.Forms.Button btnProfile;
    }
}