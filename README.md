Dicitonary to Javascript for Umbraco (DicToJs)
================================

Tested on Umbraco 4.7.2 and Umbraco 4.8.0.

This plugin generates javascript dictionary files. Usage:
```html
<script type="text/javascript" src="<%=ResolveUrl("~/scripts/uDictionary-da-DK.js")%>"></script>
```

This exposes a global variable $uDictionary, which can be used to reolve
dictionary entries:
```javascript
// HelloUser resolves to "Hello {0}!"
$('#container').text($uDictionary['HelloUser'].replace("{0}", "World"));
```

Merge dictionaries by adding several uDictionary-*.js.
This feature is something i think is useful. But it is not natively supported
by Umbraco. By adding more languages you can have them extend each other. For
example if we developed a site and created a dictionary for en-GB and wanted to
support en-US also, then instead of copying everything we could correct the
differences intead.
```html
<script type="text/javascript" src="<%=ResolveUrl("~/scripts/uDictionary-en-US.js")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/scripts/uDictionary-en-GB.js")%>"></script>
```


*Notice*
--------------------------------
Due to a bug in Umbraco (as of 4.8.0) the javascript files are not published
when a Dictionary item is saved (only on creation and deletion), therefore an
icon was added to the menubar to force publish the files (remember to save the
item first!).


Browser test
--------------------------------
Tested on
* Chrome 21
* Firefox 14
* IE 9
* IE 7


Thanks to
--------------------------------
Dictionary Translator for Umbraco [1], i borrowed the UI part from their
solution.

I should also mention Dictionary 2 javascript for Umbraco [2] it's very similar
to this project, they differ using fancy xml handling and using per request
dictionary creation.

  [1]: http://dictionarytranslate.codeplex.com/  "Dictionary Translator for Umbraco"
  [2]: http://dic2js.codeplex.com/               "Dictionary 2 javascript for Umbraco"
