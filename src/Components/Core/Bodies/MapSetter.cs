using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace StellarMap.Core.Bodies
{
    public static class MapSetter
    {
        public static Result SetMap(IStellarMap thisMap, IStellarMap otherMap)
        {
            Result guardResult = GuardClause.Null(thisMap).Null(otherMap);
            if (!guardResult.Success) return guardResult;

            foreach (var propInfo in thisMap.GetType().GetProperties())
            {
                if (propInfo.Name == "Name" || propInfo.Name == "MetData")
                    continue;

                if (propInfo.CustomAttributes.Any(pt => pt.AttributeType == typeof(IgnoreDataMemberAttribute)))
                    continue;

                object obj = propInfo.GetValue(thisMap);
                if (obj == null)
                    continue;
                if (obj is StellarBody body)
                {
                    body.Map = otherMap;
                    continue;
                }

                Type type = obj.GetType();
                if (type.IsGenericType)
                {
                    switch (type.Name)
                    {
                        case "Dictionary`2":
                        case "IDictionary`2":
                            HandleDictionary(obj, type, otherMap);
                            break;
                    }
                }
            }

            return Result.Ok();
        }

        public static void HandleDictionary(object obj, Type type, IStellarMap otherMap)
        {
            Type[] genericTypes = type.GetGenericArguments();
            for (int i = 0; i < genericTypes.Count(); i++)
            {
                bool isStellarBody = IsStellarBody(genericTypes[i]);
                HandleDictionaryStellarBody(obj, i, otherMap);
            }
        }

        public static bool IsStellarBody(Type type)
        {
            Type baseType = type.BaseType;
            while (baseType != null)
            {
                if (baseType.Name == "StellarBody" || baseType.Name == "StellarParentBody")
                    return true;
                baseType = baseType.BaseType;
            }

            return false;
        }

        public static void HandleDictionaryStellarBody(object obj, int position, IStellarMap otherMap)
        {
            string property = position == 0 ? "Keys" : "Values";

            Type iDictType = obj.GetType().GetInterface("IDictionary`2")!;

            IEnumerable enumerator = (IEnumerable)iDictType.GetProperty(property).GetValue(obj, null);
            foreach (object o in enumerator!)
            {
                if (o is StellarBody body)
                    body.Map = otherMap;
            }

            enumerator = (IEnumerable)iDictType.GetProperty(property).GetValue(obj, null);
            foreach (object o in enumerator!)
            {
                if (o is StellarBody body)
                    body.Map = otherMap;
            }
        }
    }
}
