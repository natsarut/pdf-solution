using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    internal static class JsonHelper
    {
        private static readonly JsonSerializerOptions options;

        public static JsonSerializerOptions Options 
        { 
            get 
            { 
                return options; 
            } 
        }

        static JsonHelper()
        {
            options = new()
            {
                WriteIndented = true,
                IndentSize=4,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }
    }
}
