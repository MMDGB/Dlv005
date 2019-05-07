using Dlv005.DL;
using System.Data;

namespace Dlv005.BL
{
    public partial class Dlv005DataSet
    {
        Dlv005DataSet GetData()
        {
            return this;
        }
        /// <summary>
        /// Initializes the tables.
        /// </summary>
        /// <returns></returns>
        public Dlv005DataSet InitializeTables()
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            SetSelectionTables(dataAccessComponent);
            SetOverviewAndAllocation(dataAccessComponent);
            SetDropDownTables(dataAccessComponent);
            SetColumnNames();
            SetSeriesNumber(BasicDataTable);
            return this;
        }

        /// <summary>
        /// Sets the table names.
        /// </summary>
        private void SetColumnNames()
        {
            BD09_PERSON.BD09_PERSIDColumn.ColumnName = "Id";
            BD09_PERSON.BD09_PERSIDColumn.ColumnMapping = MappingType.Hidden;

            BD09_PERSON.BD09_NAMEColumn.ColumnName = "Name";
            BD09_PERSON.BD09_VORNAMEColumn.ColumnName = "Vorname";
            BD09_PERSON.BD09_OEColumn.ColumnName = "Department";

            BD06_ORG_EINHEIT_TBL.BD06_OEColumn.ColumnMapping = MappingType.Hidden;
            BD06_ORG_EINHEIT_TBL.BD06_OEColumn.ColumnName = "Section";
            BD06_ORG_EINHEIT_TBL.BD06_KURZ_BEZColumn.ColumnName = "Short description";

            BD12_BAUREIHE.BD12_BENENNUNGColumn.ColumnName = "Name";
            BD12_BAUREIHE.BD12_BAUREIHEColumn.ColumnName = "Series";
            BD12_BAUREIHE.BD12_ENDEDATUMColumn.ColumnName = "Available";

            DL32_EXT_KOMM_KONTO.DL32_EXT_KOMM_ANFORDERUNG_IDColumn.ColumnName = "SourceID";
        }

        /// <summary>
        ///   <para></para>
        ///   <para>Sets the selection tables.
        /// </para>
        /// </summary>
        /// <param name="dataAccessComponent">The data access component.</param>
        private void SetSelectionTables(Dlv005DataAccessComponent dataAccessComponent)
        {
            dataAccessComponent.PopulateBD06().Fill(BD06_ORG_EINHEIT_TBL);
            dataAccessComponent.PopulateDL32().Fill(DL32_EXT_KOMM_KONTO);
            dataAccessComponent.PopulateBD09().Fill(BD09_PERSON);
            dataAccessComponent.PopulateBD12().Fill(BD12_BAUREIHE);
            dataAccessComponent.PopulateSD111().Fill(SD111_QUALIFIKATIONEN);
            dataAccessComponent.PopulateSD111().Fill(SD111_QUALIFIKATIONENLicence);
            dataAccessComponent.PopulateSD111().Fill(SD111_QUALIFIKATIONENHV);
            dataAccessComponent.PopulateSD111().Fill(SD111_QUALIFIKATIONENSpecial);
            dataAccessComponent.PopulateBasicDataTable().Fill(BasicDataTable);
        }

        private void SetSeriesNumber(BasicDataTableDataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                string seriesNumber = string.Empty;

                string[] series = row["SeriesText"].ToString().Split(',');
                foreach (string serie in series)
                {
                    foreach (DataRow bd12row in BD12_BAUREIHE.Rows)
                    {
                        if (bd12row["Name"].ToString() == serie)
                        {
                            seriesNumber += bd12row["Series"] + ",";
                            break;
                        }
                    }
                }
                seriesNumber = seriesNumber.Remove(seriesNumber.Length - 1);
                row["SeriesNumber"] = seriesNumber;
            }
            BasicDataTable.AcceptChanges();
        }

        /// <summary>
        ///   <para></para>
        ///   <para>Sets the overview and allocation.
        /// </para>
        /// </summary>
        /// <param name="dataAccessComponent">The data access component.</param>
        private void SetOverviewAndAllocation(Dlv005DataAccessComponent dataAccessComponent)
        {
            dataAccessComponent.PopulateAllocation().Fill(Alocation);
        }

        /// <summary>
        ///   <para></para>
        ///   <para>Sets the drop down tables.
        /// </para>
        /// </summary>
        /// <param name="dataAccessComponent">The data access component.</param>
        private void SetDropDownTables(Dlv005DataAccessComponent dataAccessComponent)
        {
            dataAccessComponent.PopulateDL31().Fill(DL31_KOMM_ANFORDERUNG);
            dataAccessComponent.PopulateDL37().Fill(DL37_KOMM_STATUS_TBL);
            dataAccessComponent.PopulateDL38().Fill(DL38_KOMM_ERPROBUNGSORT_TBL);
            dataAccessComponent.PopulateDL39().Fill(DL39_KOMM_STRECKENART_TBL);
            dataAccessComponent.PopulateDL40().Fill(DL40_KOMM_ERPROBUNGSART_TBL);
        }

        /// <summary>
        /// Gets the last testing nr value.
        /// </summary>
        /// <param name="dataAccessComponent">The data access component.</param>
        /// <returns></returns>
        public string GetLastTestingNrValue(Dlv005DataAccessComponent dataAccessComponent)
        {
            return dataAccessComponent.GetTestingNr();
        }

        public decimal GetLastID(Dlv005DataAccessComponent dataAccessComponent)
        {
            return dataAccessComponent.GetLastID();
        }
    }
}