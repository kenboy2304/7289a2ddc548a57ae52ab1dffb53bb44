
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNS.WebAPI
{
    public class ModuleViewEngine : RazorViewEngine
    {

        private static readonly string[] NewPartialViewFormats = new[] {
        "~/Views/{1}/Module/{0}.cshtml",
        "~/Views/Shared/Module/{0}.cshtml"
    };
        
        public ModuleViewEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
        }
    }
}