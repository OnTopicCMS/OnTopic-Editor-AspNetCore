/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

/*==============================================================================================================================
| USING DIRECTIVES (GLOBAL)
\-----------------------------------------------------------------------------------------------------------------------------*/
global using System.Collections.ObjectModel;
global using OnTopic.Editor.AspNetCore.Models;
global using OnTopic.Editor.AspNetCore.Models.Metadata;
global using OnTopic.Internal.Diagnostics;
global using OnTopic.Metadata;

/*==============================================================================================================================
| USING DIRECTIVES (LOCAL)
\-----------------------------------------------------------------------------------------------------------------------------*/
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/*==============================================================================================================================
| DEFINE ASSEMBLY ATTRIBUTES
>===============================================================================================================================
| Declare and define attributes used in the compiling of the finished assembly.
\-----------------------------------------------------------------------------------------------------------------------------*/
[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]
[assembly: Guid("18159da7-b44e-4681-98c9-f81a2007196b")]

/*==============================================================================================================================
| HANDLE SUPPRESSIONS
>===============================================================================================================================
| Suppress warnings from code analysis that are either false positives or not relevant for this assembly.
\-----------------------------------------------------------------------------------------------------------------------------*/
[assembly: SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Expected by convention for OnTopic Editor", Scope = "namespaceanddescendants", Target = "~N:OnTopic.Editor.AspNetCore")]
[assembly: SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "By design", Scope = "member", Target = "~P:OnTopic.Editor.AspNetCore.Models.Metadata.ContentTypeDescriptorViewModel.AttributeDescriptors")]
[assembly: SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "By design", Scope = "member", Target = "~P:OnTopic.Editor.AspNetCore.Models.AttributeBindingModel.Value")]