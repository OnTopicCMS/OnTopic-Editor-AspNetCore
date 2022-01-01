﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Hosting;
using OnTopic.Editor.AspNetCore.Attributes.BooleanAttribute;
using OnTopic.Editor.AspNetCore.Attributes.DateTimeAttribute;
using OnTopic.Editor.AspNetCore.Attributes.FileListAttribute;
using OnTopic.Editor.AspNetCore.Attributes.FilePathAttribute;
using OnTopic.Editor.AspNetCore.Attributes.HtmlAttribute;
using OnTopic.Editor.AspNetCore.Attributes.IncomingRelationshipAttribute;
using OnTopic.Editor.AspNetCore.Attributes.InstructionAttribute;
using OnTopic.Editor.AspNetCore.Attributes.LastModifiedAttribute;
using OnTopic.Editor.AspNetCore.Attributes.LastModifiedByAttribute;
using OnTopic.Editor.AspNetCore.Attributes.MetadataListAttribute;
using OnTopic.Editor.AspNetCore.Attributes.NestedTopicListAttribute;
using OnTopic.Editor.AspNetCore.Attributes.NumberAttribute;
using OnTopic.Editor.AspNetCore.Attributes.ReflexiveAttribute;
using OnTopic.Editor.AspNetCore.Attributes.RelationshipAttribute;
using OnTopic.Editor.AspNetCore.Attributes.TextAreaAttribute;
using OnTopic.Editor.AspNetCore.Attributes.TextAttribute;
using OnTopic.Editor.AspNetCore.Attributes.TokenizedTopicListAttribute;
using OnTopic.Editor.AspNetCore.Attributes.TopicListAttribute;
using OnTopic.Editor.AspNetCore.Attributes.TopicReferenceAttribute;
using OnTopic.Editor.AspNetCore.Components;
using OnTopic.Editor.AspNetCore.Controllers;
using OnTopic.Editor.AspNetCore.Infrastructure;
using OnTopic.Lookup;
using OnTopic.Mapping;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Attributes {

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
    private readonly            ITypeLookupService              _typeLookupService;
    private readonly            ITopicMappingService            _topicMappingService;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of a <see cref="StandardEditorComposer"/> with dependencies which are expected to have a
    ///   singleton lifestyle.
    /// </summary>
    /// <remarks>
    ///   The constructor is responsible for establishing dependencies with the singleton lifestyle so that they are available
    ///   to all requests. If this isn't the appropriate lifestyle for these dependencies given the specific requirements of the
    ///   application, then the components should be composed separately, instead of relying on the convenience of the <see
    ///   cref="StandardEditorComposer"/>.
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
      _typeLookupService        = new EditorViewModelLookupService();
      _topicMappingService      = new TopicMappingService(_topicRepository, _typeLookupService);

    }

    /*==========================================================================================================================
    | METHOD: IS EDITOR COMPONENT?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether a given type is capable of being activated by the <see cref="ActivateEditorComponent(Type,
    ///   ITopicRepository)"/> method.
    /// </summary>
    public static bool IsEditorComponent(Type type) {
      Contract.Requires(type, nameof(type));
      return
        typeof(ViewComponent).IsAssignableFrom(type) &&
        (type.Assembly == typeof(StandardEditorComposer).Assembly || type.Assembly == typeof(EditorController).Assembly);
    }

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
      type?.Name switch {
        nameof(BooleanViewComponent)                            => new BooleanViewComponent(),
        nameof(DateTimeViewComponent)                           => new DateTimeViewComponent(),
        nameof(FileListViewComponent)                           => new FileListViewComponent(_webHostEnvironment),
        nameof(FilePathViewComponent)                           => new FilePathViewComponent(topicRepository),
        nameof(HtmlViewComponent)                               => new HtmlViewComponent(),
        nameof(IncomingRelationshipViewComponent)               => new IncomingRelationshipViewComponent(_topicRepository),
        nameof(InstructionViewComponent)                        => new InstructionViewComponent(),
        nameof(LastModifiedViewComponent)                       => new LastModifiedViewComponent(),
        nameof(LastModifiedByViewComponent)                     => new LastModifiedByViewComponent(),
        nameof(MetadataListViewComponent)                       => new MetadataListViewComponent(),
        nameof(NestedTopicListViewComponent)                    => new NestedTopicListViewComponent(_topicRepository),
        nameof(NumberViewComponent)                             => new NumberViewComponent(),
        nameof(ReflexiveViewComponent)                          => new ReflexiveViewComponent(_topicRepository, _topicMappingService),
        nameof(RelationshipViewComponent)                       => new RelationshipViewComponent(),
        nameof(TextViewComponent)                               => new TextViewComponent(),
        nameof(TextAreaViewComponent)                           => new TextAreaViewComponent(),
        nameof(TokenizedTopicListViewComponent)                 => new TokenizedTopicListViewComponent(_topicRepository),
        nameof(TopicListViewComponent)                          => new TopicListViewComponent(_topicRepository),
        nameof(TopicReferenceViewComponent)                     => new TopicReferenceViewComponent(),
        nameof(ContentTypeListViewComponent)                    => new ContentTypeListViewComponent(_topicRepository),
        _                                                       => throw new InvalidOperationException($"Unknown view component {type?.Name}")
      };

  } // Class
} // Namespace