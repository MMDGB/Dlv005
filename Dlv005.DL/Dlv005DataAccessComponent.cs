using MySql.Data.MySqlClient;
using System;

namespace Dlv005.DL
{
    public class Dlv005DataAccessComponent
    {
        //private static string myConnectionString = "server=localhost;uid=root;" +
        //         "pwd=lstoker1;database=dlv005;";

        /// <summary>
        /// My connection string
        /// </summary>
        private static readonly string ConnectionString = "server=localhost;uid=root;" +
                "pwd=nzec3ecz;database=dlv_005;";

        /// <summary>
        /// The connection.
        /// </summary>
        private readonly MySqlConnection Connection = new MySqlConnection(ConnectionString);

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection()
        {
            return Connection;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////  P O P U L A T E     T A B E L S  ////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Populates the allocation.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateAllocation()
        {
            string selectQuerry = "SELECT DL32_KONTIERUNG, DL32_ANTEIL_PROZENT FROM DL32_EXT_KOMM_KONTO";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        /// Populates the basic data table.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateBasicDataTable()
        {
            string selectQuerry = @"SELECT

                                            DL31_KOMM_ANFORDERUNG_ID as 'Id',
			                                DL31_KOMM_ANFORDERUNG_NR as 'TestingNr',
			                                DL31_ERPROBUNGSINHALT as 'TestingContent',

                                            DL31_KOMM__STATUS_ID as 'StatusID',
			                                DL37_BEZEICHNUNG as 'StatusText',

                                            DL31_START_DATUM as 'From',
                                            DL31_ENDE_DATUM as 'To',

                                            DL31_KOMM_ERPROBUNGSORT_ID as 'SortID',
			                                DL38_BEZEICHNUNG as 'SortingTest',

                                            DL31_KOMM_STRECKENART_ID as 'RouteID',
			                                DL39_BEZEICHNUNG as 'RouteOfTEsting',

                                            DL31_KOMM_ERPROBUNGSART_ID as 'KindID',
                                            DL40_BEZEICHNUNG as 'KindOfTesting',

                                            DL31_BAUREIHEN AS 'SeriesText',

                                            DL31_AUFTRAGGEBER_OE as 'CustomerOEID',
			                                Dl.BD06_KURZ_BEZ as 'CustomerOE',

                                            DL31_AUFTRAGGEBER_PERSID as 'CustomerID',
                                            CONCAT(Customer.BD09_NAME,', ',Customer.BD09_VORNAME,', ',Cu.BD06_KURZ_BEZ) as 'Customer',

                                            DL31_FAHRTENLEITER_PERSID as 'ChiefID',
			                                CONCAT(Chief.BD09_NAME,', ',Chief.BD09_VORNAME,', ',Ch.BD06_KURZ_BEZ) as 'Chief',

                                            DL31_ENGINEERING_AST_PERSID as 'EngineeringID',
                                            CONCAT(Engineering.BD09_NAME,', ',Engineering.BD09_VORNAME,', ',E.BD06_KURZ_BEZ) as 'Engineering',

                                            DL31_FAHRBERECHTIGUNG_ID as 'LicenceID',
		 	                                Licence.SD111_WERT as 'Licence',

                                            DL31_HV_QUALIFIKATION_ID as 'HvID',
		 	                                Hv.SD111_WERT as 'Hv',

                                            DL31_SONDERQUALIFIKATION_ID as 'SpecialID',
		 	                                Special.SD111_WERT as 'Special',

                                            DL31_SAMSTAGSARBEIT as 'Saturday',
                                            DL31_SONNTAGSARBEIT as 'Sunday'

                                    FROM DL31_KOMM_ANFORDERUNG
                                            JOIN BD06_ORG_EINHEIT_TBL DL					ON DL31_AUFTRAGGEBER_OE = DL.BD06_OE

                                            JOIN DL38_KOMM_ERPROBUNGSORT_TBL 				ON DL38_KOMM_ERPROBUNGSORT_ID = DL31_KOMM_ERPROBUNGSORT_ID

                                            JOIN DL39_KOMM_STRECKENART_TBL					ON DL39_KOMM_STRECKENART_ID = DL31_KOMM_STRECKENART_ID

                                            JOIN DL40_KOMM_ERPROBUNGSART_TBL 				ON DL40_KOMM_ERPROBUNGSART_ID = DL31_KOMM_ERPROBUNGSART_ID

                                            JOIN DL37_KOMM_STATUS_TBL 						ON DL37_KOMM_STATUS_ID = DL31_KOMM__STATUS_ID

                                            JOIN BD09_PERSON Customer 						ON DL31_AUFTRAGGEBER_PERSID = Customer.BD09_PERSID

                                            JOIN BD06_ORG_EINHEIT_TBL Cu					ON Customer.BD09_OE = Cu.BD06_OE

                                            JOIN BD09_PERSON Chief							ON DL31_FAHRTENLEITER_PERSID = Chief.BD09_PERSID

                                            JOIN BD06_ORG_EINHEIT_TBL Ch				    ON Chief.BD09_OE = Ch.BD06_OE

                                            JOIN BD09_PERSON Engineering					ON DL31_ENGINEERING_AST_PERSID = Engineering.BD09_PERSID

                                            JOIN BD06_ORG_EINHEIT_TBL E						ON Engineering.BD09_OE = E.BD06_OE

                                            JOIN SD111_QUALIFIKATIONEN Licence				ON Licence.SD111_QUALIFIKATIONEN_ID = DL31_FAHRBERECHTIGUNG_ID

                                            JOIN SD111_QUALIFIKATIONEN Hv					ON Hv.SD111_QUALIFIKATIONEN_ID = DL31_HV_QUALIFIKATION_ID

                                            JOIN SD111_QUALIFIKATIONEN Special				ON Special.SD111_QUALIFIKATIONEN_ID = DL31_SONDERQUALIFIKATION_ID ";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        /// Gets the testing nr.
        /// </summary>
        /// <returns></returns>
        public string GetTestingNr()
        {
            string querry = @"SELECT MAX(DL31_KOMM_ANFORDERUNG_NR) FROM DL31_KOMM_ANFORDERUNG";
            string treatment = "";
            using (MySqlCommand command = new MySqlCommand(querry, Connection))
            {
                try
                {
                    using (MySqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            treatment = dr[0].ToString();
                        }
                    }
                }
                catch
                {
                }
            }
            return treatment;
        }

        /// <summary>
        /// Gets the last identifier.
        /// </summary>
        /// <returns></returns>
        public decimal GetLastID()
        {
            string querry = @"SELECT MAX(DL31_KOMM_ANFORDERUNG_ID) FROM DL31_KOMM_ANFORDERUNG";
            decimal treatment = 0;
            using (MySqlCommand command = new MySqlCommand(querry, Connection))
            {
                try
                {
                    using (MySqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            treatment = Convert.ToDecimal(dr[0]);
                        }
                    }
                }
                catch
                {
                }
            }
            return treatment;
        }

        /// <summary>
        /// Populates the DL_31.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateDL31()
        {
            string selectQuerry = @"SELECT
                                        DL31_KOMM_ANFORDERUNG_ID,
                                        DL31_KOMM_ANFORDERUNG_NR ,
                                        DL31_KOMM__STATUS_ID,
                                        DL31_ERPROBUNGSINHALT,
                                        DL31_START_DATUM,
                                        DL31_ENDE_DATUM,
                                        DL31_KOMM_ERPROBUNGSORT_ID,
                                        DL31_KOMM_STRECKENART_ID,
                                        DL31_KOMM_ERPROBUNGSART_ID,
                                        DL31_BAUREIHEN,
                                        DL31_AUFTRAGGEBER_OE,
                                        DL31_AUFTRAGGEBER_PERSID,
                                        DL31_FAHRTENLEITER_PERSID,
                                        DL31_ENGINEERING_AST_PERSID,
                                        DL31_FAHRBERECHTIGUNG_ID,
                                        DL31_HV_QUALIFIKATION_ID,
                                        DL31_SONDERQUALIFIKATION_ID,
                                        DL31_REISE_ART_ID,
                                        DL31_BESPRECHUNG_DATUM,
                                        DL31_BESPRECHUNG_RAUM,

                                        DL31_SAMSTAGSARBEIT,
                                        DL31_SONNTAGSARBEIT
                                  FROM DL31_KOMM_ANFORDERUNG";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        /// Populates the d L32.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateDL32()
        {
            string selectQuerry = @"SELECT
                                        DL32_KOMM_ANFORDERUNG_KONTO_ID,
                                        DL32_KONTIERUNG,
                                        DL32_ANTEIL_PROZENT,
                                        DL32_EXT_KOMM_ANFORDERUNG_ID

                                  FROM DL32_EXT_KOMM_KONTO";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        /// Populates the BD09.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateSD111()
        {
            string selectQuerry = @"SELECT
                                        SD111_QUALIFIKATIONEN_ID,
                                        SD111_WERT,
                                        SD111_TYP
                                  FROM SD111_QUALIFIKATIONEN";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        /// Populates the BD12.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateBD12()
        {
            string selectQuerry = @"SELECT
                                        BD12_BAUREIHE,
                                        BD12_BENENNUNG,
                                        BD12_ENDEDATUM
                                  FROM BD12_BAUREIHE";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        ///   <para></para>
        ///   <para>Populates the BD06.
        /// </para>
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateBD06()
        {
            string selectQuerry = @"SELECT
                                        BD06_KURZ_BEZ,
                                        BD06_OE
                                  FROM BD06_ORG_EINHEIT_TBL";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        ///   <para></para>
        ///   <para>Populates the BD09.
        /// </para>
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateBD09()
        {
            string selectQuerry = @"SELECT
                                        BD09_PERSID,
                                        BD09_OE,
                                        BD09_NAME,
                                        BD09_VORNAME
                                  FROM BD09_PERSON";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        /// Populates the DL38.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateDL38()
        {
            string selectQuerry = @"SELECT
                                        DL38_BEZEICHNUNG,
                                        DL38_KOMM_ERPROBUNGSORT_ID
                                  FROM DL38_KOMM_ERPROBUNGSORT_TBL";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        /// Populates the DL39.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateDL39()
        {
            string selectQuerry = @"SELECT
                                        DL39_BEZEICHNUNG,
                                        DL39_KOMM_STRECKENART_ID
                                  FROM DL39_KOMM_STRECKENART_TBL";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        /// Populates the DL40.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateDL40()
        {
            string selectQuerry = @"SELECT
                                        DL40_BEZEICHNUNG,
                                        DL40_KOMM_ERPROBUNGSART_ID
                                  FROM DL40_KOMM_ERPROBUNGSART_TBL";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        /// <summary>
        /// Populates the DL37.
        /// </summary>
        /// <returns></returns>
        public MySqlDataAdapter PopulateDL37()
        {
            string selectQuerry = @"SELECT
                                        DL37_KOMM_STATUS_ID,
                                        DL37_BEZEICHNUNG
                                  FROM DL37_KOMM_STATUS_TBL";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return adapter;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////  U P D A T E  //////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Updates the DL31.
        /// </summary>
        /// <returns></returns>
        public MySqlCommand UpdateDL31()
        {
            string updateQuerry = @"UPDATE `dlv_005`.`DL31_KOMM_ANFORDERUNG`
                                       SET
                                            DL31_KOMM_ANFORDERUNG_NR = @TestingNr,
                                            DL31_KOMM__STATUS_ID = (SELECT DL37_KOMM_STATUS_ID  FROM DL37_KOMM_STATUS_TBL  WHERE  DL37_BEZEICHNUNG = @Status),
                                            DL31_ERPROBUNGSINHALT = @TestingContent,
                                            DL31_START_DATUM = @FromDateTime,
                                            DL31_ENDE_DATUM = @ToDateTime,
                                            DL31_KOMM_ERPROBUNGSORT_ID = @Sort,
                                            DL31_KOMM_STRECKENART_ID =@Route,
                                            DL31_KOMM_ERPROBUNGSART_ID =@Kind,
                                            DL31_BAUREIHEN =  @Series,
                                            DL31_AUFTRAGGEBER_OE =  @CustomerOE,
                                            DL31_AUFTRAGGEBER_PERSID = @Customer,
                                            DL31_FAHRTENLEITER_PERSID =	@Chief,
                                            DL31_ENGINEERING_AST_PERSID = @Engineering,
                                            DL31_FAHRBERECHTIGUNG_ID = @Licence,
                                            DL31_HV_QUALIFIKATION_ID = @Hv,
                                            DL31_SONDERQUALIFIKATION_ID = @Special,
                                            DL31_SAMSTAGSARBEIT = @Saturday,
                                            DL31_SONNTAGSARBEIT = @Sunday

                                       WHERE DL31_KOMM_ANFORDERUNG_ID = @Id";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(updateQuerry, Connection);
            adapter.SelectCommand = command;
            return command;
        }

        public MySqlCommand UpdateDL32()
        {
            string updateQuerry = @"UPDATE `dlv_005`.`DL32_EXT_KOMM_KONTO`
                                       SET
                                            DL32_KONTIERUNG = @Text,
                                            DL32_ANTEIL_PROZENT = @Procentage

                                       WHERE DL32_KOMM_ANFORDERUNG_KONTO_ID = @Id";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(updateQuerry, Connection);
            adapter.SelectCommand = command;
            return command;
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <returns></returns>
        public MySqlCommand UpdateStatus()
        {
            string updateQuerry = @"UPDATE `dlv_005`.`DL31_KOMM_ANFORDERUNG`
                                       SET
                                            DL31_KOMM__STATUS_ID = @StatusId
                                       WHERE DL31_KOMM_ANFORDERUNG_ID = @Id";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(updateQuerry, Connection);
            adapter.SelectCommand = command;
            return command;
        }

        /// <summary>
        /// Updates the testing nr.
        /// </summary>
        /// <returns></returns>
        public MySqlCommand UpdateTestingNr()
        {
            string updateQuerry = @"UPDATE `dlv_005`.`DL31_KOMM_ANFORDERUNG`
                                       SET
                                     DL31_KOMM_ANFORDERUNG_NR = @TestingNr
                                       WHERE DL31_KOMM_ANFORDERUNG_ID = @Id";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(updateQuerry, Connection);
            adapter.SelectCommand = command;
            return command;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////  D E L E T E  //////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns></returns>
        public MySqlCommand Delete()
        {
            string deleteQuerry = @"
                                    START TRANSACTION;
                                        DELETE FROM DL31_KOMM_ANFORDERUNG
                                            WHERE DL31_KOMM_ANFORDERUNG.DL31_KOMM_ANFORDERUNG_ID = @Id;
                                    COMMIT;";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(deleteQuerry, Connection);
            adapter.SelectCommand = command;
            return command;
        }

        public MySqlCommand DeleteDL32()
        {
            string deleteQuerry = @"
                                    START TRANSACTION;
                                        DELETE FROM DL32_EXT_KOMM_KONTO
                                            WHERE
                                        DL32_KOMM_ANFORDERUNG_KONTO_ID = @Id;
                                    COMMIT;";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(deleteQuerry, Connection);
            adapter.SelectCommand = command;
            return command;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////  I N S E R T  //////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Inserts the in d L31.
        /// </summary>
        /// <returns></returns>
        public MySqlCommand InsertInDL31()
        {
            string selectQuerry = @"INSERT INTO `dlv_005`.`DL31_KOMM_ANFORDERUNG`
                                                (
                                                    `DL31_KOMM_ANFORDERUNG_NR`,
                                                    `DL31_KOMM__STATUS_ID`,
                                                    `DL31_ERPROBUNGSINHALT`,
                                                    `DL31_START_DATUM`,
                                                    `DL31_ENDE_DATUM`,
                                                    `DL31_KOMM_ERPROBUNGSORT_ID`,
                                                    `DL31_KOMM_STRECKENART_ID`,
                                                    `DL31_KOMM_ERPROBUNGSART_ID`,
                                                    `DL31_BAUREIHEN`,
                                                    `DL31_AUFTRAGGEBER_OE`,
                                                    `DL31_AUFTRAGGEBER_PERSID`,
                                                    `DL31_FAHRTENLEITER_PERSID`,
                                                    `DL31_ENGINEERING_AST_PERSID`,
                                                    `DL31_FAHRBERECHTIGUNG_ID`,
                                                    `DL31_HV_QUALIFIKATION_ID`,
                                                    `DL31_SONDERQUALIFIKATION_ID`,
                                                    `DL31_SAMSTAGSARBEIT`,
                                                    `DL31_SONNTAGSARBEIT`)
                                                VALUES(
                                                    @TestingNr,
                                                    @Status,
                                                    @TestingContent,
                                                    @FromDateTime,
                                                    @ToDateTime,
                                                    @Sort,
                                                    @Route,
                                                    @Kind,
                                                    @Series,
                                                    @CustomerOE,
                                                    @Customer,
													@Chief,
													@Engineering,
                                                    @Licence,
                                                    @Hv,
                                                    @Special,
                                                    @Saturday,
                                                    @Sunday
                                                );";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return command;
        }

        /// <summary>
        /// Inserts the in DL32.
        /// </summary>
        /// <returns></returns>
        public MySqlCommand InsertInDL32()
        {
            string selectQuerry = @"INSERT INTO `dlv_005`.`DL32_EXT_KOMM_KONTO`
                                                (
                                                `DL32_KONTIERUNG`,
                                                `DL32_ANTEIL_PROZENT`,
                                                `DL32_EXT_KOMM_ANFORDERUNG_ID`
                                                )
                                                VALUES
                                                (
                                                    @Text,
                                                    @Procentage,
                                                    @Id
                                                );";

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(selectQuerry, Connection);
            adapter.SelectCommand = command;
            return command;
        }
    }
}