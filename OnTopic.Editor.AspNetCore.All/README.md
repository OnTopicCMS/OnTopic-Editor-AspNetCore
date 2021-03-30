# OnTopic Editor Metapackage
The `OnTopic.Editor.AspNetCore.All` metapackage includes a reference to both the core OnTopic Editor as well as all standard attribute type plugins. It is recommended that implementers reference this package instead of referencing each of the OnTopic Editor packages individually, unless they have a specific need to customize e.g. which attribute plugins are referenced.

[![OnTopic.Editor.AspNetCore.All package in Internal feed in Azure Artifacts](https://igniasoftware.feeds.visualstudio.com/_apis/public/Packaging/Feeds/46d5f49c-5e1e-47bb-8b14-43be6c719ba8/Packages/faa44518-dd61-4e2c-8904-b2aecbbf70e5/Badge)](https://www.nuget.org/packages/OnTopic.Editor.AspNetCore.All/)
[![Build Status](https://igniasoftware.visualstudio.com/OnTopic/_apis/build/status/OnTopic-Editor-CI-V1?branchName=master)](https://igniasoftware.visualstudio.com/OnTopic/_build/latest?definitionId=8&branchName=master)
![NuGet Deployment Status](https://rmsprodscussu1.vsrm.visualstudio.com/A09668467-721c-4517-8d2e-aedbe2a7d67f/_apis/public/Release/badge/bd7f03e0-6fcf-4ec6-939d-4e995668d40f/2/2)

### Contents
- [Scope](#scope)
- [Installation](#installation)

## Scope
The `OnTopic.Editor.AspNetCore.All` metapackage maintains a reference to the following packages:
- [`OnTopic.Editor.AspNetCore`](../OnTopic.Editor.AspNetCore/README.md): The core OnTopic Editor interface.
- [`OnTopic.Editor.AspNetCore.Attributes`](../OnTopic.Editor.AspNetCore.Attributes/README.md): The standard attribute types for supporting typing implementations of the OnTopic Editor.

## Installation
Installation can be performed by providing a `<PackageReference /`> to the `OnTopic.Editor.AspNetCore.All` **NuGet** package.
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  …
  <ItemGroup>
    <PackageReference Include="OnTopic.Editor.AspNetCore.All" Version="5.0.0" />
  </ItemGroup>
</Project>
```

> *Note:* This package is currently only available on Ignia's private **NuGet** repository. For access, please contact [Ignia](http://www.ignia.com/).