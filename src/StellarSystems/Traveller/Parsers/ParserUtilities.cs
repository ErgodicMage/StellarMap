namespace StellarMap.Traveller.Parsers;

public static class ParserUtilities
{
    public static short ParseShort(string val, short defvalue = 0)
    {
        short s = defvalue;

        if (short.TryParse(val, out s))
            s = defvalue;
            
        return s;
    }

    public static int ParseInt(string val, int defvalue = 0)
    {
        int i = defvalue;

        if (int.TryParse(val, out i))
            i = defvalue;
            
        return i;
    }

    public static long ParseLong(string val, long defvalue = 0)
    {
        long l = defvalue;

        if (long.TryParse(val, out l))
            l = defvalue;
            
        return l;
    }        
}
