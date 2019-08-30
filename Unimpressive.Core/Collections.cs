using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Unimpressive.Core
{
    /// <summary>
    /// Extension methods for IEnumerables and the like
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Overload of LINQ select where you can have different mapping functions for keys and the values
        /// </summary>
        /// <typeparam name="TOldKey">Type of source key</typeparam>
        /// <typeparam name="TOldValue">Type of source value</typeparam>
        /// <typeparam name="TNewKey">Type of target key</typeparam>
        /// <typeparam name="TNewValue">Type of target value</typeparam>
        /// <param name="source">Source dictionary</param>
        /// <param name="keymapper">Key mapping function</param>
        /// <param name="valuemapper">Value mapping function</param>
        /// <returns>New dictionary with the target types</returns>
        public static Dictionary<TNewKey, TNewValue> Select<TOldKey, TOldValue, TNewKey, TNewValue>
        (
            this IDictionary<TOldKey, TOldValue> source,
            Func<TOldKey, TNewKey> keymapper,
            Func<TOldValue, TNewValue> valuemapper
        )
        {
            return (from kvp in source select new KeyValuePair<TNewKey, TNewValue>(
                    keymapper(kvp.Key),
                    valuemapper(kvp.Value)
                )).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /// <summary>
        /// Shortcut to add or set a value in dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="d"></param>
        /// <param name="k"></param>
        /// <param name="v"></param>
        /// <returns>Value pass through</returns>
        public static TVal Update<TKey, TVal>(this IDictionary d, TKey k, TVal v)
        {
            if (d.Contains(k)) d[k] = v;
            else d.Add(k, v);
            return v;
        }

        /// <summary>
        /// Shortcut to add or set a value in dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="d"></param>
        /// <param name="k"></param>
        /// <param name="v"></param>
        /// <returns>Value pass through</returns>
        public static TVal UpdateGeneric<TKey, TVal>(this IDictionary<TKey, TVal> d, TKey k, TVal v)
        {
            if (d.ContainsKey(k)) d[k] = v;
            else d.Add(k, v);
            return v;
        }

        /// <summary>
        /// Shortcut to add a value to a hashset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hs"></param>
        /// <param name="v"></param>
        /// <returns>Value pass through</returns>
        public static T Update<T>(this HashSet<T> hs, T v)
        {
            if (!hs.Contains(v)) hs.Add(v);
            return v;
        }

        /// <summary>
        /// Fill a list or array from a starting point with the contents of another list or array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="from">The other list or array</param>
        /// <param name="start">Starting index offset</param>
        public static void Fill<T>(this IList<T> list, IList<T> from, int start = 0)
        {
            if(start >= list.Count) return;
            for (int i = 0; i < Math.Min(list.Count-start, from.Count); i++)
            {
                int ii = i + start;
                list[ii] = from[i];
            }
        }

        private static void opaqAddResults<TData>(List<TData> results, IEnumerable<TData> data,
            Func<TData, TData, bool> dataEqualityComparer = null)
        {
            foreach (var d in data)
            {
                bool contains = dataEqualityComparer == null ?
                    results.Contains(d) :
                    results.Any(o => dataEqualityComparer(d, o));
                if(!contains)
                    results.Add(d);
            }
        }
        
        private static void Opaq<TSrc, TData>(
            this TSrc obj,
            string path,
            List<TData> results,
            List<TSrc> children,
            string separator,
            Func<TSrc, IEnumerable<string>> dataKeysQuery,
            Func<TSrc, IEnumerable<string>> childrenKeysQuery,
            Func<TSrc, string, IEnumerable<TData>> dataFromKey,
            Func<TSrc, string, IEnumerable<TSrc>> childrenFromKey,
            Func<TData, TData, bool> dataEqualityComparer = null,
            Func<TSrc, TSrc, bool> childEqualityComparer = null)
        {
            if(string.IsNullOrWhiteSpace(path)) return;

            var levels = path.SplitIgnoringBetween(separator, "`");
            string nextpath = string.Join(separator, levels, 1, levels.Length - 1);

            void NextStep(string currkey)
            {
                foreach (var cobj in childrenFromKey(obj, currkey))
                {
                    bool contains = childEqualityComparer == null ?
                        children.Contains(cobj) :
                        children.Any(o => childEqualityComparer(cobj, o));
                    if (!contains)
                    {
                        children.Add(cobj);
                        cobj.Opaq(nextpath, results, children, separator, dataKeysQuery, childrenKeysQuery, dataFromKey, childrenFromKey, dataEqualityComparer, childEqualityComparer);
                    }
                }
            }
            
            if (levels[0][0] == '`' && levels[0][levels[0].Length - 1] == '`')
            {
                string key = levels[0].Trim('`');
                Regex Pattern = new Regex(key);
                foreach (string k in levels.Length == 1 ? dataKeysQuery(obj) : childrenKeysQuery(obj))
                {
                    if (Pattern.Match(k).Value == string.Empty) continue;
                    if (levels.Length == 1)
                        opaqAddResults(results, dataFromKey(obj, k), dataEqualityComparer);
                    else NextStep(k);
                }
            }
            else
            {
                if (levels.Length == 1)
                {
                    opaqAddResults(results, dataFromKey(obj, levels[0]), dataEqualityComparer);
                    return;
                }
                NextStep(levels[0]);
            }
        }

        /// <summary>
        /// Object PAth Query. General purpose object path query backend for accessing data with path like string and regex. It will also check for duplicates before adding to the result
        /// </summary>
        /// <typeparam name="TSrc">The source object type which contains the queryable data</typeparam>
        /// <typeparam name="TData">The endpoint data type which is queried</typeparam>
        /// <param name="obj">The source object which contains the queryable data</param>
        /// <param name="path">The path with set separator. Each path component excluding the endpoint represents a source object level. The endpoint should yield the Data</param>
        /// <param name="separator">The separator string used to distinguish path components</param>
        /// <param name="dataKeysQuery">(srcObject): possibleKeys; A function which returns the possible data key values for the next path component level</param>
        /// <param name="childrenKeysQuery">(srcObject): possibleKeys; A function which returns the possible children key values for the next path component level</param>
        /// <param name="dataFromKey">(srcObject, key): resultData; A function which returns data objects via an endpoint key </param>
        /// <param name="childrenFromKey">(srcObject, key): nextObjects; A function which returns the objects to be queried for the next component level</param>
        /// <param name="dataEqualityComparer">(a, b): equals; An optional function to use for deciding if the results already contains the queried data. If null, the built in Contains will be used</param>
        /// <param name="childEqualityComparer">(a, b): equals; An optional function to use for deciding if the results already contains data from previous children. If null, the built in Contains will be used</param>
        /// <returns>A list containing the resulting Data</returns>
        /// <remarks>
        /// This is a very generic method, Implementing simplified extension method versions for your class structure is recommended
        /// If you set data~ or childEqualityComparer to return always false you can turn off the duplication checking in either the children discovered or the results list.
        /// </remarks>
        public static List<TData> Opaq<TSrc, TData>(
            this TSrc obj,
            string path,
            string separator,
            Func<TSrc, IEnumerable<string>> dataKeysQuery,
            Func<TSrc, IEnumerable<string>> childrenKeysQuery,
            Func<TSrc, string, IEnumerable<TData>> dataFromKey,
            Func<TSrc, string, IEnumerable<TSrc>> childrenFromKey,
            Func<TData, TData, bool> dataEqualityComparer = null,
            Func<TSrc, TSrc, bool> childEqualityComparer = null)
        {
            var res = new List<TData>();
            var children = new List<TSrc>();
            obj.Opaq(path, res, children, separator, dataKeysQuery, childrenKeysQuery, dataFromKey, childrenFromKey, dataEqualityComparer, childEqualityComparer);
            return res;
        }

        /// <summary>
        /// Non recursive version of Opaq. General purpose object path query backend for accessing data with path like string and regex
        /// </summary>
        /// <typeparam name="TSrc">The source object type which contains the queryable data</typeparam>
        /// <typeparam name="TChild">The type of children</typeparam>
        /// <typeparam name="TData">The endpoint data type which is queried</typeparam>
        /// <param name="obj">The source object which contains the queryable data</param>
        /// <param name="path">The path with set separator. Each path component excluding the endpoint represents a source object level. The endpoint should yield the Data</param>
        /// <param name="results">A list containing the resulting Data</param>
        /// <param name="children">List of children which you would execute the recursive Opaq on</param>
        /// <param name="separator">The separator string used to distinguish path components</param>
        /// <param name="dataKeysQuery">(srcObject): possibleKeys; A function which returns the possible data key values for the next path component level</param>
        /// <param name="childrenKeysQuery">(srcObject): possibleKeys; A function which returns the possible children key values for the next path component level</param>
        /// <param name="dataFromKey">(srcObject, key): resultData; A function which returns data objects via an endpoint key </param>
        /// <param name="childrenFromKey">(srcObject, key): nextObjects; A function which returns the objects to be queried for the next component level</param>
        /// <param name="dataEqualityComparer">(a, b): equals; An optional function to use for deciding if the results already contains the queried data. If null, the built in Contains will be used</param>
        /// <param name="childEqualityComparer">(a, b): equals; An optional function to use for deciding if the results already contains data from previous children. If null, the built in Contains will be used</param>
        /// <remarks>
        /// Can be used for the situation when the first source element is not the same type as its children (for example a context or a container type). If the first level of the path is Data then children will be empty, otherwise when first level of the path is a Child, the results will be empty
        /// If you set dataEqualityComparer to return always false you can turn off the duplication checking.
        /// </remarks>
        public static string OpaqNonRecursive<TSrc, TChild, TData>(
            this TSrc obj,
            string path,
            List<TData> results,
            List<TChild> children,
            string separator,
            Func<TSrc, IEnumerable<string>> dataKeysQuery,
            Func<TSrc, IEnumerable<string>> childrenKeysQuery,
            Func<TSrc, string, IEnumerable<TData>> dataFromKey,
            Func<TSrc, string, IEnumerable<TChild>> childrenFromKey,
            Func<TData, TData, bool> dataEqualityComparer = null,
            Func<TChild, TChild, bool> childEqualityComparer = null)
        {
            var levels = path.SplitIgnoringBetween(separator, "`");
            string nextpath = string.Join(separator, levels, 1, levels.Length - 1);

            if (levels[0][0] == '`' && levels[0][levels[0].Length - 1] == '`')
            {
                string key = levels[0].Trim('`');
                Regex Pattern = new Regex(key);
                foreach (string k in levels.Length == 1 ? dataKeysQuery(obj) : childrenKeysQuery(obj))
                {
                    if (Pattern.Match(k).Value == string.Empty) continue;
                    if (levels.Length == 1)
                        opaqAddResults(results, dataFromKey(obj, k), dataEqualityComparer);
                    else opaqAddResults(children, childrenFromKey(obj, k), childEqualityComparer);
                }
            }
            else
            {
                if (levels.Length == 1)
                    opaqAddResults(results, dataFromKey(obj, levels[0]), dataEqualityComparer);
                else opaqAddResults(children, childrenFromKey(obj, levels[0]), childEqualityComparer);
            }
            return nextpath;
        }

        /// <summary>
        /// Shortcut to thread safe enumeration of Concurrent Dictionaries
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForeachConcurrent<TKey, TVal>(
            this ConcurrentDictionary<TKey, TVal> collection,
            Action<TKey, TVal> action)
        {
            using (var enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    action(enumerator.Current.Key, enumerator.Current.Value);
                }
            }
        }

        /// <summary>
        /// Shortcut to thread safe enumeration of Concurrent Bags
        /// </summary>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForeachConcurrent<TVal>(
            this ConcurrentBag<TVal> collection,
            Action<TVal> action)
        {
            using (var enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    action(enumerator.Current);
                }
            }
        }

        /// <summary>
        /// Shortcut to thread safe enumeration of Concurrent Queues
        /// </summary>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForeachConcurrent<TVal>(
            this ConcurrentQueue<TVal> collection,
            Action<TVal> action)
        {
            using (var enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    action(enumerator.Current);
                }
            }
        }

        /// <summary>
        /// Shortcut to thread safe enumeration of Concurrent Stacks
        /// </summary>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForeachConcurrent<TVal>(
            this ConcurrentStack<TVal> collection,
            Action<TVal> action)
        {
            using (var enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    action(enumerator.Current);
                }
            }
        }
    }
}
