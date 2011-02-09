﻿//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       matthiash
//
// Copyright 2004-2010 by OM International
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

using Ict.Common;
using Ict.Common.DB;
using Ict.Common.Verification;
using Ict.Petra.Server.MFinance.Account.Data.Access;
using Ict.Petra.Server.MFinance.Gift.WebConnectors;
using Ict.Petra.Server.MFinance.Gift.Data.Access;
using Ict.Petra.Shared.MFinance.Account.Data;
using Ict.Petra.Server.MPartner.Partner.Data.Access;
using Ict.Petra.Server.MSysMan.Data.Access;
using Ict.Petra.Shared;
using Ict.Petra.Shared.MFinance.Gift.Data;
using Ict.Petra.Shared.MFinance.GL.Data;
using Ict.Petra.Shared.MPartner.Partner.Data;
using Ict.Petra.Shared.MSysMan.Data;

//using Ict.Petra.Server.MFinance.Account.Data.Access;
//using Ict.Petra.Shared.MFinance.Account.Data;


namespace Ict.Petra.Server.MFinance.Gift
{
    /// <summary>
    /// Import a Gift Batch
    /// </summary>
    public class TGiftImporting
    {
        String FDelimiter;
        Int32 FLedgerNumber;
        String FDateFormatString;
        bool FExtraColumns;
        TDBTransaction FTransaction;
        GLSetupTDS FSetupTDS;
        GiftBatchTDS FMainDS;
        CultureInfo FCultureInfoNumberFormat;
        CultureInfo FCultureInfoDate;


        private String FImportMessage;
        private String FImportLine;
        private String FNewLine;


