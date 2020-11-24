/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using Microsoft.AspNetCore.Mvc;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Components.ViewModels;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Internal.Diagnostics;
using OnTopic.Repositories;
using OnTopic.ViewModels;

namespace OnTopic.Editor.AspNetCore.Components {

  /*============================================================================================================================
  | CLASS: INCOMING RELATIONSHIP (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for an incoming relationship attribute type.
  /// </summary>
  public class IncomingRelationshipViewComponent : ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="IncomingRelationshipViewComponent"/> with necessary dependencies.
    /// </summary>
    public IncomingRelationshipViewComponent(ITopicRepository topicRepository) : base() {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="IncomingRelationshipViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      IncomingRelationshipAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(currentTopic, nameof(currentTopic));
      Contract.Requires(attribute, nameof(attribute));

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new IncomingRelationshipAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set incoming relationships
      >-------------------------------------------------------------------------------------------------------------------------
      | ### NOTE JJC20200929: This would be a lot cleaner using the ITopicMappingService. But that would introduce an additional
      | dependency on the StandardEditorComposer, which would be a breaking change. We can reevaluate this in the future if
      | other view components would benefit from this.
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topic                 = _topicRepository.Load(currentTopic.UniqueKey);

      foreach(var relatedTopic in topic.IncomingRelationships.GetTopics(attribute.RelationshipKey?? attribute.Key)) {
        if (
          !String.IsNullOrWhiteSpace(attribute.AttributeKey) &&
          relatedTopic.Attributes.GetValue(attribute.AttributeKey) != attribute.AttributeValue
        ) {
          continue;
        }
        var relatedViewModel    = new TopicViewModel {
          Id                    = relatedTopic.Id,
          Key                   = relatedTopic.Key,
          ContentType           = relatedTopic.ContentType,
          UniqueKey             = relatedTopic.GetUniqueKey(),
          WebPath               = relatedTopic.GetWebPath(),
          IsHidden              = relatedTopic.IsHidden,
          View                  = relatedTopic.View,
          Title                 = relatedTopic.Title,
          LastModified          = relatedTopic.LastModified
        };
        model.RelatedTopics.Add(relatedViewModel);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

  } // Class
} // Namespace