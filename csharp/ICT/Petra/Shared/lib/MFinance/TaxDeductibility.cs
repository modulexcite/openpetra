//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       peters
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

using Ict.Petra.Shared.MFinance.Gift.Data;

namespace Ict.Petra.Shared.MFinance
{
    /// <summary>
    /// Contains several functions which are specific to the Petra Finance Module.
    /// </summary>
    public static class TaxDeductibility
    {
        /// <summary>
        /// Calculate the Tax-Deductible and Non-Deductible Transaction, Base and Intl amounts for a Gift Detail
        /// </summary>
        /// <param name="AGiftDetail"></param>
        public static void UpdateTaxDeductibiltyAmounts(ref AGiftDetailRow AGiftDetail)
        {
            /* Update transaction amounts */

            decimal TaxDeductAmount;
            decimal NonDeductAmount;

            CalculateTaxDeductibilityAmounts(
                out TaxDeductAmount, out NonDeductAmount, AGiftDetail.GiftTransactionAmount, AGiftDetail.TaxDeductiblePct);

            AGiftDetail.TaxDeductibleAmount = TaxDeductAmount;
            AGiftDetail.NonDeductibleAmount = NonDeductAmount;

            /* Update base amounts */

            CalculateTaxDeductibilityAmounts(
                out TaxDeductAmount, out NonDeductAmount, AGiftDetail.GiftAmount, AGiftDetail.TaxDeductiblePct);

            AGiftDetail.TaxDeductibleAmountBase = TaxDeductAmount;
            AGiftDetail.NonDeductibleAmountBase = NonDeductAmount;

            /* Update intl amounts */

            CalculateTaxDeductibilityAmounts(
                out TaxDeductAmount, out NonDeductAmount, AGiftDetail.GiftAmountIntl, AGiftDetail.TaxDeductiblePct);

            AGiftDetail.TaxDeductibleAmountIntl = TaxDeductAmount;
            AGiftDetail.NonDeductibleAmountIntl = NonDeductAmount;
        }

        /// <summary>
        /// Calculate Tax-Deductible Amount and Non-Deductible Amount for a gift amount using the tax deductible percentage
        /// </summary>
        /// <param name="ATaxDeductAmount"></param>
        /// <param name="ANonDeductAmount"></param>
        /// <param name="AGiftAmount"></param>
        /// <param name="ADeductiblePercentage"></param>
        private static void CalculateTaxDeductibilityAmounts(
            out decimal ATaxDeductAmount, out decimal ANonDeductAmount, decimal AGiftAmount, decimal ADeductiblePercentage)
        {
            ATaxDeductAmount =
                AGiftAmount * (ADeductiblePercentage / 100);
            ANonDeductAmount =
                AGiftAmount * (1 - (ADeductiblePercentage / 100));

            // correct rounding errors
            while (ATaxDeductAmount + ANonDeductAmount
                   < AGiftAmount)
            {
                if (ADeductiblePercentage >= 50)
                {
                    ATaxDeductAmount += (decimal)0.01;
                }
                else
                {
                    ANonDeductAmount += (decimal)0.01;
                }
            }

            while (ATaxDeductAmount + ANonDeductAmount
                   > AGiftAmount)
            {
                if (ADeductiblePercentage >= 50)
                {
                    ATaxDeductAmount -= (decimal)0.01;
                }
                else
                {
                    ANonDeductAmount -= (decimal)0.01;
                }
            }
        }
    }
}