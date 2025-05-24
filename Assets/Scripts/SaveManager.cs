
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public static class SaveManager
{
    
    public static void saveData(PlayerStats ps)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (Application.platform == RuntimePlatform.WebGLPlayer) 
        {
            //save all data as a json string
            SaveState data = new SaveState(ps);
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, data);
                byte[] bytes = ms.ToArray();
                string b64 = Convert.ToBase64String(bytes);
                //place in playerprefs
                PlayerPrefs.SetString("SaveData", b64);
                PlayerPrefs.Save();
                Debug.Log("Saved");
            }

           
        } else
        {
            string path = Application.persistentDataPath + "/game.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            SaveState data = new SaveState(ps);


            formatter.Serialize(stream, data);
            stream.Close();
            Debug.Log("Saved");
        }
    }

    public static SaveState loadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            //retrieve from playerprefs and convert back to savestate
            if (PlayerPrefs.HasKey("SaveData"))
            {
                string b64 = PlayerPrefs.GetString("SaveData");
                byte[] bytes = Convert.FromBase64String(b64);
                MemoryStream ms = new MemoryStream();
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                SaveState data = formatter.Deserialize(ms) as SaveState;
                ms.Close();
                return data;
            } else
            {
                Debug.Log("Save file not found in PlayerPrefs SaveData");
                return null;
            }
        }
        else
        {
            string path = Application.persistentDataPath + "/game.save";
            if (File.Exists(path))
            {
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
}
