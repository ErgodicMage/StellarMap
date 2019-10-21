using System;
using System.Collections.Generic;
using System.Text;

namespace StellarMap.Core.Types
{
    public class NestedDictionary<TOuter, TInner, TValue> : Dictionary<TOuter, Dictionary<TInner, TValue>>
    {
        #region Constructors
        #endregion

        #region Properties
        public TValue this[TOuter outer, TInner inner] => this[outer][inner];
        #endregion

        #region Add Methods
        public void Add(TOuter outer)
        {
            if (!ContainsKey(outer))
                Add(outer, new Dictionary<TInner, TValue>());
        }

        public void Add(TOuter outer, TInner inner, TValue value)
        {
            Dictionary<TInner, TValue> innerDictionary;

            if (TryGetValue(outer, out innerDictionary) && !innerDictionary.ContainsKey(inner))
            {
                innerDictionary.Add(inner, value);
            }
        }

        public void AddToOuter(TOuter outer, IEnumerable<KeyValuePair<TInner, TValue>> innerDictionary)
        {
            Dictionary<TInner, TValue> currentDictionary;

            if (!TryGetValue(outer, out currentDictionary))
            {
                currentDictionary = new Dictionary<TInner, TValue>();
                Add(outer, currentDictionary);
            }

            foreach(var kvp in innerDictionary)
            {
                if (!currentDictionary.ContainsKey(kvp.Key))
                    currentDictionary.Add(kvp.Key, kvp.Value);
            }
        }

        public void AddToInner(TOuter outer, IEnumerable<KeyValuePair<TInner, TValue>> innerDictionary)
        {
            Dictionary<TInner, TValue> currentDictionary;

            if (TryGetValue(outer, out currentDictionary))
            {
                foreach (var kvp in innerDictionary)
                {
                    if (!currentDictionary.ContainsKey(kvp.Key))
                        currentDictionary.Add(kvp.Key, kvp.Value);
                }
            }
        }
        #endregion

        #region Remove Methods
        public bool Remove(TOuter outer, TInner inner)
        {
            bool retValue = false;

            Dictionary<TInner, TValue> innerDictionary = Get(outer);

            if (innerDictionary != null)
                retValue = innerDictionary.Remove(inner);

            return retValue;
        }
        #endregion

        #region Get Set Methods
        public Dictionary<TInner, TValue> Get(TOuter outer)
        {
            Dictionary<TInner, TValue> innerDictionary;
            TryGetValue(outer, out innerDictionary);
            return innerDictionary;
        }

        public TValue Get(TOuter outer, TInner inner)
        {
            TValue value = default(TValue);

            Dictionary<TInner, TValue> innerDictionary = Get(outer);
            if (innerDictionary != null)
                innerDictionary.TryGetValue(inner, out value);

            return value;
        }

        public void Set(TOuter outer, Dictionary<TInner, TValue> innervalues)
        {
            if (!ContainsKey(outer))
                Add(outer, innervalues);
            else
            {
                Dictionary<TInner, TValue> innerDictionary;
                if (TryGetValue(outer, out innerDictionary))
                {
                    foreach (var kvp in innervalues)
                    {
                        if (innerDictionary.ContainsKey(kvp.Key))
                            innerDictionary[kvp.Key] = kvp.Value;
                    }
                }
            }
        }

        public void Set(TOuter outer, TInner inner, TValue value)
        {
            if (ContainsKey(outer) && this[outer].ContainsKey(inner))
                this[outer][inner] = value;
        }
        #endregion

        #region Misc Methods
        public bool ContainsKey(TOuter outer, TInner inner) => ContainsKey(outer) && this[outer].ContainsKey(inner);

        public bool ContainsValue(TValue value)
        {
            bool retValue = false;

            foreach(var outer in this)
            {
                if (outer.Value.ContainsValue(value))
                {
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            bool isFirst = true;

            foreach (TOuter outer in Keys)
            {
                if (isFirst)
                    isFirst = false;
                else
                    sb.AppendLine();

                sb.Append(outer.ToString());
                sb.Append(":");

                var inners = this[outer];
                foreach (var inner in inners)
                {
                    sb.AppendLine();
                    sb.Append('\t');
                    sb.Append(inner.Key.ToString());
                    sb.Append(" : ");
                    sb.Append(inner.Value.ToString());
                }
            }

            return sb.ToString();
        }
        #endregion
    }
}
