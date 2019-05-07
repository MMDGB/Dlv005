using MySql.Data.MySqlClient;
using System;
using System.Data;
using static Dlv005.BL.Dlv005DataSet;

namespace Dlv005.BL
{
    public class Dlv005BusinessOperations
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        private readonly DL.Dlv005DataAccessComponent DataAccess = new DL.Dlv005DataAccessComponent();

        /// <summary>
        /// Gets the application connection.
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection()
        {
            MySqlConnection Connection = DataAccess.GetConnection();

            return Connection;
        }

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        public void CreateNew(Dlv005DataSet dataModel)
        {
            BasicDataTableRow newRow = dataModel.BasicDataTable.NewBasicDataTableRow();
            newRow.Id = GetLastID(dataModel) + 1;
            newRow.TestingNr = string.Empty;
            newRow.Customer = string.Empty;
            newRow.Chief = string.Empty;
            newRow.Engineering = string.Empty;
            newRow.To = DateTime.Now;
            newRow.From = DateTime.Now;
            newRow.StatusText = string.Empty;
            newRow.SortingTest = string.Empty;
            newRow.RouteOfTesting = string.Empty;
            newRow.KindOfTesting = string.Empty;
            newRow.SeriesText = string.Empty;
            newRow.TestingContent = string.Empty;
            newRow.Special = string.Empty;
            newRow.Hv = string.Empty;
            newRow.Licence = string.Empty;
            newRow.CustomerOE = string.Empty;
            newRow.Saturday = string.Empty;
            newRow.Sunday = string.Empty;
            dataModel.BasicDataTable.AddBasicDataTableRow(newRow);
        }

        /// <summary>
        /// Creates the new copy.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <param name="row">The row.</param>
        public void CreateNewCopy(Dlv005DataSet dataModel, DataRow row)
        {
            BasicDataTableRow newRow = dataModel.BasicDataTable.NewBasicDataTableRow();
            newRow.Id = GetLastID(dataModel) + 1;
            newRow.TestingNr = string.Empty;
            newRow.To = DateTime.Now;
            newRow.From = DateTime.Now;

            newRow.StatusText = string.Empty;
            newRow.StatusID = 1;

            newRow.TestingContent = string.Empty;
            newRow.Saturday = row["Saturday"].ToString();
            newRow.Sunday = row["Sunday"].ToString();

            newRow.Customer = row["Customer"].ToString();
            newRow.CustomerID = Convert.ToDecimal(row["CustomerID"]);

            newRow.Chief = row["Chief"].ToString();
            newRow.ChiefID = Convert.ToDecimal(row["ChiefID"]);

            newRow.Engineering = row["Engineering"].ToString();
            newRow.EngineeringID = Convert.ToDecimal(row["EngineeringID"]);

            newRow.SeriesText = row["SeriesText"].ToString();
            newRow.SeriesNumber = row["SeriesNumber"].ToString();

            newRow.SortID = Convert.ToDecimal(row["SortID"]);
            newRow.SortingTest = row["SortingTest"].ToString();

            newRow.RouteID = Convert.ToDecimal(row["RouteID"]);
            newRow.RouteOfTesting = row["RouteOfTesting"].ToString();

            newRow.KindID = Convert.ToDecimal(row["KindID"]);
            newRow.KindOfTesting = row["KindOfTesting"].ToString();

            newRow.SpecialID = Convert.ToDecimal(row["SpecialID"]);
            newRow.Special = row["Special"].ToString();

            newRow.HvID = Convert.ToDecimal(row["HvID"]);
            newRow.Hv = row["Hv"].ToString();

            newRow.LicenceID = Convert.ToDecimal(row["LicenceID"]);
            newRow.Licence = row["Licence"].ToString();

            newRow.CustomerOE = row["CustomerOE"].ToString();
            newRow.CustomerOEID = Convert.ToDecimal(row["CustomerOEID"]);

            dataModel.BasicDataTable.AddBasicDataTableRow(newRow);
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public bool UpdateStatus(Dlv005DataSet dataModel, decimal id, string status)
        {
            MySqlCommand command = DataAccess.UpdateStatus();
            if (status == "Requested")
            {
                command.Parameters.AddWithValue("@StatusId", 2);
            }
            else
            {
                command.Parameters.AddWithValue("@StatusId", 3);
            }
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Update Exception !!");

                return false;
            }
            dataModel.AcceptChanges();
            return true;
        }

