using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static Dlv005.UI.Dlv005View;

namespace Dlv005.UI
{
    public partial class SelectionTable : Form
    {
        /// <summary>
        /// The binding source for grid.
        /// </summary>
        private BindingSource bindingSourceTable = new BindingSource();

        /// <summary>
        /// The direction of sorting.
        /// </summary>
        private int sortingDirection = 1;

        /// <summary>
        /// The DLV005VIEW .
        /// </summary>
        private Dlv005View view;

        /// <summary>
        /// The data set with my filled tables.
        /// </summary>
        private BL.Dlv005DataSet DataSet = new BL.Dlv005DataSet();

        /// <summary>
        /// The delegate from Dlv005View.
        /// </summary>
        private SelectionTablesDelegate selectionTable;

        /// <summary>
        /// The key values that will send data to DLV005.
        /// </summary>
        private List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// The Call Back Delegate that sends data to DLV005VIEW.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="keyValues">The key values.</param>
        public delegate void CallBackSelectionTable(SelectionTablesDelegate list, List<KeyValuePair<string, string>> keyValues);

        /// <summary>
        /// Initializes the values from DLV005
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="keyValues">The key values.</param>
        /// <param name="view">The view.</param>
        public void InitializeValues(SelectionTablesDelegate table, List<KeyValuePair<string, string>> keyValues, Dlv005View view)
        {
            this.view = view;
            this.keyValues = keyValues;
            selectionTable = table;
            InitializeUI();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionTable" /> class.
        /// </summary>
        /// <param name="table">The table.</param>
        public SelectionTable()
        {
            InitializeComponent();
            DataSet.InitializeTables();
        }

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        private void InitializeUI()
        {
            InitializeName();
            InitializeBindingSources();
            InitializeComboBoxItemps();
            InitializeMultiSelect();
            InitializeEvents();
            UpdateDataGridViewElementsCount();
        }

        /// <summary>
        /// Initializes selection table from name.
        /// </summary>
        private void InitializeName()
        {
            switch (selectionTable)
            {
                case SelectionTablesDelegate.BD12:

                    Text = "Selection Series";

                    break;

                case SelectionTablesDelegate.BD09:
                    Text = "Selection Persons";
                    break;

                case SelectionTablesDelegate.BD06:
                    Text = "Selection Departaments";

                    break;
            }
        }

        /// <summary>
        /// Initializes the binding sources.
        /// </summary>
        /// <param name="table">The table.</param>
        private void InitializeBindingSources()
        {
            switch (selectionTable)
            {
                case SelectionTablesDelegate.BD12:

                    bindingSourceTable.DataSource = DataSet.BD12_BAUREIHE;
                    dataGridViewSelectionTable.DataSource = DataSet.BD12_BAUREIHE;

                    break;

                case SelectionTablesDelegate.BD09:
                    bindingSourceTable.DataSource = DataSet.BD09_PERSON;
                    dataGridViewSelectionTable.DataSource = bindingSourceTable;
                    break;

                case SelectionTablesDelegate.BD06:

                    bindingSourceTable.DataSource = DataSet.BD06_ORG_EINHEIT_TBL;
                    dataGridViewSelectionTable.DataSource = DataSet.BD06_ORG_EINHEIT_TBL;
                    break;
            }
        }

        /// <summary>
        /// Updates the data grid view elements count.
        /// </summary>
        private void UpdateDataGridViewElementsCount()
        {
            textBoxCountData.Text = dataGridViewSelectionTable.Rows.Count.ToString();
        }

        /// <summary>
        /// Initializes the ComboBox itemps.
        /// </summary>
        private void InitializeComboBoxItemps()
        {
            foreach (DataGridViewColumn column in dataGridViewSelectionTable.Columns)
            {
                if (column.Visible == true)
                {
                    comboBoxColumn.Items.Add(column.Name);
                }
            }
            foreach (DataGridViewColumn column in dataGridViewSelectionTable.Columns)
            {
                if (column.Visible == true)
                {
                    comboBoxSorting.Items.Add(column.Name);
                }
            }
        }

        /// <summary>
        /// Initializes UI events.
        /// </summary>
        private void InitializeEvents()
        {
            dataGridViewSelectionTable.DoubleClick += SelectFullRow;
            comboBoxColumn.SelectionChangeCommitted += SelectionChangedEvent;
            textBoxFilter.TextChanged += FilterFieldTextChangedEvent;
            comboBoxSorting.GotFocus += SortingComboBoxGotFocusEvent;
        }

        /// <summary>
        /// Doddges the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SelectFullRow(object sender, EventArgs e)
        {
            dataGridViewSelectionTable.Rows[GetCurrentDataGridViewRowIndex()].Selected = true;
        }

        /// <summary>
        /// Initializes the multi select.
        /// </summary>
        /// <param name="table">The table.</param>
        private void InitializeMultiSelect()
        {
            switch (selectionTable)
            {
                case SelectionTablesDelegate.BD12:
                    dataGridViewSelectionTable.MultiSelect = true;
                    break;

                default:
                    dataGridViewSelectionTable.MultiSelect = false;
                    break;
            }
        }

        /// <summary>
        /// <para></para>
        /// <para>Handles the TextChanged event of the TextBoxFilter control.
        /// </para>
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FilterFieldTextChangedEvent(object sender, EventArgs e)
        {
            if (textBoxFilter.Text == string.Empty)
            {
                bindingSourceTable.Filter = null;
                btnUpdate.Enabled = false;
            }
            else
            {
                btnUpdate.Enabled = true;
            }
            UpdateDataGridViewElementsCount();
        }

        /// <summary>
        /// <para></para>
        /// <para>Handles the SelectionChangeCommitted event of the ComboBoxColumn control.
        /// </para>
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SelectionChangedEvent(object sender, EventArgs e)
        {
            textBoxFilter.Enabled = true;
        }

        /// <summary>
        /// Handles the GotFocus event of the ComboBoxSorting control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SortingComboBoxGotFocusEvent(object sender, EventArgs e)
        {
            btnArrow.Enabled = true;
            btnSort.Enabled = true;
        }

        /// <summary>
        /// Gets the index of the current data grid view row.
        /// </summary>
        /// <returns></returns>
        private int GetCurrentDataGridViewRowIndex()
        {
            foreach (DataGridViewRow row in dataGridViewSelectionTable.SelectedRows)
            {
                return row.Index;
            }
            if (dataGridViewSelectionTable.CurrentCell != null)
            {
                return dataGridViewSelectionTable.CurrentCell.RowIndex;
            }
            return 0;
        }

        /// <summary>
        /// Handles the Click event of the BtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void SaveOperation(object sender, System.EventArgs e)
        {
            if (dataGridViewSelectionTable.Rows.Count != 0)
            {
                btnSave.Enabled = true;

                List<DataGridViewRow> rows = new List<DataGridViewRow>();
                switch (selectionTable)
                {
                    case SelectionTablesDelegate.BD12:
                        foreach (DataGridViewRow row in dataGridViewSelectionTable.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.Selected == true)
                                {
                                    row.Selected = true;
                                }
                            }
                        }
                        string passedId = string.Empty;
                        string passedRow = string.Empty;
                        string getSection = string.Empty;
                        foreach (DataGridViewRow row in dataGridViewSelectionTable.SelectedRows)
                        {
                            passedRow += row.Cells[0].Value.ToString() + ",";
                            passedId += row.Cells[1].Value.ToString() + ",";
                        }
                        passedRow = passedRow.Remove(passedRow.Length - 1);
                        passedId = passedId.Remove(passedId.Length - 1);

                        keyValues.Add(new KeyValuePair<string, string>(passedId, passedRow));
                        CallBackSelectionTable callBack12 = new CallBackSelectionTable(view.TakeValues);
                        callBack12(SelectionTablesDelegate.BD12, keyValues);
                        break;

                    case SelectionTablesDelegate.BD06:

                        keyValues.Add(new KeyValuePair<string, string>
                            (DataSet.BD06_ORG_EINHEIT_TBL.Rows[GetCurrentDataGridViewRowIndex()]["Section"].ToString(),
                            DataSet.BD06_ORG_EINHEIT_TBL.Rows[GetCurrentDataGridViewRowIndex()]["Short description"].ToString()
                           ));

                        CallBackSelectionTable callBack06 = new CallBackSelectionTable(view.TakeValues);
                        callBack06(SelectionTablesDelegate.BD06, keyValues);
                        break;

                    case SelectionTablesDelegate.BD09:
                        string sectionText = string.Empty;

                        foreach (DataRow row in DataSet.BD06_ORG_EINHEIT_TBL.Rows)
                        {
                            if (row[1].ToString() == DataSet.BD09_PERSON.Rows[GetCurrentDataGridViewRowIndex()]["Department"].ToString())
                            {
                                sectionText = row[0].ToString();
                                break;
                            }
                        }

                        string str = DataSet.BD09_PERSON.Rows[GetCurrentDataGridViewRowIndex()]["Name"].ToString() + ", "
                            + DataSet.BD09_PERSON.Rows[GetCurrentDataGridViewRowIndex()]["Vorname"].ToString()
                            + ", " + sectionText;

                        keyValues.Add(new KeyValuePair<string, string>
                            (DataSet.BD09_PERSON.Rows[GetCurrentDataGridViewRowIndex()]["Id"].ToString(), str));

                        CallBackSelectionTable callBack09 = new CallBackSelectionTable(view.TakeValues);
                        callBack09(SelectionTablesDelegate.BD09, keyValues);
                        break;
                }
                Close();
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void UpdateOperation(object sender, EventArgs e)
        {
            try
            {
                string filter = comboBoxColumn.Text + " = " + "'" + textBoxFilter.Text + "'";
                bindingSourceTable.Filter = filter;
            }
            catch
            {
                string filter = comboBoxColumn.Text + " = " + "'" + "-1" + "'";
                bindingSourceTable.Filter = filter;
            }
            UpdateDataGridViewElementsCount();
            if (dataGridViewSelectionTable.FirstDisplayedCell != null)
            {
                dataGridViewSelectionTable.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnDownArrow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnDownArrow_Click(object sender, EventArgs e)
        {
            if (sortingDirection == 0)
            {
                btnArrow.BackgroundImage = Properties.Resources.downArrow;
                sortingDirection = 1;
                return;
            }
            if (sortingDirection == 1)
            {
                btnArrow.BackgroundImage = Properties.Resources.upArrow;
                sortingDirection = 0;
                return;
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnSort control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SortOperation(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridViewSelectionTable.Columns)
                if (column.HeaderText == comboBoxSorting.Text)
                    if (sortingDirection == 1)
                        dataGridViewSelectionTable.Sort(column, ListSortDirection.Descending);
                    else
                        dataGridViewSelectionTable.Sort(column, ListSortDirection.Ascending);
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CancelOperation(object sender, EventArgs e)
        {
            Close();
        }
    }
}