using System.IO;
using System;
using UnityEngine;

public static class FileLogger
{
    static string logPath = Path.Combine(Application.persistentDataPath, "game.log");

    public static void Log(string msg)
    {
        File.AppendAllText(logPath, $"{DateTime.Now:HH:mm:ss} {msg}\n");
        Debug.Log(msg);
    }
}