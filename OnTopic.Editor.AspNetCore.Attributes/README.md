# OnTopic Editor Attribute Types
The `OnTopic.Editor.AspNetCore.Attributes` project provides a standard set of attribute type plugins for the [**OnTopic Library**](https://github.com/OnTopicCMS/OnTopic-Library). The editor is distributed as a **Razor Class Library** via **NuGet** so it can easily be added to a website that implements the [`OnTopic.AspNetCore.Mvc`](https://github.com/OnTopicCMS/OnTopic-Library/tree/master/OnTopic.AspNetCore.Mvc) library. Each plugin represents an individual user interface element for editing a type of attribute, such as a `Boolean` or a `DateTime` value.

[![OnTopic.Editor.AspNetCore.Attributes package in Internal feed in Azure Artifacts](https://igniasoftware.feeds.visualstudio.com/_apis/public/Packaging/Feeds/46d5f49c-5e1e-47bb-8b14-43be6c719ba8/Packages/3c8689d0-6ff6-4696-b82e-483bfb921977/Badge)](https://www.nuget.org/packages/OnTopic.Editor.AspNetCore.Attributes/)
[![Build Status](https://igniasoftware.visualstudio.com/OnTopic/_apis/build/status/OnTopic-Editor-CI-V1?branchName=master)](https://igniasoftware.visualstudio.com/OnTopic/_build/latest?definitionId=8&branchName=master)
![NuGet Deployment Status](https://rmsprodscussu1.vsrm.visualstudio.com/A09668467-721c-4517-8d2e-aedbe2a7d67f/_apis/public/Release/badge/bd7f03e0-6fcf-4ec6-939d-4e995668d40f/2/2)

## Attribute Types
The following attribute types are supported by the `OnTopic.Editor.AspNetCore.Attributes` project:
- [Boolean](BooleanAttribute/BooleanViewComponent.cs): A yes/no choice.
- [Date/Time](DateTimeAttribute/DateTimeViewComponent.cs): A date, time, or date-time picker.
- [File List](FileListAttribute/FileListViewComponent.cs): A list of files from the file system.
- [File Path](FilePathAttribute/FilePathViewComponent.cs): A path relative to the current topic.
- [HTML](HtmlAttribute/HtmlViewComponent.cs): A WYSIWYG editor for modifying HTML.
- [Incoming Relationships](IncomingRelationshipAttribute/IncomingRelationshipViewComponent.cs): Provides a read-only list of relationships referencing the current topic.
- [Instruction](InstructionAttribute/InstructionViewComponent.cs): Provides read-only instructional text.
- [Last Modified](LastModifiedAttribute/LastModifiedViewComponent.cs): A timestamp for the current version based on the current date.
- [Last Modified By](LastModifiedByAttribute/LastModifiedByViewComponent.cs): A byline for the current version based on the current user.
- [Nested Topic List](NestedTopicListAttribute/NestedTopicListViewComponent.cs): A list of topics embedded within the current topic.
- [Number](NumberAttribute/NumberViewComponent.cs): A numeric value.
- [Relationship](RelationshipAttribute/RelationshipViewComponent.cs): A tree of topics that can be selected as related to the current topic.
- [Text Area](TextAreaAttribute/TextAreaViewComponent.cs): A multi-line text box.
- [Text](TextAttribute/TextViewComponent.cs): A text box.
- [Tokenized Topic List](TokenizedTopicListAttribute/TokenizedTopicListViewComponent.cs): A combo-box which creates relationships with multiple topics.
- [Topic List](TopicListAttribute/TopicListViewComponent.cs): A list of topics exposed as a drop-down list.
- [Topic Reference](TopicReferenceAttribute/TopicReferenceViewComponent.cs): A reference to a single topic, exposed as a tokenized topic list. 