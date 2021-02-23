/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models.Metadata;
using OnTopic.Lookup;

namespace OnTopic.Editor.AspNetCore.Infrastructure {

  /*============================================================================================================================
  | CLASS: EDITOR VIEW MODEL LOOKUP SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Dynamically looks up all view models associated with attribute type plugins.
  /// </summary>
  /// <remarks>
  ///   Each OnTopic Editor attribute type plugin includes a view model derived from <see cref="ContentTypeDescriptorViewModel"
  ///   /> and another derive from <see cref="AttributeDescriptorViewModel"/>. The latter is responsible for exposing not only
  ///   the attribute configuration, but also for registered any client-side resources that the attribute type's view will
  ///   depend upon. The <see cref="EditorViewModelLookupService"/> will discover these regardless of what assembly they're
  ///   located in, thus providing support for both first-party and third-party plugins.
  /// </remarks>
  public class EditorViewModelLookupService: DynamicTypeLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instantiates a new instance of the <see cref="EditorViewModelLookupService"/>.
    /// </summary>
    /// <returns>A new instance of the <see cref="EditorViewModelLookupService"/>.</returns>
    public EditorViewModelLookupService() : base(t =>
      typeof(ContentTypeDescriptorViewModel).IsAssignableFrom(t) ||
      typeof(AttributeDescriptorViewModel).IsAssignableFrom(t)
    ) { }

  } //Class
} //Namespace