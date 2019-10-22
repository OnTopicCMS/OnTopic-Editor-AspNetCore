﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Metadata;
using System;

#nullable enable

namespace Ignia.Topics.Editor.Models.Components.Options {

  /*============================================================================================================================
  | CLASS: TOPIC LIST (OPTIONS)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Establishes options associated with the <see cref="TopicListViewComponentView"/>.
  /// </summary>
  public class TopicListOptions: DefaultOptions {

    /*==========================================================================================================================
    | PROPERTY: CONTENT TYPES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a list of comma-delimited list of content types that should be displayed in this list.
    /// </summary>
    /// <remarks>
    ///   As an example, if the <see cref="ContentTypes"/> value is <c>AttributeDescriptor,ContentTypeDescriptor</c> then the
    ///   <see cref="TopicListViewComponent"/> should only display topics of the types <see cref="AttributeDescriptor"/> or
    ///   <see cref="ContentTypeDescriptor"/>.
    /// </remarks>
    public string? ContentTypes { get; set; }

    /*==============================================================================================================================
    | TARGET POPUP
    >-------------------------------------------------------------------------------------------------------------------------------
    | Toggle to set whether or not to use a popup (modal window) when editing the Topic, rather than a full page.
    \-----------------------------------------------------------------------------------------------------------------------------*/
    public bool? TargetPopup { get; set; }

  } // Class
} // Namespace

#nullable restore