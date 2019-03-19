/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for a <see cref="Topic"/>. Since attributes are handled via the <see cref="AttributeViewModel"/>, the
  ///   <see cref="TopicViewModel"/> needn't account for them. It only accounts for items that will be exposed to the general
  ///   interface of the Topic Editor.
  /// </summary>
  public class TopicViewModel: ViewModels.TopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeValue"/> class, using the specified key/value pair.
    /// </summary>
    public TopicViewModel() : base() {}

    /*==========================================================================================================================
    | PROPERTY: WEB PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="Topic.GetUniqueKey()"/> result.
    /// </summary>
    public string WebPath { get; set; }

    /*==========================================================================================================================
    | PROPERTY: VERSION HISTORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a collection of <see cref="DateTime"/> instances during which the represented <see cref="Topic"/> was
    ///   modified.
    /// </summary>
    public List<DateTime> VersionHistory { get; set; }

  } // Class

} // Namespace
