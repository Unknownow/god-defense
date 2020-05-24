using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    public static JsonObject ConvertStringToJsonObject<JsonObject>(string text)
    {
        JsonObject jsonObject = JsonUtility.FromJson<JsonObject>(text);
        return jsonObject;
    }

    public static string ReadFile(string path, string fileName)
    {
        if (!File.Exists(path + "/" + fileName))
        {
            return null;
        }
        StreamReader sr = File.OpenText(path + "/" + fileName);
        string file = sr.ReadToEnd();
        return file;
    }

    public static void WriteFile(string path, string fileName, string fileData)
    {
        if (!File.Exists(path + "/" + fileName))
        {
            Directory.CreateDirectory(path);
            File.Create(path + "/" + fileName);
        }
        //TODO: return whether or not write successfully.
    }
}
