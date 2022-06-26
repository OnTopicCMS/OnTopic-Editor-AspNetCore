/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Attributes.TextAreaAttribute;

namespace OnTopic.Editor.AspNetCore.Attributes.HtmlAttribute {

  /*============================================================================================================================
  | CLASS: HTML ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="HtmlViewComponent"/>.
  /// </summary>
  public record HtmlAttributeDescriptorViewModel: TextAreaAttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="HtmlAttributeDescriptorViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public HtmlAttributeDescriptorViewModel(AttributeDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      Height                    = attributes.GetInteger(nameof(Height));
      Rows                      = 20;
      RegisterResources();
    }

    /// <summary>
    ///   Initializes a new instance of a <see cref="HtmlAttributeDescriptorViewModel"/>
    /// </summary>
    public HtmlAttributeDescriptorViewModel() {
      Rows = 20;
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
      Scripts.Register(new("https://cdn.ckeditor.com/4.14.0/standard/ckeditor.js"), true, false);
    }

    /*==========================================================================================================================
    | HEIGHT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of pixels that the <see cref="HtmlViewComponent"/> should take up. Defaults to <see
    ///   cref="TextAreaAttributeDescriptorViewModel.Rows"/> x <c>20</c>.
    /// </summary>
    /// <remarks>
    ///   If set, this value overrides <see cref="TextAreaAttributeDescriptorViewModel.Rows"/>.
    /// </remarks>
    public int? Height { get; init; }

  } //Class
} //Namespace