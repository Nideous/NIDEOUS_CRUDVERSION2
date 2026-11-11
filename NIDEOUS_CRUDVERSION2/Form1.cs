using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NIDEOUS_CRUDVERSION2
{
    public partial class Form1 : Form
    {
        private string connStr = @"Server=.\SQLEXPRESS;Database=Capistrano;Trusted_Connection=True;";

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            btnAddSupplier.Click += btnAddSupplier_Click;
            btnViewCategories.Click += btnViewCategories_Click_1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] testServers = {
                @".\SQLEXPRESS",
                @"(localdb)\MSSQLLocalDB",
                "localhost",
                "."
            };

            bool connected = false;
            foreach (string server in testServers)
            {
                string testConnStr = $"Server={server};Database=Capistrano;Trusted_Connection=True;";
                using (SqlConnection conn = new SqlConnection(testConnStr))
                {
                    try
                    {
                        conn.Open();
                        connStr = testConnStr;
                        connected = true;
                        break;
                    }
                    catch { }
                }
            }

            if (!connected)
            {
                MessageBox.Show("Could not connect to database 'Capistrano'. Ensure SQL Server is running and database exists.", "Connection Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            LoadSuppliersIntoComboBox();
        }

        private DataTable ExecuteProcedure(string procName, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void CreateProduct(string sku, string name, int categoryId, int supplierId, decimal cost, int reorderLevel, int stock)
        {
            SqlParameter[] p = {
                new SqlParameter("@Action", "CREATE"),
                new SqlParameter("@SKU", sku),
                new SqlParameter("@ProductName", name),
                new SqlParameter("@CategoryId", categoryId),
                new SqlParameter("@SupplierId", supplierId),
                new SqlParameter("@UnitCost", cost),
                new SqlParameter("@ReorderLevel", reorderLevel),
                new SqlParameter("@StockOnHand", stock)
            };
            ExecuteProcedure("usp_Product_CRUD", p);
        }
        public DataTable SearchProducts(string keyword)
        {
            SqlParameter[] p = {
                new SqlParameter("@Action", "SEARCH"),
                new SqlParameter("@SearchKeyword", string.IsNullOrEmpty(keyword) ? (object)DBNull.Value : keyword)
            };
            return ExecuteProcedure("usp_Product_CRUD", p);
        }

        public DataTable GetPurchaseOrderLines(int poId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT l.*, ISNULL(p.ProductName, 'Unknown Product') AS ProductName " +
                               "FROM PurchaseOrderLines l LEFT JOIN Products p ON l.ProductId = p.ProductId " +
                               "WHERE l.PurchaseOrderId = @POId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@POId", poId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetAllPurchaseOrders()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT po.PurchaseOrderId, po.PONumber, po.SupplierId, s.SupplierName, po.OrderDate, po.Status " +
                               "FROM PurchaseOrders po INNER JOIN Suppliers s ON po.SupplierId = s.SupplierId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetPurchaseOrderStatusReport()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT po.PurchaseOrderId, po.PONumber, s.SupplierName, po.OrderDate, po.POStatus, po.TotalAmount " +
                               "FROM PurchaseOrders po INNER JOIN Suppliers s ON po.SupplierId = s.SupplierId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetAllSuppliers()
        {
            SqlParameter[] p = { new SqlParameter("@Action", "GETALL") };
            return ExecuteProcedure("usp_Supplier_CRUD", p);
        }
        public int CreateDraftPO(string poNumber, int supplierId)
        {
            SqlParameter[] p = {
                new SqlParameter("@PONumber", poNumber),
                new SqlParameter("@SupplierId", supplierId)
            };
            DataTable dt = ExecuteProcedure("usp_PurchaseOrder_CreateDraft", p);
            return Convert.ToInt32(dt.Rows[0]["PurchaseOrderId"]);
        }

        private void LoadSuppliersIntoComboBox()
        {
            try
            {
                DataTable dt = GetAllSuppliers();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No suppliers found in the database. Please add a supplier first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!dt.Columns.Contains("DisplayText"))
                {
                    dt.Columns.Add("DisplayText", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        row["DisplayText"] = $"{row["SupplierName"]} ({row["SupplierStatus"]})";
                    }
                }

                cmbSuppliers.DataSource = dt;
                cmbSuppliers.DisplayMember = "DisplayText";
                cmbSuppliers.ValueMember = "SupplierId";
                cmbSuppliers.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AddPOLine(int poId, int productId, int qty, decimal unitCost)
        {
            try
            {
                SqlParameter[] p = {
                    new SqlParameter("@PurchaseOrderId", poId),
                    new SqlParameter("@ProductId", productId),
                    new SqlParameter("@Quantity", qty),
                    new SqlParameter("@UnitCost", unitCost)
                };
                ExecuteProcedure("usp_PurchaseOrder_AddLine", p);
                MessageBox.Show("Line item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReceivePO(int poId)
        {
            try
            {
                SqlParameter[] p = {
                    new SqlParameter("@PurchaseOrderId", poId)
                };
                ExecuteProcedure("usp_PurchaseOrder_Receive", p);
                MessageBox.Show("Purchase order received successfully! Stock updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable GetLowStockReport()
        {
            return ExecuteProcedure("usp_Inventory_LowStockReport");
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSKU.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please enter SKU and Product Name.", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCategoryId.Text, out int categoryId) ||
                !int.TryParse(txtSupplierId.Text, out int supplierId) ||
                !decimal.TryParse(txtUnitCost.Text, out decimal unitCost) ||
                !int.TryParse(txtReorderLevel.Text, out int reorderLevel) ||
                !int.TryParse(txtStockOnHand.Text, out int stock))
            {
                MessageBox.Show("Category ID, Supplier ID, Unit Cost, Reorder Level, and Stock must be valid numbers.", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CreateProduct(txtSKU.Text, txtProductName.Text, categoryId, supplierId, unitCost, reorderLevel, stock);
                MessageBox.Show("Product created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvResults.DataSource = SearchProducts("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvResults.DataSource = SearchProducts(txtSearch.Text);
        }

        public void CreateCategory(string categoryName)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetAllCategories()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT CategoryId, CategoryName FROM Categories";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Please enter a category name.", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CreateCategory(txtCategoryName.Text.Trim());
                MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCategoryName.Clear();
                dgvResults.DataSource = GetAllCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding category: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToggleSupplierStatus(int supplierId)
        {
            try
            {
                DataTable suppliers = GetAllSuppliers();
                DataRow[] rows = suppliers.Select($"SupplierId = {supplierId}");

                if (rows.Length == 0)
                {
                    MessageBox.Show("Supplier ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string currentStatus = rows[0]["SupplierStatus"].ToString();
                string newStatus = (currentStatus == "Active") ? "Inactive" : "Active";

                SqlParameter[] p = {
                    new SqlParameter("@Action", "UPDATE"),
                    new SqlParameter("@SupplierId", supplierId),
                    new SqlParameter("@SupplierStatus", newStatus)
                };

                ExecuteProcedure("usp_Supplier_CRUD", p);
                MessageBox.Show($"Supplier #{supplierId} status changed from '{currentStatus}' to '{newStatus}'!", "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSuppliersIntoComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLowStockReport_Click(object sender, EventArgs e)
        {
            dgvResults.DataSource = GetLowStockReport();
        }

        private void btnCreatePO_Click(object sender, EventArgs e)
        {
            if (cmbSuppliers.SelectedItem == null || string.IsNullOrWhiteSpace(txtPONumber.Text))
            {
                MessageBox.Show("Please select a supplier from the dropdown and enter a PO Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView selectedRow = (DataRowView)cmbSuppliers.SelectedItem;
            int supplierId = Convert.ToInt32(selectedRow["SupplierId"]);

            try
            {
                string status = selectedRow["SupplierStatus"].ToString();
                if (status != "Active")
                {
                    MessageBox.Show("Selected supplier is currently Inactive. Cannot issue Purchase Orders.", "Inactive Supplier", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                int newPOId = CreateDraftPO(txtPONumber.Text, supplierId);
                txtPOId.Text = newPOId.ToString();
                MessageBox.Show($"Draft Purchase Order #{txtPONumber.Text} created successfully! (PO ID: {newPOId})", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvResults.DataSource = GetAllPurchaseOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating Purchase Order: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddLine_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPOId.Text, out int poId) ||
                !int.TryParse(txtPOProductId.Text, out int productId) ||
                !int.TryParse(txtPOQty.Text, out int qty) ||
                !decimal.TryParse(txtPOLineCost.Text, out decimal unitCost))
            {
                MessageBox.Show("PO ID, Product ID, Quantity, and Unit Cost must be valid numbers.", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddPOLine(poId, productId, qty, unitCost);
            dgvResults.DataSource = GetPurchaseOrderLines(poId);
        }

        private void btnReceivePO_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPOId.Text, out int poId))
            {
                MessageBox.Show("Please enter a valid numeric PO ID.", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReceivePO(poId);
            dgvResults.DataSource = GetLowStockReport();
        }

        public void CreateSupplier(string supplierName)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("usp_Supplier_CRUD", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetAllSuppliersTable()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Supplier_CRUD", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "SELECT");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Please enter a supplier name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CreateSupplier(txtSupplierName.Text.Trim());
                MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSupplierName.Clear();

                dgvResults.DataSource = null;
                dgvResults.DataSource = GetAllSuppliers();
                dgvResults.Refresh();

                LoadSuppliersIntoComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding supplier: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewSuppliers_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = GetAllSuppliers();

                dgvResults.DataSource = null; 
                dgvResults.DataSource = dt;  
                dgvResults.Update();
                dgvResults.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error viewing suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewSuppliers_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = GetAllSuppliers();

                MessageBox.Show($"Rows found: {dt.Rows.Count}", "Debug Check", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dgvResults.DataSource = null; 
                dgvResults.DataSource = dt;
                dgvResults.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error viewing suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnViewCategories_Click_1(object sender, EventArgs e)
        {
            dgvResults.DataSource = GetAllCategories();
        }

        private void btnPOStatusReport_Click_1(object sender, EventArgs e)
        {
            dgvResults.DataSource = GetPurchaseOrderStatusReport();
        }

        private void btnToggleSupplier_Click_1(object sender, EventArgs e)
        {
            if (cmbSuppliers.SelectedItem == null)
            {
                MessageBox.Show("Please select a supplier from the dropdown first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView selectedRow = (DataRowView)cmbSuppliers.SelectedItem;
            int supplierId = Convert.ToInt32(selectedRow["SupplierId"]);

            ToggleSupplierStatus(supplierId);
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void tabPage3_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void grpPurchaseOrders_Enter(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label2_Click_1(object sender, EventArgs e) { }
        private void label4_Click_1(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void txtPOSupplierId_TextChanged(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void lblCategoryId_Click(object sender, EventArgs e) { }
        private void cmbSuppliers_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button1_Click_1(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void txtSupplierContact_TextChanged(object sender, EventArgs e) { }
    }

}