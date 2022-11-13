using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonHelper : MonoBehaviour
{
    public static void WriteJson<T>(T data,string fileName)
    {
        string json = JsonUtility.ToJson(data);
        string filePath = Application.streamingAssetsPath + "\\" + fileName + ".txt";
        StreamWriter sw = new StreamWriter(filePath);
        sw.WriteLine(json);
        sw.Close();
    }

    public static T ReadJson<T>(string fileName)
    {
        string filePath = Application.streamingAssetsPath + "\\" + fileName + ".txt";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string json = sr.ReadToEnd();
            if (json == "" || json==string.Empty) return default(T);
            sr.Close();
            try
            {
                T data = JsonUtility.FromJson<T>(json);
                return data;
            }
            catch (System.Exception)
            {

                return default(T);
            }
        }
        else
        {
            return default(T);
        }
    }
}