        /// <summary>
        /// Import Gift batch data
        /// The data file contents from the client is sent as a string, imported in the database
        /// and committed immediately
        /// </summary>
        /// <param name="requestParams">Hashtable containing the given params </param>
        /// <param name="importString">Big parts of the export file as a simple String</param>
        /// <param name="AMessages">Additional messages to display in a messagebox</param>
        /// <returns>false if error</returns>
        public bool ImportGiftBatches(
            Hashtable requestParams,
            String importString,
            out TVerificationResultCollection AMessages
            )
        {
            AMessages = new TVerificationResultCollection();
            FMainDS = new GiftBatchTDS();
            FSetupTDS = new GLSetupTDS();
            StringReader sr = new StringReader(importString);

            FDelimiter = (String)requestParams["Delimiter"];
            FLedgerNumber = (Int32)requestParams["ALedgerNumber"];
            FDateFormatString = (String)requestParams["DateFormatString"];
            String NumberFormat = (String)requestParams["NumberFormat"];
            FNewLine = (String)requestParams["NewLine"];

            FCultureInfoNumberFormat = new CultureInfo(NumberFormat.Equals("American") ? "en-US" : "de-DE");
            FCultureInfoDate = new CultureInfo("en-GB");
            FCultureInfoDate.DateTimeFormat.ShortDatePattern = FDateFormatString;

            FTransaction = DBAccess.GDBAccessObj.BeginTransaction(IsolationLevel.ReadCommitted);

            AGiftBatchRow giftBatch = null;
            //AGiftRow gift = null;
            FImportMessage = Catalog.GetString("Parsing first line");
            Int32 RowNumber = 0;
            bool ok = false;

            try
            {
                ALedgerTable LedgerTable = ALedgerAccess.LoadByPrimaryKey(FLedgerNumber, FTransaction);

                while ((FImportLine = sr.ReadLine()) != null)
                {
                    RowNumber++;

                    // skip empty lines and commented lines
                    if ((FImportLine.Trim().Length > 0) && !FImportLine.StartsWith("/*") && !FImportLine.StartsWith("#"))
                    {
                        string RowType = ImportString(Catalog.GetString("row type"));

                        if (RowType == "B")
                        {
                            giftBatch = TGiftBatchFunctions.CreateANewGiftBatchRow(ref FMainDS,
                                ref FTransaction,
                                ref LedgerTable,
                                FLedgerNumber,
                                DateTime.Today);
                            giftBatch.BatchDescription = ImportString(Catalog.GetString("batch description"));
                            giftBatch.BankAccountCode = ImportString(Catalog.GetString("bank account  code"));
                            giftBatch.HashTotal = ImportDecimal(Catalog.GetString("hash total"));
                            giftBatch.GlEffectiveDate = ImportDate(Catalog.GetString("effective Date"));
                            giftBatch.CurrencyCode = ImportString(Catalog.GetString("currency code"));
                            giftBatch.ExchangeRateToBase = ImportDecimal(Catalog.GetString("exchange rate to base"));
                            giftBatch.BankCostCentre = ImportString(Catalog.GetString("bank cost centre"));
                            giftBatch.GiftType = ImportString(Catalog.GetString("gift type"));
                            FImportMessage = Catalog.GetString("Saving gift batch");

                            if (!AGiftBatchAccess.SubmitChanges(FMainDS.AGiftBatch, FTransaction, out AMessages))
                            {
                                return false;
                            }

                            FMainDS.AGiftBatch.AcceptChanges();
                        }
                        else if (RowType == "T")
                        {
                            int numberOfElements = FImportLine.Split(FDelimiter.ToCharArray()).Length;
                            //this is the format with extra columns
                            FExtraColumns = numberOfElements > 22;

                            if (giftBatch == null)
                            {
                                FImportMessage = Catalog.GetString("Expected a GiftBatch line, but found a Gift");
                                throw new Exception();
                            }

                            AGiftRow gift = FMainDS.AGift.NewRowTyped(true);
                            gift.LedgerNumber = giftBatch.LedgerNumber;
                            gift.BatchNumber = giftBatch.BatchNumber;
                            gift.GiftTransactionNumber = giftBatch.LastGiftNumber + 1;
                            giftBatch.LastGiftNumber++;
                            gift.LastDetailNumber = 1;
                            FMainDS.AGift.Rows.Add(gift);
                            AGiftDetailRow giftDetails = FMainDS.AGiftDetail.NewRowTyped(true);
                            giftDetails.DetailNumber = 1;
                            giftDetails.LedgerNumber = gift.LedgerNumber;
                            giftDetails.BatchNumber = giftBatch.BatchNumber;
                            giftDetails.GiftTransactionNumber = gift.GiftTransactionNumber;
                            FMainDS.AGiftDetail.Rows.Add(giftDetails);


                            gift.DonorKey = ImportInt64(Catalog.GetString("donor key"));
                            String unused = ImportString(Catalog.GetString("short name of donor (unused)"));


                            gift.MethodOfGivingCode = ImportString(Catalog.GetString("method of giving Code"));
                            gift.MethodOfPaymentCode = ImportString(Catalog.GetString("method Of Payment Code"));
                            gift.Reference = ImportString(Catalog.GetString("reference"));
                            gift.ReceiptLetterCode = ImportString(Catalog.GetString("receipt letter code"));

                            if (FExtraColumns)
                            {
                                gift.ReceiptNumber = ImportInt32(Catalog.GetString("receipt number"));
                                gift.FirstTimeGift = ImportBoolean(Catalog.GetString("first time gift"));
                                gift.ReceiptPrinted = ImportBoolean(Catalog.GetString("receipt printed"));
                            }

                            giftDetails.RecipientKey = ImportInt64(Catalog.GetString("recipient key"));
                            unused = ImportString(Catalog.GetString("short name of recipient (unused)"));

                            if (FExtraColumns)
                            {
                                giftDetails.RecipientLedgerNumber = ImportInt32(Catalog.GetString("recipient ledger number"));
                            }

                            giftDetails.GiftAmount = ImportDecimal(Catalog.GetString("Gift amount"));
                            giftDetails.GiftTransactionAmount = giftDetails.GiftAmount;
                            // TODO: currency translation

                            if (FExtraColumns)
                            {
                                giftDetails.GiftAmountIntl = ImportDecimal(Catalog.GetString("gift amount intl"));
                            }

                            giftDetails.ConfidentialGiftFlag = ImportBoolean(Catalog.GetString("confidential gift"));
                            giftDetails.MotivationGroupCode = ImportString(Catalog.GetString("motivation group code"));
                            giftDetails.MotivationDetailCode = ImportString(Catalog.GetString("motivation detail"));
                            giftDetails.CostCentreCode = ImportString(Catalog.GetString("cost centre code"));
                            giftDetails.GiftCommentOne = ImportString(Catalog.GetString("comment one"));
                            giftDetails.CommentOneType = ImportString(Catalog.GetString("comment one type"));


                            giftDetails.MailingCode = ImportString(Catalog.GetString("mailing code"));

                            giftDetails.GiftCommentTwo = ImportString(Catalog.GetString("gift comment two"));
                            giftDetails.CommentTwoType = ImportString(Catalog.GetString("comment two type"));
                            giftDetails.GiftCommentThree = ImportString(Catalog.GetString("gift comment three"));
                            giftDetails.CommentThreeType = ImportString(Catalog.GetString("comment three type"));
                            giftDetails.TaxDeductable = ImportBoolean(Catalog.GetString("tax deductable"));
                            FImportMessage = Catalog.GetString("Saving gift");

                            if (!AGiftAccess.SubmitChanges(FMainDS.AGift, FTransaction, out AMessages))
                            {
                                return false;
                            }

                            FMainDS.AGift.AcceptChanges();
                            FImportMessage = Catalog.GetString("Saving giftdetails");

                            if (!AGiftDetailAccess.SubmitChanges(FMainDS.AGiftDetail, FTransaction, out AMessages))
                            {
                                return false;
                            }

                            FMainDS.AGiftDetail.AcceptChanges();
                        }
                    }
                }

                FImportMessage = Catalog.GetString("Saving all data into the database");

                //Finally save pending changes (the last number is updated !)
                if (AGiftBatchAccess.SubmitChanges(FMainDS.AGiftBatch, FTransaction, out AMessages))
                {
                    if (ALedgerAccess.SubmitChanges(LedgerTable, FTransaction, out AMessages))
                    {
                        FMainDS.AGiftBatch.AcceptChanges();
                        FMainDS.ALedger.AcceptChanges();
                        ok = true;
                    }
                }
            }
            catch (Exception ex)
            {
                String speakingExceptionText = SpeakingExceptionMessage(ex);
                AMessages.Add(new TVerificationResult(Catalog.GetString("Import"),

                        String.Format(Catalog.GetString("There is a problem parsing the file in row {0}:"), RowNumber) +
                        FNewLine +
                        Catalog.GetString(FImportMessage) + FNewLine + speakingExceptionText,
                        TResultSeverity.Resv_Critical));
                DBAccess.GDBAccessObj.RollbackTransaction();
                return false;
            }
            finally
            {
                try
                {
                    sr.Close();
                }
                catch
                {
                };
            }

            if (ok)
            {
                DBAccess.GDBAccessObj.CommitTransaction();
            }
            else
            {
                DBAccess.GDBAccessObj.RollbackTransaction();
                AMessages.Add(new TVerificationResult(Catalog.GetString("Import"),
                        Catalog.GetString("Data could not be saved."),
                        TResultSeverity.Resv_Critical));
            }

            return true;
        }

