﻿//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop
//
// Copyright 2004-2013 by OM International
//
// This file is part of OpenPetra.org.
//
// OpenPetra.org is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra.org is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using Ict.Common;
using Ict.Common.DB;
using Ict.Petra.Shared;
using Ict.Petra.Shared.MReporting;
using Ict.Petra.Shared.MFinance;
using Ict.Petra.Server.MSysMan.Maintenance.SystemDefaults.WebConnectors;

namespace Ict.Petra.Server.MFinance.queries
{
    /// <summary>
    /// contains a method that calculates the gifts for a HOSA
    /// </summary>
    public class QueryFinanceReport
    {
        /*
         * Not used
         * /// <summary>
         * /// get all gifts for the current costcentre and account
         * /// </summary>
         * public static DataTable HosaCalculateGifts(TParameterList AParameters, TResultList AResults)
         * {
         *  SortedList<string, string> Defines = new SortedList<string, string>();
         *  List<OdbcParameter> SqlParameterList = new List<OdbcParameter>();
         *
         *  if (AParameters.Get("param_filter_cost_centres").ToString() == "PersonalCostcentres")
         *  {
         *      Defines.Add("PERSONALHOSA", "true");
         *  }
         *
         *  SqlParameterList.Add(new OdbcParameter("ledgernumber", OdbcType.Decimal)
         *      {
         *          Value = AParameters.Get("param_ledger_number_i").ToDecimal()
         *      });
         *  SqlParameterList.Add(new OdbcParameter("costcentre", OdbcType.VarChar)
         *      {
         *          Value = AParameters.Get("line_a_cost_centre_code_c")
         *      });
         *
         *  if (AParameters.Get("param_ich_number").ToInt32() == 0)
         *  {
         *      Defines.Add("NOT_LIMITED_TO_ICHNUMBER", "true");
         *  }
         *  else
         *  {
         *      SqlParameterList.Add(new OdbcParameter("ichnumber", OdbcType.Int)
         *          {
         *              Value = AParameters.Get("param_ich_number").ToInt32()
         *          });
         *  }
         *
         *  SqlParameterList.Add(new OdbcParameter("batchstatus", OdbcType.VarChar)
         *      {
         *          Value = MFinanceConstants.BATCH_POSTED
         *      });
         *
         *  if (AParameters.Get("param_period").ToBool() == true)
         *  {
         *      Defines.Add("BYPERIOD", "true");
         *      SqlParameterList.Add(new OdbcParameter("batchyear", OdbcType.Int)
         *          {
         *              Value = AParameters.Get("param_year_i").ToInt32()
         *          });
         *      SqlParameterList.Add(new OdbcParameter("batchperiod_start", OdbcType.Int)
         *          {
         *              Value = AParameters.Get("param_start_period_i").ToInt32()
         *          });
         *      SqlParameterList.Add(new OdbcParameter("batchperiod_end", OdbcType.Int)
         *          {
         *              Value = AParameters.Get("param_end_period_i").ToInt32()
         *          });
         *  }
         *  else
         *  {
         *      SqlParameterList.Add(new OdbcParameter("param_start_date", OdbcType.Int)
         *          {
         *              Value = AParameters.Get("param_start_date").ToInt32()
         *          });
         *      SqlParameterList.Add(new OdbcParameter("param_end_date", OdbcType.Int)
         *          {
         *              Value = AParameters.Get("param_end_date").ToInt32()
         *          });
         *  }
         *
         *  SqlParameterList.Add(new OdbcParameter("accountcode", OdbcType.VarChar)
         *      {
         *          Value = AParameters.Get("line_a_account_code_c")
         *      });
         *
         *  string SqlStmt = TDataBase.ReadSqlFile("ICH.HOSAReportGiftSummary.sql", Defines);
         *  DataTable resultTable = null;
         *
         *  TDBTransaction Transaction = null;
         *  DBAccess.GDBAccessObj.GetNewOrExistingAutoReadTransaction(IsolationLevel.ReadCommitted, TEnforceIsolationLevel.eilMinimum, ref Transaction,
         *      delegate
         *      {
         *
         *          // now run the database query
         *          resultTable = DBAccess.GDBAccessObj.SelectDT(SqlStmt, "result", Transaction,
         *              SqlParameterList.ToArray());
         *      });
         *  // if this is taking a long time, every now and again update the TLogging statusbar, and check for the cancel button
         *  if (AParameters.Get("CancelReportCalculation").ToBool() == true)
         *  {
         *      return null;
         *  }
         *
         *  resultTable.Columns.Add("a_transaction_amount_n", typeof(Decimal));
         *  resultTable.Columns.Add("a_amount_in_base_currency_n", typeof(Decimal));
         *  resultTable.Columns.Add("a_amount_in_intl_currency_n", typeof(Decimal));
         *  resultTable.Columns.Add("a_reference_c", typeof(string));
         *  resultTable.Columns.Add("a_narrative_c", typeof(string));
         *
         *  Boolean InternationalCurrency = AParameters.Get("param_currency").ToString() == "International";
         *  Double ExchangeRate = 1.00;  // TODO Get exchange rate!
         *
         *  foreach (DataRow Row in resultTable.Rows)
         *  {
         *      Row["a_transaction_amount_n"] = Convert.ToDecimal(Row["GiftTransactionAmount"]);
         *      Row["a_amount_in_base_currency_n"] = Convert.ToDecimal(Row["GiftBaseAmount"]);
         *
         *      if (InternationalCurrency)
         *      {
         *          Row["a_amount_in_intl_currency_n"] = (Decimal)(Convert.ToDouble(Row["GiftBaseAmount"]) * ExchangeRate);
         *      }
         *
         *      Row["a_reference_c"] = StringHelper.PartnerKeyToStr(Convert.ToInt64(Row["RecipientKey"]));
         *      Row["a_narrative_c"] = Row["RecipientShortname"].ToString();
         *  }
         *
         *  return resultTable;
         * }
         */

