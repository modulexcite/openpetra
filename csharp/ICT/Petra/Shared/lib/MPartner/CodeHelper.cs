﻿//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       christiank
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
using System.Data;

using Ict.Petra.Shared.MPartner.Partner.Data;

namespace Ict.Petra.Shared.MPartner
{
    /// <summary>
    /// Gets descriptions for codes in the Partner Module.
    /// </summary>
    public class PartnerCodeHelper
    {
        /// <summary>
        /// Returns the description of a Marital Status Code.
        /// </summary>
        /// <param name="ACacheRetriever">Delegate that returns the specified DataTable from the data cache (client- or serverside).
        /// Delegate Method needs to be for the MPartner Cache (that is, it needs to work with the <see cref="TCacheablePartnerTablesEnum" /> Enum!</param>
        /// <param name="AMaritalStatusCode">Marital Status Code.</param>
        /// <returns>The description of a Marital Status Code, or empty string if the Marital Status Code could not be identified.</returns>
        public static string GetMaritalStatusDescription(TGetCacheableDataTableFromCache ACacheRetriever, string AMaritalStatusCode)
        {
            DataTable CachedDT;
            DataRow FoundDR;
            string ReturnValue = "";
            Type tmp;

            CachedDT = ACacheRetriever(Enum.GetName(typeof(TCacheablePartnerTablesEnum), TCacheablePartnerTablesEnum.MaritalStatusList), out tmp);
            FoundDR = CachedDT.Rows.Find(new object[] { AMaritalStatusCode });

            if (FoundDR != null)
            {
                ReturnValue = FoundDR[PtMaritalStatusTable.ColumnDescriptionId].ToString();
            }

            return ReturnValue;
        }

        /// <summary>
        /// Update extra location specific fields in table APartnerLocationTable
        /// </summary>
        /// <param name="ALocationTable">Table containing location records to be used to update APartnerLocation records</param>
        /// <param name="APartnerLocationTable">Table to be updated with Location specific information</param>
        /// <returns></returns>
        public static void SyncPartnerEditTDSPartnerLocation(PLocationTable ALocationTable, PartnerEditTDSPPartnerLocationTable APartnerLocationTable)
        {
            DataRow Row;
            PLocationRow LocationRow;

            foreach (PartnerEditTDSPPartnerLocationRow PartnerLocationRow in APartnerLocationTable.Rows)
            {
                Row = ALocationTable.Rows.Find(new Object[] { PartnerLocationRow.SiteKey, PartnerLocationRow.LocationKey });

                if (Row != null)
                {
                    LocationRow = (PLocationRow)Row;

                    PartnerLocationRow.LocationLocality = LocationRow.Locality;
                    PartnerLocationRow.LocationStreetName = LocationRow.StreetName;
                    PartnerLocationRow.LocationAddress3 = LocationRow.Address3;
                    PartnerLocationRow.LocationCity = LocationRow.City;
                    PartnerLocationRow.LocationCounty = LocationRow.County;
                    PartnerLocationRow.LocationPostalCode = LocationRow.PostalCode;
                    PartnerLocationRow.LocationCountryCode = LocationRow.CountryCode;

                    PartnerLocationRow.LocationCreatedBy = LocationRow.CreatedBy;

                    if (!LocationRow.IsDateCreatedNull())
                    {
                        PartnerLocationRow.LocationDateCreated = (DateTime)LocationRow.DateCreated;
                    }

                    PartnerLocationRow.LocationModifiedBy = LocationRow.ModifiedBy;

                    if (!LocationRow.IsDateModifiedNull())
                    {
                        PartnerLocationRow.LocationDateModified = (DateTime)LocationRow.DateModified;
                    }
                }
            }
        }
    }
}