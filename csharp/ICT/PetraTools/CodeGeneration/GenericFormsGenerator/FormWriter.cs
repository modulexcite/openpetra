﻿//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop
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
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using Ict.Common.IO;
using Ict.Common;
using Ict.Tools.DBXML;
using Ict.Tools.CodeGeneration;

namespace Ict.Tools.CodeGeneration
{
    /*
     * This is the abstract base class for all other form writers.
     * manages the code generators for all the controls
     */
    public abstract class TFormWriter
    {
        public abstract void CreateCode(TCodeStorage AStorage, string AXamlFilename, string ATemplate);
        public abstract void CreateResourceFile(string AResourceFile, string AResourceTemplate);
        public abstract void CreateDesignerFile(string AYamlFilename, XmlNode ARootNode, string ATemplateDir);
        public abstract void SetControlProperty(string AControlName, string APropertyName, string APropertyValue);

        /// <summary>
        /// check if the control has an attribute with the property name in the xml definition
        /// if such an attribute exists, then set it
        /// </summary>
        /// <param name="ACtrl"></param>
        /// <param name="APropertyName"></param>
        public virtual void SetControlProperty(TControlDef ACtrl, string APropertyName)
        {
            if (TYml2Xml.HasAttribute(ACtrl.xmlNode, APropertyName))
            {
                SetControlProperty(ACtrl.controlName, APropertyName, TYml2Xml.GetAttribute(ACtrl.xmlNode, APropertyName));
            }
        }

        public virtual void ApplyDerivedFunctionality(IControlGenerator generator, XmlNode curNode)
        {
            generator.ApplyDerivedFunctionality(this, curNode);
        }

        public abstract void CallControlFunction(string AControlName, string AFunctionCall);
        public abstract void SetEventHandlerToControl(string AControlName, string AEvent, string AEventHandlerType, string AEventHandlingMethod);
        public abstract void SetEventHandlerFunction(string AControlName, string AEvent, string AEventImplementation);
        public abstract void AddContainer(string AControlName);
        public abstract void AddImageToResource(string AControlName, string AImageName, string AImageOrIcon);
        public abstract void InitialiseDataSource(XmlNode curNode, string AControlName);

        public abstract string CodeFileExtension
        {
            get;
        }

        public TCodeStorage FCodeStorage = null;
        public ProcessTemplate FTemplate;

        public TCodeStorage CodeStorage
        {
            get
            {
                return FCodeStorage;
            }
        }

        public ProcessTemplate Template
        {
            get
            {
                return FTemplate;
            }
        }

        public abstract bool IsUserControlTemplate
        {
            get;
        }

        /// List of all available controls, with prefix and
        ///    function to find out if this fits (e.g. same prefixes for same control)
        private List <IControlGenerator>AvailableControlGenerators = new List <IControlGenerator>();

        /// <summary>
        /// add a control generator so that it can be used for the form generation
        /// </summary>
        /// <param name="AControlGeneratorType"></param>
        public void AddControlGenerator(IControlGenerator AControlGeneratorType)
        {
            AvailableControlGenerators.Add(AControlGeneratorType);
        }

        System.Type FBaseControlGeneratorType = null;

        /// <summary>
        /// this is the type of TControlGenerator. FindControlGenerator uses this base type to generate a generic control generator
        /// </summary>
        public System.Type BaseControlGeneratorType
        {
            set
            {
                FBaseControlGeneratorType = value;
            }
        }

        /// <summary>
        /// get the correct control generator for the control, depending on the prefix of the name, and other parameters
        /// </summary>
        /// <param name="ACtrlDef"></param>
        /// <returns></returns>
        public IControlGenerator FindControlGenerator(TControlDef ACtrlDef)
        {
            IControlGenerator fittingGenerator = null;

            if (ACtrlDef.controlGenerator != null)
            {
                return ACtrlDef.controlGenerator;
            }

            foreach (IControlGenerator generator in AvailableControlGenerators)
            {
                if (generator.ControlFitsNode(ACtrlDef.xmlNode))
                {
                    if (fittingGenerator != null)
                    {
                        throw new Exception(
                            "Error: control with name " + ACtrlDef.xmlNode.Name + " does fit both control generators " +
                            fittingGenerator.ControlType +
                            " and " +
                            generator.ControlType);
                    }

                    fittingGenerator = generator;
                }
            }

            if ((fittingGenerator == null)
                && (!ACtrlDef.controlName.StartsWith("Empty")))
            {
                if (TYml2Xml.HasAttribute(ACtrlDef.xmlNode, "Type") && (FBaseControlGeneratorType != null))
                {
                    return (IControlGenerator)Activator.CreateInstance(FBaseControlGeneratorType, new Object[] { ACtrlDef.xmlNode.Name.Substring(0,
                                                                                                                     3),
                                                                                                                 TYml2Xml.GetAttribute(ACtrlDef.
                                                                                                                     xmlNode, "Type") });
                }

                throw new Exception("Error: cannot find a generator for control with name " + ACtrlDef.xmlNode.Name);
            }

            ACtrlDef.controlGenerator = fittingGenerator;

            return fittingGenerator;
        }

        /// <summary>
        /// check if the label should be translated;
        /// e.g. separators for menu items and empty strings cannot be translated;
        /// special workarounds for linebreaks are required;
        /// also called by GenerateI18N, class TGenerateCatalogStrings
        /// </summary>
        /// <param name="ALabelText">the label in english</param>
        /// <returns>true if this is a proper string</returns>
        public static bool ProperI18NCatalogGetString(string ALabelText)
        {
            // if there is MANUALTRANSLATION then don't translate; that is a workaround for \r\n in labels;
            // see eg. Client\lib\MPartner\gui\UC_PartnerInfo.Designer.cs, lblLoadingPartnerLocation.Text
            if (ALabelText.Contains("MANUALTRANSLATION"))
            {
                return false;
            }

            if (ALabelText.Trim().Length == 0)
            {
                return false;
            }

            if (ALabelText.Trim() == "-")
            {
                // menu separators etc
                return false;
            }

            if (StringHelper.TryStrToInt32(ALabelText, -1).ToString() == ALabelText)
            {
                // don't translate digits?
                return false;
            }

            // careful with \n and \r in the string; that is not allowed by gettext
            if (ALabelText.Contains("\\r") || ALabelText.Contains("\\n"))
            {
                throw new Exception("Problem with \\r or \\n");
            }

            return true;
        }

        public bool WriteFile(string ADestinationFile)
        {
            return FTemplate.FinishWriting(ADestinationFile, Path.GetExtension(ADestinationFile), true);
        }

        public bool WriteFile(string ADestinationFile, string ATemplateFile)
        {
            ProcessTemplate LocalTemplate = new ProcessTemplate(ATemplateFile);

            // reuse the codelets that have been generated by CreateCode
            LocalTemplate.FCodelets = FTemplate.FCodelets;
            return LocalTemplate.FinishWriting(ADestinationFile, Path.GetExtension(ADestinationFile), true);
        }
    }
}