        /// <summary>
        /// Find all the gifts for a specific month, returning "worker", "field" and "total" results.
        /// </summary>
        public static DataTable TotalGiftsThroughFieldMonth(TParameterList AParameters, TResultList AResults)
        {
            Int32 LedgerNum = AParameters.Get("param_ledger_number_i").ToInt32();
            Int32 Year = AParameters.Get("param_YearBlock").ToInt32();
            string YearStart = String.Format("#{0:0000}-01-01#", Year);
            string YearEnd = String.Format("#{0:0000}-12-31#", Year);

            bool TaxDeductiblePercentageEnabled = Convert.ToBoolean(
                TSystemDefaults.GetSystemDefault(SharedConstants.SYSDEFAULT_TAXDEDUCTIBLEPERCENTAGE, "FALSE"));

            string SqlQuery = "SELECT batch.a_gl_effective_date_d as Date, motive.a_report_column_c AS ReportColumn, ";

            if (AParameters.Get("param_currency").ToString() == "Base")
            {
                SqlQuery += "detail.a_gift_amount_n AS Amount";

                if (TaxDeductiblePercentageEnabled)
                {
                    SqlQuery += ", detail.a_tax_deductible_amount_base_n AS TaxDeductAmount";
                }
            }
            else
            {
                SqlQuery += "detail.a_gift_amount_intl_n AS Amount";

                if (TaxDeductiblePercentageEnabled)
                {
                    SqlQuery += ", detail.a_tax_deductible_amount_intl_n AS TaxDeductAmount";
                }
            }

            SqlQuery += (" FROM PUB_a_gift as gift, PUB_a_gift_detail as detail, PUB_a_gift_batch as batch, PUB_a_motivation_detail AS motive"

                         + " WHERE detail.a_ledger_number_i = " + LedgerNum +
                         " AND batch.a_batch_status_c = 'Posted'" +
                         " AND batch.a_batch_number_i = gift.a_batch_number_i" +
                         " AND batch.a_ledger_number_i = " + LedgerNum +
                         " AND batch.a_gl_effective_date_d >= " + YearStart +
                         " AND batch.a_gl_effective_date_d <= " + YearEnd

                         + " AND gift.a_ledger_number_i = " + LedgerNum +
                         " AND detail.a_batch_number_i = gift.a_batch_number_i" +
                         " AND detail.a_gift_transaction_number_i = gift.a_gift_transaction_number_i"

                         + " AND motive.a_ledger_number_i = " + LedgerNum +
                         " AND motive.a_motivation_group_code_c = detail.a_motivation_group_code_c" +
                         " AND motive.a_motivation_detail_code_c = detail.a_motivation_detail_code_c" +
                         " AND motive.a_receipt_l=true");
            DataTable tempTbl = null;
            TDBTransaction Transaction = null;
            DBAccess.GDBAccessObj.GetNewOrExistingAutoReadTransaction(IsolationLevel.ReadCommitted,
                TEnforceIsolationLevel.eilMinimum,
                ref Transaction,
                delegate
                {
                    tempTbl = DBAccess.GDBAccessObj.SelectDT(SqlQuery, "result", Transaction);
                });

            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("MonthName", typeof(String));               //
            resultTable.Columns.Add("MonthWorker", typeof(Decimal));            // These are the names of the variables
            resultTable.Columns.Add("MonthWorkerCount", typeof(Int32));         // returned by this calculation.
            resultTable.Columns.Add("MonthField", typeof(Decimal));             //
            resultTable.Columns.Add("MonthFieldCount", typeof(Int32));          //
            resultTable.Columns.Add("MonthTotal", typeof(Decimal));             //
            resultTable.Columns.Add("MonthTotalCount", typeof(Int32));          //

            resultTable.Columns.Add("MonthWorkerTaxDeduct", typeof(Decimal));
            resultTable.Columns.Add("MonthFieldTaxDeduct", typeof(Decimal));
            resultTable.Columns.Add("MonthTotalTaxDeduct", typeof(Decimal));

            for (int mnth = 1; mnth <= 12; mnth++)
            {
                string monthStart = String.Format("#{0:0000}-{1:00}-01#", Year, mnth);
                string nextMonthStart = String.Format("#{0:0000}-{1:00}-01#", Year, mnth + 1);

                if (mnth == 12)
                {
                    nextMonthStart = String.Format("#{0:0000}-01-01#", Year + 1);
                }

                tempTbl.DefaultView.RowFilter = "Date >= " + monthStart + " AND Date < " + nextMonthStart;

                Decimal WorkerTotal = 0;
                Decimal FieldTotal = 0;
                Int32 WorkerCount = 0;
                Int32 FieldCount = 0;
                Int32 TotalCount = tempTbl.DefaultView.Count;

                Decimal WorkerTotalTaxDeduct = 0;
                Decimal FieldTotalTaxDeduct = 0;

                for (int i = 0; i < TotalCount; i++)
                {
                    DataRow Row = tempTbl.DefaultView[i].Row;

                    if (Row["ReportColumn"].ToString() == "Worker")
                    {
                        WorkerCount++;
                        WorkerTotal += Convert.ToDecimal(Row["Amount"]);

                        if (TaxDeductiblePercentageEnabled)
                        {
                            WorkerTotalTaxDeduct += Convert.ToDecimal(Row["TaxDeductAmount"]);
                        }
                    }
                    else
                    {
                        FieldCount++;
                        FieldTotal += Convert.ToDecimal(Row["Amount"]);

                        if (TaxDeductiblePercentageEnabled)
                        {
                            FieldTotalTaxDeduct += Convert.ToDecimal(Row["TaxDeductAmount"]);
                        }
                    }
                }

                DataRow resultRow = resultTable.NewRow();

                resultRow["MonthName"] = StringHelper.GetLongMonthName(mnth);
                resultRow["MonthWorker"] = WorkerTotal;
                resultRow["MonthWorkerCount"] = WorkerCount;
                resultRow["MonthField"] = FieldTotal;
                resultRow["MonthFieldCount"] = FieldCount;
                resultRow["MonthTotal"] = WorkerTotal + FieldTotal;
                resultRow["MonthTotalCount"] = TotalCount;

                resultRow["MonthWorkerTaxDeduct"] = WorkerTotalTaxDeduct;
                resultRow["MonthFieldTaxDeduct"] = FieldTotalTaxDeduct;
                resultRow["MonthTotalTaxDeduct"] = WorkerTotalTaxDeduct + FieldTotalTaxDeduct;

                resultTable.Rows.Add(resultRow);
            }

            return resultTable;
        } // Total Gifts Through Field Month

