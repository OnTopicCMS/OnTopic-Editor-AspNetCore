# OnTopic Editor
The `OnTopic.Editor.AspNetCore` project provides a web-based interface for the [**OnTopic Library**](https://github.com/OnTopicCMS/OnTopic-Library). The editor id distributed as a **Razor Class Library** via **NuGet** so it can easily be added to a website that implements the [`OnTopic.AspNetCore.Mvc`](https://github.com/OnTopicCMS/OnTopic-Library/tree/master/OnTopic.AspNetCore.Mvc) library.

[![OnTopic.Editor.AspNetCore package in Internal feed in Azure Artifacts](https://igniasoftware.feeds.visualstudio.com/_apis/public/Packaging/Feeds/46d5f49c-5e1e-47bb-8b14-43be6c719ba8/Packages/682244bf-1062-48de-949e-16f9cb11a6cf/Badge)](https://igniasoftware.visualstudio.com/OnTopic/_packaging?_a=package&feed=46d5f49c-5e1e-47bb-8b14-43be6c719ba8&package=682244bf-1062-48de-949e-16f9cb11a6cf&preferRelease=true)
[![Build Status](https://igniasoftware.visualstudio.com/OnTopic/_apis/build/status/OnTopic-Editor-CI-V1?branchName=master)](https://igniasoftware.visualstudio.com/OnTopic/_build/latest?definitionId=8&branchName=master)
![NuGet Deployment Status](https://rmsprodscussu1.vsrm.visualstudio.com/A09668467-721c-4517-8d2e-aedbe2a7d67f/_apis/public/Release/badge/bd7f03e0-6fcf-4ec6-939d-4e995668d40f/2/2)

### Contents
- [Installation](#installation)
- [Configuration](#configuration)
  - [Services](#services)
  - [Routes](#routes)
  - [Dependencies](#dependencies)
    - [`IControllerActivator`](#icontrolleractivator) 
    - [`IViewComponentActivator`](#iviewcomponentactivator) 


## Installation
Installation can be performed by providing a `<PackageReference />` to the `OnTopic.Editor.AspNetCore` **NuGet** package.
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  …
  <ItemGroup>
    <PackageReference Include="OnTopic.Editor.AspNetCore" Version="5.0.0" />
  </ItemGroup>
</Project>
```

> *Note:* This package is currently only available on Ignia's private **NuGet** repository. For access, please contact [Ignia](http://www.ignia.com/).

## Configuration
There are a lot of moving parts to the editor, and it requires the configuration of services, routes, and service dependencies. This process is aided by a set of extension methods, which are recommended.

### Services
The editor necessitates a custom model binder—[`AttributeBindingModelBinderProvider`](OnTopic.Editor.AspNetCore/Infrastructure/AttributeBindingModelBinderProvider.cs)—in order to work properly. This can be manually configured via `AddMvcOptions()`, or can be added using the `AddTopicEditor()` extension method:
```c#
public class Startup {
  …
  public void ConfigureServices(IServiceCollection services) {
    services.AddControllersWithViews()
      .AddTopicSupport()
      .AddTopicEditor();
  }
}
```

### Routes
The editor lives in an area called `Editor` and a controller called `EditorController`. That said, a custom route can be conigured using the `MapTopicEditorRoute()` extension method to setup the `/OnTopic` route (e.g., `/OnTopic/Edit/Root/Web`):
```c#
public class Startup {
  …
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
    app.UseEndpoints(endpoints => {
      endpoints.MapTopicEditorRoute();
    });
  }
}
```

### Dependencies
The editor is implemented through a set of **ASP.NET Core View Components**, some of which have external dependencies on a `ITopicRepository`. These should be configured via dependency injection. If you're using a _dependency injection container_, those dependencies should be the same as those required for the **OnTopic Library**. If you are manually configuring your dependencies, however, then the following provides a bare-bones example:

#### `IControllerActivator`
```c#
public class ControllerActivator : IControllerActivator {
  …
  public object Create(ControllerContext context) {
    var type = context.ActionDescriptor.ControllerTypeInfo.AsType();
    if (type == typeof(EditorController)) {
      return new EditorController(_topicRepository, _topicMappingService);
    }
  }
}
```
> _Note:_ This assumes a `_topicRepository` and `_topicMappingService` have already been configured; see the [`OnTopic.AspNetCore.Mvc`](https://github.com/OnTopicCMS/OnTopic-Library/tree/master/OnTopic.AspNetCore.Mvc#composition-root) documentation for details.

#### `IViewComponentActivator`
The [`StandardEditorComposer`](OnTopic.Editor.AspNetCore/StandardEditorComposer.cs) class acts as a clearing house for accepting common dependencies and then composing the appropriate dependency graph for the view components:
```c#
public class ViewComponentActivator : IViewComponentActivator {
  …
  public object Create(ViewComponentContext context) {

    var standardEditorComposer = new StandardEditorComposer(_topicRepository, _webHostEnvironment);
    var type                   = context.ViewComponentDescriptor.TypeInfo.AsType();

    if (standardEditorComposer.IsEditorComponent(type)) {
      return standardEditorComposer.ActivateEditorComponent(type, _topicRepository);
    }
  }
} 
```
> _Note:_ For a full example, see the [`SampleActivator`](OnTopic.Editor.AspNetCore.Host/SampleActivator.cs) in the [`OnTopic.Editor.AspNetCore.Host`](OnTopic.Editor.AspNetCore.Host) project.