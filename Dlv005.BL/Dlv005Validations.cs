using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dlv005.BL
{
    public class Dlv005Validations
    {
        /// <summary>
        /// Validates the series.
        /// </summary>
        /// <param name="valueFromTextBox">The value from text box.</param>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public bool ValidateSeries(string valueFromTextBox, Dlv005DataSet.BD12_BAUREIHEDataTable table,
            System.ComponentModel.CancelEventArgs e, ErrorProvider error, TextBox box, string messageNUll, string messageWrong)
        {
            if (CheckNull(valueFromTextBox) == false)
            {
                e.Cancel = true;
                error.SetError(box, messageNUll);
                return false;
            }
            else
            {
                e.Cancel = false;
                error.SetError(box, null);
            }

            if (Regex.Matches(valueFromTextBox, @"[a-zA-Z]").Count != 0)
            {
                e.Cancel = true;
                error.SetError(box, messageWrong);
                return false;
            }
            else
            {
                e.Cancel = false;
                error.SetError(box, null);
            }

            if (CheckSeriesTrashValue(valueFromTextBox, table) == false)
            {
                e.Cancel = true;
                error.SetError(box, messageWrong);
                return false;
            }
            else
            {
                e.Cancel = false;
                error.SetError(box, null);
            }

            return true;
        }

        private bool CheckSeriesTrashValue(string valueFromTextBox, Dlv005DataSet.BD12_BAUREIHEDataTable table)
        {
            char[] delimiterChars = { ',', ';', '`' };
            string[] words = valueFromTextBox.Split(delimiterChars);

            foreach (var word in words)
            {
                bool isValue = false;
                foreach (DataRow row in table)
                {
                    if (row["Series"].ToString() == word)
                    {
                        isValue = true;
                        break;
                    }
                }
                if (!isValue)
                {
                    return false;
                }
            }
            foreach (char chr in delimiterChars)
            {
                if (!char.IsDigit(Convert.ToChar(valueFromTextBox.Substring(valueFromTextBox.IndexOf(chr) + 1, 1))))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckNull(string valueFromTextBox)
        {
            char[] delimiterChars = { ',', ';', '`' };
            string[] words = valueFromTextBox.Split(delimiterChars);
            if (words.Length == 1 && words[0] == string.Empty)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the customer oe.
        /// </summary>
        /// <param name="valueFromBox">The value from box.</param>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public bool ValidateCustomerOE(string valueFromBox, Dlv005DataSet.BD06_ORG_EINHEIT_TBLDataTable table)
        {
            foreach (DataRow row in table)
            {
                if (row["Short description"].ToString() == valueFromBox)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates the engineering.
        /// </summary>
        /// <param name="valueFromBox">The value from box.</param>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public bool ValidateCustomerAndChiefAndEngineering(string valueFromBox, Dlv005DataSet.BD09_PERSONDataTable table, Dlv005DataSet.BD06_ORG_EINHEIT_TBLDataTable BD06Table)
        {
            foreach (DataRow row in table)
            {
                string totalValue = string.Empty;

                totalValue += row["Name"].ToString() + ", " + row["Vorname"].ToString() + ", " + GetSection(row["Department"].ToString(), BD06Table);

                if (totalValue == valueFromBox)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the section.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private string GetSection(string v, Dlv005DataSet.BD06_ORG_EINHEIT_TBLDataTable table)
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

        /// <summary>
        /// Validates the allocation table.
        /// </summary>
        /// <param name="grid">The grid.</param>
        /// <returns></returns>
        public bool ValidateAllocationTable(DataGridView grid, System.ComponentModel.CancelEventArgs e, ErrorProvider error, string incorect, string allocation, string empty)
        {
            decimal sum = 0;
            if (grid.Rows.Count < 1)
            {
                e.Cancel = true;
                error.SetError(grid, empty);
                return false;
            }

            for (int i = 0; i <= grid.Rows.Count - 1; i++)
            {
                if (grid.Rows[i].Cells[1].Value.ToString() != string.Empty)
                {
                    sum += Convert.ToDecimal(grid.Rows[i].Cells[1].Value);

                    if (grid.Rows[i].Cells[0].Value == null)
                    {
                        e.Cancel = true;
                        error.SetError(grid, incorect);
                        return false;
                    }
                    foreach (char c in grid.Rows[i].Cells[0].Value.ToString())
                    {
                        if (char.IsLetter(c) == false && char.IsDigit(c) == false)
                        {
                            e.Cancel = true;
                            error.SetError(grid, incorect);
                            return false;
                        }
                    }
                    if (grid.Rows[i].Cells[1].Value == null)
                    {
                        e.Cancel = true;
                        error.SetError(grid, incorect);
                        return false;
                    }
                    try
                    {
                        if (((Convert.ToDecimal(grid.Rows[i].Cells[1].Value) >= 100 && Convert.ToDecimal(grid.Rows[i].Cells[1].Value) <= 0)))
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    e.Cancel = true;
                    error.SetError(grid, empty);
                    return false;
                }
            }

            if (sum != 100)
            {
                e.Cancel = true;
                error.SetError(grid, allocation);
                return false;
            }
            e.Cancel = false;
            error.SetError(grid, null);
            return true;
        }

        /// <summary>
        /// Validates the special.
        /// </summary>
        /// <param name="valueFromBox">The value from box.</param>
        /// <param name="sD111_QUALIFIKATIONENSpecial">The s D111 qualifikationen special.</param>
        /// <returns></returns>
        public bool ValidateSpecial(string valueFromBox, Dlv005DataSet.SD111_QUALIFIKATIONENSpecialDataTable sD111_QUALIFIKATIONENSpecial)
        {
            foreach (DataRow row in sD111_QUALIFIKATIONENSpecial)
            {
                if (row["SD111_WERT"].ToString() == valueFromBox)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates the hv.
        /// </summary>
        /// <param name="valueFromBox">The value from box.</param>
        /// <param name="sD111_QUALIFIKATIONENHV">The s D111 qualifikationenhv.</param>
        /// <returns></returns>
        public bool ValidateHV(string valueFromBox, Dlv005DataSet.SD111_QUALIFIKATIONENHVDataTable sD111_QUALIFIKATIONENHV)
        {
            foreach (DataRow row in sD111_QUALIFIKATIONENHV)
            {
                if (row["SD111_WERT"].ToString() == valueFromBox)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates the licence.
        /// </summary>
        /// <param name="valueFromBox">The value from box.</param>
        /// <param name="sD111_QUALIFIKATIONENLicence">The s D111 qualifikationen licence.</param>
        /// <returns></returns>
        public bool ValidateLicence(string valueFromBox, Dlv005DataSet.SD111_QUALIFIKATIONENLicenceDataTable sD111_QUALIFIKATIONENLicence)
        {
            foreach (DataRow row in sD111_QUALIFIKATIONENLicence)
            {
                if (row["SD111_WERT"].ToString() == valueFromBox)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates the sort.
        /// </summary>
        /// <param name="valueFromBox">The value from box.</param>
        /// <param name="dL38_KOMM_ERPROBUNGSORT_TBL">The d L38 komm erprobungsort table.</param>
        /// <returns></returns>
        public bool ValidateSort(string valueFromBox, Dlv005DataSet.DL38_KOMM_ERPROBUNGSORT_TBLDataTable dL38_KOMM_ERPROBUNGSORT_TBL)
        {
            foreach (DataRow row in dL38_KOMM_ERPROBUNGSORT_TBL)
            {
                if (row["DL38_BEZEICHNUNG"].ToString() == valueFromBox)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates the routes.
        /// </summary>
        /// <param name="valueFromBox">The value from box.</param>
        /// <param name="dL39_KOMM_STRECKENART_TBL">The d L39 komm streckenart table.</param>
        /// <returns></returns>
        public bool ValidateRoutes(string valueFromBox, Dlv005DataSet.DL39_KOMM_STRECKENART_TBLDataTable dL39_KOMM_STRECKENART_TBL)
        {
            foreach (DataRow row in dL39_KOMM_STRECKENART_TBL)
            {
                if (row["DL39_BEZEICHNUNG"].ToString() == valueFromBox)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates the kind of testing.
        /// </summary>
        /// <param name="valueFromBox">The value from box.</param>
        /// <param name="dL40_KOMM_ERPROBUNGSART_TBL">The d L40 komm erprobungsart table.</param>
        /// <returns></returns>
        public bool ValidateKindOfTesting(string valueFromBox, Dlv005DataSet.DL40_KOMM_ERPROBUNGSART_TBLDataTable dL40_KOMM_ERPROBUNGSART_TBL)
        {
            foreach (DataRow row in dL40_KOMM_ERPROBUNGSART_TBL)
            {
                if (row["DL40_BEZEICHNUNG"].ToString() == valueFromBox)
                {
                    return true;
                }
            }
            return false;
        }
    }
}