        /// <summary>
        /// Find all the gifts for a year, returning "worker", "field" and "total" results.
        /// </summary>
        public static DataTable TotalGiftsThroughFieldYear(TParameterList AParameters, TResultList AResults)
        {
            bool TaxDeductiblePercentageEnabled = Convert.ToBoolean(
                TSystemDefaults.GetSystemDefault(SharedConstants.SYSDEFAULT_TAXDEDUCTIBLEPERCENTAGE, "FALSE"));

            Int32 LedgerNum = AParameters.Get("param_ledger_number_i").ToInt32();
            Int32 NumberOfYears = AParameters.Get("param_NumberOfYears").ToInt32();
            string SqlQuery = "SELECT batch.a_gl_effective_date_d as Date, motive.a_report_column_c AS ReportColumn, ";

            if (AParameters.Get("param_currency").ToString() == "Base")
            {
                SqlQuery += "detail.a_gift_amount_n AS Amount";

                if (TaxDeductiblePercentageEnabled)
                {
                    SqlQuery += ", detail.a_tax_deductible_amount_base_n AS TaxDeductAmount";
                }
            }
            else
            {
                SqlQuery += "detail.a_gift_amount_intl_n AS Amount";

                if (TaxDeductiblePercentageEnabled)
                {
                    SqlQuery += ", detail.a_tax_deductible_amount_intl_n AS TaxDeductAmount";
                }
            }

            SqlQuery += (" FROM PUB_a_gift as gift, PUB_a_gift_detail as detail, PUB_a_gift_batch as batch, PUB_a_motivation_detail AS motive"

                         + " WHERE detail.a_ledger_number_i = " + LedgerNum +
                         " AND batch.a_batch_status_c = 'Posted'" +
                         " AND batch.a_batch_number_i = gift.a_batch_number_i" +
                         " AND batch.a_ledger_number_i = " + LedgerNum

                         + " AND gift.a_ledger_number_i = " + LedgerNum +
                         " AND detail.a_batch_number_i = gift.a_batch_number_i" +
                         " AND detail.a_gift_transaction_number_i = gift.a_gift_transaction_number_i"

                         + " AND motive.a_ledger_number_i = " + LedgerNum +
                         " AND motive.a_motivation_group_code_c = detail.a_motivation_group_code_c" +
                         " AND motive.a_motivation_detail_code_c = detail.a_motivation_detail_code_c" +
                         " AND motive.a_receipt_l=true");

            DataTable tempTbl = null;
            TDBTransaction Transaction = null;
            DBAccess.GDBAccessObj.GetNewOrExistingAutoReadTransaction(IsolationLevel.ReadCommitted,
                TEnforceIsolationLevel.eilMinimum,
                ref Transaction,
                delegate
                {
                    tempTbl = DBAccess.GDBAccessObj.SelectDT(SqlQuery, "result", Transaction);
                });

            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("SummaryYear", typeof(Int32));              //
            resultTable.Columns.Add("YearWorker", typeof(Decimal));             // These are the names of the variables
            resultTable.Columns.Add("YearWorkerCount", typeof(Int32));          // returned by this calculation.
            resultTable.Columns.Add("YearField", typeof(Decimal));              //
            resultTable.Columns.Add("YearFieldCount", typeof(Int32));           //
            resultTable.Columns.Add("YearTotal", typeof(Decimal));              //
            resultTable.Columns.Add("YearTotalCount", typeof(Int32));           //

            resultTable.Columns.Add("YearWorkerTaxDeduct", typeof(Decimal));
            resultTable.Columns.Add("YearFieldTaxDeduct", typeof(Decimal));
            resultTable.Columns.Add("YearTotalTaxDeduct", typeof(Decimal));

            Int32 Year = DateTime.Now.Year;

            for (Int32 YearIdx = 0; YearIdx < NumberOfYears; YearIdx++)
            {
                string yearStart = String.Format("#{0:0000}-01-01#", Year - YearIdx);
                string yearEnd = String.Format("#{0:0000}-12-31#", Year - YearIdx);

                tempTbl.DefaultView.RowFilter = "Date >= " + yearStart + " AND Date < " + yearEnd;

                Decimal WorkerTotal = 0;
                Decimal FieldTotal = 0;
                Int32 WorkerCount = 0;
                Int32 FieldCount = 0;
                Int32 TotalCount = tempTbl.DefaultView.Count;

                Decimal WorkerTotalTaxDeduct = 0;
                Decimal FieldTotalTaxDeduct = 0;

                for (int i = 0; i < TotalCount; i++)
                {
                    DataRow Row = tempTbl.DefaultView[i].Row;

                    if (Row["ReportColumn"].ToString() == "Worker")
                    {
                        WorkerCount++;
                        WorkerTotal += Convert.ToDecimal(Row["Amount"]);

                        if (TaxDeductiblePercentageEnabled)
                        {
                            WorkerTotalTaxDeduct += Convert.ToDecimal(Row["TaxDeductAmount"]);
                        }
                    }
                    else
                    {
                        FieldCount++;
                        FieldTotal += Convert.ToDecimal(Row["Amount"]);

                        if (TaxDeductiblePercentageEnabled)
                        {
                            FieldTotalTaxDeduct += Convert.ToDecimal(Row["TaxDeductAmount"]);
                        }
                    }
                }

                DataRow resultRow = resultTable.NewRow();

                resultRow["SummaryYear"] = Year - YearIdx;
                resultRow["YearWorker"] = WorkerTotal;
                resultRow["YearWorkerCount"] = WorkerCount;
                resultRow["YearField"] = FieldTotal;
                resultRow["YearFieldCount"] = FieldCount;
                resultRow["YearTotal"] = WorkerTotal + FieldTotal;
                resultRow["YearTotalCount"] = TotalCount;

                resultRow["YearWorkerTaxDeduct"] = WorkerTotalTaxDeduct;
                resultRow["YearFieldTaxDeduct"] = FieldTotalTaxDeduct;
                resultRow["YearTotalTaxDeduct"] = WorkerTotalTaxDeduct + FieldTotalTaxDeduct;

                resultTable.Rows.Add(resultRow);
            }

            return resultTable;
        }

