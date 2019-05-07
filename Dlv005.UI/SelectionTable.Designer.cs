namespace Dlv005.UI
{
    partial class SelectionTable
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
            this.components = new System.ComponentModel.Container();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.labelSpalte = new System.Windows.Forms.Label();
            this.labelFilter = new System.Windows.Forms.Label();
            this.groupBoxGrid = new System.Windows.Forms.GroupBox();
            this.textBoxCountData = new System.Windows.Forms.TextBox();
            this.labelNumber = new System.Windows.Forms.Label();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnArrow = new System.Windows.Forms.Button();
            this.comboBoxSorting = new System.Windows.Forms.ComboBox();
            this.labelSorting = new System.Windows.Forms.Label();
            this.dataGridViewSelectionTable = new System.Windows.Forms.DataGridView();
            this.bindingSourceColumn = new System.Windows.Forms.BindingSource(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxFilter.SuspendLayout();
            this.groupBoxGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectionTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceColumn)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFilter.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBoxFilter.Controls.Add(this.comboBoxColumn);
            this.groupBoxFilter.Controls.Add(this.btnUpdate);
            this.groupBoxFilter.Controls.Add(this.textBoxFilter);
            this.groupBoxFilter.Controls.Add(this.labelSpalte);
            this.groupBoxFilter.Controls.Add(this.labelFilter);
            this.groupBoxFilter.Location = new System.Drawing.Point(6, 2);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(560, 81);
            this.groupBoxFilter.TabIndex = 0;
            this.groupBoxFilter.TabStop = false;
            // 
            // comboBoxColumn
            // 
            this.comboBoxColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.Location = new System.Drawing.Point(79, 43);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(346, 21);
            this.comboBoxColumn.TabIndex = 5;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(482, 42);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.UpdateOperation);
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilter.Enabled = false;
            this.textBoxFilter.Location = new System.Drawing.Point(79, 15);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(228, 20);
            this.textBoxFilter.TabIndex = 2;
            // 
            // labelSpalte
            // 
            this.labelSpalte.AutoSize = true;
            this.labelSpalte.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpalte.Location = new System.Drawing.Point(13, 44);
            this.labelSpalte.Name = "labelSpalte";
            this.labelSpalte.Size = new System.Drawing.Size(59, 17);
            this.labelSpalte.TabIndex = 1;
            this.labelSpalte.Text = "Column:";
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.BackColor = System.Drawing.SystemColors.Control;
            this.labelFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilter.Location = new System.Drawing.Point(13, 16);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(43, 17);
            this.labelFilter.TabIndex = 0;
            this.labelFilter.Text = "Filter:";
            // 
            // groupBoxGrid
            // 
            this.groupBoxGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGrid.Controls.Add(this.textBoxCountData);
            this.groupBoxGrid.Controls.Add(this.labelNumber);
            this.groupBoxGrid.Controls.Add(this.btnSort);
            this.groupBoxGrid.Controls.Add(this.btnArrow);
            this.groupBoxGrid.Controls.Add(this.comboBoxSorting);
            this.groupBoxGrid.Controls.Add(this.labelSorting);
            this.groupBoxGrid.Controls.Add(this.dataGridViewSelectionTable);
            this.groupBoxGrid.Location = new System.Drawing.Point(6, 89);
            this.groupBoxGrid.Name = "groupBoxGrid";
            this.groupBoxGrid.Size = new System.Drawing.Size(560, 279);
            this.groupBoxGrid.TabIndex = 1;
            this.groupBoxGrid.TabStop = false;
            // 
            // textBoxCountData
            // 
            this.textBoxCountData.AcceptsTab = true;
            this.textBoxCountData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCountData.Enabled = false;
            this.textBoxCountData.Location = new System.Drawing.Point(482, 252);
            this.textBoxCountData.Name = "textBoxCountData";
            this.textBoxCountData.Size = new System.Drawing.Size(69, 20);
            this.textBoxCountData.TabIndex = 6;
            // 
            // labelNumber
            // 
            this.labelNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNumber.AutoSize = true;
            this.labelNumber.Location = new System.Drawing.Point(429, 254);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(47, 13);
            this.labelNumber.TabIndex = 5;
            this.labelNumber.Text = "Number:";
            // 
            // btnSort
            // 
            this.btnSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSort.Enabled = false;
            this.btnSort.Location = new System.Drawing.Point(290, 252);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 4;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.SortOperation);
            // 
            // btnArrow
            // 
            this.btnArrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnArrow.BackgroundImage = global::Dlv005.UI.Properties.Resources.downArrow;
            this.btnArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnArrow.Enabled = false;
            this.btnArrow.Location = new System.Drawing.Point(263, 252);
            this.btnArrow.Name = "btnArrow";
            this.btnArrow.Size = new System.Drawing.Size(21, 21);
            this.btnArrow.TabIndex = 3;
            this.btnArrow.UseVisualStyleBackColor = true;
            this.btnArrow.Click += new System.EventHandler(this.BtnDownArrow_Click);
            // 
            // comboBoxSorting
            // 
            this.comboBoxSorting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxSorting.FormattingEnabled = true;
            this.comboBoxSorting.Location = new System.Drawing.Point(66, 252);
            this.comboBoxSorting.Name = "comboBoxSorting";
            this.comboBoxSorting.Size = new System.Drawing.Size(188, 21);
            this.comboBoxSorting.TabIndex = 2;
            // 
            // labelSorting
            // 
            this.labelSorting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSorting.AutoSize = true;
            this.labelSorting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSorting.Location = new System.Drawing.Point(6, 252);
            this.labelSorting.Name = "labelSorting";
            this.labelSorting.Size = new System.Drawing.Size(57, 17);
            this.labelSorting.TabIndex = 1;
            this.labelSorting.Text = "Sorting:";
            // 
            // dataGridViewSelectionTable
            // 
            this.dataGridViewSelectionTable.AllowUserToAddRows = false;
            this.dataGridViewSelectionTable.AllowUserToDeleteRows = false;
            this.dataGridViewSelectionTable.AllowUserToResizeColumns = false;
            this.dataGridViewSelectionTable.AllowUserToResizeRows = false;
            this.dataGridViewSelectionTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSelectionTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSelectionTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelectionTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewSelectionTable.Location = new System.Drawing.Point(0, 10);
            this.dataGridViewSelectionTable.Name = "dataGridViewSelectionTable";
            this.dataGridViewSelectionTable.Size = new System.Drawing.Size(560, 230);
            this.dataGridViewSelectionTable.TabIndex = 0;
            // 
            // bindingSourceColumn
            // 
            this.bindingSourceColumn.DataSource = typeof(Dlv005.BL.Dlv005DataSet.BD12_BAUREIHEDataTable);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(410, 374);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.SaveOperation);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(491, 374);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.CancelOperation);
            // 
            // SelectionTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(572, 406);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxGrid);
            this.Controls.Add(this.groupBoxFilter);
            this.Name = "SelectionTable";
            this.Text = "Location Selection";
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            this.groupBoxGrid.ResumeLayout(false);
            this.groupBoxGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectionTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceColumn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.GroupBox groupBoxGrid;
        private System.Windows.Forms.Label labelSpalte;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.DataGridView dataGridViewSelectionTable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelSorting;
        private System.Windows.Forms.ComboBox comboBoxSorting;
        private System.Windows.Forms.Button btnArrow;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.TextBox textBoxCountData;
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.ComboBox comboBoxColumn;
        private System.Windows.Forms.BindingSource bindingSourceColumn;
    }
}