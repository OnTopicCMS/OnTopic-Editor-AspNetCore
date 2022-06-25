/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Attributes.QueryableTopicListAttribute;

namespace OnTopic.Editor.AspNetCore.Attributes.RelationshipAttribute {

  /*============================================================================================================================
  | CLASS: RELATIONSHIP ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="RelationshipViewComponent"/>.
  /// </summary>
  public record RelationshipAttributeDescriptorViewModel: QueryableTopicListAttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="RelationshipAttributeDescriptorViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeValueDictionary"/> of attribute values.</param>
    public RelationshipAttributeDescriptorViewModel(AttributeValueDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      ShowRoot                  = attributes.GetBoolean(nameof(ShowRoot));
      ExpandRelated             = attributes.GetBoolean(nameof(ExpandRelated));
      CheckAscendants           = attributes.GetBoolean(nameof(CheckAscendants));
      RegisterResources();
    }

    /// <summary>
    ///   Initializes a new instance of a <see cref="RelationshipAttributeDescriptorViewModel"/>
    /// </summary>
    public RelationshipAttributeDescriptorViewModel() {
      RegisterResources();
    }

    /*==========================================================================================================================
    | REGISTER RESOURCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Derived classes may optionally override this method in order to register resources, as an alternative to setting these
    ///   in the constructor.
    /// </summary>
    protected void RegisterResources() {
      Scripts.Register(GetNamespacedUri("/Shared/Scripts/SelectableTreeView.js"));
    }

    /*==========================================================================================================================
    | PROPERTY: SHOW ROOT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given the <see cref="QueryableTopicListAttributeDescriptorViewModel.RootTopic"/>, determines whether the root node is
    ///   displayed, or only the children. The default is <c>false</c>.
    /// </summary>
    public bool? ShowRoot { get; init; }

    /*==========================================================================================================================
    | PROPERTY: EXPAND RELATED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the tree panel should be expanded to ensure visibility of any related (checked)
    ///   relationships upon load. Defaults to <c>true</c>.
    /// </summary>
    public bool? ExpandRelated { get; init; }

    /*==========================================================================================================================
    | PROPERTY: CHECK ASCENDANTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   When a <see cref="Topic"/> is selected as a relationship, determines if the client should automatically select all
    ///   descendent topics. The default is false.
    /// </summary>
    public bool? CheckAscendants { get; init; }

  } //Class
} //Namespace