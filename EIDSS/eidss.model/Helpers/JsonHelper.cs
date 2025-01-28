using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace eidss.model.Helpers
{
    public static class JsonHelper
    {
        public static bool IsValidJson(this string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private static bool HasUndefinedValue(JToken token)
        {
            var result = false;
            JContainer container = token as JContainer;
            if (container == null)
                return false;

            foreach (JToken el in container.Children())
            {
                if ((el.Type == JTokenType.Undefined) || (el.Type == JTokenType.Null) || (el.Type == JTokenType.None))
                {
                    return true;
                }

                if (el.Type == JTokenType.Property)
                {
                    JProperty p = el as JProperty;
                    if ((p != null) && p.HasValues && (p.Value != null && p.Value.Type == JTokenType.Undefined))
                    {
                        return true;
                    }
                }
                else if (((el.Type == JTokenType.Object) || (el.Type == JTokenType.Array)) && HasUndefinedValue(el))
                {
                    return true;
                }
            }

            return result;
        }

        public static JsonValidationResult IsValidJson<TSchema>(this string value, ref JToken actualJson)
        {
            var result = new JsonValidationResult();
            IList<string> errorMessages;

            if (!value.IsValidJson())
                return result;

            JsonSchemaGenerator generator = new JsonSchemaGenerator();
            JsonSchema schema = generator.Generate(typeof(TSchema));

            actualJson = JToken.Parse(value);
            bool valid = actualJson.IsValid(schema, out errorMessages);

            result.ErrorMessages = errorMessages.Where(x => !x.Contains("Invalid type. Expected Array but got Object.") && !x.Contains("Invalid type. Expected Object, Null but got String.")).ToList();
            result.IsValid = valid || ((result.ErrorMessages != null) && (result.ErrorMessages.Count == 0));

            if (result.IsValid)
            {
                if ((actualJson.Type != JTokenType.Array) && (actualJson.Type != JTokenType.Object))
                {
                    result.IsValid = false;
                }
                else
                {
                    result.IsValid = !HasUndefinedValue(actualJson);
                }
            }

            return result;
        }

        public static bool IsValidJson<TSchema>(this string value)
        {
            if (!value.IsValidJson())
                return false;

            JsonSchemaGenerator generator = new JsonSchemaGenerator();
            JsonSchema schema = generator.Generate(typeof(TSchema));

            var actualJson = JToken.Parse(value);
            bool valid = actualJson.IsValid(schema);

            return valid;
        }

        public static void WriteToXml(XmlDocument doc, string filename)
        {
            doc.Save(filename + ".xml");
        }

        public static JObject PrepareForXml(JToken json)
        {
            if (json == null)
                return null;

            if (json.Type != JTokenType.Object)
                return null;

            removeFields(json, new string[] { "errors" });
            if ((json == null) || (!json.HasValues))
                json["rawData"] = string.Empty;
            else
                json["rawData"] = json.ToString();

            return (JObject)json;
        }


        public static XmlDocument ConvertToXml(JToken json)
        {
            if (json == null)
                return null;

            if ((json.Type != JTokenType.Object) && (json.Type != JTokenType.Array))
                return null;

            if (json.Type == JTokenType.Array)
            {
                foreach (var item in json)
                {
                    PrepareForXml(item);
                }
            }
            else
            {
                PrepareForXml(json);
            }

            var jsonWithRoot = string.Format("{{'element': {0}}}", json);
            return JsonConvert.DeserializeXmlNode(jsonWithRoot, "elements");
        }

        public static XmlDocument ConvertToXml(string strJson)
        {
            //TODO "Danger" code. Add few checks.

            JToken json = JToken.Parse(strJson);

            return ConvertToXml(json);
        }

        public static void removeFields(JToken token, string[] fields)
        {
            JContainer container = token as JContainer;
            if (container == null) return;

            List<JToken> removeList = new List<JToken>();
            foreach (JToken el in container.Children())
            {
                JProperty p = el as JProperty;
                if (p != null && fields.Contains(p.Name))
                {
                    removeList.Add(el);
                }
                removeFields(el, fields);
            }

            foreach (JToken el in removeList)
            {
                el.Remove();
            }
        }
    }

    public interface IJsonSchema
    { 
    
    }

    public class JsonValidationResult
    {
        public JsonValidationResult()
        {
            ErrorMessages = new List<string>();
        }
        public bool IsValid { get; set; }
        public IList<string> ErrorMessages { get; set; }
    }
}
