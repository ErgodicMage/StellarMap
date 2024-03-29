﻿using System.Linq;

namespace StellarMap.Core.Types;

public sealed class NestedDictionary<TOuter, TInner, TValue> : 
    Dictionary<TOuter, Dictionary<TInner, TValue>>, IEquatable<NestedDictionary<TOuter, TInner, TValue>>
{
    #region Constructors
    #endregion

    #region Properties
    public TValue? this[TOuter outer, TInner inner] => this[outer][inner];
    #endregion

    #region Add Methods
    public void Add(TOuter outer)
    {
        if (!ContainsKey(outer))
            Add(outer, new Dictionary<TInner, TValue>());
    }

    public void Add(TOuter outer, TInner inner, TValue value)
    {
        if (TryGetValue(outer, out Dictionary<TInner, TValue> innerDictionary) && !innerDictionary.ContainsKey(inner))
            innerDictionary.Add(inner, value);
    }

    public void AddToOuter(TOuter outer, IEnumerable<KeyValuePair<TInner, TValue>> innerDictionary)
    {
        if (!TryGetValue(outer, out Dictionary<TInner, TValue> currentDictionary))
        {
            currentDictionary = new Dictionary<TInner, TValue>();
            Add(outer, currentDictionary);
        }

        foreach (var kvp in innerDictionary)
        {
            if (!currentDictionary.ContainsKey(kvp.Key))
                currentDictionary.Add(kvp.Key, kvp.Value);
        }
    }

    public void AddToInner(TOuter outer, IEnumerable<KeyValuePair<TInner, TValue>> innerDictionary)
    {
        if (TryGetValue(outer, out Dictionary<TInner, TValue> currentDictionary))
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
        Dictionary<TInner, TValue> innerDictionary = Get(outer);

        return innerDictionary?.Remove(inner) ?? false;
    }
    #endregion

    #region Get Set Methods
    public Dictionary<TInner, TValue>? Get(TOuter outer)
    {
        TryGetValue(outer, out Dictionary<TInner, TValue> innerDictionary);
        return innerDictionary;
    }

    public TValue? Get(TOuter outer, TInner inner)
    {
        TValue? value = default;

        Dictionary<TInner, TValue>? innerDictionary = Get(outer);
        innerDictionary?.TryGetValue(inner, out value);

        return value;
    }

    public void Set(TOuter outer, Dictionary<TInner, TValue> innervalues)
    {
        if (!ContainsKey(outer))
            Add(outer, innervalues);
        else
            this[outer] = innervalues;
    }

    public void Set(TOuter outer, TInner inner, TValue value)
    {
        if (ContainsKey(outer) && this[outer].ContainsKey(inner))
            this[outer][inner] = value;
    }
    #endregion

    #region Misc Methods
    public bool ContainsKey(TOuter outer, TInner inner) => ContainsKey(outer) && this[outer].ContainsKey(inner);

    public bool ContainsValue(TValue value) => Values.Any(kvp => kvp.ContainsValue(value));

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

            sb.Append(outer?.ToString());
            sb.Append(":");

            var inners = this[outer];
            foreach (var inner in inners)
            {
                sb.AppendLine();
                sb.Append('\t');
                sb.Append(inner.Key?.ToString());
                sb.Append(" : ");
                sb.Append(inner.Value?.ToString());
            }
        }

        return sb.ToString();
    }
    #endregion

    #region IEquatable
    public bool Equals(NestedDictionary<TOuter, TInner, TValue>? other)
    {
        bool bRet = true;

        if (other is null)
            bRet = false;
        else if (this.Count != other.Count)
            bRet = false;
        else if (this.Count == 0)
            bRet = true;
        else if (!ReferenceEquals(this, other))
        {
            var thisEnumerator = GetEnumerator();
            var otherEnumerator = other.GetEnumerator();

            while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
            {
                if (thisEnumerator.Current.Key is not null && thisEnumerator.Current.Key.Equals(otherEnumerator.Current.Key))
                {
                    if (!InnerEquals(thisEnumerator.Current.Value, otherEnumerator.Current.Value))
                    {
                        bRet = false;
                        break;
                    }
                }
                else
                {
                    bRet = false;
                    break;
                }
            }
        }

        return bRet;
    }

    public override bool Equals(object? obj) => Equals(obj as NestedDictionary<TOuter, TInner, TValue>);

    public override int GetHashCode()
    {
        int hash = 15;

        foreach (TOuter outer in Keys)
        {
            hash ^= outer is null ? 1 : outer.GetHashCode();
            foreach (var inner in this[outer])
            {
                hash ^= (inner.Key is null ? 1 : inner.Key.GetHashCode()) ^ (inner.Value is null ? 1 : inner.Value.GetHashCode());
            }
        }

        return hash;
    }

    private static bool InnerEquals(Dictionary<TInner, TValue>? thisObject, Dictionary<TInner, TValue>? otherObject)
    {
        bool bRet = true;

        if (thisObject is null && otherObject is null)
            bRet = true;
        else if ((thisObject is null) || (otherObject is null))
            bRet = false;
        else if (thisObject.Count != otherObject.Count)
            bRet = false;
        else if (thisObject.Count == 0)
            bRet = true;
        else if (!ReferenceEquals(thisObject, otherObject))
        {
            var thisEnumerator = thisObject.GetEnumerator();
            var otherEnumerator = otherObject.GetEnumerator();

            while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
            {
                if (thisEnumerator.Current.Key is not null && thisEnumerator.Current.Key.Equals(otherEnumerator.Current.Key))
                {
                    if (thisEnumerator.Current.Value is not null && !thisEnumerator.Current.Value.Equals(otherEnumerator.Current.Value))
                    {
                        bRet = false;
                        break;
                    }
                }
                else
                {
                    bRet = false;
                    break;
                }
            }
        }

        return bRet;
    }
    #endregion
}

