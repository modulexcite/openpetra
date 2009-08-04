﻿/*************************************************************************
 *
 * DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
 *
 * @Authors:
 *       christiank
 *
 * Copyright 2004-2009 by OM International
 *
 * This file is part of OpenPetra.org.
 *
 * OpenPetra.org is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * OpenPetra.org is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
 *
 ************************************************************************/
using System;
using System.Data;
using Ict.Petra.Shared.MPartner;
using Ict.Petra.Shared.MPartner.Mailroom.Data;
using Ict.Petra.Shared.MPartner.Partner.Data;
using System.Text;
using System.Windows.Forms;
using Ict.Common;
using Ict.Common.Data;

namespace Ict.Petra.Shared.MPartner
{
    /// <summary>
    /// Contains functions to be used by the Server and the Client that perform
    /// certain calculations - specific for the Partner Module.
    /// </summary>
    public class Calculations
    {
        /// <summary>
        /// column name for best address
        /// </summary>
        public const String PARTNERLOCATION_BESTADDR_COLUMN = "BestAddress";

        /// <summary>
        /// column name for the location icon
        /// </summary>
        public const String PARTNERLOCATION_ICON_COLUMN = "Icon";

        /// <summary>
        /// message for when no information is available
        /// </summary>
        public const String StrNoNameInfoAvailable = "  No name information available";

        /// <summary>
        /// Specifies how to format the String that is returned by Method
        /// <see cref="DetermineLocationString(PLocationRow, TPartnerLocationFormatEnum)" />.
        /// </summary>
        public enum TPartnerLocationFormatEnum
        {
            /// <summary>Return Location Part Strings separated by comma</summary>
            plfCommaSeparated,

            /// <summary>Return Location Part Strings separated by CR+LF</summary>
            plfLineBreakSeparated
        }

        /// <summary>
        /// check the validity of each location and update the icon for each location (current address, old address, future address)
        /// </summary>
        /// <param name="APartnerLocationsDS">the dataset with the locations</param>
        public static void DeterminePartnerLocationsDateStatus(DataSet APartnerLocationsDS)
        {
            DataTable ProcessDT;

            if ((APartnerLocationsDS is PartnerEditTDS)
                || (APartnerLocationsDS.Tables.Contains(TTypedDataTable.GetTableName(PPartnerLocationTable.TableId)) == true))
            {
                ProcessDT = APartnerLocationsDS.Tables[TTypedDataTable.GetTableName(PPartnerLocationTable.TableId)];
            }
            else
            {
                ProcessDT = APartnerLocationsDS.Tables["PartnerLocation"];
            }

            DeterminePartnerLocationsDateStatus(ProcessDT);
        }

        /// <summary>
        /// check the validity of each location and update the icon of each location (current address, old address, future address)
        /// </summary>
        /// <param name="APartnerLocationsDT">the datatable to check</param>
        public static void DeterminePartnerLocationsDateStatus(DataTable APartnerLocationsDT)
        {
            System.DateTime pDateEffective;
            System.DateTime pDateGoodUntil;
            System.DateTime pDateToday;
            pDateToday = (DateTime.Today).Date;

            /*
             *  Add custom DataColumn if its not part of the DataTable yet
             */
            if (!APartnerLocationsDT.Columns.Contains(PARTNERLOCATION_ICON_COLUMN))
            {
                APartnerLocationsDT.Columns.Add(new System.Data.DataColumn(PARTNERLOCATION_ICON_COLUMN, typeof(Int32)));
            }

            /*
             * Loop over all DataRows and determine their 'Date Status'. The result is then
             * stored in the 'Icon' DataColumn.
             */
            foreach (DataRow pRow in APartnerLocationsDT.Rows)
            {
                if (pRow.RowState != DataRowState.Deleted)
                {
                    pDateEffective = TSaveConvert.ObjectToDate(pRow[PPartnerLocationTable.GetDateEffectiveDBName()]);
                    pDateGoodUntil = TSaveConvert.ObjectToDate(
                        pRow[PPartnerLocationTable.GetDateGoodUntilDBName()], TNullHandlingEnum.nhReturnHighestDate);

                    // Current Address: Icon = 1,
                    // Future Address:  Icon = 2,
                    // Expired Address: Icon = 3.
                    if ((pDateEffective <= pDateToday) && ((pDateGoodUntil >= pDateToday) || (pDateGoodUntil == new DateTime(9999, 12, 31))))
                    {
                        pRow[PartnerEditTDSPPartnerLocationTable.GetIconDBName()] = ((object)1);
                    }
                    else if (pDateEffective > pDateToday)
                    {
                        pRow[PartnerEditTDSPPartnerLocationTable.GetIconDBName()] = ((object)2);
                    }
                    else
                    {
                        pRow[PartnerEditTDSPPartnerLocationTable.GetIconDBName()] = ((object)3);
                    }
                }
            }
        }

