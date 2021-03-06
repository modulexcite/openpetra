//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       wolfgangb
//
// Copyright 2004-2012 by OM International
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
using System.Windows.Forms;
using Ict.Common;
using Ict.Common.Data;
using Ict.Common.Verification;
using Ict.Petra.Shared;
using Ict.Petra.Shared.MPersonnel.Personnel.Data;
using Ict.Petra.Shared.MPersonnel;
using Ict.Petra.Shared.MPartner.Validation;
using Ict.Petra.Client.App.Core;
using Ict.Petra.Client.App.Gui;
using Ict.Petra.Client.CommonControls;

namespace Ict.Petra.Client.MPartner.Gui
{
    public partial class TUC_Application_Field
    {
        // <summary>holds a reference to the Proxy System.Object of the Serverside UIConnector</summary>
        //private IPartnerUIConnectorsPartnerEdit FPartnerEditUIConnector;

        private IndividualDataTDS FMainDS;          // FMainDS is NOT of Type 'PartnerEditTDS' in this UserControl!!!
        private ApplicationTDS FApplicationDS;
        private int CurrentTabIndex = 0;

        /// <summary>Application Field changed</summary>
        public delegate void TDelegateApplicationFieldChanged(Int64 APartnerKey,
            int AApplicationKey,
            Int64 ARegistrationOffice,
            Int64 AFieldKey,
            String AFieldName);

        /// <summary>event to signalize change in field applied for</summary>
        public event TDelegateApplicationFieldChanged ApplicationFieldChanged;

        #region Properties

        /// dataset for the whole screen
        public IndividualDataTDS MainDS
        {
            get
            {
                return FMainDS;
            }

            set
            {
                FMainDS = value;
            }
        }

        /// return label text for "Field" field
        public String FieldLabelText
        {
            get
            {
                return ucoField.FieldLabelText;
            }
        }

        /// return code value for "Field"
        public String FieldValueCode
        {
            get
            {
                return ucoField.FieldValueCode;
            }
        }

