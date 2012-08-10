using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.BusinessLogic;
using umbraco.presentation.masterpages;
using umbraco.uicontrols;

namespace Vertica.Umbraco.DictionaryToJavascriptUI
{
    public class MyStartupHandler : ApplicationBase
    {
        public MyStartupHandler()
        {
            umbracoPage.Load += UmbracoPageOnLoad;
        }

        private static void UmbracoPageOnLoad(object sender, EventArgs eventArgs)
        {
            var pageReference = (umbracoPage)sender;

            var path = pageReference.Page.Request.Path.ToLower();

            //http://dictionary.translate/umbraco/settings/editDictionaryItem.aspx?id=1
            if (path.EndsWith("settings/editdictionaryitem.aspx") != true) return;

            var panel = GetPanel1Control(pageReference) as UmbracoPanel;
            if (panel == null) return;

            //Insert splitter in menu to make menu nicer on the eye
            panel.Menu.InsertSplitter();

            //Add new image button - for our translate button
            var button = panel.Menu.NewImageButton();
            button.ImageUrl = umbraco.GlobalSettings.Path + "/images/publish.gif";
            button.Click += ForcePublishJavascriptTranslationOnClick;
            button.AltText = "Publish javascript dictionary (uDictionary.js)";
        }

        // Shamelessly borrowed from Dictionary Translator for Umbraco
        private static Control GetPanel1Control(Control up)
        {
            var cph = (ContentPlaceHolder)up.FindControl("body");
            return cph.FindControl("Panel1");
        }

        private static void ForcePublishJavascriptTranslationOnClick(object sender, ImageClickEventArgs imageClickEventArgs)
        {
            DictionaryToJavascript.MyStartupHandler.DictionaryItemChanged(null, EventArgs.Empty);
        }
    }
}