        /// <summary>
        /// find which address is the best, and mark it the column BestAddress
        /// </summary>
        /// <param name="APartnerLocationsDS">the dataset with the addresses</param>
        /// <returns></returns>
        public static TLocationPK DetermineBestAddress(DataSet APartnerLocationsDS)
        {
            DataTable ProcessDT;

            if ((APartnerLocationsDS is PartnerEditTDS)
                || (APartnerLocationsDS.Tables.Contains(TTypedDataTable.GetTableName(PPartnerLocationTable.TableId)) == true))
            {
                ProcessDT = APartnerLocationsDS.Tables[TTypedDataTable.GetTableName(PPartnerLocationTable.TableId)];
            }
            else
            {
                ProcessDT = APartnerLocationsDS.Tables["PartnerLocation"];
            }

            return DetermineBestAddress(ProcessDT);
        }

        /// <summary>
        /// find which address is the best, and mark it the column BestAddress
        /// </summary>
        /// <param name="APartnerLocationsDT">the datatable with the addresses</param>
        /// <returns></returns>
        public static TLocationPK DetermineBestAddress(DataTable APartnerLocationsDT)
        {
            TLocationPK ReturnValue;

            DataRow[] OrderedRows;
            System.Int32 CurrentRow;
            System.Int32 BestRow;
            System.Int16 FirstRowAddrOrder;
            bool FirstRowMailingAddress;
            System.DateTime BestRowDate;
            System.DateTime TempDate;
            CurrentRow = 0;
            BestRow = 0;

#if DEBUGMODE
            if (TSrvSetting.DL >= 8)
            {
                Console.WriteLine("Calculations.DetermineBestAddress: processing " + APartnerLocationsDT.Rows.Count.ToString() + " rows...");
            }
#endif

            /*
             *  Add custom DataColumn if its not part of the DataTable yet
             */
            if (!APartnerLocationsDT.Columns.Contains(PARTNERLOCATION_BESTADDR_COLUMN))
            {
                APartnerLocationsDT.Columns.Add(new System.Data.DataColumn(PARTNERLOCATION_BESTADDR_COLUMN, typeof(Boolean)));
            }

            /*
             * Order tables' rows: first all records with p_send_mail_l = true, these are ordered
             * ascending by Icon, then all records with p_send_mail_l = false, these are ordered
             * ascending by Icon.
             */
            OrderedRows = APartnerLocationsDT.Select("",
                PPartnerLocationTable.GetSendMailDBName() + " DESC, " + PartnerEditTDSPPartnerLocationTable.GetIconDBName() + " ASC",
                DataViewRowState.CurrentRows);

            if (OrderedRows.Length > 1)
            {
                FirstRowAddrOrder = Convert.ToInt16(OrderedRows[0][PartnerEditTDSPPartnerLocationTable.GetIconDBName()]);
                FirstRowMailingAddress = Convert.ToBoolean(OrderedRows[0][PPartnerLocationTable.GetSendMailDBName()]);

                // determine pBestRowDate
                if (FirstRowAddrOrder != 3)
                {
                    BestRowDate = TSaveConvert.ObjectToDate(OrderedRows[CurrentRow][PPartnerLocationTable.GetDateEffectiveDBName()]);
                }
                else
                {
                    BestRowDate = TSaveConvert.ObjectToDate(OrderedRows[CurrentRow][PPartnerLocationTable.GetDateGoodUntilDBName()]);
                }

                // iterate through the sorted rows
                for (CurrentRow = 0; CurrentRow <= OrderedRows.Length - 1; CurrentRow += 1)
                {
                    // reset any row that might have been marked as 'best' before
                    OrderedRows[CurrentRow][PartnerEditTDSPPartnerLocationTable.GetBestAddressDBName()] = ((object)0);

                    // determine pTempDate
                    if (FirstRowAddrOrder != 3)
                    {
                        TempDate = TSaveConvert.ObjectToDate(OrderedRows[CurrentRow][PPartnerLocationTable.GetDateEffectiveDBName()]);
                    }
                    else
                    {
                        TempDate = TSaveConvert.ObjectToDate(OrderedRows[CurrentRow][PPartnerLocationTable.GetDateGoodUntilDBName()]);
                    }

                    // still the same ADDR_ORDER than the ADDR_ORDER of the first row and
                    // still the same Mailing Address than the Mailing Address flag of the first row > proceed
                    if ((Convert.ToInt16(OrderedRows[CurrentRow][PartnerEditTDSPPartnerLocationTable.GetIconDBName()]) == FirstRowAddrOrder)
                        && (Convert.ToBoolean(OrderedRows[CurrentRow][PPartnerLocationTable.GetSendMailDBName()]) == FirstRowMailingAddress))
                    {
                        switch (FirstRowAddrOrder)
                        {
                            case 1:
                            case 3:

                                // find the Row with the highest p_date_effective_d (or p_date_good_until_d) date
                                if (TempDate > BestRowDate)
                                {
                                    BestRowDate = TempDate;
                                    BestRow = CurrentRow;
                                }

                                break;

                            case 2:

                                // find the Row with the lowest p_date_effective_d date
                                if (TempDate < BestRowDate)
                                {
                                    BestRowDate = TempDate;
                                    BestRow = CurrentRow;
                                }

                                break;
                        }
                    }
                }

                // mark the location that was determined to be the 'best'
                OrderedRows[BestRow][PartnerEditTDSPPartnerLocationTable.GetBestAddressDBName()] = ((object)1);
                ReturnValue =
                    new TLocationPK(Convert.ToInt64(OrderedRows[BestRow][PLocationTable.GetSiteKeyDBName()]),
                        Convert.ToInt32(OrderedRows[BestRow][PLocationTable.GetLocationKeyDBName()]));
            }
            else
            {
                if (OrderedRows.Length == 1)
                {
                    // mark the only location to be the 'best'
                    OrderedRows[0][PartnerEditTDSPPartnerLocationTable.GetBestAddressDBName()] = ((object)1);
                }

                ReturnValue = new TLocationPK(-1, -1);
            }

            return ReturnValue;
        }

