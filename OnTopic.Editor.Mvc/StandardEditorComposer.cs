/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using OnTopic.Editor.Mvc.Components;
using OnTopic.Editor.Mvc.Infrastructure;
using OnTopic.Internal.Diagnostics;
using OnTopic.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace OnTopic.Editor.Mvc {

  /*============================================================================================================================
  | CLASS: STANDARD EDITOR COMPOSER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides tools that aid in composing dependency graphs for a standard topic editor configuration.
  /// </summary>
  /// <remarks>
  ///   This should only be used for handling out-of-the-box scenarios. For topic editor instances relying on custom attribute
  ///   types, or which need fine-tuned control over dependency lifestyles, it may be appropriate to manually compose
  ///   dependencies.
  /// </remarks>
  public class StandardEditorComposer {

    /*==========================================================================================================================
    | PRIVATE INSTANCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;
    private readonly            IWebHostEnvironment             _webHostEnvironment;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of a <see cref="EditorComposer"/> with dependencies which are expected to have a singleton
    ///   lifestyle.
    /// </summary>
    /// <remarks>
    ///   The constructor is responsible for establishing dependencies with the singleton lifestyle so that they are available
    ///   to all requests. If this isn't the appropriate lifestyle for these dependencies given the specific requirements of the
    ///   application, then the components should be composed separately, instead of relying on the convenience of the <see
    ///   cref="EditorComposer"/>.
    /// </remarks>
    public StandardEditorComposer(ITopicRepository topicRepository, IWebHostEnvironment webHostEnvironment) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate dependencies
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(topicRepository, nameof(topicRepository));
      Contract.Requires(webHostEnvironment, nameof(webHostEnvironment));

      /*------------------------------------------------------------------------------------------------------------------------
      | Initialize Topic Repository
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicRepository          = topicRepository;
      _webHostEnvironment       = webHostEnvironment;

    }

    /*==========================================================================================================================
    | METHOD: IS EDITOR COMPONENT?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether a given type is capable of being activated by the <see cref="Activate"/> method.
    /// </summary>
    public bool IsEditorComponent(Type type) =>
      typeof(StandardEditorComposer).Assembly.Equals(type.Assembly) &&
      typeof(ViewComponent).IsAssignableFrom(type);

    /*==========================================================================================================================
    | METHOD: ACTIVATE EDITOR COMPONENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given a type, as well as any potential depenencies, activates the type.
    /// </summary>
    /// <remarks>
    ///   This only works because most of the dependencies are either a) cheap to construct, or b) expected to use the singleton
    ///   lifestyle. If in a future version we end up with more expensive transient dependencies, this approach should be
    ///   reevaluated.
    /// </remarks>
    public ViewComponent ActivateEditorComponent(
      Type                      type,
      ITopicRepository          topicRepository
    ) =>
      type.Name switch {
        nameof(BooleanViewComponent)                            => new BooleanViewComponent(),
        nameof(DateTimeViewComponent)                           => new DateTimeViewComponent(),
        nameof(DisplayOptionsViewComponent)                     => new DisplayOptionsViewComponent(),
        nameof(FileListViewComponent)                           => new FileListViewComponent(_webHostEnvironment),
        nameof(FilePathViewComponent)                           => new FilePathViewComponent(topicRepository),
        nameof(HtmlViewComponent)                               => new HtmlViewComponent(),
        nameof(LastModifiedViewComponent)                       => new LastModifiedViewComponent(),
        nameof(LastModifiedByViewComponent)                     => new LastModifiedByViewComponent(),
        nameof(NestedTopicListViewComponent)                    => new NestedTopicListViewComponent(_topicRepository),
        nameof(NumberViewComponent)                             => new NumberViewComponent(),
        nameof(RelationshipViewComponent)                       => new RelationshipViewComponent(),
        nameof(TextViewComponent)                               => new TextViewComponent(),
        nameof(TextAreaViewComponent)                           => new TextAreaViewComponent(),
        nameof(TokenizedTopicListViewComponent)                 => new TokenizedTopicListViewComponent(_topicRepository),
        nameof(TopicListViewComponent)                          => new TopicListViewComponent(_topicRepository),
        nameof(TopicReferenceViewComponent)                     => new TopicReferenceViewComponent(),
        _                                                       => throw new Exception($"Unknown view component {type.Name}")
      };

  } // Class
} // Namespace