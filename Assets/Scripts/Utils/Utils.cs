using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class Utils
{
    public static float DistanceInXZ(Vector3 vec1, Vector3 vec2)
    {
        return Vector2.Distance(new Vector2(vec1.x, vec1.z), new Vector2(vec2.x, vec2.z));
    }

    public static JsonObject ReadResourcesToJson<JsonObject>(string path)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(path);
        JsonObject jsonObject = JsonUtility.FromJson<JsonObject>(jsonFile.text);
        return jsonObject;
    }

    // public static JsonObject ConvertStringToJsonObject<JsonObject>(string text)
    // {
    //     JsonObject jsonObject = JsonUtility.FromJson<JsonObject>(text);
    //     return jsonObject;
    // }

    public static string ReadFile(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();
        sr.Close();
        return file;
    }

    public static void WriteFile(string path, string fileData)
    {
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Create(path).Close();
        }
        StreamWriter sw = new StreamWriter(path, false);
        try
        {
            sw.Write(fileData);
        }
        catch (ObjectDisposedException e)
        {
            Debug.Log(e.ToString());
        }
        catch (NotSupportedException e)
        {
            Debug.Log(e.ToString());
        }
        catch (IOException e)
        {
            Debug.Log(e.ToString());
        }
        sw.Close();
    }
}
