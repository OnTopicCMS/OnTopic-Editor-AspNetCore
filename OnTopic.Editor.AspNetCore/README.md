# OnTopic Editor
The `OnTopic.Editor.AspNetCore` project provides a web-based interface for the [**OnTopic Library**](https://github.com/OnTopicCMS/OnTopic-Library). The editor is distributed as a **Razor Class Library** via **NuGet** so it can easily be added to a website that implements the [`OnTopic.AspNetCore.Mvc`](https://github.com/OnTopicCMS/OnTopic-Library/tree/master/OnTopic.AspNetCore.Mvc) library.

[![OnTopic.Editor.AspNetCore package in Internal feed in Azure Artifacts](https://igniasoftware.feeds.visualstudio.com/_apis/public/Packaging/Feeds/46d5f49c-5e1e-47bb-8b14-43be6c719ba8/Packages/682244bf-1062-48de-949e-16f9cb11a6cf/Badge)](https://www.nuget.org/packages/OnTopic.Editor.AspNetCore/)
[![Build Status](https://igniasoftware.visualstudio.com/OnTopic/_apis/build/status/OnTopic-Editor-CI-V1?branchName=master)](https://igniasoftware.visualstudio.com/OnTopic/_build/latest?definitionId=8&branchName=master)
![NuGet Deployment Status](https://rmsprodscussu1.vsrm.visualstudio.com/A09668467-721c-4517-8d2e-aedbe2a7d67f/_apis/public/Release/badge/bd7f03e0-6fcf-4ec6-939d-4e995668d40f/2/2)

### Contents
- [Attribute Type Plugins](#attribute-type-plugins)
  - [View Component](#view-component)
  - [Attribute Descriptor](#attribute-descriptor)
  - [Attribute Descriptor (View Model)](#attribute-descriptor-view-model)
  - [View Model](#view-model)
  - [Binding Model](#binding-model)
- [Infrastructure](#infrastructure)
  - [Controllers](#controllers)
  - [Components](#components)
  - [Services](#services)

## Attribute Type Plugins
When a topic is edited in the OnTopic Editor, every attribute is exposed as a particular attribute type—such as a text box, dropdown menu, etc. Those attribute types are implemented as plugins, which can be developed by third-party developers as a means of customizing or extending the editor. The [`OnTopic.Editor.AspNetCore.Attributes`](../OnTopic.Editor.AspNetCore.Attributes/README.md) provides a standard set of attribute types for the editor. The following summarizes the structure of plugins.

#### View Component
The `{Type}ViewComponent)` (e.g., `BooleanViewComponent`) is responsible for evaluating contextual information against services to return view models for each attribute type.

#### Attribute Descriptor
The `{Type}AttributeDescriptor` (e.g., `BooleanAttributeDescriptor`) derives from `AttributeDescriptor` and is primarily responsible for customizing the `ModelType` for cases where the attribute should not be stored in `Topic.Attributes`.

```c#
public class FooAttributeDescriptor: AttributeDescriptor {
  public FooAttributeDescriptor(string key, string contentType, Topic parent, int id = -1): base(key, contentType, parent, id) {
    ModelType = ModelType.Reference
  }
}
```
##### Notes
- The signature of the constructor should match `Topic`, which this derives from. That allows the `TopicFactory` to create new instances of it.
- Generally, the only customization required is to modify the `ModelType`, if it shouldn't be set to the default of `ModelType.ScalarValue`.

#### Attribute Descriptor (View Model)
The `{Type}AttributeDescriptorViewModel` (e.g., `BooleanAttributeDescriptorViewModel`) exposes the configuration of the `AttributeDescriptor` to the view. It will typically have a property for each attribute associated with the `AttributeDescriptor` in the Editor. For exxample,

```c#
public record FooAttributeDescriptorViewModel: AttributeDescriptorViewModel {
  public string Height { get; init; }
}
```
In this example, the `FooAttributeDescriptorViewModel` has a single configuration value, which specifies the `Height` of the view component.

##### Client Resources
In addition to exposing configurable properties for the attribute, the attribute descriptor view model is also responsble for registering client-side resources that should be injected into the header or footer of the layout. This can be done in the constructor using the following calls:
```c#
public record FooAttributeDescriptorViewModel: AttributeDescriptorViewModel {
  public FooAttributeDescriptorViewModel() {
    StyleSheets.Register(GetNamespacedUri("%/Shared/Styles/Foo.css"));
    Scripts.Register(GetNamespacedUri("%/Shared/Scripts/Foo.js"));
  }
}
```
Note that the `GetNameSpaceUri()` helper will automatically replace the `%/` with the Razor Class Library namespace for the plugin's assembly—e.g., `/_Content/FooAttribute/`.

#### View Model (Optional)
Typically, view components will return an instance of the `AttributeViewModel<T>` view model, where `T` is the `AttributeDescriptorViewModel`. On occassion, however, there is additional non-configuration data that needs to be returned to the view. For example, an attribute type which displays a dropdown list may include a `Collection<SelectListItem>` to include the data for that dropdown list:

```c#
public record FooViewModel {
  public Collection<SelectListItem> Values { get; } = new();
}
```

#### Binding Model
The `{Type}AttributeBindingModel` (e.g., `BooleanAttributeBindingModel`) is assembled by the `AttributeBindingModelBinder` to bind attribute data posted to the `EditorController` to an `EditorBindingModel`. Typically, there's no need to customize this unless there's a need to translate how the value is read. If there's a need to translate the value submitted from the editor prior to it being set on the `Topic`, implementers can override the `GetValue()` method:

```c#
public record FooBindingModel {
  public string GetValue() => Value.ToLower();
}
```

#### View
Per ASP.NET Core conventions, views must be stored in `/Views/Editor/Components/{Type}/Default.cshtml`. To ensure they are consistently rendered in the OnTopic Editor, and property bound to the `EditorBindingModel`, they should have their layout set to:
```c#
@{
  Layout = "~/Areas/Editor/Views/Editor/Components/_Layout.cshtml";
}
```
This will automatically add the header, tooltip, as well as hidden metada needed to correctly bind to the appropriate `AttributeBindingModel`.

## Infrastructure
Typically, consumers of the OnTopic Editor shouldn't need to know much about the architecture of the code. The following overview of types used by the OnTopic Editor may, however, be of use to those looking to extend the functionality of the editor.

### Controllers
- [`EditorController`](Controllers/EditorController.cs): The primary controller fo the OnTopic Editor, with actions for `/Edit`, `/Import`, `/Export`, `/Move`, `/Delete`, `/Rollback`, and `/Json`.

### Components
- [`ContentTypeListViewComponent`](Components/ContentTypeListViewComponent.cs): A view component for displaying a list of content types that can be created in the current context. Used for opening the new topic form.
  - [`ContentTypeListAttributeDescriptorViewModel`](Models/Components/ContentTypeListAttributeDescriptorViewModel.cs): The attribute descriptor for the `ContentTypeList` view component; includes an `EnableModal` property for optinally opening the new topic form in a popup window.
  - [`ContentTypeListViewModel`](Models/Components/ContentTypeListViewModel.cs): The view model for the `ContentType` view component includes, critically, a `TopicList` of `SelectListItem`s for binding to the dropdown list.

### Services
- [`EditorViewModelLookupService`](Infrastructure/EditorViewModelLookupService.cs): Looks up view models associated with plugins needed to render the OnTopic Editor views.
- [`TopicQueryService`](Infrastructure/TopicQueryService.cs): Queries the topic graph based on a set of `TopicQueryOptions` and results a collection of `QueryResultTopicViewModel`s.
  - [`TopicQueryOptions`](Models/Queryable/TopicQueryOptions.cs): Arguments and options for the `EditorViewModelLookupService`.
  - [`QueryResultTopicViewModel`](Models/Queryable/QueryResultTopicViewModel.cs): Model for returning results from the `TopicQueryService`.
  

#### Internal
- [`AttributeBindingModelBinder`](Infrastructure/AttributeBindingModelBinder.cs): Uses metadata from `Components/_Layout.cshtml` to identify an appropriate `AttributeBindingModel` for each attribute, and bind the form data to it.
- [`AttributeBindingModelBinderProvider`](Infrastructure/AttributeBindingModelBinderProvider.cs): Infrastructure component for constructing a new `AttributeBindingModelBinder`.
- [`AttributeBindingModelLookupService`](Infrastructure/AttributeBindingModelLookupService.cs): Looks up the appropriate `AttributeBindingModel` for a given attribute name.

### Models
- [`EditorViewModel`](Models/EditorViewModel.cs): The core view model used to represent each page in the OnTopic Editor.
  - [`EditingTopicViewModel`](Models/EditingTopicViewModel.cs): The current topic being edited; exposed via the `Topic` property on the `EditorViewModel`.
- [`ContenTypeDescriptorViewModel`](Models/Metadata/ContentTypeDescriptorViewModel.cs): Provides metadata associated with a given `ContentTypeDescriptor`; exposed via the `ContentType` property on the `EditorViewModel`.
  - [`AttributeDescriptorViewModel`](Models/Metadata/AttributeDescriptorViewModel.cs): Provides metadata associated with a specific `AttributeTypeDescriptor`; exposed via the `AttributeDescriptors` property of the `ContentTypeDescriptorViewModel`.
- [`AttributeViewModel`](Models/AttributeViewModel.cs): Contains the serialized metadata for an attribute, with a link to the associated `AttributeDescriptorViewModel` and `EditingTopicViewModel`; constructed by `Edit.cshtml` for each attribute type view component.
  - [`ClientResourceCollection`](Models/ClientResources/ClientResourceCollection{T}.cs): Provides a base class for registering client-side resources associated with a given attribute.
    - [`ClientResource<T>`](Models/ClientResources/ClientResource.cs): Provides a base class for representing an individual client-side resources.
    - [`ScriptCollection`](Models/ClientResources/ScriptCollection.cs): A concrete implementation of the `ClientResourceColletion` for registering client-side scripts.
      - [`ScriptResource`](Models/ClientResources/ScriptResource.cs): A concrete implementation of the `ClientResource` for representing a client-side script.
    - [`StyleSheetCollection`](Models/ClientResources/StyleSheetCollection.cs): A concrete implementation of the `ClientResourceColletion` for registering client-side style sheets. 
      - [`StyleSheetResource`](Models/ClientResources/StyleSheetResource.cs): A concrete implementation of the `ClientResource` for representing a client-side script.
- [`EditorBindingModel`](Models/EditorBindingModel.cs): The core binding model used to respond to `POST` requests in the OnTopic Editor.
  - [`EditorAttributeCollection`](Models/Collections/EditorAttributeCollection.cs): A collection of `AttributeBindingModel` instance, exposed by the `Attributes` property of the `EditorBindingModel`.
      - [`AttributeBindingModel`](Models/AttributeBindingModel.cs): Binds the metadata and submitted value for an individual attribute control, as submitted via a `POST` request.