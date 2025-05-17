using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnixTime
{

    public static long GetUnixTime(DateTime date)
    {
        return ((DateTimeOffset)date).ToUnixTimeSeconds();
    }
    public static DateTime GetDateTime(long time)
    {
        return DateTimeOffset.FromUnixTimeSeconds(time).UtcDateTime;
    }
    public static long GetUnixTimeMinutes(long minutes)
    {
        return (minutes * 60);
    }
}