        /// return label value for "Field"
        public String FieldValueLabel
        {
            get
            {
                return ucoField.FieldValueLabel;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>todoComment</summary>
        public event THookupPartnerEditDataChangeEventHandler HookupDataChange;

        /// <summary>
        /// Sets up the screen logic, retrieves data, databinds the Grid and the Detail
        /// UserControl.
        /// </summary>
        /// <returns>void</returns>
        public void InitialiseUserControl()
        {
            FApplicationDS = new ApplicationTDS();
            FApplicationDS.InitVars();

            // the following lines are just dummy code to remove compiler warnings as those members are never used
            if (FTabSetup == null)
            {
                FTabSetup = null;
            }

            if (FTabPageEvent == null)
            {
                FTabPageEvent = null;
            }

            ucoField.PetraUtilsObject = FPetraUtilsObject;
            ucoApplicant.PetraUtilsObject = FPetraUtilsObject;

            ucoField.MainDS = FApplicationDS;
            ucoApplicant.MainDS = FApplicationDS;

            // enable control to react to modified event or field key in details part
            ucoField.ApplicationFieldChanged += new TDelegatePartnerChanged(ProcessApplicationFieldChanged);

            // handle tab changing in case validation fails
            tabApplicationField.Selecting += new TabControlCancelEventHandler(TabSelectionChanging);
        }

        /// <summary>
        /// This Method is needed for UserControls who get dynamicly loaded on TabPages.
        /// Since we don't have controls on this UserControl that need adjusting after resizing
        /// on 'Large Fonts (120 DPI)', we don't need to do anything here.
        /// </summary>
        public void AdjustAfterResizing()
        {
        }

        /// <summary>
        /// Display data in control based on data from Rows
        /// </summary>
        /// <param name="AGeneralAppRow"></param>
        /// <param name="AFieldAppRow"></param>
        public void ShowDetails(PmGeneralApplicationRow AGeneralAppRow, PmYearProgramApplicationRow AFieldAppRow)
        {
            ShowData(AGeneralAppRow, AFieldAppRow);
        }

        /// <summary>
        /// Read data from controls into Row parameters
        /// </summary>
        /// <param name="ARow"></param>
        /// <param name="AFieldAppRow"></param>
        public void GetDetails(PmGeneralApplicationRow ARow, PmYearProgramApplicationRow AFieldAppRow)
        {
            GetDataFromControls(ARow, AFieldAppRow);
        }

        /// <summary>
        /// Performs data validation.
        /// </summary>
        /// <remarks>May be called by the Form that hosts this UserControl to invoke the data validation of
        /// the UserControl.</remarks>
        /// <param name="AProcessAnyDataValidationErrors">Set to true if data validation errors should be shown to the
        /// user, otherwise set it to false.</param>
        /// <returns>True if data validation succeeded or if there is no current row, otherwise false.</returns>
        public bool ValidateAllData(bool AProcessAnyDataValidationErrors)
        {
            bool ReturnValue = true;

            if (!ucoField.ValidateAllData(AProcessAnyDataValidationErrors))
            {
                ReturnValue = false;
            }

            if (!ucoApplicant.ValidateAllData(AProcessAnyDataValidationErrors))
            {
                ReturnValue = false;
            }

            return ReturnValue;
        }

        #endregion

        #region Private Methods

        private void OnHookupDataChange(THookupPartnerEditDataChangeEventArgs e)
        {
            if (HookupDataChange != null)
            {
                HookupDataChange(this, e);
            }
        }

        private void InitializeManualCode()
        {
        }

        private void GetDataFromControlsManual(PmGeneralApplicationRow ARow)
        {
        }

        private void ShowData(PmGeneralApplicationRow AGeneralAppRow, PmYearProgramApplicationRow AFieldAppRow)
        {
            // clear dataset and create a copy of the row to be displayed so Dataset contains only one set of records
            FApplicationDS.PmYearProgramApplication.Rows.Clear();
            FApplicationDS.PmGeneralApplication.Rows.Clear();

            PmGeneralApplicationRow GeneralAppRowCopy = (PmGeneralApplicationRow)FApplicationDS.PmGeneralApplication.NewRow();
            PmYearProgramApplicationRow FieldAppRowCopy = (PmYearProgramApplicationRow)FApplicationDS.PmYearProgramApplication.NewRow();

            DataUtilities.CopyAllColumnValues(AGeneralAppRow, GeneralAppRowCopy);
            DataUtilities.CopyAllColumnValues(AFieldAppRow, FieldAppRowCopy);

            FApplicationDS.PmGeneralApplication.Rows.Add(GeneralAppRowCopy);
            FApplicationDS.PmYearProgramApplication.Rows.Add(FieldAppRowCopy);

            ucoField.ShowDetails(GeneralAppRowCopy);
            ucoApplicant.ShowDetails(GeneralAppRowCopy);
        }

        private void GetDataFromControls(PmGeneralApplicationRow ARow, PmYearProgramApplicationRow AFieldAppRow)
        {
            ucoField.GetDetails(FApplicationDS.PmGeneralApplication[0]);
            ucoApplicant.GetDetails(FApplicationDS.PmGeneralApplication[0]);

            DataUtilities.CopyAllColumnValues(FApplicationDS.PmGeneralApplication[0], ARow);
            DataUtilities.CopyAllColumnValues(FApplicationDS.PmYearProgramApplication[0], AFieldAppRow);
        }

        private void ProcessApplicationFieldChanged(Int64 AFieldKey, String AFieldName, bool AValidSelection)
        {
            PmGeneralApplicationRow Row;

            Row = (PmGeneralApplicationRow)FApplicationDS.PmGeneralApplication.Rows[0];

            // trigger event so parent controls can react
            this.ApplicationFieldChanged(Row.PartnerKey, Row.ApplicationKey, Row.RegistrationOffice, AFieldKey, AFieldName);
        }

        private int standardTabIndex = 0;

        private void TUC_Application_Field_Load(object sender, EventArgs e)
        {
            FPetraUtilsObject.TFrmPetra_Load(sender, e);

            tabApplicationField.SelectedIndex = standardTabIndex;
            //TabSelectionChanged(null, null);
            tabApplicationField.Selecting += new TabControlCancelEventHandler(TabSelectionChanging);
        }

        private void TabSelectionChanging(object sender, TabControlCancelEventArgs e)
        {
            FPetraUtilsObject.VerificationResultCollection.Clear();

            if (CurrentTabIndex == 0)
            {
                FCurrentUserControl = ucoField;

                if (!ucoField.ValidateAllData(true, FCurrentUserControl))
                {
                    e.Cancel = true;

                    FPetraUtilsObject.VerificationResultCollection.FocusOnFirstErrorControlRequested = true;
                }
            }
            else
            {
                FCurrentUserControl = ucoApplicant;

                if (!ucoApplicant.ValidateAllData(true, FCurrentUserControl))
                {
                    e.Cancel = true;

                    FPetraUtilsObject.VerificationResultCollection.FocusOnFirstErrorControlRequested = true;
                }
            }

            if (!e.Cancel)
            {
                CurrentTabIndex = tabApplicationField.SelectedIndex;
            }
        }

        #endregion
    }
}