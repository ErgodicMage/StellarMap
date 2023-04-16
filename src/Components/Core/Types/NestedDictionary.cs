using System.Linq;
using System.Net.Http.Headers;

namespace StellarMap.Core.Types;

public sealed class NestedDictionary<TOuter, TInner, TValue> : 
    Dictionary<TOuter, Dictionary<TInner, TValue>>, IEquatable<NestedDictionary<TOuter, TInner, TValue>>
{
    #region Constructors
    #endregion

    #region Properties
    public TValue? this[TOuter outer, TInner inner] => Get(outer, inner).Value;
    #endregion

    #region Add Methods
    public Result Add(TOuter outer)
    {
        Result resultGuard = GuardClause.Null(outer);
        if (!resultGuard.Success) return resultGuard;

        if (ContainsKey(outer))
            return Result.Error("NestedDictionary:Add outer key already exists");

        Add(outer, new Dictionary<TInner, TValue>());

        return Result.Ok();
    }

    public Result Add(TOuter outer, TInner inner, TValue value)
    {
        Result resultGuard = GuardClause.Null(outer).Null(inner);
        if (!resultGuard.Success) return resultGuard;

        if (!TryGetValue(outer, out Dictionary<TInner, TValue> innerDictionary))
            return Result.Error("NestedDictionary:Add can not get inner dictionary");

        if (innerDictionary.ContainsKey(inner))
            return Result.Error("NestedDictionary:Add inner key already exists");

        innerDictionary.Add(inner, value);

        return Result.Ok();
    }

    public Result AddToOuter(TOuter outer, IEnumerable<KeyValuePair<TInner, TValue>> innerDictionary)
    {
        Result resultGuard = GuardClause.Null(outer).Null(innerDictionary);
        if (!resultGuard.Success) return resultGuard;

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

        return Result.Ok();
    }

    public Result AddToInner(TOuter outer, IEnumerable<KeyValuePair<TInner, TValue>> innerDictionary)
    {
        Result resultGuard = GuardClause.Null(outer).Null(innerDictionary);
        if (!resultGuard.Success) return resultGuard;

        if (TryGetValue(outer, out Dictionary<TInner, TValue> currentDictionary))
        {
            foreach (var kvp in innerDictionary)
            {
                if (!currentDictionary.ContainsKey(kvp.Key))
                    currentDictionary.Add(kvp.Key, kvp.Value);
            }
        }

        return Result.Ok();
    }
    #endregion

    #region Remove Methods
    public Result Remove(TOuter outer, TInner inner)
    {
        Result resultGuard = GuardClause.Null(outer).Null(inner);
        if (!resultGuard.Success) return resultGuard;

        var result = Get(outer);
        if (!result.Success) return result;

        return result.Value?.Remove(inner) ?? Result.Error("NestedDictionary:Remove can not remove value from inner dictionary");
    }
    #endregion

    #region Get Set Methods
    public Result<IDictionary<TInner, TValue>> Get(TOuter outer)
    {
        Result resultGuard = GuardClause.Null(outer);
        if (!resultGuard.Success) return resultGuard;

        if (!TryGetValue(outer, out var innerDictionary))
            return Result.Error("NestedDictionary:Get can not get inner dictionary");

        return innerDictionary;
    }

    public Result<TValue> Get(TOuter outer, TInner inner)
    {
        Result resultGuard = GuardClause.Null(outer).Null(inner);
        if (!resultGuard.Success) return resultGuard;

        var resultInnerDictionary = Get(outer);
        if (!resultInnerDictionary.Success)
            return Result.Error("NestedDictionary:Get can not get inner dictionary");

        if (!resultInnerDictionary.Value.TryGetValue(inner, out var value))
            return Result.Error("NestedDictionary:Get can not get value from inner dectionary");

        return value;
    }

    public Result Set(TOuter outer, Dictionary<TInner, TValue> innervalues)
    {
        Result resultGuard = GuardClause.Null(outer).Null(innervalues);
        if (!resultGuard.Success) return resultGuard;

        if (!ContainsKey(outer))
            Add(outer, innervalues);
        else
            this[outer] = innervalues;

        return Result.Ok();
    }

    public Result Set(TOuter outer, TInner inner, TValue value)
    {
        Result resultGuard = GuardClause.Null(outer).Null(inner);
        if (!resultGuard.Success) return resultGuard;

        if (!ContainsKey(outer, inner))
            return Result.Error("NestedDictionary:Set can not get key for outer and inner");
 
        this[outer][inner] = value;

        return Result.Ok();
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

