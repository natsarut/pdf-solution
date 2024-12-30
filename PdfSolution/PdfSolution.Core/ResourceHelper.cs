using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    internal static class ResourceHelper
    {
        private static Assembly Assembly => typeof(ResourceHelper).Assembly;

        public static string GetLayoutHtml()
        {
            string result=string.Empty;
            using Stream? stream = Assembly.GetManifestResourceStream("PdfSolution.Core.Resources.layout.html");

            if (stream != null)
            {
                using var reader = new StreamReader(stream);
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
