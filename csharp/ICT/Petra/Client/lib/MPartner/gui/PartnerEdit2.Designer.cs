/* auto generated with nant generateWinforms from PartnerEdit2.yaml
 *
 * DO NOT edit manually, DO NOT edit with the designer
 * use a user control if you need to modify the screen content
 *
 */
/*************************************************************************
 *
 * DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
 *
 * @Authors:
 *       auto generated
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
using System.Windows.Forms;
using Mono.Unix;
using Ict.Common.Controls;
using Ict.Petra.Client.CommonControls;

namespace Ict.Petra.Client.MPartner.Gui
{
    partial class TFrmPartnerEdit2
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TFrmPartnerEdit2));

            this.pnlContent = new System.Windows.Forms.Panel();
            this.ucoUpperPart = new Ict.Petra.Client.MPartner.Gui.TUC_PartnerEdit_TopPart();
            this.pnlLowerPart = new System.Windows.Forms.Panel();
            this.ucoLowerPart = new Ict.Petra.Client.MPartner.Gui.TUC_PartnerEdit_LowerPart();
            this.tbrMain = new System.Windows.Forms.ToolStrip();
            this.tbbSave = new System.Windows.Forms.ToolStripButton();
            this.tbbNewPartner = new System.Windows.Forms.ToolStripButton();
            this.tbbSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbViewPartnerData = new System.Windows.Forms.ToolStripButton();
            this.tbbViewPersonnelData = new System.Windows.Forms.ToolStripButton();
            this.tbbViewFinanceData = new System.Windows.Forms.ToolStripButton();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mniFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mniNewPartner = new System.Windows.Forms.ToolStripMenuItem();
            this.mniNewPartnerWithShepherd = new System.Windows.Forms.ToolStripMenuItem();
            this.mniNewPartnerWithShepherdPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.mniNewPartnerWithShepherdFamily = new System.Windows.Forms.ToolStripMenuItem();
            this.mniNewPartnerWithShepherdChurch = new System.Windows.Forms.ToolStripMenuItem();
            this.mniNewPartnerWithShepherdOrganisation = new System.Windows.Forms.ToolStripMenuItem();
            this.mniNewPartnerWithShepherdUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.mniFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mniDeactivatePartner = new System.Windows.Forms.ToolStripMenuItem();
            this.mniDeletePartner = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mniSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mniPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mniPrintSection = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mniExportPartner = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mniClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEditUndoCurrentField = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEditUndoScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mniEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMaintain = new System.Windows.Forms.ToolStripMenuItem();
            this.mniAddresses = new System.Windows.Forms.ToolStripMenuItem();
            this.mniView = new System.Windows.Forms.ToolStripMenuItem();
            this.mniViewPartnerData = new System.Windows.Forms.ToolStripMenuItem();
            this.mniViewPersonnelData = new System.Windows.Forms.ToolStripMenuItem();
            this.mniViewFinanceData = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelpPetraHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mniHelpBugReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mniHelpAboutPetra = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelpDevelopmentTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.stbMain = new Ict.Common.Controls.TExtStatusBarHelp();

            this.pnlContent.SuspendLayout();
            this.pnlLowerPart.SuspendLayout();
            this.tbrMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.stbMain.SuspendLayout();

            //
            // pnlContent
            //
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.AutoSize = true;
            this.pnlContent.Controls.Add(this.pnlLowerPart);
            this.pnlContent.Controls.Add(this.ucoUpperPart);
            //
            // ucoUpperPart
            //
            this.ucoUpperPart.Name = "ucoUpperPart";
            this.ucoUpperPart.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucoUpperPart.AutoSize = true;
            //
            // pnlLowerPart
            //
            this.pnlLowerPart.Name = "pnlLowerPart";
            this.pnlLowerPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLowerPart.AutoSize = true;
            this.pnlLowerPart.Controls.Add(this.ucoLowerPart);
            //
            // ucoLowerPart
            //
            this.ucoLowerPart.Name = "ucoLowerPart";
            this.ucoLowerPart.Dock = System.Windows.Forms.DockStyle.Fill;
            //
            // tbbSave
            //
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.AutoSize = true;
            this.tbbSave.Click += new System.EventHandler(this.FileSave);
            this.tbbSave.Image = ((System.Drawing.Bitmap)resources.GetObject("tbbSave.Glyph"));
            this.tbbSave.ToolTipText = "Saves changed data";
            this.tbbSave.Text = "&Save";
            //
            // tbbNewPartner
            //
            this.tbbNewPartner.Name = "tbbNewPartner";
            this.tbbNewPartner.AutoSize = true;
            this.tbbNewPartner.Click += new System.EventHandler(this.NewPartner);
            this.tbbNewPartner.Image = ((System.Drawing.Bitmap)resources.GetObject("tbbNewPartner.Glyph"));
            this.tbbNewPartner.Text = "&New Partner";
            //
            // tbbSeparator0
            //
            this.tbbSeparator0.Name = "tbbSeparator0";
            this.tbbSeparator0.AutoSize = true;
            this.tbbSeparator0.Text = "Separator";
            //
            // tbbViewPartnerData
            //
            this.tbbViewPartnerData.Name = "tbbViewPartnerData";
            this.tbbViewPartnerData.AutoSize = true;
            this.tbbViewPartnerData.Click += new System.EventHandler(this.ViewPartnerData);
            this.tbbViewPartnerData.Image = ((System.Drawing.Bitmap)resources.GetObject("tbbViewPartnerData.Glyph"));
            this.tbbViewPartnerData.Text = "&Partner Data";
            //
            // tbbViewPersonnelData
            //
            this.tbbViewPersonnelData.Name = "tbbViewPersonnelData";
            this.tbbViewPersonnelData.AutoSize = true;
            this.tbbViewPersonnelData.Click += new System.EventHandler(this.ViewPersonnelData);
            this.tbbViewPersonnelData.Image = ((System.Drawing.Bitmap)resources.GetObject("tbbViewPersonnelData.Glyph"));
            this.tbbViewPersonnelData.Text = "P&ersonnel Data";
            //
            // tbbViewFinanceData
            //
            this.tbbViewFinanceData.Name = "tbbViewFinanceData";
            this.tbbViewFinanceData.AutoSize = true;
            this.tbbViewFinanceData.Click += new System.EventHandler(this.ViewFinanceData);
            this.tbbViewFinanceData.Image = ((System.Drawing.Bitmap)resources.GetObject("tbbViewFinanceData.Glyph"));
            this.tbbViewFinanceData.Text = "&Finance Data";
            //
            // tbrMain
            //
            this.tbrMain.Name = "tbrMain";
            this.tbrMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbrMain.AutoSize = true;
            this.tbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           tbbSave,
                        tbbNewPartner,
                        tbbSeparator0,
                        tbbViewPartnerData,
                        tbbViewPersonnelData,
                        tbbViewFinanceData});
            //
            // mniNewPartner
            //
            this.mniNewPartner.Name = "mniNewPartner";
            this.mniNewPartner.AutoSize = true;
            this.mniNewPartner.Click += new System.EventHandler(this.NewPartner);
            this.mniNewPartner.Image = ((System.Drawing.Bitmap)resources.GetObject("mniNewPartner.Glyph"));
            this.mniNewPartner.Text = "&New Partner...";
            //
            // mniNewPartnerWithShepherdPerson
            //
            this.mniNewPartnerWithShepherdPerson.Name = "mniNewPartnerWithShepherdPerson";
            this.mniNewPartnerWithShepherdPerson.AutoSize = true;
            this.mniNewPartnerWithShepherdPerson.Click += new System.EventHandler(this.NewPartnerWithShepherdPerson);
            this.mniNewPartnerWithShepherdPerson.Text = "Add &Person with Shepherd...";
            //
            // mniNewPartnerWithShepherdFamily
            //
            this.mniNewPartnerWithShepherdFamily.Name = "mniNewPartnerWithShepherdFamily";
            this.mniNewPartnerWithShepherdFamily.AutoSize = true;
            this.mniNewPartnerWithShepherdFamily.Click += new System.EventHandler(this.NewPartnerWithShepherdFamily);
            this.mniNewPartnerWithShepherdFamily.Text = "Add &Family with Shepherd...";
            //
            // mniNewPartnerWithShepherdChurch
            //
            this.mniNewPartnerWithShepherdChurch.Name = "mniNewPartnerWithShepherdChurch";
            this.mniNewPartnerWithShepherdChurch.AutoSize = true;
            this.mniNewPartnerWithShepherdChurch.Click += new System.EventHandler(this.NewPartnerWithShepherdChurch);
            this.mniNewPartnerWithShepherdChurch.Text = "Add &Church with Shepherd...";
            //
            // mniNewPartnerWithShepherdOrganisation
            //
            this.mniNewPartnerWithShepherdOrganisation.Name = "mniNewPartnerWithShepherdOrganisation";
            this.mniNewPartnerWithShepherdOrganisation.AutoSize = true;
            this.mniNewPartnerWithShepherdOrganisation.Click += new System.EventHandler(this.NewPartnerWithShepherdOrganisation);
            this.mniNewPartnerWithShepherdOrganisation.Text = "Add &Organisation with Shepherd...";
            //
            // mniNewPartnerWithShepherdUnit
            //
            this.mniNewPartnerWithShepherdUnit.Name = "mniNewPartnerWithShepherdUnit";
            this.mniNewPartnerWithShepherdUnit.AutoSize = true;
            this.mniNewPartnerWithShepherdUnit.Click += new System.EventHandler(this.NewPartnerWithShepherdUnit);
            this.mniNewPartnerWithShepherdUnit.Text = "Add &Unit with Shepherd...";
            //
            // mniNewPartnerWithShepherd
            //
            this.mniNewPartnerWithShepherd.Name = "mniNewPartnerWithShepherd";
            this.mniNewPartnerWithShepherd.AutoSize = true;
            this.mniNewPartnerWithShepherd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniNewPartnerWithShepherdPerson,
                        mniNewPartnerWithShepherdFamily,
                        mniNewPartnerWithShepherdChurch,
                        mniNewPartnerWithShepherdOrganisation,
                        mniNewPartnerWithShepherdUnit});
            this.mniNewPartnerWithShepherd.Text = "New Partner With Shepherd";
            //
            // mniFileSave
            //
            this.mniFileSave.Name = "mniFileSave";
            this.mniFileSave.AutoSize = true;
            this.mniFileSave.Click += new System.EventHandler(this.FileSave);
            this.mniFileSave.Image = ((System.Drawing.Bitmap)resources.GetObject("mniFileSave.Glyph"));
            this.mniFileSave.ToolTipText = "Saves changed data";
            this.mniFileSave.Text = "&Save";
            //
            // mniSeparator0
            //
            this.mniSeparator0.Name = "mniSeparator0";
            this.mniSeparator0.AutoSize = true;
            this.mniSeparator0.Text = "-";
            //
            // mniFilePrint
            //
            this.mniFilePrint.Name = "mniFilePrint";
            this.mniFilePrint.AutoSize = true;
            this.mniFilePrint.Text = "&Print...";
            //
            // mniSeparator1
            //
            this.mniSeparator1.Name = "mniSeparator1";
            this.mniSeparator1.AutoSize = true;
            this.mniSeparator1.Text = "-";
            //
            // mniDeactivatePartner
            //
            this.mniDeactivatePartner.Name = "mniDeactivatePartner";
            this.mniDeactivatePartner.AutoSize = true;
            this.mniDeactivatePartner.Click += new System.EventHandler(this.DeactivatePartner);
            this.mniDeactivatePartner.Image = ((System.Drawing.Bitmap)resources.GetObject("mniDeactivatePartner.Glyph"));
            this.mniDeactivatePartner.Text = "Deacti&vate Partner...";
            //
            // mniDeletePartner
            //
            this.mniDeletePartner.Name = "mniDeletePartner";
            this.mniDeletePartner.AutoSize = true;
            this.mniDeletePartner.Click += new System.EventHandler(this.DeletePartner);
            this.mniDeletePartner.Image = ((System.Drawing.Bitmap)resources.GetObject("mniDeletePartner.Glyph"));
            this.mniDeletePartner.Text = "&Delete THIS Partner...";
            //
            // mniSeparator2
            //
            this.mniSeparator2.Name = "mniSeparator2";
            this.mniSeparator2.AutoSize = true;
            this.mniSeparator2.Text = "Separator";
            //
            // mniSendEmail
            //
            this.mniSendEmail.Name = "mniSendEmail";
            this.mniSendEmail.AutoSize = true;
            this.mniSendEmail.Click += new System.EventHandler(this.SendEmail);
            this.mniSendEmail.Image = ((System.Drawing.Bitmap)resources.GetObject("mniSendEmail.Glyph"));
            this.mniSendEmail.Text = "Send E&mail to Partner";
            //
            // mniSeparator3
            //
            this.mniSeparator3.Name = "mniSeparator3";
            this.mniSeparator3.AutoSize = true;
            this.mniSeparator3.Text = "Separator";
            //
            // mniPrint
            //
            this.mniPrint.Name = "mniPrint";
            this.mniPrint.AutoSize = true;
            this.mniPrint.Click += new System.EventHandler(this.PrintPartner);
            this.mniPrint.Image = ((System.Drawing.Bitmap)resources.GetObject("mniPrint.Glyph"));
            this.mniPrint.Text = "Print Partner...";
            //
            // mniPrintSection
            //
            this.mniPrintSection.Name = "mniPrintSection";
            this.mniPrintSection.AutoSize = true;
            this.mniPrintSection.Click += new System.EventHandler(this.DeletePartner);
            this.mniPrintSection.Image = ((System.Drawing.Bitmap)resources.GetObject("mniPrintSection.Glyph"));
            this.mniPrintSection.Text = "P&rint Section...";
            //
            // mniSeparator4
            //
            this.mniSeparator4.Name = "mniSeparator4";
            this.mniSeparator4.AutoSize = true;
            this.mniSeparator4.Text = "Separator";
            //
            // mniExportPartner
            //
            this.mniExportPartner.Name = "mniExportPartner";
            this.mniExportPartner.AutoSize = true;
            this.mniExportPartner.Click += new System.EventHandler(this.ExportPartner);
            this.mniExportPartner.Image = ((System.Drawing.Bitmap)resources.GetObject("mniExportPartner.Glyph"));
            this.mniExportPartner.Text = "E&xport Partner";
            //
            // mniSeparator5
            //
            this.mniSeparator5.Name = "mniSeparator5";
            this.mniSeparator5.AutoSize = true;
            this.mniSeparator5.Text = "Separator";
            //
            // mniClose
            //
            this.mniClose.Name = "mniClose";
            this.mniClose.AutoSize = true;
            this.mniClose.Click += new System.EventHandler(this.actClose);
            this.mniClose.Image = ((System.Drawing.Bitmap)resources.GetObject("mniClose.Glyph"));
            this.mniClose.ToolTipText = "Closes this window";
            this.mniClose.Text = "&Close";
            //
            // mniFile
            //
            this.mniFile.Name = "mniFile";
            this.mniFile.AutoSize = true;
            this.mniFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniNewPartner,
                        mniNewPartnerWithShepherd,
                        mniFileSave,
                        mniSeparator0,
                        mniFilePrint,
                        mniSeparator1,
                        mniDeactivatePartner,
                        mniDeletePartner,
                        mniSeparator2,
                        mniSendEmail,
                        mniSeparator3,
                        mniPrint,
                        mniPrintSection,
                        mniSeparator4,
                        mniExportPartner,
                        mniSeparator5,
                        mniClose});
            this.mniFile.Text = "&File";
            //
            // mniEditUndoCurrentField
            //
            this.mniEditUndoCurrentField.Name = "mniEditUndoCurrentField";
            this.mniEditUndoCurrentField.AutoSize = true;
            this.mniEditUndoCurrentField.Text = "Undo &Current Field";
            //
            // mniEditUndoScreen
            //
            this.mniEditUndoScreen.Name = "mniEditUndoScreen";
            this.mniEditUndoScreen.AutoSize = true;
            this.mniEditUndoScreen.Text = "&Undo Screen";
            //
            // mniSeparator6
            //
            this.mniSeparator6.Name = "mniSeparator6";
            this.mniSeparator6.AutoSize = true;
            this.mniSeparator6.Text = "-";
            //
            // mniEditFind
            //
            this.mniEditFind.Name = "mniEditFind";
            this.mniEditFind.AutoSize = true;
            this.mniEditFind.Text = "&Find...";
            //
            // mniEdit
            //
            this.mniEdit.Name = "mniEdit";
            this.mniEdit.AutoSize = true;
            this.mniEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniEditUndoCurrentField,
                        mniEditUndoScreen,
                        mniSeparator6,
                        mniEditFind});
            this.mniEdit.Text = "&Edit";
            //
            // mniAddresses
            //
            this.mniAddresses.Name = "mniAddresses";
            this.mniAddresses.AutoSize = true;
            this.mniAddresses.Click += new System.EventHandler(this.MaintainAddresses);
            this.mniAddresses.Image = ((System.Drawing.Bitmap)resources.GetObject("mniAddresses.Glyph"));
            this.mniAddresses.Text = "&Addresses";
            //
            // mniMaintain
            //
            this.mniMaintain.Name = "mniMaintain";
            this.mniMaintain.AutoSize = true;
            this.mniMaintain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniAddresses});
            this.mniMaintain.Text = "Ma&intain";
            //
            // mniViewPartnerData
            //
            this.mniViewPartnerData.Name = "mniViewPartnerData";
            this.mniViewPartnerData.AutoSize = true;
            this.mniViewPartnerData.Click += new System.EventHandler(this.ViewPartnerData);
            this.mniViewPartnerData.Image = ((System.Drawing.Bitmap)resources.GetObject("mniViewPartnerData.Glyph"));
            this.mniViewPartnerData.Text = "&Partner Data";
            //
            // mniViewPersonnelData
            //
            this.mniViewPersonnelData.Name = "mniViewPersonnelData";
            this.mniViewPersonnelData.AutoSize = true;
            this.mniViewPersonnelData.Click += new System.EventHandler(this.ViewPersonnelData);
            this.mniViewPersonnelData.Image = ((System.Drawing.Bitmap)resources.GetObject("mniViewPersonnelData.Glyph"));
            this.mniViewPersonnelData.Text = "P&ersonnel Data";
            //
            // mniViewFinanceData
            //
            this.mniViewFinanceData.Name = "mniViewFinanceData";
            this.mniViewFinanceData.AutoSize = true;
            this.mniViewFinanceData.Click += new System.EventHandler(this.ViewFinanceData);
            this.mniViewFinanceData.Image = ((System.Drawing.Bitmap)resources.GetObject("mniViewFinanceData.Glyph"));
            this.mniViewFinanceData.Text = "&Finance Data";
            //
            // mniView
            //
            this.mniView.Name = "mniView";
            this.mniView.AutoSize = true;
            this.mniView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniViewPartnerData,
                        mniViewPersonnelData,
                        mniViewFinanceData});
            this.mniView.Text = "Vie&w";
            //
            // mniHelpPetraHelp
            //
            this.mniHelpPetraHelp.Name = "mniHelpPetraHelp";
            this.mniHelpPetraHelp.AutoSize = true;
            this.mniHelpPetraHelp.Text = "&Petra Help";
            //
            // mniSeparator7
            //
            this.mniSeparator7.Name = "mniSeparator7";
            this.mniSeparator7.AutoSize = true;
            this.mniSeparator7.Text = "-";
            //
            // mniHelpBugReport
            //
            this.mniHelpBugReport.Name = "mniHelpBugReport";
            this.mniHelpBugReport.AutoSize = true;
            this.mniHelpBugReport.Text = "Bug &Report";
            //
            // mniSeparator8
            //
            this.mniSeparator8.Name = "mniSeparator8";
            this.mniSeparator8.AutoSize = true;
            this.mniSeparator8.Text = "-";
            //
            // mniHelpAboutPetra
            //
            this.mniHelpAboutPetra.Name = "mniHelpAboutPetra";
            this.mniHelpAboutPetra.AutoSize = true;
            this.mniHelpAboutPetra.Text = "&About Petra";
            //
            // mniHelpDevelopmentTeam
            //
            this.mniHelpDevelopmentTeam.Name = "mniHelpDevelopmentTeam";
            this.mniHelpDevelopmentTeam.AutoSize = true;
            this.mniHelpDevelopmentTeam.Text = "&The Development Team...";
            //
            // mniHelp
            //
            this.mniHelp.Name = "mniHelp";
            this.mniHelp.AutoSize = true;
            this.mniHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniHelpPetraHelp,
                        mniSeparator7,
                        mniHelpBugReport,
                        mniSeparator8,
                        mniHelpAboutPetra,
                        mniHelpDevelopmentTeam});
            this.mniHelp.Text = "&Help";
            //
            // mnuMain
            //
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.mnuMain.AutoSize = true;
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniFile,
                        mniEdit,
                        mniMaintain,
                        mniView,
                        mniHelp});
            //
            // stbMain
            //
            this.stbMain.Name = "stbMain";
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.AutoSize = true;

            //
            // TFrmPartnerEdit2
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 700);
            // this.rpsForm.SetRestoreLocation(this, false);  for the moment false, to avoid problems with size
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.tbrMain);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = mnuMain;
            this.Controls.Add(this.stbMain);
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            this.Name = "TFrmPartnerEdit2";
            this.Text = "Partner Edit";

	        this.Activated += new System.EventHandler(this.TFrmPetra_Activated);
	        this.Closing += new System.ComponentModel.CancelEventHandler(this.TFrmPetra_Closing);
	        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
	        this.Closed += new System.EventHandler(this.TFrmPetra_Closed);
	        this.Load += new System.EventHandler(this.TFrmPartnerEdit2_Load);
	
            this.stbMain.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.tbrMain.ResumeLayout(false);
            this.pnlLowerPart.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Panel pnlContent;
        private Ict.Petra.Client.MPartner.Gui.TUC_PartnerEdit_TopPart ucoUpperPart;
        private System.Windows.Forms.Panel pnlLowerPart;
        private Ict.Petra.Client.MPartner.Gui.TUC_PartnerEdit_LowerPart ucoLowerPart;
        private System.Windows.Forms.ToolStrip tbrMain;
        private System.Windows.Forms.ToolStripButton tbbSave;
        private System.Windows.Forms.ToolStripButton tbbNewPartner;
        private System.Windows.Forms.ToolStripSeparator tbbSeparator0;
        private System.Windows.Forms.ToolStripButton tbbViewPartnerData;
        private System.Windows.Forms.ToolStripButton tbbViewPersonnelData;
        private System.Windows.Forms.ToolStripButton tbbViewFinanceData;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mniFile;
        private System.Windows.Forms.ToolStripMenuItem mniNewPartner;
        private System.Windows.Forms.ToolStripMenuItem mniNewPartnerWithShepherd;
        private System.Windows.Forms.ToolStripMenuItem mniNewPartnerWithShepherdPerson;
        private System.Windows.Forms.ToolStripMenuItem mniNewPartnerWithShepherdFamily;
        private System.Windows.Forms.ToolStripMenuItem mniNewPartnerWithShepherdChurch;
        private System.Windows.Forms.ToolStripMenuItem mniNewPartnerWithShepherdOrganisation;
        private System.Windows.Forms.ToolStripMenuItem mniNewPartnerWithShepherdUnit;
        private System.Windows.Forms.ToolStripMenuItem mniFileSave;
        private System.Windows.Forms.ToolStripSeparator mniSeparator0;
        private System.Windows.Forms.ToolStripMenuItem mniFilePrint;
        private System.Windows.Forms.ToolStripSeparator mniSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mniDeactivatePartner;
        private System.Windows.Forms.ToolStripMenuItem mniDeletePartner;
        private System.Windows.Forms.ToolStripSeparator mniSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mniSendEmail;
        private System.Windows.Forms.ToolStripSeparator mniSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mniPrint;
        private System.Windows.Forms.ToolStripMenuItem mniPrintSection;
        private System.Windows.Forms.ToolStripSeparator mniSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mniExportPartner;
        private System.Windows.Forms.ToolStripSeparator mniSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mniClose;
        private System.Windows.Forms.ToolStripMenuItem mniEdit;
        private System.Windows.Forms.ToolStripMenuItem mniEditUndoCurrentField;
        private System.Windows.Forms.ToolStripMenuItem mniEditUndoScreen;
        private System.Windows.Forms.ToolStripSeparator mniSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mniEditFind;
        private System.Windows.Forms.ToolStripMenuItem mniMaintain;
        private System.Windows.Forms.ToolStripMenuItem mniAddresses;
        private System.Windows.Forms.ToolStripMenuItem mniView;
        private System.Windows.Forms.ToolStripMenuItem mniViewPartnerData;
        private System.Windows.Forms.ToolStripMenuItem mniViewPersonnelData;
        private System.Windows.Forms.ToolStripMenuItem mniViewFinanceData;
        private System.Windows.Forms.ToolStripMenuItem mniHelp;
        private System.Windows.Forms.ToolStripMenuItem mniHelpPetraHelp;
        private System.Windows.Forms.ToolStripSeparator mniSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mniHelpBugReport;
        private System.Windows.Forms.ToolStripSeparator mniSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mniHelpAboutPetra;
        private System.Windows.Forms.ToolStripMenuItem mniHelpDevelopmentTeam;
        private Ict.Common.Controls.TExtStatusBarHelp stbMain;
    }
}
