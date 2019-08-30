using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;

namespace md.stdl.json
{
    /// <summary>
    /// Collection of methods dealing with YAML to Json.NET conversion
    /// </summary>
    public static class YamlToJson
    {
        /// <summary>
        /// Convert yaml into JObject
        /// </summary>
        /// <param name="yaml"></param>
        /// <param name="maxRecursion"></param>
        /// <returns></returns>
        public static JObject ParseYamlToJson(string yaml, int maxRecursion = 500)
        {

            var sr = new StringReader(yaml);
            var deserializer = new DeserializerBuilder().Build();
            var yobj = deserializer.Deserialize(sr);

            var serializer = new SerializerBuilder()
                .DisableAliases()
                .WithMaximumRecursion(maxRecursion)
                .JsonCompatible()
                .Build();
            var immediatejson = serializer.Serialize(yobj);
            var res = JObject.Parse(immediatejson);
            ExpandYamlAliases(res);
            return res;
        }

        private static void FillYamlAlias(JObject curralias, JObject parent, LinkedList<JObject> parrentAliases)
        {
            //var removables = new LinkedList<JToken>();
            foreach (var aliaskvp in curralias)
            {
                if (aliaskvp.Key == "<<" && aliaskvp.Value.Type == JTokenType.Object)
                {
                    FillYamlAlias((JObject)aliaskvp.Value, parent, parrentAliases);
                    parrentAliases.AddLast((JObject)aliaskvp.Value);
                }
                else
                {
                    switch (aliaskvp.Value.Type)
                    {
                        case JTokenType.Object:
                            ExpandYamlAliases((JObject)aliaskvp.Value);
                            break;
                        case JTokenType.Array:
                            foreach (var jel in (JArray)aliaskvp.Value)
                            {
                                if (jel.Type == JTokenType.Object)
                                    ExpandYamlAliases((JObject)jel);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Expand "&lt;&lt;" YAML alias properties into their host object recursively.
        /// This method has side effects on the input JObject
        /// </summary>
        /// <param name="jobj"></param>
        public static void ExpandYamlAliases(JObject jobj)
        {
            //var removables = new LinkedList<JToken>();

            var aliases = new LinkedList<JObject>();

            foreach (var kvp in jobj)
            {
                if (kvp.Value.Type != JTokenType.Object && kvp.Value.Type != JTokenType.Array) continue;
                if (kvp.Key == "<<" && kvp.Value.Type == JTokenType.Object)
                {
                    FillYamlAlias((JObject)kvp.Value, jobj, aliases);
                    aliases.AddLast((JObject) kvp.Value);
                }
                else if (kvp.Value.Type == JTokenType.Object)
                {
                    ExpandYamlAliases((JObject)kvp.Value);
                }
                else
                {
                    var jarray = (JArray)kvp.Value;
                    foreach (var token in jarray)
                    {
                        if (token.Type == JTokenType.Object)
                            ExpandYamlAliases((JObject)token);
                    }
                }
            }

            jobj.Remove("<<");

            foreach (var alias in aliases)
            {
                alias.Merge(jobj, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Merge,
                    MergeNullValueHandling = MergeNullValueHandling.Merge
                });
                jobj.Merge(alias, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Merge,
                    MergeNullValueHandling = MergeNullValueHandling.Merge
                });
            }
        }
    }
}
