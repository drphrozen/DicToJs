using System;
using System.IO;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.language;

namespace Vertica.Umbraco.DictionaryToJavascript
{
    public class MyStartupHandler : ApplicationBase
    {
        public MyStartupHandler()
        {
            Dictionary.DictionaryItem.Deleting += DictionaryItemChanged;
            Dictionary.DictionaryItem.New += DictionaryItemChanged;
            Dictionary.DictionaryItem.Saving += DictionaryItemChanged;
        }

        public static void DictionaryItemChanged(Dictionary.DictionaryItem sender, EventArgs e)
        {
            foreach (var language in Language.GetAllAsList())
            {
                var path = Path.Combine(umbraco.IO.IOHelper.MapPath(umbraco.IO.SystemDirectories.Scripts, false), string.Format("uDictionary-{0}.js", language.CultureAlias));
                using (var stream = File.CreateText(path))
                {
                    WriteJavascriptDictionary(stream, language);
                }
            }
        }

        public static string EscapeSingleQuote(string str)
        {
            return str.Replace("'", @"\'");
        }

        public static void WriteJavascriptDictionary(StreamWriter stream, Language language)
        {
            var processDictionaryItems = new ProcessDictionaryItems();

            stream.WriteLine("(function () {");

            // IE7 can't handle trailing "," in javascript array
            var lastLine = "  var tmp = {";
            processDictionaryItems.Start(item =>
            {
                stream.WriteLine(lastLine);
                var key = EscapeSingleQuote(item.key);
                var value = EscapeSingleQuote(item.Value(language.id));
                lastLine = string.Format("    '{0}': '{1}',", key, value);
            }, Dictionary.getTopMostItems);
            stream.WriteLine(lastLine.TrimEnd(','));
            
            stream.WriteLine("  };");

            stream.WriteLine("");
            stream.WriteLine("  if (window['$uDictionary'] === undefined) {");
            stream.WriteLine("    window['$uDictionary'] = tmp;");
            stream.WriteLine("  } else {");
            stream.WriteLine("    var $uDic = window['$uDictionary'];");
            stream.WriteLine("    for (var attrname in tmp) { ");
            stream.WriteLine("      var value = tmp[attrname];");
            stream.WriteLine("      if(value != '')");
            stream.WriteLine("        $uDic[attrname] = value;");
            stream.WriteLine("    }");
            stream.WriteLine("  }");
            stream.WriteLine("})();");
        }
    }
}