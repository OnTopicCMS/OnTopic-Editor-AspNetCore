# OnTopic Editor
The `OnTopic.Editor.AspNetCore` project provides a web-based interface for the [**OnTopic Library**](https://github.com/OnTopicCMS/OnTopic-Library). The editor is distributed as a **Razor Class Library** via **NuGet** so it can easily be added to a website that implements the [`OnTopic.AspNetCore.Mvc`](https://github.com/OnTopicCMS/OnTopic-Library/tree/master/OnTopic.AspNetCore.Mvc) library.

[![OnTopic.Editor.AspNetCore package in Internal feed in Azure Artifacts](https://igniasoftware.feeds.visualstudio.com/_apis/public/Packaging/Feeds/46d5f49c-5e1e-47bb-8b14-43be6c719ba8/Packages/682244bf-1062-48de-949e-16f9cb11a6cf/Badge)](https://igniasoftware.visualstudio.com/OnTopic/_packaging?_a=package&feed=46d5f49c-5e1e-47bb-8b14-43be6c719ba8&package=682244bf-1062-48de-949e-16f9cb11a6cf&preferRelease=true)
[![Build Status](https://igniasoftware.visualstudio.com/OnTopic/_apis/build/status/OnTopic-Editor-CI-V1?branchName=master)](https://igniasoftware.visualstudio.com/OnTopic/_build/latest?definitionId=8&branchName=master)
![NuGet Deployment Status](https://rmsprodscussu1.vsrm.visualstudio.com/A09668467-721c-4517-8d2e-aedbe2a7d67f/_apis/public/Release/badge/bd7f03e0-6fcf-4ec6-939d-4e995668d40f/2/2)

### Contents
- [Infrastructure](#infrastructure)
  - [Controllers](#controllers)
  - [Components](#components)
  - [Services](#services)
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