﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebUI.Resources.Components {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SalaryCalculationOptions {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SalaryCalculationOptions() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebUI.Resources.Components.SalaryCalculationOptions", typeof(SalaryCalculationOptions).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pension second pillar payments.
        /// </summary>
        internal static string UsePensionSecondPillar {
            get {
                return ResourceManager.GetString("UsePensionSecondPillar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use tax free.
        /// </summary>
        internal static string UseTaxFree {
            get {
                return ResourceManager.GetString("UseTaxFree", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Employee unemployment insurance payment.
        /// </summary>
        internal static string UseUnemploymentInsuranceEmployee {
            get {
                return ResourceManager.GetString("UseUnemploymentInsuranceEmployee", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Employer unemployment insurance payment.
        /// </summary>
        internal static string UseUnemploymentInsuranceEmployer {
            get {
                return ResourceManager.GetString("UseUnemploymentInsuranceEmployer", resourceCulture);
            }
        }
    }
}