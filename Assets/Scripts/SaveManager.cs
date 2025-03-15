
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class SaveManager
{
    
    public static void saveData(PlayerStats ps)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveState data = new SaveState(ps);


        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved");
    }

    public static SaveState loadData()
    {
        string path = Application.persistentDataPath + "/game.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveState data = formatter.Deserialize(stream) as SaveState;
            stream.Close();
            Debug.Log("Loaded");
            return data;
        } 
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
