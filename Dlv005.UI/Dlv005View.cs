using Dlv005.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Dlv005.UI
{
    public partial class Dlv005View : Form
    {
        /// <summary>
        /// Static messages for validations.
        /// </summary>
        private readonly string incorectFormat = "The field has incorrect format! Please correct your entry.";

        /// <summary>
        /// The allocation100
        /// </summary>
        private readonly string allocation100 = "The total of the account assignment shares must be 100%";

        /// <summary>
        /// The date in the past
        /// </summary>
        private readonly string dateInThePast = "The date must be in the future!";

        /// <summary>
        /// To lower then from date
        /// </summary>
        private readonly string toLowerThenFromDate = "Bis date must be greater or equal with von date!";

        /// <summary>
        /// The value not contained in selection table
        /// </summary>
        private readonly string valueNotContainedInSelectionTable = "The value is not contained into the selection table. Please select a valid value.";

        /// <summary>
        /// The empty mandatory
        /// </summary>
        private readonly string emptyMandatory = "The mandatory field does not contain any data. Please enter a value.";

        /// <summary>
        /// Static filters for overview hide checkboxes.
        /// </summary>
        private readonly string confirmedCondition = "StatusText <> 'Confirmed'";

        /// <summary>
        /// The requested condition
        /// </summary>
        private string requestedCondition = "StatusText <> 'Requested'";

        /// <summary>
        /// The confirmed and requested condition
        /// </summary>
        private string confirmedAndRequestedCondition = "StatusText <> 'Confirmed' and StatusText <> 'Requested'";

        /// <summary>
        /// Static initialize checkboxes in basicdata tab.
        /// </summary>
        private string saturday = "j";

        /// <summary>
        /// The sunday
        /// </summary>
        private string sunday = "j";

        /// <summary>
        /// The allocation deleted i ds
        /// </summary>
        private string allocationDeletedIDs = string.Empty;

        /// <summary>
        /// The new copy current row
        /// </summary>
        private DataRow newCopyCurrentRow;

        /// <summary>
        /// Booleans that marks events around the application.
        /// </summary>
        private bool succesfullySave = false;

        /// <summary>
        /// The new button was clicked
        /// </summary>
        private bool newButtonWasClicked = false;

        /// <summary>
        /// The new copy button was clicked
        /// </summary>
        private bool newCopyButtonWasClicked = false;

        /// <summary>
        /// The has the application started
        /// </summary>
        private bool hasTheApplicationStarted = false;

        /// <summary>
        /// The can check for edit mode
        /// </summary>
        private bool canCheckForEditMode = false;

        /// <summary>
        /// The insert allocation first row
        /// </summary>
        private bool insertAllocationFirstRow = true;

        /// <summary>
        /// The TestingNr and Current Index keeper.
        /// </summary>
        private int testingNr = 0;

        /// <summary>
        /// The row index
        /// </summary>
        private int rowIndex;

        /// <summary>
        /// The delegate engine.
        /// </summary>
        private string delegateTable;

        /// <summary>
        ///
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="keyValues">The key values.</param>
        /// <param name="view">The view.</param>
        public delegate void CallSelectionTable(SelectionTablesDelegate list, List<KeyValuePair<string, string>> keyValues, Dlv005View view);

        /// <summary>
        /// The key values
        /// </summary>
        private List<KeyValuePair<string, string>> keyValues;

        /// <summary>
        ///
        /// </summary>
        public enum SelectionTablesDelegate
        { BD06, BD09, BD12 };

        /// <summary>
        /// Business Operations/Validations and DataSet initializations.
        /// </summary>
        private Dlv005BusinessOperations BusinessOperations = new Dlv005BusinessOperations();

        /// <summary>
        /// The business validations
        /// </summary>
        private Dlv005Validations BusinessValidations = new Dlv005Validations();

        /// <summary>
        /// The data set
        /// </summary>
        private Dlv005DataSet DataSet = new Dlv005DataSet();

        /// <summary>
        /// Initializes a new instance of the <see cref="Dlv005View"/> class.
        /// </summary>
        public Dlv005View()
        {
            BusinessOperations.GetConnection().Open();
            InitializeComponent();
            DataSet.InitializeTables();
            InitializeUI();
            InitializeDefaultGridSelectedRow();
            Activated += Dlv005View_Activated;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////   I N I T I A L I Z A T I O N S    A N D     U N D O  I N I T I A L I Z A T I O N S //////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        private void InitializeUI()
        {
            InitializeUIBindings();
            InitializeUIElements();
            InitializeUIMandatoryFields();
            InitializeOrUpdateDataGridViewOverViewElementsCount();
            InitializeUIStartingEvents();
        }

        /// <summary>
        /// Initializes the UI bindings.
        /// </summary>
        private void InitializeUIBindings()
        {
            bindingSourceBasicData.DataSource = DataSet.BasicDataTable;
            bindingSourceAllocation.DataSource = DataSet.DL32_EXT_KOMM_KONTO;
            bindingSourceDL38Table.DataSource = DataSet.DL38_KOMM_ERPROBUNGSORT_TBL;
            bindingSourceDL39Table.DataSource = DataSet.DL39_KOMM_STRECKENART_TBL;
            bindingSourceDL40Table.DataSource = DataSet.DL40_KOMM_ERPROBUNGSART_TBL;
            bindingSourceSD111TableLicence.DataSource = DataSet.SD111_QUALIFIKATIONENLicence;
            bindingSourceSD111TableLicence.Filter = "SD111_TYP = 'FAHRBERECHTIGUNG3'";
            bindingSourceSD111TableHV.DataSource = DataSet.SD111_QUALIFIKATIONENHV;
            bindingSourceSD111TableHV.Filter = "SD111_TYP = 'HVQUALIFIKATION'";
            bindingSourceSD111TableSpecial.DataSource = DataSet.SD111_QUALIFIKATIONENSpecial;
            bindingSourceSD111TableSpecial.Filter = "SD111_TYP = 'SONDQUALIFIKATION'";
        }

        /// <summary>
        /// Initializes the UI elements.
        /// </summary>
        private void InitializeUIElements()
        {
            tabPageOverview.Enter += EnterInOverViewPageEvent;
            tabPageBasicData.Enter += PreventEnterBasicDataIfNoDataOnGrid;
            if (CheckIfNullOrNoDisplay())
            {
                GetCurrentRow()["Saturday"] = 'j';
                GetCurrentRow()["Sunday"] = 'j';
            }
            IsAutoValidate(true);
            testingNr = BusinessOperations.SetCount(DataSet);
            btnSave.Visible = false;
            btnNewCopy.Enabled = true;
        }

        /// <summary>
        /// Initializes the UI mandatory fields.
        /// </summary>
        private void InitializeUIMandatoryFields()
        {
            labelTestingContent.Font = new Font(labelTestingContent.Font, FontStyle.Bold);
            labelAllocation.Font = labelTestingContent.Font;
            labelSeries.Font = labelTestingContent.Font;
            labelSort.Font = labelTestingContent.Font;
            labelHV.Font = labelTestingContent.Font;
            labelSeries.Font = labelTestingContent.Font;
            labelSpecial.Font = labelTestingContent.Font;
            labelCustomer.Font = labelTestingContent.Font;
            labelCustomerOE.Font = labelTestingContent.Font;
            labelRoutes.Font = labelTestingContent.Font;
            labelChief.Font = labelTestingContent.Font;
            labelTesting.Font = labelTestingContent.Font;
            labelEngineering.Font = labelTestingContent.Font;
            labelDriving.Font = labelTestingContent.Font;
            labelFrom.Font = labelTestingContent.Font;
            labelTo.Font = labelTestingContent.Font;
        }

        /// <summary>
        /// Initializes the or update data grid view over view elements count.
        /// </summary>
        private void InitializeOrUpdateDataGridViewOverViewElementsCount()
        {
            tbxNumber.Text = bindingSourceDataGridViewOverview.Rows.Count.ToString();
        }

        /// <summary>
        /// Initializes the UI starting events.
        /// </summary>
        private void InitializeUIStartingEvents()
        {
            InitializeValidationsEvents();
            InitializeDataGridViewsEvents();
            InitializeCheckBoxesEvents();
            InitializeButtonsEvents();
            InitializeChecksForEditMode();
        }

        /// <summary>
        /// Initializes the validations events.
        /// </summary>
        private void InitializeValidationsEvents()
        {
            TextBoxSeries.Validating += ValidatingTextBoxSeries;
            TextBoxCustomerOE.Validating += ValidatingTextBoxCustomerOE;
            TextBoxCustomer.Validating += ValidatingTextBoxCustomer;
            TextBoxChief.Validating += ValidatingTextBoxChief;
            TextBoxEngineeringAST.Validating += ValidatingTextBoxEngineering;
            TextBoxTestingContent.Validating += ValidatingTextBoxTestingContent;
            bindingSourceComboBoxSort.Validating += ValidatingSortField;
            bindingSourceComboBoxRoutes.Validating += ValidatingRoutesField;
            bindingSourceComboBoxKindOfTesting.Validating += ValidatingTestField;
            bindingSourceComboBoxDrivingAutorization.Validating += ValidatingDrivingLicenceField;
            bindingSourceComboBoxHVQualification.Validating += ValidatingHvQualificationField;
            bindingSourceComboBoxSpecialQualification.Validating += ValidatingSpecialQualificationField;
            bindingSourceDateTimePickerTo.Validating += ValidatingDateTimePickerTo;
            bindingSourceDataGridViewAllocation.Validating += ValidatingDataGridViewAllocation;
        }

        /// <summary>
        /// Initializes the checks for edit mode.
        /// </summary>
        private void InitializeChecksForEditMode()
        {
            TextBoxSeries.Click += SeriesClick;
            TextBoxCustomerOE.Click += CustomerOEClick;
            TextBoxCustomer.Click += CustomerClick;
            TextBoxChief.Click += ChiefClick;
            TextBoxEngineeringAST.Click += EngineeringClick;
            TextBoxTestingContent.Click += TestingContentClick;
            bindingSourceComboBoxSort.Click += SortClick;
            bindingSourceComboBoxRoutes.Click += RouteClick;
            bindingSourceComboBoxKindOfTesting.Click += KindClick;
            bindingSourceComboBoxDrivingAutorization.Click += DrivingClick;
            bindingSourceComboBoxHVQualification.Click += HvClick;
            bindingSourceComboBoxSpecialQualification.Click += SpecialClick;
            bindingSourceDateTimePickerFrom.GotFocus += FromClick;
            bindingSourceDateTimePickerTo.GotFocus += ToClick;
            chbWorkSaturday.Click += TextBoxTextChangedEvent;
            chbWorkSunday.Click += TextBoxTextChangedEvent;
            bindingSourceDataGridViewAllocation.GotFocus += AllocationClick;
        }

        /// <summary>
        /// Initializes the default grid selected row.
        /// </summary>
        private void InitializeDefaultGridSelectedRow()
        {
            if (bindingSourceDataGridViewOverview.Rows.Count != 0 && bindingSourceDataGridViewOverview.FirstDisplayedCell != null)
            {
                bindingSourceDataGridViewOverview.CurrentCell = bindingSourceDataGridViewOverview.Rows[0].Cells[1];
                bindingSourceDataGridViewOverview.Rows[GetCurrentDataGridViewRowIndex()].Selected = true;
            }
        }

        /// <summary>
        /// Initializes the data grid views events.
        /// </summary>
        private void InitializeDataGridViewsEvents()
        {
            bindingSourceDataGridViewOverview.RowHeaderMouseDoubleClick += DoubleClickOverview;
            bindingSourceDataGridViewOverview.SelectionChanged += SelectingRowOnOverViewEvent;
            bindingSourceDataGridViewOverview.CellDoubleClick += SelectFullRowWhenCellDoubleClickEvent;
            bindingSourceDataGridViewOverview.CellMouseClick += BindingSourceDataGridViewOverview_CellMouseClick;
            bindingSourceDataGridViewAllocation.UserDeletingRow += GetIdOfAllocationDeletedRow;
            bindingSourceDataGridViewAllocation.KeyDown += DownArrowKeyPressed;
            bindingSourceDataGridViewAllocation.UserDeletedRow += TextBoxTextChangedEvent;
            bindingSourceDataGridViewAllocation.RowsAdded += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Initializes the check boxes events.
        /// </summary>
        private void InitializeCheckBoxesEvents()
        {
            checkBoxHideConfirmed.CheckedChanged += CheckedChangedCheckBoxConfirmed;
            checkBoxHideRequested.CheckedChanged += CheeckedChangedCheckBoxRequested;
        }

        /// <summary>
        /// Initializes the buttons events.
        /// </summary>
        private void InitializeButtonsEvents()
        {
            btnSelectionTableEngineering.Click += OpenAndSetEngineeringSelectionTable;
            btnSelectionTableChief.Click += OpenAndSetChiefSelectionTable;
            btnSelectionTableCustomer.Click += OpenAndSetCustomerSelectionTable;
            btnSelectionTableCustomerOE.Click += OpenAndSetCustomerOESelectionTable;
            btnSelectionTableSeries.Click += OpenAndSetSeriesSelectionTable;
            btnConfirm.Click += ConfirmOperation;
            btnRequest.Click += RequestOperation;
            btnDelete.Click += DeleteOperaion;
            btnClose.Click += CloseOperation;
            btnSave.Click += SaveOperaion;
            btnNew.Click += CreateNewOperation;
            btnNewCopy.Click += CreateNewCopyOperation;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////  S E L E C T I O N    T A B L E S   U P D A T E  //////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Takes the values from selection table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="keyValues">The key values.</param>
        public void TakeValues(SelectionTablesDelegate table, List<KeyValuePair<string, string>> keyValues)
        {
            switch (table)
            {
                case SelectionTablesDelegate.BD12:
                    GetCurrentRow()["SeriesNumber"] = keyValues[0].Value;
                    GetCurrentRow()["SeriesText"] = keyValues[0].Key;
                    TextBoxSeries.Text = keyValues[0].Value;
                    DataSet.BasicDataTable.AcceptChanges();
                    break;

                case SelectionTablesDelegate.BD09:
                    switch (delegateTable)
                    {
                        case "Customer":
                            TextBoxCustomer.Text = keyValues[0].Value;
                            TextBoxCustomer.Tag = keyValues[0].Key;
                            GetCurrentRow()["CustomerID"] = keyValues[0].Key;
                            GetCurrentRow()["Customer"] = keyValues[0].Value;
                            DataSet.BasicDataTable.AcceptChanges();

                            break;

                        case "Chief":
                            TextBoxChief.Text = keyValues[0].Value;
                            TextBoxChief.Tag = keyValues[0].Key;
                            GetCurrentRow()["ChiefID"] = keyValues[0].Key;
                            GetCurrentRow()["Chief"] = keyValues[0].Value;
                            DataSet.BasicDataTable.AcceptChanges();

                            break;

                        case "Engineering":
                            TextBoxEngineeringAST.Text = keyValues[0].Value;
                            TextBoxEngineeringAST.Tag = keyValues[0].Key;
                            GetCurrentRow()["EngineeringID"] = keyValues[0].Key;
                            GetCurrentRow()["Engineering"] = keyValues[0].Value;
                            DataSet.BasicDataTable.AcceptChanges();

                            break;

                        default:
                            break;
                    }
                    break;

                case SelectionTablesDelegate.BD06:
                    TextBoxCustomerOE.Text = keyValues[0].Value;
                    TextBoxCustomerOE.Tag = keyValues[0].Key;
                    GetCurrentRow()["CustomerOEID"] = keyValues[0].Key;
                    GetCurrentRow()["CustomerOE"] = keyValues[0].Value;
                    DataSet.BasicDataTable.AcceptChanges();
                    break;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////  V A L I D A T I O N S  ////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Does the validation.
        /// </summary>
        /// <param name="valid">if set to <c>true</c> [valid].</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        /// <param name="error">The error.</param>
        /// <param name="control">The control.</param>
        private void DoValidation(bool valid, System.ComponentModel.CancelEventArgs e, ErrorProvider error, Control control)
        {
            string errorMessage = SetErrorMessage(error);
            if (valid == false)
            {
                e.Cancel = true;
                error.SetError(control, errorMessage);
            }
            else
            {
                e.Cancel = false;
                error.SetError(control, null);
            }
        }

        /// <summary>
        /// Validatings the data grid view allocation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingDataGridViewAllocation(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BusinessValidations.ValidateAllocationTable(bindingSourceDataGridViewAllocation, e, errorProviderAllocation,
                 incorectFormat, allocation100, emptyMandatory);
        }

        /// <summary>
        /// Validatings the text box series.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingTextBoxSeries(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BusinessValidations.ValidateSeries(TextBoxSeries.Text, DataSet.BD12_BAUREIHE, e, errorProviderSeries, TextBoxSeries, emptyMandatory, valueNotContainedInSelectionTable);
        }

        /// <summary>
        /// Validatings the special qualification field.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingSpecialQualificationField(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(bindingSourceComboBoxSpecialQualification.SelectedValue != null &&
               BusinessValidations.ValidateSpecial(bindingSourceComboBoxSpecialQualification.Text, DataSet.SD111_QUALIFIKATIONENSpecial), e, errorProviderSpecial, bindingSourceComboBoxSpecialQualification);
        }

        /// <summary>
        /// Validatings the hv qualification field.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingHvQualificationField(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(bindingSourceComboBoxHVQualification.SelectedValue != null
               && BusinessValidations.ValidateHV(bindingSourceComboBoxHVQualification.Text, DataSet.SD111_QUALIFIKATIONENHV), e, errorProviderHV, bindingSourceComboBoxHVQualification);
        }

        /// <summary>
        /// Validatings the driving licence field.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingDrivingLicenceField(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(bindingSourceComboBoxDrivingAutorization.SelectedValue != null
                && BusinessValidations.ValidateLicence(bindingSourceComboBoxDrivingAutorization.Text, DataSet.SD111_QUALIFIKATIONENLicence), e, errorProviderDriving, bindingSourceComboBoxDrivingAutorization);
        }

        /// <summary>
        /// Validatings the test field.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingTestField(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(bindingSourceComboBoxKindOfTesting.SelectedValue != null
                && BusinessValidations.ValidateKindOfTesting(bindingSourceComboBoxKindOfTesting.Text, DataSet.DL40_KOMM_ERPROBUNGSART_TBL), e, errorProviderKindOfTesting, bindingSourceComboBoxKindOfTesting);
        }

        /// <summary>
        /// Validatings the routes field.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingRoutesField(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(bindingSourceComboBoxRoutes.SelectedValue != null &&
                BusinessValidations.ValidateRoutes(bindingSourceComboBoxRoutes.Text, DataSet.DL39_KOMM_STRECKENART_TBL), e, errorProviderRoutes, bindingSourceComboBoxRoutes);
        }

        /// <summary>
        /// Validatings the sort field.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingSortField(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(bindingSourceComboBoxSort.SelectedValue != null &&
                BusinessValidations.ValidateSort(bindingSourceComboBoxSort.Text, DataSet.DL38_KOMM_ERPROBUNGSORT_TBL), e, errorProviderSort, bindingSourceComboBoxSort);
        }

        /// <summary>
        /// Validatings the text box engineering.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingTextBoxEngineering(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(TextBoxEngineeringAST.Text != string.Empty &&
                BusinessValidations.ValidateCustomerAndChiefAndEngineering(TextBoxEngineeringAST.Text, DataSet.BD09_PERSON, DataSet.BD06_ORG_EINHEIT_TBL), e, errorProviderEngineering, TextBoxEngineeringAST);
        }

        /// <summary>
        /// Validatings the text box chief.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingTextBoxChief(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(TextBoxChief.Text != string.Empty &&
                BusinessValidations.ValidateCustomerAndChiefAndEngineering(TextBoxChief.Text, DataSet.BD09_PERSON, DataSet.BD06_ORG_EINHEIT_TBL), e, errorProviderChief, TextBoxChief);
        }

        /// <summary>
        /// Validatings the text box customer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingTextBoxCustomer(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(TextBoxCustomer.Text != string.Empty &&
                BusinessValidations.ValidateCustomerAndChiefAndEngineering(TextBoxCustomer.Text, DataSet.BD09_PERSON, DataSet.BD06_ORG_EINHEIT_TBL), e, errorProviderCustomer, TextBoxCustomer);
        }

        /// <summary>
        /// Validatings the text box customer oe.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingTextBoxCustomerOE(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(TextBoxCustomerOE.Text != string.Empty &&
                BusinessValidations.ValidateCustomerOE(TextBoxCustomerOE.Text, DataSet.BD06_ORG_EINHEIT_TBL), e, errorProviderCustomerOE, TextBoxCustomerOE);
        }

        /// <summary>
        /// Validatings the date time picker to.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingDateTimePickerTo(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(bindingSourceDateTimePickerTo.Value != DateTime.Now
               && bindingSourceDateTimePickerTo.Value > bindingSourceDateTimePickerFrom.Value, e, errorProviderTo, bindingSourceDateTimePickerTo);
        }

        /// <summary>
        /// Validatings the content of the text box testing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ValidatingTextBoxTestingContent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(TextBoxTestingContent.Text != string.Empty, e, errorProviderTestingContent, TextBoxTestingContent);
        }

        /// <summary>
        /// Handles the Validating event of the BindingSourceDateTimePickerFrom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void BindingSourceDateTimePickerFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidation(bindingSourceDateTimePickerFrom.Value > DateTime.Now.AddDays(7), e, errorProviderFrom, bindingSourceDateTimePickerFrom);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////  B U T T O N S   E V E N T S ////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Executes the selection table operation.
        /// </summary>
        /// <param name="del">The delete.</param>
        private void ExecuteSelectionTableOperation(SelectionTablesDelegate del)
        {
            SelectionTable seriesTable = new SelectionTable();
            keyValues = new List<KeyValuePair<string, string>>();
            CallSelectionTable callTable = new CallSelectionTable(seriesTable.InitializeValues);
            switch (del)
            {
                case SelectionTablesDelegate.BD06:
                    callTable(SelectionTablesDelegate.BD06, keyValues, this);
                    break;

                case SelectionTablesDelegate.BD09:
                    callTable(SelectionTablesDelegate.BD09, keyValues, this);
                    break;

                case SelectionTablesDelegate.BD12:
                    callTable(SelectionTablesDelegate.BD12, keyValues, this);
                    break;
            }
            seriesTable.Show();
        }

        /// <summary>
        /// Opens the and set engineering selection table.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenAndSetEngineeringSelectionTable(object sender, EventArgs e)
        {
            delegateTable = "Engineering";
            ExecuteSelectionTableOperation(SelectionTablesDelegate.BD09);
        }

        /// <summary>
        /// Opens the and set chief selection table.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenAndSetChiefSelectionTable(object sender, EventArgs e)
        {
            delegateTable = "Chief";
            ExecuteSelectionTableOperation(SelectionTablesDelegate.BD09);
        }

        /// <summary>
        /// Opens the and set customer selection table.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenAndSetCustomerSelectionTable(object sender, EventArgs e)
        {
            delegateTable = "Customer";
            ExecuteSelectionTableOperation(SelectionTablesDelegate.BD09);
        }

        /// <summary>
        /// Opens the and set series selection table.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenAndSetSeriesSelectionTable(object sender, EventArgs e)
        {
            ExecuteSelectionTableOperation(SelectionTablesDelegate.BD12);
        }

        /// <summary>
        /// Opens the and set customer oe selection table.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenAndSetCustomerOESelectionTable(object sender, EventArgs e)
        {
            ExecuteSelectionTableOperation(SelectionTablesDelegate.BD06);
        }

        /// <summary>
        /// Requests the operation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RequestOperation(object sender, EventArgs e)
        {
            UpdateStatus("Requested");
        }

        /// <summary>
        /// Confirms the operation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ConfirmOperation(object sender, EventArgs e)
        {
            UpdateStatus("Confirmed");
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="status">The status.</param>
        private void UpdateStatus(string status)
        {
            if (CheckIfNullOrNoDisplay())
            {
                decimal id = Convert.ToDecimal(GetCurrentRow()["Id"]); /////// where you call grid.cells["Id"] or [0] there will be problems when you hide the column in grid

                if (status == "Requested")
                {
                    string str = (testingNr / 100 != 0) ? "" : (testingNr / 10 != 0) ? "0" : "00";
                    str += testingNr;

                    GetCurrentRow()["TestingNr"] = DateTime.Today.ToString("yy") + "/" + str;

                    BusinessOperations.UpdateStatus(DataSet, id, status);
                    BusinessOperations.UpdateTestingNr(DataSet, id, testingNr);

                    testingNr += 1;
                }
                else
                {
                    BusinessOperations.UpdateStatus(DataSet, id, "Confirmed");
                }
                GetCurrentRow()["StatusText"] = status;

                DataSet.BasicDataTable.AcceptChanges();
            }
            else
            {
                btnRequest.Enabled = false;
                btnConfirm.Enabled = false;
            }
            ExitEditMode(true);
        }

        /// <summary>
        /// Creates the new operation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CreateNewOperation(object sender, EventArgs e)
        {
            succesfullySave = false;
            newButtonWasClicked = true;
            btnSave.Visible = true;
            btnClose.Text = "Cancel";
            btnConfirm.Visible = false;
            btnRequest.Visible = false;
            btnNew.Visible = false;
            btnNewCopy.Visible = false;
            btnDelete.Visible = false;
            CreateOverViewRowAddedEvent();
            BusinessOperations.CreateNew(DataSet);
            SetNewRowEmpty();
            SetAllocation();
            ChangeSelectedControlTab(tabPageBasicData);
            insertAllocationFirstRow = true;
        }

        /// <summary>
        /// Creates the new copy operation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CreateNewCopyOperation(object sender, EventArgs e)
        {
            newCopyButtonWasClicked = true;
            // newButtonWasClicked = true;

            succesfullySave = false;

            if (CheckIfNullOrNoDisplay())
            {
                InitializeNewCopyMode(GetCurrentRow());
            }
            else
            {
                btnNewCopy.Enabled = false;
            }
        }

        /// <summary>
        /// Closes the operation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CloseOperation(object sender, EventArgs e)
        {
            ClearErrorProviders();

            if (btnClose.Text == "Cancel")
            {
                UndoDataChanges();
                canCheckForEditMode = false;
            }
            else
            {
                BusinessOperations.GetConnection().Close();
                Close();
            }
        }

        /// <summary>
        /// Saves the operaion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SaveOperaion(object sender, EventArgs e)
        {
            bool wasSaveSuccesfull = false;

            if (bindingSourceDateTimePickerFrom.Value < DateTime.Now.AddDays(7))
            {
                if (!(MessageBox.Show("The date entered for starting picking is very short-term. Do you really want to save the data ?? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    return;
                }
            }
            if (ValidateChildren() && CheckIfNullOrNoDisplay())
            {
                //FillAllocationTable();
                DataSet.BasicDataTable.AcceptChanges();
                wasSaveSuccesfull = (newButtonWasClicked) ? BusinessOperations.Save(DataSet, GetCurrentRow(), "New", allocationDeletedIDs) :
                    (newCopyButtonWasClicked) ? BusinessOperations.Save(DataSet, newCopyCurrentRow, "Copy", allocationDeletedIDs) : BusinessOperations.Save(DataSet, GetCurrentRow(), "Update", allocationDeletedIDs); ;
                if (wasSaveSuccesfull)
                {
                    succesfullySave = true;
                    ExitEditMode(true);
                }
                else
                {
                    ExitEditMode(false);
                    MessageBox.Show("UnsuccessFull Save Operation !");
                }
                InitializeOrUpdateDataGridViewOverViewElementsCount();
            }
            if ((newButtonWasClicked || newCopyButtonWasClicked) && wasSaveSuccesfull == true)
            {
                GetCurrentRow()["StatusText"] = "Unchecked";
                DataSet.BasicDataTable.AcceptChanges();
                newCopyButtonWasClicked = false;
                newButtonWasClicked = false;
            }
        }

        /// <summary>
        /// Deletes the operaion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DeleteOperaion(object sender, EventArgs e)
        {
            if (MessageBox.Show("Should the external picking really be deleted? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (CheckIfNullOrNoDisplay())
                {
                    if (bindingSourceDataGridViewOverview.CurrentRow.Cells["StatusTextColumn"].Value.ToString() != "Confirmed")
                    {
                        BusinessOperations.DoDelete(DataSet, GetCurrentRow());
                    }
                    else
                    {
                        MessageBox.Show("You can't delete a confirmed commission !");
                    }
                }
                else
                {
                    btnDelete.Enabled = false;
                }
                ChangeSelectedControlTab(tabPageOverview);
                InitializeDefaultGridSelectedRow();
                InitializeOrUpdateDataGridViewOverViewElementsCount();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////  C H E C K B O X E S     E V E N T S  /////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Cheeckeds the changed CheckBox requested.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheeckedChangedCheckBoxRequested(object sender, EventArgs e)
        {
            if (bindingSourceBasicData.Filter == null || bindingSourceBasicData.Filter == confirmedCondition)
            {
                bindingSourceBasicData.Filter = (bindingSourceBasicData.Filter == null) ? requestedCondition : confirmedAndRequestedCondition;
            }
            else
            {
                bindingSourceBasicData.Filter = (bindingSourceBasicData.Filter == requestedCondition) ? null : confirmedCondition;
            }
            CheckIfNullOrNoDisplay();
        }

        /// <summary>
        /// Checkeds the changed CheckBox confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckedChangedCheckBoxConfirmed(object sender, EventArgs e)
        {
            if (bindingSourceBasicData.Filter == null || bindingSourceBasicData.Filter == requestedCondition)
            {
                bindingSourceBasicData.Filter = (bindingSourceBasicData.Filter == null) ? confirmedCondition : confirmedAndRequestedCondition;
            }
            else
            {
                bindingSourceBasicData.Filter = (bindingSourceBasicData.Filter == confirmedCondition) ? null : requestedCondition;
            }
            CheckIfNullOrNoDisplay();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////  O V E R V I E W   E V E N T S   //////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Handles the CellMouseClick event of the BindingSourceDataGridViewOverview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void BindingSourceDataGridViewOverview_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (bindingSourceDataGridViewOverview.CurrentCell != null && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                bindingSourceDataGridViewOverview.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu m = new ContextMenu();
                    MenuItem copyRow = new MenuItem("CopyRow");
                    MenuItem copyColumn = new MenuItem("CopyColumn");
                    m.MenuItems.Add(copyRow);
                    m.MenuItems.Add(copyColumn);
                    DataGridViewCell cell = bindingSourceDataGridViewOverview.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    var cellRectangle = bindingSourceDataGridViewOverview.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    m.Show(bindingSourceDataGridViewOverview, new Point(cellRectangle.Right, cellRectangle.Top));
                    copyRow.Click += CopyRow_Click;
                    copyColumn.Click += CopyColumn_Click;
                }
            }
        }

        /// <summary>
        /// Gets the identifier of allocation deleted row.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataGridViewRowCancelEventArgs"/> instance containing the event data.</param>
        private void GetIdOfAllocationDeletedRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataRow row = (e.Row.DataBoundItem as DataRowView).Row;
            allocationDeletedIDs += row["DL32_KOMM_ANFORDERUNG_KONTO_ID"].ToString() + ',';
        }

        /// <summary>
        /// Handles the Click event of the CopyColumn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CopyColumn_Click(object sender, EventArgs e)
        {
            string column = string.Empty;
            column += bindingSourceDataGridViewOverview.Rows[GetCurrentDataGridViewRowIndex()].Cells[bindingSourceDataGridViewOverview.CurrentCell.ColumnIndex].Value.ToString();

            Clipboard.SetText(column);
        }

        /// <summary>
        /// Handles the Click event of the CopyRow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CopyRow_Click(object sender, EventArgs e)
        {
            string row = string.Empty;

            foreach (DataGridViewCell cell in bindingSourceDataGridViewOverview.Rows[bindingSourceDataGridViewOverview.CurrentCell.RowIndex].Cells)
            {
                if (cell.Visible == true)
                {
                    row += cell.Value.ToString() + "          ";
                }
            }

            Clipboard.SetText(row);
        }

        /// <summary>
        /// Selects the full row when cell double click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SelectFullRowWhenCellDoubleClickEvent(object sender, DataGridViewCellEventArgs e)
        {
            if (CheckIfNullOrNoDisplay())
            {
                if (bindingSourceDataGridViewOverview.SelectedCells.Count == 0)
                {
                    bindingSourceDataGridViewOverview.Rows[0].Selected = true;
                }
                bindingSourceDataGridViewOverview.CurrentRow.Selected = true;
                btnDelete.Enabled = true;
                btnNewCopy.Enabled = true;
            }
        }

        /// <summary>
        /// Doubles the click overview.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DoubleClickOverview(object sender, DataGridViewCellMouseEventArgs e)
        {
            ChangeSelectedControlTab(tabPageBasicData);
        }

        /// <summary>
        /// Creates the over view row added event.
        /// </summary>
        private void CreateOverViewRowAddedEvent()
        {
            bindingSourceDataGridViewOverview.RowsAdded += OverviewAddRowEvent;
        }

        /// <summary>
        /// Selectings the row on over view event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SelectingRowOnOverViewEvent(object sender, EventArgs e)
        {
            if (CheckIfNullOrNoDisplay())
            {
                if (GetCurrentRow()["StatusText"] != null)
                {
                    if (GetCurrentRow()["StatusText"].ToString() == "Unchecked")
                    {
                        btnConfirm.Enabled = false;
                        btnRequest.Enabled = true;
                    }
                    if (GetCurrentRow()["StatusText"].ToString() == "Requested")
                    {
                        btnConfirm.Enabled = true;
                        btnRequest.Enabled = false;
                    }
                    if (GetCurrentRow()["StatusText"].ToString() == "Confirmed")
                    {
                        btnDelete.Enabled = false;
                        btnConfirm.Enabled = false;
                        btnRequest.Enabled = false;
                    }
                }
            }
            else
            {
                btnConfirm.Enabled = false;
                btnRequest.Enabled = false;
            }
        }

        /// <summary>
        /// Overviews the add row event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataGridViewRowsAddedEventArgs"/> instance containing the event data.</param>
        private void OverviewAddRowEvent(object sender, DataGridViewRowsAddedEventArgs e)
        {
            bindingSourceDataGridViewOverview.Rows[e.RowIndex].Selected = true;
            rowIndex = e.RowIndex;
        }

        /// <summary>
        /// Prevents the enter basic data if no data on grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PreventEnterBasicDataIfNoDataOnGrid(object sender, EventArgs e)
        {
            if (CheckIfNullOrNoDisplay() == false)
            {
                tabControl.SelectedTab = tabPageOverview;
                return;
            }

            SetAllocation();

            if (newButtonWasClicked == false && succesfullySave == false)
            {
                chbWorkSunday.Checked = (GetCurrentRow()["Sunday"].ToString() == "j") ?
               true : false;

                chbWorkSaturday.Checked = (GetCurrentRow()["Saturday"].ToString() == "j") ?
               true : false;
            }
            else
            {
                chbWorkSunday.Checked = (GetCurrentRow()["Sunday"].ToString() == "j") ?
                      true : false;

                chbWorkSaturday.Checked = (GetCurrentRow()["Saturday"].ToString() == "j") ?
               true : false;
            }

            if (GetCurrentRow()["StatusText"].ToString() == "Confirmed" && newCopyButtonWasClicked == false && newButtonWasClicked == false)
            {
                ViewConfirmedCommission();
                btnRequest.Enabled = false;
                btnConfirm.Enabled = false;
            }
            else
            {
                UndoViewConfirmedCommission();
            }

            if (GetCurrentRow()["StatusText"].ToString() == "Requested" && newCopyButtonWasClicked == false && newButtonWasClicked == false)
            {
                btnRequest.Enabled = false;
                btnConfirm.Enabled = true;
            }

            if (GetCurrentRow()["StatusText"].ToString() == "Unchecked" && newCopyButtonWasClicked == false && newButtonWasClicked == false)
            {
                btnRequest.Enabled = true;
                btnConfirm.Enabled = false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////  P R I V A T E  U S E F U L L  F U N C T I O N S   ////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets the index of the current data grid view row.
        /// </summary>
        /// <returns></returns>
        private int GetCurrentDataGridViewRowIndex()
        {
            foreach (DataGridViewRow row in bindingSourceDataGridViewOverview.SelectedRows)
            {
                return row.Index;
            }
            if (bindingSourceDataGridViewOverview.CurrentCell != null)
            {
                return bindingSourceDataGridViewOverview.CurrentCell.RowIndex;
            }
            return 0;
        }

        /// <summary>
        /// Determines whether [is automatic validate] [the specified validate].
        /// </summary>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        private void IsAutoValidate(bool validate)
        {
            AutoValidate = (validate == true) ? AutoValidate.EnableAllowFocusChange : AutoValidate.Disable;
        }

        /// <summary>
        /// Changes the selected control tab.
        /// </summary>
        /// <param name="control">The control.</param>
        private void ChangeSelectedControlTab(TabPage control)
        {
            tabControl.SelectedTab = control;
        }

        /// <summary>
        /// Sets the new row empty.
        /// </summary>
        private void SetNewRowEmpty()
        {
            if (bindingSourceDataGridViewOverview.Rows.Count != 1 && bindingSourceDataGridViewOverview.FirstDisplayedCell != null)
            {
                bindingSourceDataGridViewOverview.CurrentCell = bindingSourceDataGridViewOverview.Rows[rowIndex].Cells[1];
            }
        }

        /// <summary>
        /// Checks if null or no display.
        /// </summary>
        /// <returns></returns>
        private bool CheckIfNullOrNoDisplay()
        {
            if (bindingSourceDataGridViewOverview.Rows.Count == 0 || bindingSourceDataGridViewOverview.FirstDisplayedCell == null)
            {
                btnNewCopy.Enabled = false;
                btnDelete.Enabled = false;
                btnConfirm.Enabled = false;
                btnRequest.Enabled = false;
                return false;
            }
            btnNewCopy.Enabled = true;
            btnDelete.Enabled = true;
            btnConfirm.Enabled = true;
            btnRequest.Enabled = true;
            return true;
        }

        /// <summary>
        /// Sets the allocation.
        /// </summary>
        private void SetAllocation()
        {
            bindingSourceAllocation.Filter = "SourceID = " + GetCurrentRow()["Id"];
            if (insertAllocationFirstRow == true && newButtonWasClicked == true)
            {
                DataRow newRow = DataSet.DL32_EXT_KOMM_KONTO.NewDL32_EXT_KOMM_KONTORow();
                newRow["SourceID"] = GetCurrentRow()["Id"];
                DataSet.DL32_EXT_KOMM_KONTO.Rows.Add(newRow);
                insertAllocationFirstRow = false;
            }
        }

        /// <summary>
        /// Gets the current row.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private DataRow GetCurrentRow(Dlv005DataSet dataModel, decimal id)
        {
            foreach (DataRow row in dataModel.BasicDataTable.Rows)
            {
                if (Convert.ToDecimal(row["Id"]) == id)
                {
                    return row;
                }
            }
            return null;
        }

        /// <summary>
        /// Clears the error providers.
        /// </summary>
        private void ClearErrorProviders()
        {
            errorProviderAllocation.Clear();
            errorProviderCustomerOE.Clear();
            errorProviderTo.Clear();
            errorProviderFrom.Clear();
            errorProviderChief.Clear();
            errorProviderCustomer.Clear();
            errorProviderCustomerOE.Clear();
            errorProviderEngineering.Clear();
            errorProviderSeries.Clear();
            errorProviderSort.Clear();
            errorProviderRoutes.Clear();
            errorProviderKindOfTesting.Clear();
            errorProviderSeries.Clear();
            errorProviderHV.Clear();
            errorProviderDriving.Clear();
            errorProviderSpecial.Clear();
            errorProviderTestingContent.Clear();
        }

        /// <summary>
        /// Fills the allocation table.
        /// </summary>
        private void FillAllocationTable()
        {
            if (newButtonWasClicked == false)
            {
                foreach (DataGridViewRow row in bindingSourceDataGridViewAllocation.Rows)
                {
                    DataSet.Alocation.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, GetCurrentRow()["Id"]);
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////  V I E W     M O D E     I N I T I A L I Z A T I O N   ////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Undoes the data changes.
        /// </summary>
        private void UndoDataChanges()
        {
            if (newButtonWasClicked)
            {
                newButtonWasClicked = false;
                ExitEditMode(false);
                tabControl.SelectedTab = tabPageOverview;
                InitializeDefaultGridSelectedRow();
            }
            else
            {
                tabControl.SelectedTab = tabPageOverview;
                bindingSourceBasicData.ResetBindings(true);
                tabControl.SelectedTab = tabPageBasicData;
            }
            DataSet.RejectChanges();
        }

        /// <summary>
        /// Enters the in edit mode.
        /// </summary>
        public void EnterInEditMode()
        {
            succesfullySave = false;
            if (tabControl.SelectedTab == tabPageBasicData && canCheckForEditMode == true)
            {
                EditMode();
            }
        }

        /// <summary>
        /// Views the confirmed commission.
        /// </summary>
        private void ViewConfirmedCommission()
        {
            foreach (Control control in tabPageBasicData.Controls)
            {
                control.Enabled = false;
            }
        }

        /// <summary>
        /// Undoes the view confirmed commission.
        /// </summary>
        private void UndoViewConfirmedCommission()
        {
            foreach (Control control in tabPageBasicData.Controls)
            {
                control.Enabled = true;
            }
        }

        /// <summary>
        /// Enters the in over view page event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void EnterInOverViewPageEvent(object sender, EventArgs e)
        {
            if (newButtonWasClicked)
            {
                tabControl.SelectedTab = tabPageBasicData;
                return;
            }
            else
            {
                ExitEditMode(false);
            }
        }

        /// <summary>
        /// Edits the mode.
        /// </summary>
        private void EditMode()
        {
            succesfullySave = false;
            btnSave.Visible = true;
            btnClose.Text = "Cancel";
            btnNew.Visible = false;
            btnNewCopy.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
            btnRequest.Visible = false;
            btnConfirm.Visible = false;
        }

        /// <summary>
        /// Exits the edit mode.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        private void ExitEditMode(bool success)
        {
            canCheckForEditMode = false;
            if (success == false)
            {
                DataSet.RejectChanges();
                return;
            }
            ClearErrorProviders();
            btnSave.Visible = false;
            btnClose.Text = "Close";
            btnNew.Visible = true;
            btnNewCopy.Visible = true;
            btnDelete.Visible = true;
            btnConfirm.Visible = true;
            btnRequest.Visible = true;
            DataSet.AcceptChanges();
        }

        /// <summary>
        /// Initializes the new copy mode.
        /// </summary>
        /// <param name="row">The row.</param>
        private void InitializeNewCopyMode(DataRow row)
        {
            ChangeSelectedControlTab(tabPageBasicData);
            newCopyCurrentRow = row;
            newButtonWasClicked = true;
            btnRequest.Visible = false;
            btnConfirm.Visible = false;
            btnClose.Text = "Cancel";
            btnNew.Visible = false;
            btnNewCopy.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
            CreateOverViewRowAddedEvent();
            BusinessOperations.CreateNewCopy(DataSet, row);
            bindingSourceDataGridViewOverview.CurrentCell = bindingSourceDataGridViewOverview.Rows[rowIndex].Cells[1];
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ChbWorkSaturday control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ChbWorkSaturday_CheckedChanged(object sender, EventArgs e)
        {
            saturday = (saturday == "j") ? "n" : "j";
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ChbWorkSunday control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ChbWorkSunday_CheckedChanged(object sender, EventArgs e)
        {
            sunday = (sunday == "j") ? "n" : "j";
        }

        /// <summary>
        /// Handles the Activated event of the Dlv005View control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Dlv005View_Activated(object sender, EventArgs e)
        {
            checkBoxHideConfirmed.Checked = true;
            checkBoxHideRequested.Checked = true;
            if (CheckIfNullOrNoDisplay() && hasTheApplicationStarted == false)
            {
                bindingSourceDataGridViewOverview.Columns[0].Visible = false;
                bindingSourceDataGridViewOverview.Rows[0].Selected = true;
                hasTheApplicationStarted = true;
            }
            else
            {
                Activated -= Dlv005View_Activated;
            }
        }

        /// <summary>
        /// Sets the error message.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        private string SetErrorMessage(ErrorProvider error)
        {
            if (error == errorProviderFrom)
            {
                return dateInThePast;
            }
            if (error == errorProviderSeries)
            {
                return incorectFormat;
            }
            if (error == errorProviderTo)
            {
                return toLowerThenFromDate;
            }
            if (error == errorProviderCustomerOE || error == errorProviderCustomer || error == errorProviderChief || error == errorProviderEngineering)
            {
                return valueNotContainedInSelectionTable;
            }

            return emptyMandatory;
        }

        /// <summary>
        /// Gets the current row.
        /// </summary>
        /// <returns></returns>
        private DataRow GetCurrentRow()
        {
            return (bindingSourceDataGridViewOverview.Rows[GetCurrentDataGridViewRowIndex()].DataBoundItem as DataRowView).Row;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////// C H E C K   I F   W E   A R E   I N   E D I T   M O D E    E V E N T S /////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Allocations the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AllocationClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            bindingSourceDataGridViewAllocation.CellValueChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Converts to click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ToClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            bindingSourceDateTimePickerTo.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Froms the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FromClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            bindingSourceDateTimePickerFrom.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Specials the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SpecialClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            bindingSourceComboBoxSpecialQualification.SelectionChangeCommitted += GetIdFromSelect;
            bindingSourceComboBoxSpecialQualification.SelectionChangeCommitted += TextBoxTextChangedEvent;
            bindingSourceComboBoxSpecialQualification.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Hvs the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HvClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            bindingSourceComboBoxHVQualification.SelectionChangeCommitted += GetIdFromHv; ;
            bindingSourceComboBoxHVQualification.SelectionChangeCommitted += TextBoxTextChangedEvent;
            bindingSourceComboBoxHVQualification.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Drivings the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DrivingClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            bindingSourceComboBoxDrivingAutorization.SelectionChangeCommitted += GetIdFromDriving; ;
            bindingSourceComboBoxDrivingAutorization.SelectionChangeCommitted += TextBoxTextChangedEvent;
            bindingSourceComboBoxDrivingAutorization.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Kinds the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void KindClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            bindingSourceComboBoxKindOfTesting.SelectionChangeCommitted += GetIdFromKind; ;
            bindingSourceComboBoxKindOfTesting.SelectionChangeCommitted += TextBoxTextChangedEvent;
            bindingSourceComboBoxKindOfTesting.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Routes the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RouteClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            bindingSourceComboBoxRoutes.SelectionChangeCommitted += GetIdFromRoute; ;
            bindingSourceComboBoxRoutes.SelectionChangeCommitted += TextBoxTextChangedEvent;
            bindingSourceComboBoxRoutes.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Sorts the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SortClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            bindingSourceComboBoxSort.SelectionChangeCommitted += GetIdFromSort; ;
            bindingSourceComboBoxSort.SelectionChangeCommitted += TextBoxTextChangedEvent;
            bindingSourceComboBoxSort.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Testings the content click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TestingContentClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            TextBoxTestingContent.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Engineerings the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void EngineeringClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            TextBoxEngineeringAST.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Chiefs the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ChiefClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            TextBoxChief.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Customers the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CustomerClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            TextBoxCustomer.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Customers the oe click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CustomerOEClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            TextBoxCustomerOE.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Serieses the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SeriesClick(object sender, EventArgs e)
        {
            canCheckForEditMode = true;
            TextBoxSeries.TextChanged += TextBoxTextChangedEvent;
        }

        /// <summary>
        /// Texts the box text changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TextBoxTextChangedEvent(object sender, EventArgs e)
        {
            if (((System.Windows.Forms.Control)sender).Name == "chbWorkSaturday" || ((System.Windows.Forms.Control)sender).Name == "chbWorkSunday")
            {
                canCheckForEditMode = true;
            }

            if (tabControl.SelectedTab == tabPageBasicData)
            {
                if (((System.Windows.Forms.Control)sender).Name == "bindingSourceDateTimePickerFrom")
                {
                    bindingSourceDateTimePickerFrom.Validating += BindingSourceDateTimePickerFrom_Validating;
                }
                EnterInEditMode();
            }
        }

        /// <summary>
        /// Gets the identifier from select.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void GetIdFromSelect(object sender, EventArgs e)
        {
            if (newButtonWasClicked)
            {
                DataSet.BasicDataTable.Rows[DataSet.BasicDataTable.Rows.Count - 1]["SpecialID"] = ((ComboBox)sender).SelectedValue;
            }
        }

        /// <summary>
        /// Gets the identifier from hv.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GetIdFromHv(object sender, EventArgs e)
        {
            if (newButtonWasClicked)
            {
                DataSet.BasicDataTable.Rows[DataSet.BasicDataTable.Rows.Count - 1]["HvID"] = ((ComboBox)sender).SelectedValue;
            }
        }

        /// <summary>
        /// Gets the identifier from driving.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GetIdFromDriving(object sender, EventArgs e)
        {
            if (newButtonWasClicked)
            {
                DataSet.BasicDataTable.Rows[DataSet.BasicDataTable.Rows.Count - 1]["LicenceID"] = ((ComboBox)sender).SelectedValue;
            }
        }

        /// <summary>
        /// Downs the arrow key pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void DownArrowKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                DataRow row = DataSet.DL32_EXT_KOMM_KONTO.NewDL32_EXT_KOMM_KONTORow();
                row["SourceID"] = GetCurrentRow()["Id"];
                DataSet.DL32_EXT_KOMM_KONTO.Rows.Add(row);
            }
        }

        /// <summary>
        /// Gets the kind of the identifier from.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GetIdFromKind(object sender, EventArgs e)
        {
            if (newButtonWasClicked)
            {
                DataSet.BasicDataTable.Rows[DataSet.BasicDataTable.Rows.Count - 1]["KindID"] = ((ComboBox)sender).SelectedValue;
            }
        }

        /// <summary>
        /// Gets the identifier from route.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GetIdFromRoute(object sender, EventArgs e)
        {
            if (newButtonWasClicked)
            {
                DataSet.BasicDataTable.Rows[DataSet.BasicDataTable.Rows.Count - 1]["RouteID"] = ((ComboBox)sender).SelectedValue;
            }
        }

        /// <summary>
        /// Gets the identifier from sort.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GetIdFromSort(object sender, EventArgs e)
        {
            if (newButtonWasClicked)
            {
                DataSet.BasicDataTable.Rows[DataSet.BasicDataTable.Rows.Count - 1]["SortID"] = ((ComboBox)sender).SelectedValue;
            }
        }
    }
}