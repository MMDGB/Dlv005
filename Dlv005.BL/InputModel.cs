using System;
using System.Data;

namespace Dlv005.BL
{
    internal class InputModel
    {
        public string TestingNr;
        public string TestingContent;
        public DateTime To;
        public DateTime From;

        public string StatusString;
        public string StatusID;

        public string CustomerString;
        public decimal CustomerID;

        public string ChiefString;
        public decimal CheifID;

        public string EngineeringString;
        public decimal EngineeringID;

        public string SortString;
        public decimal SortID;

        public string RouteString;
        public decimal RouteID;

        public string KindString;
        public decimal KindID;

        public string SeriesString;
        public string SeriesID;

        public string SpecialString;
        public decimal SpecialID;

        public string HvString;
        public decimal HvID;

        public string LicenceString;
        public decimal LicenceID;

        public string CustomerOEString;
        public decimal CustomerOEID;

        public string Saturday;
        public string Sunday;

        public InputModel(Dlv005DataSet DataSet, DataRow row, string action)
        {
            int lastRow = DataSet.BasicDataTable.Rows.Count - 1;

            DataRow lrow = DataSet.BasicDataTable.Rows[lastRow];

            switch (action)
            {
                case "New":

                    TestingContent = row["TestingContent"].ToString();

                    To = Convert.ToDateTime(row["To"]);

                    From = Convert.ToDateTime(row["From"]);

                    TestingNr = row["TestingNr"].ToString();

                    StatusString = row["StatusText"].ToString();

                    StatusID = row["StatusID"].ToString();

                    Saturday = row["Saturday"].ToString();

                    Sunday = row["Sunday"].ToString();

                    CustomerOEString = (lrow["CustomerOE"].ToString() == string.Empty) ? row["CustomerOE"].ToString() : lrow["CustomerOE"].ToString();
                    CustomerOEID = (lrow["CustomerOEID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["CustomerOEID"].ToString()) : Convert.ToDecimal(lrow["CustomerOEID"].ToString());

                    CustomerString = (lrow["Customer"].ToString() == string.Empty) ? row["Customer"].ToString() : lrow["Customer"].ToString();
                    CustomerID = (lrow["CustomerID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["CustomerID"].ToString()) : Convert.ToDecimal(lrow["CustomerID"].ToString());

                    ChiefString = (lrow["Chief"].ToString() == string.Empty) ? row["Chief"].ToString() : lrow["Chief"].ToString();
                    CheifID = (lrow["ChiefID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["ChiefID"].ToString()) : Convert.ToDecimal(lrow["ChiefID"].ToString());

                    EngineeringString = (lrow["Engineering"].ToString() == string.Empty) ? row["Engineering"].ToString() : lrow["Engineering"].ToString();
                    EngineeringID = (lrow["EngineeringID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["EngineeringID"].ToString()) : Convert.ToDecimal(lrow["EngineeringID"].ToString());

                    SortString = (lrow["SortingTest"].ToString() == string.Empty) ? row["SortingTest"].ToString() : lrow["SortingTest"].ToString();
                    SortID = (lrow["SortID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["SortID"].ToString()) : Convert.ToDecimal(lrow["SortID"].ToString());

                    RouteString = (lrow["RouteOfTesting"].ToString() == string.Empty) ? row["RouteOfTesting"].ToString() : lrow["RouteOfTesting"].ToString();
                    RouteID = (lrow["RouteID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["RouteID"].ToString()) : Convert.ToDecimal(lrow["RouteID"].ToString());

                    KindString = (lrow["KindOfTesting"].ToString() == string.Empty) ? row["KindOfTesting"].ToString() : lrow["KindOfTesting"].ToString();
                    KindID = (lrow["KindID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["KindID"].ToString()) : Convert.ToDecimal(lrow["KindID"].ToString());

                    SeriesString = (lrow["SeriesText"].ToString() == string.Empty) ? row["SeriesText"].ToString() : lrow["SeriesText"].ToString();
                    SeriesID = (lrow["SeriesText"].ToString() == string.Empty) ?
                        row["SeriesText"].ToString() : lrow["SeriesText"].ToString();

                    SpecialString = (lrow["Special"].ToString() == string.Empty) ? row["Special"].ToString() : lrow["Special"].ToString();
                    SpecialID = (lrow["SpecialID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["SpecialID"].ToString()) : Convert.ToDecimal(lrow["SpecialID"].ToString());

                    HvString = (lrow["Hv"].ToString() == string.Empty) ? row["Hv"].ToString() : lrow["Hv"].ToString();
                    HvID = (lrow["HvID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["HvID"].ToString()) : Convert.ToDecimal(lrow["HvID"].ToString());

                    LicenceString = (lrow["Licence"].ToString() == string.Empty) ? row["Licence"].ToString() : lrow["Licence"].ToString();
                    LicenceID = (lrow["LicenceID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["LicenceID"].ToString()) : Convert.ToDecimal(lrow["LicenceID"].ToString());
                    break;

                case "Copy":

                    TestingContent = lrow["TestingContent"].ToString();

                    To = Convert.ToDateTime(lrow["To"]);

                    From = Convert.ToDateTime(lrow["From"]);

                    TestingNr = lrow["TestingNr"].ToString();

                    StatusString = lrow["StatusText"].ToString();

                    StatusID = lrow["StatusID"].ToString();

                    Saturday = row["Saturday"].ToString();

                    Sunday = row["Sunday"].ToString();
                    CustomerOEString = (lrow["CustomerOE"].ToString() == string.Empty) ? row["CustomerOE"].ToString() : lrow["CustomerOE"].ToString();
                    CustomerOEID = (lrow["CustomerOEID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["CustomerOEID"].ToString()) : Convert.ToDecimal(lrow["CustomerOEID"].ToString());

                    CustomerString = (lrow["Customer"].ToString() == string.Empty) ? row["Customer"].ToString() : lrow["Customer"].ToString();
                    CustomerID = (lrow["CustomerID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["CustomerID"].ToString()) : Convert.ToDecimal(lrow["CustomerID"].ToString());

                    ChiefString = (lrow["Chief"].ToString() == string.Empty) ? row["Chief"].ToString() : lrow["Chief"].ToString();
                    CheifID = (lrow["ChiefID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["ChiefID"].ToString()) : Convert.ToDecimal(lrow["ChiefID"].ToString());

                    EngineeringString = (lrow["Engineering"].ToString() == string.Empty) ? row["Engineering"].ToString() : lrow["Engineering"].ToString();
                    EngineeringID = (lrow["EngineeringID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["EngineeringID"].ToString()) : Convert.ToDecimal(lrow["EngineeringID"].ToString());

                    SortString = (lrow["SortingTest"].ToString() == string.Empty) ? row["SortingTest"].ToString() : lrow["SortingTest"].ToString();
                    SortID = (lrow["SortID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["SortID"].ToString()) : Convert.ToDecimal(lrow["SortID"].ToString());

                    RouteString = (lrow["RouteOfTesting"].ToString() == string.Empty) ? row["RouteOfTesting"].ToString() : lrow["RouteOfTesting"].ToString();
                    RouteID = (lrow["RouteID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["RouteID"].ToString()) : Convert.ToDecimal(lrow["RouteID"].ToString());

                    KindString = (lrow["KindOfTesting"].ToString() == string.Empty) ? row["KindOfTesting"].ToString() : lrow["KindOfTesting"].ToString();
                    KindID = (lrow["KindID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["KindID"].ToString()) : Convert.ToDecimal(lrow["KindID"].ToString());

                    SeriesString = (lrow["SeriesText"].ToString() == string.Empty) ? row["SeriesText"].ToString() : lrow["SeriesText"].ToString();
                    SeriesID = (lrow["SeriesText"].ToString() == string.Empty) ?
                        row["SeriesText"].ToString() : lrow["SeriesText"].ToString();

                    SpecialString = (lrow["Special"].ToString() == string.Empty) ? row["Special"].ToString() : lrow["Special"].ToString();
                    SpecialID = (lrow["SpecialID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["SpecialID"].ToString()) : Convert.ToDecimal(lrow["SpecialID"].ToString());

                    HvString = (lrow["Hv"].ToString() == string.Empty) ? row["Hv"].ToString() : lrow["Hv"].ToString();
                    HvID = (lrow["HvID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["HvID"].ToString()) : Convert.ToDecimal(lrow["HvID"].ToString());

                    LicenceString = (lrow["Licence"].ToString() == string.Empty) ? row["Licence"].ToString() : lrow["Licence"].ToString();
                    LicenceID = (lrow["LicenceID"].ToString() == string.Empty) ?
                        Convert.ToDecimal(row["LicenceID"].ToString()) : Convert.ToDecimal(lrow["LicenceID"].ToString());
                    break;

                case "Update":

                    TestingContent = row["TestingContent"].ToString();

                    To = Convert.ToDateTime(row["To"]);

                    From = Convert.ToDateTime(row["From"]);

                    TestingNr = row["TestingNr"].ToString();

                    StatusString = row["StatusText"].ToString();

                    StatusID = row["StatusID"].ToString();

                    Saturday = row["Saturday"].ToString();

                    Sunday = row["Sunday"].ToString();

                    CustomerOEString = row["CustomerOE"].ToString();
                    CustomerOEID = Convert.ToDecimal(row["CustomerOEID"].ToString());

                    CustomerString = row["Customer"].ToString();
                    CustomerID = Convert.ToDecimal(row["CustomerID"].ToString());

                    ChiefString = row["Chief"].ToString();
                    CheifID = Convert.ToDecimal(row["ChiefID"].ToString());

                    EngineeringString = row["Engineering"].ToString();
                    EngineeringID = Convert.ToDecimal(row["EngineeringID"].ToString());

                    SortString = row["SortingTest"].ToString();
                    SortID = Convert.ToDecimal(row["SortID"].ToString());

                    RouteString = row["RouteOfTesting"].ToString();
                    RouteID = Convert.ToDecimal(row["RouteID"].ToString());

                    KindString = row["KindOfTesting"].ToString();
                    KindID = Convert.ToDecimal(row["KindID"].ToString());

                    SeriesString = row["SeriesText"].ToString();
                    SeriesID = row["SeriesText"].ToString();

                    SpecialString = row["Special"].ToString();
                    SpecialID = Convert.ToDecimal(row["SpecialID"].ToString());

                    HvString = row["Hv"].ToString();
                    HvID = Convert.ToDecimal(row["HvID"].ToString());

                    LicenceString = row["Licence"].ToString();
                    LicenceID = Convert.ToDecimal(row["LicenceID"].ToString());

                    break;
            }
        }
    }
}