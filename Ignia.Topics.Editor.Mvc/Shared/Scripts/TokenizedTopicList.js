/*==============================================================================================================================
| TOKENIZED TOPIC LIST
|
| Author        Katherine Trunkey, Ignia LLC (Katherine.Trunkey@ignia.com)
| Client        Ignia
| Project       Topics Editor
|
| Purpose       Provides a "class" wrapper with associated properties set as part of its constructor, as well the
|               getTokenizedTopics() prototypical extension method, used for instantiating the Token-Input plugin.
|
>===============================================================================================================================
| Revisions     Date            Author                          Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               04.11.15        Katherine Trunkey               Moved Token-Input instantiation out of inline client-side code
|               04.14.15        Katherine Trunkey               Refactored to TokenizedTopics class structure
\-----------------------------------------------------------------------------------------------------------------------------*/

/*==============================================================================================================================
| TOKENIZED TOPICS CLASS
\-----------------------------------------------------------------------------------------------------------------------------*/
  var TokenizedTopics           = function() {

    'use strict';

  /*----------------------------------------------------------------------------------------------------------------------------
  | Constructor/internal properties
  \---------------------------------------------------------------------------------------------------------------------------*/
    this.selector               = '';
    this.scope                  = '';
    this.attributeName          = '';
    this.attributeValue         = '';
    this.searchProperty         = 'text';
    this.queryParameter         = 'Query';
    this.selectedTopics         = '';
    this.resultLimit            = '';
    this.tokenLimit             = 100;
    this.isAutoPostBack         = false;

  };

/*==============================================================================================================================
| GET TOKENIZED TOPICS (TOKENIZED TOPICS EXTENSION METHOD)
\-----------------------------------------------------------------------------------------------------------------------------*/
  TokenizedTopics.prototype.getTokenizedTopics = function() {

  /*----------------------------------------------------------------------------------------------------------------------------
  | Scope TokenizedTopics properties to local variable
  \---------------------------------------------------------------------------------------------------------------------------*/
    var self                    = this;

  /*----------------------------------------------------------------------------------------------------------------------------
  | Build Topics.Json.aspx call URL
  \---------------------------------------------------------------------------------------------------------------------------*/
    var topicsUrl               = '/OnTopic/Json/'       +
      self.scope                                         +
      '?ShowRoot=true'                                   +
      '&FlattenStructure=true'                           +
      '&UsePartialMatch=true'                            +
      '&AttributeName='         + self.attributeName     +
      '&AttributeValue='        + self.attributeValue    +
      '&ResultLimit='           + self.resultLimit;

  /*----------------------------------------------------------------------------------------------------------------------------
  | Initialize Token-Input with options set on TokenizedTopics
  \---------------------------------------------------------------------------------------------------------------------------*/
    $(self.selector).tokenInput(topicsUrl, {
      propertyToSearch          : this.searchProperty,
      queryParam                : this.queryParameter,
      minChars                  : 3,
      enableHTML                : true,
      tokenLimit                : self.tokenLimit,
      preventDuplicates         : true,
      prePopulate               : self.selectedTopics,
      onAdd                     : function(item) {
        var eventTarget         = self.selector.substring(1);
        var eventArgument       = $(self.selector).val();
        if (self.isAutoPostBack) __doPostBack(eventTarget, eventArgument);
      },
      resultsFormatter          : function(item) {
        var   breadcrumbs       = item.path;
        breadcrumbs             = breadcrumbs.substring(0, breadcrumbs.indexOf(item.key)).replace(new RegExp(':', 'g'), ': ');
        breadcrumbs             = breadcrumbs.replace(new RegExp('([a-z])([A-Z])', 'g'), '$1 $2');
        if (breadcrumbs.indexOf('Root:') >= 0) {
          breadcrumbs           = breadcrumbs.substring(breadcrumbs.indexOf('Root:') + 5);
        }
        return '' +
          '<li>' +
          '  <small class="Breadcrumbs">' + breadcrumbs + '</small>' +
          '  <div class="Selection">' + item.text + '</div>' +
          '</li>';
      }
    });

  };