        private String SpeakingExceptionMessage(Exception ex)
        {
            //note that this is only done for "user errors" not for program errors!
            String theExMessage = ex.Message;

            if (theExMessage.Contains("a_gift_batch_fk2"))
            {
                return Catalog.GetString("Invalid account code");
            }

            if (theExMessage.Contains("a_gift_batch_fk3"))
            {
                return Catalog.GetString("Invalid cost centre");
            }

            if (theExMessage.Contains("a_gift_batch_fk4"))
            {
                return Catalog.GetString("Invalid currency code");
            }

            if (theExMessage.Contains("a_gift_fk2"))
            {
                return Catalog.GetString("Invalid method of giving");
            }

            if (theExMessage.Contains("a_gift_fk3"))
            {
                return Catalog.GetString("Invalid method of payment");
            }

            if (theExMessage.Contains("a_gift_fk4"))
            {
                return Catalog.GetString("Invalid donor partner key");
            }

            if (theExMessage.Contains("a_gift_detail_fk2"))
            {
                return Catalog.GetString("Invalid motivation detail");
            }

            if (theExMessage.Contains("a_gift_detail_fk3"))
            {
                return Catalog.GetString("Invalid recipient partner key");
            }

            if (theExMessage.Contains("a_gift_detail_fk4"))
            {
                return Catalog.GetString("Invalid mailing code");
            }

            if (theExMessage.Contains("a_gift_detail_fk5"))
            {
                return Catalog.GetString("Invalid recipient ledger number");
            }

            if (theExMessage.Contains("a_gift_detail_fk6"))
            {
                return Catalog.GetString("Invalid cost centre");
            }

            return ex.ToString();
        }

        private String ImportString(String message)
        {
            FImportMessage = String.Format(Catalog.GetString("Parsing the {0}:"), message);
            String sReturn = StringHelper.GetNextCSV(ref FImportLine, FDelimiter);

            if (sReturn.Length == 0)
            {
                return null;
            }

            return sReturn;
        }

        private Boolean ImportBoolean(String message)
        {
            FImportMessage = String.Format(Catalog.GetString("Parsing the {0}:"), message);
            String sReturn = StringHelper.GetNextCSV(ref FImportLine, FDelimiter);
            return sReturn.ToLower().Equals("yes");
        }

        private Int64 ImportInt64(String message)
        {
            FImportMessage = String.Format(Catalog.GetString("Parsing the {0}:"), message);
            String sReturn = StringHelper.GetNextCSV(ref FImportLine, FDelimiter);
            return Convert.ToInt64(sReturn);
        }

        private Int32 ImportInt32(String message)
        {
            FImportMessage = String.Format(Catalog.GetString("Parsing the {0}:"), message);
            String sReturn = StringHelper.GetNextCSV(ref FImportLine, FDelimiter);
            return Convert.ToInt32(sReturn);
        }

        private decimal ImportDecimal(String message)
        {
            FImportMessage = String.Format(Catalog.GetString("Parsing the {0}:"), message);
            String sReturn = StringHelper.GetNextCSV(ref FImportLine, FDelimiter);
            decimal dec = Convert.ToDecimal(sReturn, FCultureInfoNumberFormat);
            return dec;
        }

        private DateTime ImportDate(String message)
        {
            FImportMessage = String.Format(Catalog.GetString("Parsing the {0}:"), message);
            String sDate = StringHelper.GetNextCSV(ref FImportLine, FDelimiter);
            DateTime dtReturn = Convert.ToDateTime(sDate, FCultureInfoDate);
            return dtReturn;
        }
    }
}