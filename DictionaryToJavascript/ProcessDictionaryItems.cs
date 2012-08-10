using System;
using System.Collections.Generic;
using umbraco.cms.businesslogic;

namespace Vertica.Umbraco.DictionaryToJavascript
{
    public class ProcessDictionaryItems
    {
        private Action<Dictionary.DictionaryItem> _callback;

        public void Start(Action<Dictionary.DictionaryItem> callback, IEnumerable<Dictionary.DictionaryItem> items)
        {
            _callback = callback;
            ProcessItems(items);
        }

        private void ProcessItems(IEnumerable<Dictionary.DictionaryItem> items)
        {
            foreach (var item in items)
            {
                _callback(item);
                ProcessItems(item.Children);
            }
        }
    }
}