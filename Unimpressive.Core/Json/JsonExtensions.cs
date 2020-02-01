using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Unimpressive.Core.json
{
    /// <summary>
    /// Collection of functions which makes life working with Json.NET better
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Try to get a token from jpath, return true if it exists and it's not Null or None
        /// </summary>
        /// <param name="source">The containing JToken</param>
        /// <param name="jpath"></param>
        /// <param name="token">Result JToken</param>
        /// <returns>true if desired token exists and it's not Null or None</returns>
        public static bool TryGetToken(this JToken source, string jpath, out JToken token)
        {
            var res = source.SelectToken(jpath);
            if (res != null && res.Type != JTokenType.Null && res.Type != JTokenType.None)
            {
                token = res;
                return true;
            }
            else
            {
                token = null;
                return false;
            }
        }

        /// <summary>
        /// Try to get the value of a JToken from a jpath, return true if it exists and it's not Null or None
        /// </summary>
        /// <typeparam name="T">Type of the desired value</typeparam>
        /// <param name="source">The containing JToken</param>
        /// <param name="jpath"></param>
        /// <param name="value">Result value</param>
        /// <returns>true if desired token exists and it's not Null or None</returns>
        public static bool TryGetFromPath<T>(this JToken source, string jpath, out T value)
        {
            if (source.TryGetToken(jpath, out var res))
            {
                if (res.Type != JTokenType.Array && res.Type != JTokenType.Object)
                {
                    value = res.Value<T>();
                    return true;
                }
                value = res.ToObject<T>();
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Get the value of a JToken from a jpath. return a default value if it doesn't exist or it's has jtype Null or None
        /// </summary>
        /// <typeparam name="T">Type of the desired value</typeparam>
        /// <param name="source">The containing JToken</param>
        /// <param name="jpath"></param>
        /// <param name="def">Optional input for default value if jpath points to an invalid JToken</param>
        /// <returns>Result value</returns>
        public static T GetFromPath<T>(this JToken source, string jpath, T def = default)
        {
            if (source.TryGetToken(jpath, out var res))
            {
                if (res.Type != JTokenType.Array && res.Type != JTokenType.Object)
                    return res.Value<T>();
                return res.ToObject<T>();
            }
            else return def;
        }

        /// <summary>
        /// Creates an XElement out of a JToken
        /// </summary>
        /// <param name="jtoken"></param>
        /// <returns></returns>
        public static XElement AsXElement(this JToken jtoken)
        {
            var json = jtoken.ToString();
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            try
            {
                XmlReader reader = JsonReaderWriterFactory.CreateJsonReader(buffer, new XmlDictionaryReaderQuotas());
                XElement root = XElement.Load(reader);
                return root;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Deletes fields from a JToken
        /// </summary>
        /// <param name="jtoken"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static JToken RemoveFields(this JToken jtoken, params string[] fields)
        {
            if (!(jtoken is JContainer container)) return jtoken;

            var removeList = new List<JToken>();
            foreach (var el in container.Children())
            {
                if (el is JProperty p && fields.Contains(p.Name))
                {
                    removeList.Add(el);
                }
                el.RemoveFields(fields);
            }

            foreach (var el in removeList)
            {
                el.Remove();
            }

            return jtoken;
        }
    }
}