        /// <summary>
        /// Sets the count.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <returns></returns>
        public int SetCount(Dlv005DataSet dataModel)
        {
            if (dataModel.GetLastTestingNrValue(DataAccess) != string.Empty)
            {
                string tNr = dataModel.GetLastTestingNrValue(DataAccess).Split('/')[1];
                return ((tNr[0] == '0') ? (tNr[1] == '0') ? (Convert.ToInt32(tNr[2]) - 48) :
                    (Convert.ToInt32(tNr[1]) - 48) * 10 + (Convert.ToInt32(tNr[2]) - 48) :
                    (Convert.ToInt32(tNr[0]) - 48) * 100 + (Convert.ToInt32(tNr[1]) - 48)
                    * 10 + Convert.ToInt32(tNr[2]) - 48) + 1;
            }
            return 0;
        }

        /// <summary>
        /// Gets the last identifier.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <returns></returns>
        private decimal GetLastID(Dlv005DataSet dataModel)
        {
            return dataModel.GetLastID(DataAccess);
        }

        /// <summary>
        /// Updates the testing nr.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <param name="id">The position.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool UpdateTestingNr(Dlv005DataSet dataModel, decimal id, int value)
        {
            MySqlCommand command = DataAccess.UpdateTestingNr();
            string str = (value / 100 != 0) ? "" : (value / 10 != 0) ? "0" : "00";
            str += value;
            command.Parameters.AddWithValue("@TestingNr", DateTime.Today.ToString("yy") + "/" + str);
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Adds the alocation row.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        private bool AddAlocationRow(Dlv005DataSet dataSet, DataRow row)
        {
            if (row["SourceID"].ToString() != string.Empty)
            {
                MySqlCommand allocation = DataAccess.InsertInDL32();
                allocation.Parameters.AddWithValue("@Text", row["DL32_KONTIERUNG"]);
                allocation.Parameters.AddWithValue("@Procentage", row["DL32_ANTEIL_PROZENT"]);
                allocation.Parameters.AddWithValue("@Id", row["SourceID"]);
                try
                {
                    allocation.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex + "Do DataBaseChangesException !!!!!!!!!");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Updates the allocation row.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        private bool UpdateAllocationRow(Dlv005DataSet dataSet, DataRow row)
        {
            MySqlCommand allocation = DataAccess.UpdateDL32();
            allocation.Parameters.AddWithValue("@Text", row["DL32_KONTIERUNG"]);
            allocation.Parameters.AddWithValue("@Procentage", row["DL32_ANTEIL_PROZENT"]);
            allocation.Parameters.AddWithValue("@Id", row["DL32_KOMM_ANFORDERUNG_KONTO_ID"]);
            try
            {
                allocation.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Do DataBaseChangesException !!!!!!!!!");
                return false;
            }

            dataSet.Alocation.Clear();
            return true;
        }

        /// <summary>
        /// Deletes the allocation row.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool DeleteAllocationRow(Dlv005DataSet dataSet, string id)
        {
            MySqlCommand allocation = DataAccess.DeleteDL32();
            allocation.Parameters.AddWithValue("@Id", id);
            try
            {
                allocation.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Do DataBaseChangesException !!!!!!!!!");
                return false;
            }

            dataSet.Alocation.Clear();
            return true;
        }

        /// <summary>
        /// Saves the specified data model.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <param name="row">The row.</param>
        /// <param name="action">The action.</param>
        /// <param name="allocationDeletedRowsIds">The allocation deleted rows ids.</param>
        /// <returns></returns>
        public bool Save(Dlv005DataSet dataModel, DataRow row, string action, string allocationDeletedRowsIds)
        {
            dataModel.BasicDataTable.AcceptChanges();
            int lastPosition = dataModel.BasicDataTable.Rows.Count - 1;
            MySqlCommand command = (action == "New" || action == "Copy") ? DataAccess.InsertInDL31() : DataAccess.UpdateDL31();
            InputModel model = new InputModel(dataModel, row, action);
            if (action == "New" || action == "Copy")
            {
                command.Parameters.AddWithValue("@TestingNr", string.Empty);
                command.Parameters.AddWithValue("@Status", 1);
            }
            else
            {
                command.Parameters.AddWithValue("@TestingNr", model.TestingNr);
                command.Parameters.AddWithValue("@Status", model.StatusString);
            }
            command.Parameters.AddWithValue("@Saturday", model.Saturday);
            command.Parameters.AddWithValue("@Sunday", model.Sunday);
            command.Parameters.AddWithValue("@TestingContent", model.TestingContent);
            command.Parameters.AddWithValue("@FromDateTime", model.From);
            command.Parameters.AddWithValue("@ToDateTime", model.To);
            command.Parameters.AddWithValue("@CustomerOE", model.CustomerOEID);
            command.Parameters.AddWithValue("@Customer", model.CustomerID);
            command.Parameters.AddWithValue("@Chief", model.CheifID);
            command.Parameters.AddWithValue("@Engineering", model.EngineeringID);
            command.Parameters.AddWithValue("@Id", row["Id"]);
            command.Parameters.AddWithValue("@Series", model.SeriesID);
            command.Parameters.AddWithValue("@Sort", model.SortID);
            command.Parameters.AddWithValue("@Route", model.RouteID);
            command.Parameters.AddWithValue("@Kind", model.KindID);
            command.Parameters.AddWithValue("@Licence", model.LicenceID);
            command.Parameters.AddWithValue("@Hv", model.HvID);
            command.Parameters.AddWithValue("@Special", model.SpecialID);
            try
            {
                command.ExecuteNonQuery();

                if (dataModel.DL32_EXT_KOMM_KONTO.GetChanges() != null)
                {
                    foreach (DataRow allocationRow in dataModel.DL32_EXT_KOMM_KONTO.GetChanges().Rows)
                    {
                        if (allocationRow.RowState == DataRowState.Modified)
                        {
                            UpdateAllocationRow(dataModel, allocationRow);
                        }
                        if (allocationRow.RowState == DataRowState.Added)
                        {
                            AddAlocationRow(dataModel, allocationRow);
                        }
                    }
                    string[] idS = allocationDeletedRowsIds.Split(',');

                    foreach (string id in idS)
                    {
                        DeleteAllocationRow(dataModel, id);
                    }
                }
                dataModel.DL32_EXT_KOMM_KONTO.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Do DataBaseChangesException !!!!!!!!!");
                return false;
            }
        }

        /// <summary>
        /// Does the delete.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <param name="row">The row.</param>
        public void DoDelete(Dlv005DataSet dataModel, DataRow row)
        {
            DeleteAllocationRow(dataModel, row["id"].ToString());
            MySqlCommand command = DataAccess.Delete();
            command.Parameters.AddWithValue("@Id", row["Id"]);
            try
            {
                command.BeginExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            dataModel.BasicDataTable.Rows.Remove(row);
            dataModel.BasicDataTable.AcceptChanges();
        }

        /// <summary>
        /// Gets the section.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public string GetSection(string v, Dlv005DataSet.BD06_ORG_EINHEIT_TBLDataTable table)
        {
            string str = string.Empty;
            foreach (DataRow row in table)
            {
                if (row[1].ToString() == v)
                {
                    str = row[0].ToString();
                }
            }
            return str;
        }
    }
}