namespace тест
{
    partial class UserProfileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserProfileForm));
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblLoyaltyPoints = new System.Windows.Forms.Label();
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(16, 15);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(120, 16);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Ім\'я користувача:";
            // 
            // lblLoyaltyPoints
            // 
            this.lblLoyaltyPoints.AutoSize = true;
            this.lblLoyaltyPoints.Location = new System.Drawing.Point(16, 39);
            this.lblLoyaltyPoints.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoyaltyPoints.Name = "lblLoyaltyPoints";
            this.lblLoyaltyPoints.Size = new System.Drawing.Size(117, 16);
            this.lblLoyaltyPoints.TabIndex = 1;
            this.lblLoyaltyPoints.Text = "Бали лояльності:";
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(16, 74);
            this.dataGridViewOrders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.RowHeadersWidth = 51;
            this.dataGridViewOrders.Size = new System.Drawing.Size(867, 369);
            this.dataGridViewOrders.TabIndex = 2;
            this.dataGridViewOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOrders_CellClick);
            // 
            // UserProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 492);
            this.Controls.Add(this.dataGridViewOrders);
            this.Controls.Add(this.lblLoyaltyPoints);
            this.Controls.Add(this.lblUsername);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UserProfileForm";
            this.Text = "Мій профіль";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblLoyaltyPoints;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
    }
}