        private static DataTable TotalGiftsPerRecipient = null;

        /// <summary>
        /// Find recipient Partner Key and name for all partners who received gifts in the timeframe.
        /// NOTE - the user can select the PartnerType of the recipient.
        ///
        /// With only a little more load on the DB, I can get all the data that the report will need,
        /// and store it in a DataTable local to this class, so that when more detailed data is requested below,
        /// I don't need another DB query.
        /// </summary>
        /// <returns>RecipientKey, RecipientName</returns>
        public static DataTable SelectGiftRecipients(TParameterList AParameters, TResultList AResults)
        {
            Int32 LedgerNum = AParameters.Get("param_ledger_number_i").ToInt32();
            Boolean onlySelectedTypes = AParameters.Get("param_type_selection").ToString() == "selected_types";
            Boolean onlySelectedFields = AParameters.Get("param_field_selection").ToString() == "selected_fields";
            Boolean fromExtract = AParameters.Get("param_recipient").ToString() == "Extract";
            Boolean oneRecipient = AParameters.Get("param_recipient").ToString() == "One Recipient";
            String period0Start = AParameters.Get("param_from_date_0").ToDate().ToString("yyyy-MM-dd");
            String period0End = AParameters.Get("param_to_date_0").ToDate().ToString("yyyy-MM-dd");
            String period1Start = AParameters.Get("param_from_date_1").ToDate().ToString("yyyy-MM-dd");
            String period1End = AParameters.Get("param_to_date_1").ToDate().ToString("yyyy-MM-dd");
            String period2Start = AParameters.Get("param_from_date_2").ToDate().ToString("yyyy-MM-dd");
            String period2End = AParameters.Get("param_to_date_2").ToDate().ToString("yyyy-MM-dd");
            String period3Start = AParameters.Get("param_from_date_3").ToDate().ToString("yyyy-MM-dd");
            String period3End = AParameters.Get("param_to_date_3").ToDate().ToString("yyyy-MM-dd");
            String amountFieldName = (AParameters.Get("param_currency").ToString() == "International") ?
                                     "detail.a_gift_amount_intl_n" : "detail.a_gift_amount_n";

            string SqlQuery = "SELECT DISTINCT " +
                              "gift.p_donor_key_n AS DonorKey, " +
                              "donor.p_partner_short_name_c AS DonorName, donor.p_partner_class_c AS DonorClass, " +
                              "recipient.p_partner_key_n AS RecipientKey, " +
                              "recipient.p_partner_short_name_c AS RecipientName, " +
                              "SUM(CASE WHEN gift.a_date_entered_d BETWEEN '" + period0Start + "' AND '" + period0End + "' " +
                              "THEN " + amountFieldName + " ELSE 0 END )as YearTotal0, " +
                              "SUM(CASE WHEN gift.a_date_entered_d BETWEEN '" + period1Start + "' AND '" + period1End + "' " +
                              "THEN " + amountFieldName + " ELSE 0 END )as YearTotal1, " +
                              "SUM(CASE WHEN gift.a_date_entered_d BETWEEN '" + period2Start + "' AND '" + period2End + "' " +
                              "THEN " + amountFieldName + " ELSE 0 END )as YearTotal2, " +
                              "SUM(CASE WHEN gift.a_date_entered_d BETWEEN '" + period3Start + "' AND '" + period3End + "' " +
                              "THEN " + amountFieldName + " ELSE 0 END )as YearTotal3 " +
                              "FROM PUB_a_gift as gift, PUB_a_gift_detail as detail, PUB_a_gift_batch AS GiftBatch, PUB_p_partner AS donor, PUB_p_partner AS recipient ";

            if (onlySelectedTypes)
            {
                SqlQuery += ", PUB_p_partner_type AS RecipientType ";
            }

            if (fromExtract)
            {
                String extractName = AParameters.Get("param_extract_name").ToString();
                SqlQuery += (", PUB_m_extract AS Extract, PUB_m_extract_master AS ExtractMaster" +
                             "WHERE " +
                             "partner.p_partner_key_n = Extract.p_partner_key_n " +
                             "AND Extract.m_extract_id_i = ExtractMaster.m_extract_id_i " +
                             "AND ExtractMaster.m_extract_name_c = '" + extractName + "' " +
                             "AND "
                             );
            }
            else
            {
                SqlQuery += "WHERE ";
            }

            SqlQuery += ("detail.a_ledger_number_i = " + LedgerNum + " " +
                         "AND detail.p_recipient_key_n = recipient.p_partner_key_n " +
                         "AND gift.p_donor_key_n = donor.p_partner_key_n " +
                         "AND detail.a_batch_number_i = gift.a_batch_number_i " +
                         "AND detail.a_gift_transaction_number_i = gift.a_gift_transaction_number_i " +
                         "AND gift.a_date_entered_d BETWEEN '" + period3Start + "' AND '" + period0End + "' " +
                         "AND gift.a_ledger_number_i = " + LedgerNum + " " +
                         "AND GiftBatch.a_batch_status_c = 'Posted' " +
                         "AND GiftBatch.a_batch_number_i = gift.a_batch_number_i " +
                         "AND GiftBatch.a_ledger_number_i = " + LedgerNum + " "
                         );

            if (oneRecipient)
            {
                String recipientKey = AParameters.Get("param_recipient_key").ToString();
                SqlQuery += ("AND recipient.p_partner_key_n = " + recipientKey + " ");
            }

            if (onlySelectedFields)
            {
                String selectedFieldList = AParameters.Get("param_clbFields").ToString();
                selectedFieldList = selectedFieldList.Replace('\'', ' ');
                SqlQuery += ("AND detail.a_recipient_ledger_number_n IN (" + selectedFieldList + ") ");
            }

            if (onlySelectedTypes)
            {
                String selectedTypeList = "'" + AParameters.Get("param_clbTypes").ToString() + "'";
                selectedTypeList = selectedTypeList.Replace(",", "','");

                SqlQuery += ("AND RecipientType.p_partner_key_n = detail.p_recipient_key_n " +
                             "AND RecipientType.p_type_code_c IN (" + selectedTypeList + ") ");
            }

            SqlQuery +=
                (
                    "GROUP by gift.p_donor_key_n, donor.p_partner_short_name_c, donor.p_partner_class_c, recipient.p_partner_key_n, recipient.p_partner_short_name_c "
                    +
                    "ORDER BY recipient.p_partner_short_name_c");

            TDBTransaction Transaction = null;
            DBAccess.GDBAccessObj.GetNewOrExistingAutoReadTransaction(IsolationLevel.ReadCommitted,
                TEnforceIsolationLevel.eilMinimum,
                ref Transaction,
                delegate
                {
                    TotalGiftsPerRecipient = DBAccess.GDBAccessObj.SelectDT(SqlQuery, "result", Transaction);
                });

            //
            // Ok, I've got a DataTable with ALL THE DATA I need for the report,
            // but for this calculation I only want a list of partner keys and names...
            //
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("RecipientKey", typeof(String));        // These are the names of the variables
            resultTable.Columns.Add("RecipientName", typeof(String));       // returned by this calculation.

            Int64 previousPartner = -1;

            foreach (DataRow Row in TotalGiftsPerRecipient.Rows)
            {
                Int64 partnerKey = Convert.ToInt64(Row["RecipientKey"]);

                if (partnerKey != previousPartner)
                {
                    previousPartner = partnerKey;
                    DataRow NewRow = resultTable.NewRow();
                    NewRow["RecipientKey"] = Row["RecipientKey"];
                    NewRow["RecipientName"] = Row["RecipientName"];
                    resultTable.Rows.Add(NewRow);
                }
            }

            return resultTable;
        } // Select Gift Recipients

        /// <summary>
        /// Find all the gifts for a worker, presenting the results in four year columns.
        /// NOTE - the user can select the field of the donor.
        ///
        /// All the DB work was previously done by the Select Gift Recipients function above.
        /// I only need to filter the table by recipientKey.
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectGiftDonors(TParameterList AParameters, TResultList AResults)
        {
            Int64 recipientKey = AParameters.Get("RecipientKey").ToInt64();

            TotalGiftsPerRecipient.DefaultView.RowFilter = "RecipientKey = " + recipientKey.ToString();
            DataTable resultTable = TotalGiftsPerRecipient.DefaultView.ToTable(true,
                new String[] { "DonorKey", "DonorName", "DonorClass", "YearTotal0", "YearTotal1", "YearTotal2", "YearTotal3" });
            return resultTable;
        }
    }
}