        /// <summary>
        /// format the shortname for a partner in a standardized way
        /// </summary>
        /// <param name="AName">surname of partner</param>
        /// <param name="ATitle">title</param>
        /// <param name="AFirstName">first name</param>
        /// <param name="AMiddleName">middle name</param>
        /// <returns>formatted shortname</returns>
        public static String DeterminePartnerShortName(String AName, String ATitle, String AFirstName, String AMiddleName)
        {
            String ShortName = "";

            try
            {
                if (AName.Trim().Length > 0)
                {
                    ShortName = AName.Trim();
                }

                if (AFirstName.Trim().Length > 0)
                {
                    ShortName = ShortName + ", " + AFirstName.Trim();
                }

                if (AMiddleName.Trim().Length > 0)
                {
                    ShortName = ShortName + ' ' + AMiddleName.Trim().Substring(0, 1);
                }

                if (ATitle.Trim().Length > 0)
                {
                    ShortName = ShortName + ", " + ATitle.Trim();
                }

                if (ShortName.Length == 0)
                {
                    ShortName = StrNoNameInfoAvailable;
                }
                else
                {
                    if (ShortName.Length > PPartnerTable.GetPartnerShortNameLength())
                    {
                        ShortName = ShortName.Substring(0, PPartnerTable.GetPartnerShortNameLength());
                    }
                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("Exception occured in DeterminePartnerShortName: " + Exp.ToString());
            }
            return ShortName;
        }

        /// <summary>
        /// overload for DeterminePartnerShortName, no middle name
        /// </summary>
        /// <param name="AName">surname</param>
        /// <param name="ATitle">title</param>
        /// <param name="AFirstName">firstname</param>
        /// <returns></returns>
        public static String DeterminePartnerShortName(String AName, String ATitle, String AFirstName)
        {
            return DeterminePartnerShortName(AName, ATitle, AFirstName, "");
        }

        /// <summary>
        /// overload for DeterminePartnerShortName, no middle name and no first name
        /// </summary>
        /// <param name="AName">surname</param>
        /// <param name="ATitle">title</param>
        /// <returns></returns>
        public static String DeterminePartnerShortName(String AName, String ATitle)
        {
            return DeterminePartnerShortName(AName, ATitle, "", "");
        }

        /// <summary>
        /// overload for DeterminePartnerShortName, no title, firstname and middle name
        /// </summary>
        /// <param name="AName">surname</param>
        /// <returns></returns>
        public static String DeterminePartnerShortName(String AName)
        {
            return DeterminePartnerShortName(AName, "", "", "");
        }

        /// <summary>
        /// Builds a formatted String out of the data that is contained in a Location.
        /// </summary>
        /// <param name="ALocationDR">DataRow containing the Location data.</param>
        /// <returns>Formatted String.</returns>
        public static String DetermineLocationString(PLocationRow ALocationDR)
        {
            return DetermineLocationString(ALocationDR, TPartnerLocationFormatEnum.plfLineBreakSeparated);
        }

        /// <summary>
        /// Builds a formatted String out of the data that is contained in a Location.
        /// </summary>
        /// <param name="ALocationDR">DataRow containing the Location data.</param>
        /// <param name="APartnerLocationStringFormat">Specifies how to format the String that is returned.</param>
        /// <returns>Formatted String.</returns>
        public static String DetermineLocationString(PLocationRow ALocationDR,
            TPartnerLocationFormatEnum APartnerLocationStringFormat)
        {
            return DetermineLocationString(ALocationDR.Building1,
                ALocationDR.Building2,
                ALocationDR.Locality,
                ALocationDR.StreetName,
                ALocationDR.Address3,
                ALocationDR.Suburb,
                ALocationDR.City,
                ALocationDR.County,
                ALocationDR.PostalCode,
                ALocationDR.CountryCode,
                APartnerLocationStringFormat);
        }

        /// <summary>
        /// overload
        /// </summary>
        /// <param name="ABuilding1"></param>
        /// <param name="ABuilding2"></param>
        /// <param name="ALocality"></param>
        /// <param name="AStreetName"></param>
        /// <param name="AAddress3"></param>
        /// <param name="ASuburb"></param>
        /// <param name="ACity"></param>
        /// <param name="ACounty"></param>
        /// <param name="APostalCode"></param>
        /// <param name="ACountryCode"></param>
        /// <returns></returns>
        public static String DetermineLocationString(String ABuilding1,
            String ABuilding2,
            String ALocality,
            String AStreetName,
            String AAddress3,
            String ASuburb,
            String ACity,
            String ACounty,
            String APostalCode,
            String ACountryCode)
        {
            return DetermineLocationString(ABuilding1,
                ABuilding2,
                ALocality,
                AStreetName,
                AAddress3,
                ASuburb,
                ACity,
                ACounty,
                APostalCode,
                ACountryCode,
                TPartnerLocationFormatEnum.plfLineBreakSeparated);
        }

        /// <summary>
        /// Builds a formatted String out of the data that is contained in a Location.
        /// </summary>
        /// <param name="ABuilding1">building name 1</param>
        /// <param name="ABuilding2">building name 2</param>
        /// <param name="ALocality">locality</param>
        /// <param name="AStreetName">street name</param>
        /// <param name="AAddress3">address 3</param>
        /// <param name="ASuburb">suburb</param>
        /// <param name="ACity">city</param>
        /// <param name="ACounty">county</param>
        /// <param name="APostalCode">postal code</param>
        /// <param name="ACountryCode">country code</param>
        /// <param name="PartnerLocationStringFormat">requested format</param>
        /// <returns>formatted string</returns>
        public static String DetermineLocationString(String ABuilding1,
            String ABuilding2,
            String ALocality,
            String AStreetName,
            String AAddress3,
            String ASuburb,
            String ACity,
            String ACounty,
            String APostalCode,
            String ACountryCode,
            TPartnerLocationFormatEnum PartnerLocationStringFormat)
        {
            String ReturnValue;
            String Separator;
            StringBuilder SBuilder;

            switch (PartnerLocationStringFormat)
            {
                case TPartnerLocationFormatEnum.plfCommaSeparated:
                    Separator = ", ";
                    break;

                case TPartnerLocationFormatEnum.plfLineBreakSeparated:
                    Separator = Environment.NewLine;
                    break;

                default:
                    Separator = Environment.NewLine;
                    break;
            }

            SBuilder = new StringBuilder(200);

            if (ABuilding1 != null)
            {
                if (ABuilding1 != "")
                {
                    SBuilder.Append(ABuilding1 + Separator);
                }
            }

            if (ABuilding2 != null)
            {
                if (ABuilding2 != "")
                {
                    SBuilder.Append(ABuilding2 + Separator);
                }
            }

            if (ALocality != null)
            {
                if (ALocality != "")
                {
                    SBuilder.Append(ALocality + Separator);
                }
            }

            if (AStreetName != null)
            {
                if (AStreetName != "")
                {
                    SBuilder.Append(AStreetName + Separator);
                }
            }

            if (AAddress3 != null)
            {
                if (AAddress3 != "")
                {
                    SBuilder.Append(AAddress3 + Separator);
                }
            }

            if (ASuburb != null)
            {
                if (ASuburb != "")
                {
                    SBuilder.Append(ASuburb + Separator);
                }
            }

            if (ACity != null)
            {
                if (ACity != "")
                {
                    SBuilder.Append(ACity + Separator);
                }
            }

            if (ACounty != null)
            {
                if (ACounty != "")
                {
                    SBuilder.Append(ACounty + Separator);
                }
            }

            if (APostalCode != null)
            {
                if (APostalCode != "")
                {
                    SBuilder.Append(APostalCode + Separator);
                }
            }

            if (ACountryCode != null)
            {
                if (ACountryCode != "")
                {
                    SBuilder.Append(ACountryCode + Separator);
                }
            }

            // Get the String that contains the concatenated subStrings
            ReturnValue = SBuilder.ToString();

            // Remove last Separator if the Result has them
            if (ReturnValue.Length > Separator.Length)
            {
                ReturnValue = ReturnValue.Substring(0, ReturnValue.Length - Separator.Length);
            }

            return ReturnValue;
        }

        /// <summary>
        /// get the current address from a location table
        /// </summary>
        /// <param name="ATable">table with locations</param>
        /// <returns>data view containing the current address</returns>
        public static DataView DetermineCurrentAddresses(PPartnerLocationTable ATable)
        {
            return new DataView(ATable, "((" + PPartnerLocationTable.GetDateEffectiveDBName() + " <= #" + DateTime.Now.Date.ToString(
                    "MM/dd/yyyy") + "# OR " + PPartnerLocationTable.GetDateEffectiveDBName() + " IS NULL) AND (" +
                PPartnerLocationTable.GetDateGoodUntilDBName() + " >= #" + DateTime.Now.Date.ToString(
                    "MM/dd/yyyy") + "# OR " + PPartnerLocationTable.GetDateGoodUntilDBName() + " IS NULL))", "", DataViewRowState.CurrentRows);
        }

        /// <summary>
        /// count the available current addresses and the total number of addresses
        /// </summary>
        /// <param name="ATable">table with locations</param>
        /// <param name="ATotalAddresses">returns the total number of address</param>
        /// <param name="ACurrentAddresses">returns the number of current addresses</param>
        public static void CalculateTabCountsAddresses(PPartnerLocationTable ATable, out Int32 ATotalAddresses, out Int32 ACurrentAddresses)
        {
            DataView TmpDV;

            // Inspect only CurrentRows (this excludes Deleted DataRows)
            TmpDV = new DataView(ATable, "", "", DataViewRowState.CurrentRows);
            ATotalAddresses = TmpDV.Count;

            if ((ATotalAddresses == 1) && (((PPartnerLocationRow)TmpDV[0].Row).LocationKey == 0))
            {
                // In case the only Address is linked to Location 0: we don't have a
                // Current Address, because this signalises that there is no valid address.
                // MessageBox.Show('The last Address is the ''No Address on file'' Address!');
                ACurrentAddresses = 0;
            }
            else
            {
                // MessageBox.Show('Query: ' + '((' + PPartnerLocationTable.GetDateEffectiveDBName + ' <= #'
                // + DateTime.Now.Date.ToString('MM/dd/yyyy') + '# OR ' +
                // PPartnerLocationTable.GetDateEffectiveDBName + ' IS NULL) AND (' +
                // PPartnerLocationTable.GetDateGoodUntilDBName + ' >= #'
                // + DateTime.Now.Date.ToString('MM/dd/yyyy') + '# OR ' +
                // PPartnerLocationTable.GetDateGoodUntilDBName + ' IS NULL))');
                ACurrentAddresses = DetermineCurrentAddresses(ATable).Count;
            }

            // MessageBox.Show('ACurrentAddresses: ' + ACurrentAddresses.ToString);
        }

        /// <summary>
        /// Count the subscriptions
        /// </summary>
        /// <param name="ATable">table with subscriptions</param>
        /// <param name="ATotalSubscriptions">returns the total number of subscriptions</param>
        /// <param name="AActiveSubscriptions">returns the number of active subscriptions</param>
        public static void CalculateTabCountsSubscriptions(PSubscriptionTable ATable, out Int32 ATotalSubscriptions, out Int32 AActiveSubscriptions)
        {
            // Inspect only CurrentRows (this excludes Deleted DataRows)
            ATotalSubscriptions = new DataView(ATable, "", "", DataViewRowState.CurrentRows).Count;

            // Inspect only CurrentRows (this excludes Deleted DataRows)
            AActiveSubscriptions = new DataView(ATable,
                PSubscriptionTable.GetSubscriptionStatusDBName() + " <> '" + MPartnerConstants.SUBSCRIPTIONS_STATUS_CANCELLED + "' AND " +
                PSubscriptionTable.GetSubscriptionStatusDBName() + " <> '" + MPartnerConstants.SUBSCRIPTIONS_STATUS_EXPIRED + "'", "",
                DataViewRowState.CurrentRows).Count;
        }
    }
}