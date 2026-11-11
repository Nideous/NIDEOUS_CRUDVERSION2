using System;
using System.Windows.Forms;

namespace NIDEOUS_CRUDVERSION2
{
    partial class Form1
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

        #region Windows Form Designer generated code

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtPOSupplierId.Text, out int supplierId))
            {
                ToggleSupplierStatus(supplierId);
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric Supplier ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void InitializeComponent()
        {
            this.grpProductEntry = new System.Windows.Forms.GroupBox();
            this.btnSaveProduct = new System.Windows.Forms.Button();
            this.txtStockOnHand = new System.Windows.Forms.TextBox();
            this.txtUnitCost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSupplierId = new System.Windows.Forms.Label();
            this.txtSupplierId = new System.Windows.Forms.TextBox();
            this.lblCategoryId = new System.Windows.Forms.Label();
            this.txtCategoryId = new System.Windows.Forms.TextBox();
            this.txtReorderLevel = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtSKU = new System.Windows.Forms.TextBox();
            this.Reorder = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.SKU = new System.Windows.Forms.Label();
            this.grpPurchaseOrders = new System.Windows.Forms.GroupBox();
            this.btnAddLine = new System.Windows.Forms.Button();
            this.btnReceivePO = new System.Windows.Forms.Button();
            this.txtPOLineCost = new System.Windows.Forms.TextBox();
            this.txtPOQty = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPOProductId = new System.Windows.Forms.TextBox();
            this.txtPOId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCreatePO = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPOSupplierId = new System.Windows.Forms.TextBox();
            this.txtPONumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnLowStockReport = new System.Windows.Forms.Button();
            this.cmbSuppliers = new System.Windows.Forms.ComboBox();
            this.btnPOStatusReport = new System.Windows.Forms.Button();
            this.btnToggleSupplier = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnViewCategories = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddSupplier = new System.Windows.Forms.Button();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.txtSupplierContact = new System.Windows.Forms.TextBox();
            this.grpProductEntry.SuspendLayout();
            this.grpPurchaseOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpProductEntry
            // 
            this.grpProductEntry.Controls.Add(this.btnSaveProduct);
            this.grpProductEntry.Controls.Add(this.txtStockOnHand);
            this.grpProductEntry.Controls.Add(this.txtUnitCost);
            this.grpProductEntry.Controls.Add(this.label5);
            this.grpProductEntry.Controls.Add(this.label4);
            this.grpProductEntry.Controls.Add(this.lblSupplierId);
            this.grpProductEntry.Controls.Add(this.txtSupplierId);
            this.grpProductEntry.Controls.Add(this.lblCategoryId);
            this.grpProductEntry.Controls.Add(this.txtCategoryId);
            this.grpProductEntry.Controls.Add(this.txtReorderLevel);
            this.grpProductEntry.Controls.Add(this.txtProductName);
            this.grpProductEntry.Controls.Add(this.txtSKU);
            this.grpProductEntry.Controls.Add(this.Reorder);
            this.grpProductEntry.Controls.Add(this.lblName);
            this.grpProductEntry.Controls.Add(this.SKU);
            this.grpProductEntry.Location = new System.Drawing.Point(247, 42);
            this.grpProductEntry.Margin = new System.Windows.Forms.Padding(4);
            this.grpProductEntry.Name = "grpProductEntry";
            this.grpProductEntry.Padding = new System.Windows.Forms.Padding(4);
            this.grpProductEntry.Size = new System.Drawing.Size(531, 159);
            this.grpProductEntry.TabIndex = 5;
            this.grpProductEntry.TabStop = false;
            this.grpProductEntry.Text = "Product Entry";
            // 
            // btnSaveProduct
            // 
            this.btnSaveProduct.Location = new System.Drawing.Point(436, 119);
            this.btnSaveProduct.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveProduct.Name = "btnSaveProduct";
            this.btnSaveProduct.Size = new System.Drawing.Size(91, 28);
            this.btnSaveProduct.TabIndex = 15;
            this.btnSaveProduct.Text = "Add Product";
            this.btnSaveProduct.UseVisualStyleBackColor = true;
            this.btnSaveProduct.Click += new System.EventHandler(this.btnSaveProduct_Click);
            // 
            // txtStockOnHand
            // 
            this.txtStockOnHand.Location = new System.Drawing.Point(299, 119);
            this.txtStockOnHand.Margin = new System.Windows.Forms.Padding(4);
            this.txtStockOnHand.Name = "txtStockOnHand";
            this.txtStockOnHand.Size = new System.Drawing.Size(117, 22);
            this.txtStockOnHand.TabIndex = 14;
            // 
            // txtUnitCost
            // 
            this.txtUnitCost.Location = new System.Drawing.Point(93, 119);
            this.txtUnitCost.Margin = new System.Windows.Forms.Padding(4);
            this.txtUnitCost.Name = "txtUnitCost";
            this.txtUnitCost.Size = new System.Drawing.Size(117, 22);
            this.txtUnitCost.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 123);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Stock:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 123);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Unit Cost:";
            // 
            // lblSupplierId
            // 
            this.lblSupplierId.AutoSize = true;
            this.lblSupplierId.Location = new System.Drawing.Point(207, 78);
            this.lblSupplierId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSupplierId.Name = "lblSupplierId";
            this.lblSupplierId.Size = new System.Drawing.Size(76, 16);
            this.lblSupplierId.TabIndex = 8;
            this.lblSupplierId.Text = "Supplier ID:";
            // 
            // txtSupplierId
            // 
            this.txtSupplierId.Location = new System.Drawing.Point(293, 74);
            this.txtSupplierId.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupplierId.Name = "txtSupplierId";
            this.txtSupplierId.Size = new System.Drawing.Size(79, 22);
            this.txtSupplierId.TabIndex = 9;
            // 
            // lblCategoryId
            // 
            this.lblCategoryId.AutoSize = true;
            this.lblCategoryId.Location = new System.Drawing.Point(20, 78);
            this.lblCategoryId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategoryId.Name = "lblCategoryId";
            this.lblCategoryId.Size = new System.Drawing.Size(81, 16);
            this.lblCategoryId.TabIndex = 6;
            this.lblCategoryId.Text = "Category ID:";
            // 
            // txtCategoryId
            // 
            this.txtCategoryId.Location = new System.Drawing.Point(111, 74);
            this.txtCategoryId.Margin = new System.Windows.Forms.Padding(4);
            this.txtCategoryId.Name = "txtCategoryId";
            this.txtCategoryId.Size = new System.Drawing.Size(79, 22);
            this.txtCategoryId.TabIndex = 7;
            // 
            // txtReorderLevel
            // 
            this.txtReorderLevel.Location = new System.Drawing.Point(436, 28);
            this.txtReorderLevel.Margin = new System.Windows.Forms.Padding(4);
            this.txtReorderLevel.Name = "txtReorderLevel";
            this.txtReorderLevel.Size = new System.Drawing.Size(75, 22);
            this.txtReorderLevel.TabIndex = 5;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(251, 28);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(104, 22);
            this.txtProductName.TabIndex = 4;
            // 
            // txtSKU
            // 
            this.txtSKU.Location = new System.Drawing.Point(77, 33);
            this.txtSKU.Margin = new System.Windows.Forms.Padding(4);
            this.txtSKU.Name = "txtSKU";
            this.txtSKU.Size = new System.Drawing.Size(99, 22);
            this.txtSKU.TabIndex = 3;
            // 
            // Reorder
            // 
            this.Reorder.AutoSize = true;
            this.Reorder.Location = new System.Drawing.Point(364, 32);
            this.Reorder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Reorder.Name = "Reorder";
            this.Reorder.Size = new System.Drawing.Size(60, 16);
            this.Reorder.TabIndex = 2;
            this.Reorder.Text = "Reorder:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(192, 32);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 16);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // SKU
            // 
            this.SKU.AutoSize = true;
            this.SKU.Location = new System.Drawing.Point(27, 32);
            this.SKU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SKU.Name = "SKU";
            this.SKU.Size = new System.Drawing.Size(37, 16);
            this.SKU.TabIndex = 0;
            this.SKU.Text = "SKU:";
            // 
            // grpPurchaseOrders
            // 
            this.grpPurchaseOrders.Controls.Add(this.btnAddLine);
            this.grpPurchaseOrders.Controls.Add(this.btnReceivePO);
            this.grpPurchaseOrders.Controls.Add(this.txtPOLineCost);
            this.grpPurchaseOrders.Controls.Add(this.txtPOQty);
            this.grpPurchaseOrders.Controls.Add(this.label10);
            this.grpPurchaseOrders.Controls.Add(this.label11);
            this.grpPurchaseOrders.Controls.Add(this.txtPOProductId);
            this.grpPurchaseOrders.Controls.Add(this.txtPOId);
            this.grpPurchaseOrders.Controls.Add(this.label9);
            this.grpPurchaseOrders.Controls.Add(this.label8);
            this.grpPurchaseOrders.Controls.Add(this.btnCreatePO);
            this.grpPurchaseOrders.Controls.Add(this.label7);
            this.grpPurchaseOrders.Controls.Add(this.txtPOSupplierId);
            this.grpPurchaseOrders.Controls.Add(this.txtPONumber);
            this.grpPurchaseOrders.Controls.Add(this.label6);
            this.grpPurchaseOrders.Location = new System.Drawing.Point(786, 42);
            this.grpPurchaseOrders.Margin = new System.Windows.Forms.Padding(4);
            this.grpPurchaseOrders.Name = "grpPurchaseOrders";
            this.grpPurchaseOrders.Padding = new System.Windows.Forms.Padding(4);
            this.grpPurchaseOrders.Size = new System.Drawing.Size(548, 159);
            this.grpPurchaseOrders.TabIndex = 6;
            this.grpPurchaseOrders.TabStop = false;
            this.grpPurchaseOrders.Text = "Purchase Orders";
            // 
            // btnAddLine
            // 
            this.btnAddLine.Location = new System.Drawing.Point(453, 74);
            this.btnAddLine.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddLine.Name = "btnAddLine";
            this.btnAddLine.Size = new System.Drawing.Size(88, 28);
            this.btnAddLine.TabIndex = 15;
            this.btnAddLine.Text = "Add";
            this.btnAddLine.UseVisualStyleBackColor = true;
            this.btnAddLine.Click += new System.EventHandler(this.btnAddLine_Click);
            // 
            // btnReceivePO
            // 
            this.btnReceivePO.Location = new System.Drawing.Point(453, 116);
            this.btnReceivePO.Margin = new System.Windows.Forms.Padding(4);
            this.btnReceivePO.Name = "btnReceivePO";
            this.btnReceivePO.Size = new System.Drawing.Size(88, 28);
            this.btnReceivePO.TabIndex = 14;
            this.btnReceivePO.Text = "Receive PO";
            this.btnReceivePO.UseVisualStyleBackColor = true;
            this.btnReceivePO.Click += new System.EventHandler(this.btnReceivePO_Click);
            // 
            // txtPOLineCost
            // 
            this.txtPOLineCost.Location = new System.Drawing.Point(339, 119);
            this.txtPOLineCost.Margin = new System.Windows.Forms.Padding(4);
            this.txtPOLineCost.Name = "txtPOLineCost";
            this.txtPOLineCost.Size = new System.Drawing.Size(97, 22);
            this.txtPOLineCost.TabIndex = 13;
            // 
            // txtPOQty
            // 
            this.txtPOQty.Location = new System.Drawing.Point(299, 74);
            this.txtPOQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtPOQty.Name = "txtPOQty";
            this.txtPOQty.Size = new System.Drawing.Size(132, 22);
            this.txtPOQty.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(260, 123);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 16);
            this.label10.TabIndex = 11;
            this.label10.Text = "Unit Cost:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(260, 78);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 16);
            this.label11.TabIndex = 10;
            this.label11.Text = "Qty";
            // 
            // txtPOProductId
            // 
            this.txtPOProductId.Location = new System.Drawing.Point(116, 116);
            this.txtPOProductId.Margin = new System.Windows.Forms.Padding(4);
            this.txtPOProductId.Name = "txtPOProductId";
            this.txtPOProductId.Size = new System.Drawing.Size(121, 22);
            this.txtPOProductId.TabIndex = 8;
            // 
            // txtPOId
            // 
            this.txtPOId.Location = new System.Drawing.Point(87, 69);
            this.txtPOId.Margin = new System.Windows.Forms.Padding(4);
            this.txtPOId.Name = "txtPOId";
            this.txtPOId.Size = new System.Drawing.Size(121, 22);
            this.txtPOId.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 74);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 16);
            this.label9.TabIndex = 6;
            this.label9.Text = "PO ID:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 119);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 5;
            this.label8.Text = "Product ID:";
            // 
            // btnCreatePO
            // 
            this.btnCreatePO.Location = new System.Drawing.Point(453, 32);
            this.btnCreatePO.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreatePO.Name = "btnCreatePO";
            this.btnCreatePO.Size = new System.Drawing.Size(88, 28);
            this.btnCreatePO.TabIndex = 4;
            this.btnCreatePO.Text = "Create Draft";
            this.btnCreatePO.UseVisualStyleBackColor = true;
            this.btnCreatePO.Click += new System.EventHandler(this.btnCreatePO_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(277, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "ID: ";
            // 
            // txtPOSupplierId
            // 
            this.txtPOSupplierId.Location = new System.Drawing.Point(304, 32);
            this.txtPOSupplierId.Margin = new System.Windows.Forms.Padding(4);
            this.txtPOSupplierId.Name = "txtPOSupplierId";
            this.txtPOSupplierId.Size = new System.Drawing.Size(132, 22);
            this.txtPOSupplierId.TabIndex = 2;
            // 
            // txtPONumber
            // 
            this.txtPONumber.Location = new System.Drawing.Point(121, 28);
            this.txtPONumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.Size = new System.Drawing.Size(132, 22);
            this.txtPONumber.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "PO Number:";
            // 
            // dgvResults
            // 
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(67, 248);
            this.dgvResults.Margin = new System.Windows.Forms.Padding(4);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.Size = new System.Drawing.Size(932, 185);
            this.dgvResults.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(80, 222);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 16);
            this.label12.TabIndex = 9;
            this.label12.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(147, 218);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(132, 22);
            this.txtSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(309, 218);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnLowStockReport
            // 
            this.btnLowStockReport.Location = new System.Drawing.Point(443, 218);
            this.btnLowStockReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnLowStockReport.Name = "btnLowStockReport";
            this.btnLowStockReport.Size = new System.Drawing.Size(148, 28);
            this.btnLowStockReport.TabIndex = 12;
            this.btnLowStockReport.Text = "Low Stock Report";
            this.btnLowStockReport.UseVisualStyleBackColor = true;
            this.btnLowStockReport.Click += new System.EventHandler(this.btnLowStockReport_Click);
            // 
            // cmbSuppliers
            // 
            this.cmbSuppliers.FormattingEnabled = true;
            this.cmbSuppliers.Location = new System.Drawing.Point(816, 207);
            this.cmbSuppliers.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSuppliers.Name = "cmbSuppliers";
            this.cmbSuppliers.Size = new System.Drawing.Size(160, 24);
            this.cmbSuppliers.TabIndex = 13;
            this.cmbSuppliers.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnPOStatusReport
            // 
            this.btnPOStatusReport.Location = new System.Drawing.Point(606, 218);
            this.btnPOStatusReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnPOStatusReport.Name = "btnPOStatusReport";
            this.btnPOStatusReport.Size = new System.Drawing.Size(148, 28);
            this.btnPOStatusReport.TabIndex = 14;
            this.btnPOStatusReport.Text = "PO Status Report";
            this.btnPOStatusReport.UseVisualStyleBackColor = true;
            this.btnPOStatusReport.Click += new System.EventHandler(this.btnPOStatusReport_Click_1);
            // 
            // btnToggleSupplier
            // 
            this.btnToggleSupplier.Location = new System.Drawing.Point(986, 208);
            this.btnToggleSupplier.Name = "btnToggleSupplier";
            this.btnToggleSupplier.Size = new System.Drawing.Size(121, 23);
            this.btnToggleSupplier.TabIndex = 15;
            this.btnToggleSupplier.Text = "Toggle Status";
            this.btnToggleSupplier.UseVisualStyleBackColor = true;
            this.btnToggleSupplier.Click += new System.EventHandler(this.btnToggleSupplier_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnViewCategories);
            this.groupBox1.Controls.Add(this.btnAddCategory);
            this.groupBox1.Controls.Add(this.txtCategoryName);
            this.groupBox1.Location = new System.Drawing.Point(1033, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 213);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Category Management";
            // 
            // btnViewCategories
            // 
            this.btnViewCategories.Location = new System.Drawing.Point(18, 122);
            this.btnViewCategories.Name = "btnViewCategories";
            this.btnViewCategories.Size = new System.Drawing.Size(113, 23);
            this.btnViewCategories.TabIndex = 2;
            this.btnViewCategories.Text = "View Categories";
            this.btnViewCategories.UseVisualStyleBackColor = true;
            this.btnViewCategories.Click += new System.EventHandler(this.btnViewCategories_Click_1);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Location = new System.Drawing.Point(18, 82);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(113, 23);
            this.btnAddCategory.TabIndex = 1;
            this.btnAddCategory.Text = "Add Category";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(18, 41);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(113, 22);
            this.txtCategoryName.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddSupplier);
            this.groupBox2.Controls.Add(this.txtSupplierName);
            this.groupBox2.Controls.Add(this.txtSupplierContact);
            this.groupBox2.Location = new System.Drawing.Point(25, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 159);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Supplier Management";
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.Location = new System.Drawing.Point(42, 112);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(100, 23);
            this.btnAddSupplier.TabIndex = 4;
            this.btnAddSupplier.Text = "Add Supplier";
            this.btnAddSupplier.UseVisualStyleBackColor = true;
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(42, 33);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(100, 22);
            this.txtSupplierName.TabIndex = 3;
            // 
            // txtSupplierContact
            // 
            this.txtSupplierContact.Location = new System.Drawing.Point(42, 71);
            this.txtSupplierContact.Name = "txtSupplierContact";
            this.txtSupplierContact.Size = new System.Drawing.Size(100, 22);
            this.txtSupplierContact.TabIndex = 0;
            this.txtSupplierContact.TextChanged += new System.EventHandler(this.txtSupplierContact_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 473);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnToggleSupplier);
            this.Controls.Add(this.btnPOStatusReport);
            this.Controls.Add(this.cmbSuppliers);
            this.Controls.Add(this.btnLowStockReport);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.grpPurchaseOrders);
            this.Controls.Add(this.grpProductEntry);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpProductEntry.ResumeLayout(false);
            this.grpProductEntry.PerformLayout();
            this.grpPurchaseOrders.ResumeLayout(false);
            this.grpPurchaseOrders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpProductEntry;
        private System.Windows.Forms.GroupBox grpPurchaseOrders;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label SKU;
        private System.Windows.Forms.Label Reorder;
        private System.Windows.Forms.TextBox txtSKU;
        private System.Windows.Forms.TextBox txtReorderLevel;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCategoryId;
        private System.Windows.Forms.Label lblSupplierId;
        private System.Windows.Forms.TextBox txtCategoryId;
        private System.Windows.Forms.TextBox txtSupplierId;
        private System.Windows.Forms.Button btnSaveProduct;
        private System.Windows.Forms.TextBox txtStockOnHand;
        private System.Windows.Forms.TextBox txtUnitCost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPOSupplierId;
        private System.Windows.Forms.TextBox txtPONumber;
        private System.Windows.Forms.Button btnCreatePO;
        private System.Windows.Forms.Button btnReceivePO;
        private System.Windows.Forms.TextBox txtPOLineCost;
        private System.Windows.Forms.TextBox txtPOQty;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPOProductId;
        private System.Windows.Forms.TextBox txtPOId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAddLine;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLowStockReport;
        private ComboBox cmbSuppliers;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private Button btnPOStatusReport;
        private Button btnToggleSupplier;
        private GroupBox groupBox1;
        private TextBox txtCategoryName;
        private Button btnAddCategory;
        private Button btnViewCategories;
        private GroupBox groupBox2;
        private TextBox txtSupplierContact;
        private Button btnAddSupplier;
        private TextBox txtSupplierName;
    }
}