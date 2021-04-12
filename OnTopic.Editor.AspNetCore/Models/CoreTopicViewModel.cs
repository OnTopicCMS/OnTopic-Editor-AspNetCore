/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Models;

namespace OnTopic.Editor.AspNetCore.Models {

  /*============================================================================================================================
  | CLASS: CORE TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the core <see cref="Topic"/> properties shared between the <see cref="EditingTopicViewModel"/>, <see cref="
  ///   AttributeViewModel"/>, and any topic associations, such as <see cref="EditingTopicViewModel.BaseTopic"/>.
  /// </summary>
  /// <remarks>
  ///   In addition to being useful as a base class for <see cref="EditingTopicViewModel"/> and <see cref="AttributeViewModel"
  ///   />, the <see cref="CoreTopicViewModel"/> is intended to be used for any topic associations—such as topic references and
  ///   relationships. By mapping explicitly to the <see cref="CoreTopicViewModel"/>, these models are not dependent on any
  ///   explicit view models from e.g. the OnTopic View Models library, third-party libraries, or those implemented by adopters.
  ///   It also ensures they aren't mapping extraneous properties that they aren't likely to need.
  /// </remarks>
  public record CoreTopicViewModel : ICoreTopicViewModel, IAssociatedTopicBindingModel {

    /*==========================================================================================================================
    | PROPERTY: ID
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="Topic.Id"/> of the associated <see cref="Topic"/>.
    /// </summary>
    public int Id { get; init; } = -1;

    /*==========================================================================================================================
    | PROPERTY: KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string Key { get; init; } = null!;

    /*==========================================================================================================================
    | PROPERTY: CONTENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string ContentType { get; init; } = null!;

    /*==========================================================================================================================
    | PROPERTY: UNIQUE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string UniqueKey { get; init; } = null!;

    /*==========================================================================================================================
    | PROPERTY: WEB PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Defines the <see cref="Topic.GetWebPath()"/> of the <see cref="Topic"/>.
    /// </summary>
    public string WebPath { get; init; } = null!;

    /*==========================================================================================================================
    | PROPERTY: TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Represents the title of the <see cref="Topic"/>, if defined; otherwise, falls back to the <see cref="Key"/>.
    /// </summary>
    public string Title { get; init; } = null!;

  }
}