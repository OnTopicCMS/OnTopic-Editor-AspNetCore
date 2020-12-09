/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/*==============================================================================================================================
| DEFINE ASSEMBLY ATTRIBUTES
>===============================================================================================================================
| Declare and define attributes used in the compiling of the finished assembly.
\-----------------------------------------------------------------------------------------------------------------------------*/
[assembly: ComVisible(false)]
[assembly: Guid("02a3d98a-bf8a-4150-8fec-45a0d9c322e4")]
[assembly: CLSCompliant(true)]

/*==============================================================================================================================
| HANDLE SUPPRESSIONS
>===============================================================================================================================
| Suppress warnings from code analysis that are either false positives or not relevant for this assembly.
\-----------------------------------------------------------------------------------------------------------------------------*/
[assembly: SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "By design", Scope = "member", Target = "~P:OnTopic.Editor.Models.Metadata.ContentTypeDescriptorTopicViewModel.AttributeDescriptors")]
[assembly: SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "By design", Scope = "member", Target = "~P:OnTopic.Editor.Models.Components.BindingModels.AttributeBindingModel.Value")]