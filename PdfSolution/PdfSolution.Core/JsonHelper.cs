using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public static class JsonHelper
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
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        public static T? Deserialize<T>(string json) where T : class
        {
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static string Serialize<T>(T value) where T : class
        {
            return JsonSerializer.Serialize(value, options);
        }
    }
}
