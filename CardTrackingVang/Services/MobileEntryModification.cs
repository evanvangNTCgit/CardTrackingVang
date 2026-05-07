using System;
using System.Collections.Generic;
using System.Text;

namespace CardTrackingVang.Services
{
    public static class MobileEntryModification
    {
        public static void modifyEntry()
        {
            /*
             * Can access a platformview property.
             * This property is accessed to set teh native view properties
             * set like this to improve our lives on cross platform support.
             */

            // What this handler does is when you select the entry it highlights all text at once.
            // This is helpful especially when editing since you likely want to respell.
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("EntryCapitilizaton", (handler, view) =>
            {
#if IOS
            handler.PlatformView.EditingDidBegin += (s, e) =>
            {
                handler.PlatformView.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
            };
#endif
#if ANDROID
                handler.PlatformView.SetSelectAllOnFocus(true);
#endif
#if WINDOWS        
                handler.PlatformView.GotFocus += (s, e) =>
                {
                    handler.PlatformView.SelectAll();
                };
#endif
            });
        }
    }
}
