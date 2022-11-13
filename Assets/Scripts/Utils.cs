using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.EventSystems;
using System;

public class Utils : MonoBehaviour
{
    public static GameObject FindObj(Transform parent, string name)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.name == name)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public static string CreateRandomId(int n, bool upper = true, bool lower = true, bool numeric = true)
    {
        List<int> startList = new List<int>();
        if (upper) startList.Add(65);
        if (lower) startList.Add(97);
        if (numeric) startList.Add(49);
        int[] starts = startList.ToArray();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            int r = starts[UnityEngine.Random.Range(0, starts.Length)];
            int d = r == 49 ? 9 : 26;
            sb.Append((char)(r + d * UnityEngine.Random.Range(0, 1f)));
        }
        return sb.ToString();
    }

    public static byte[] GetImageBytes(string imgPath)
    {
        FileStream fs = new FileStream(imgPath, FileMode.Open);
        byte[] bytes = new byte[fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        return bytes;
    }

    public static Sprite GetSprite(string imgPath, float width, float height)
    {
        byte[] bytes = GetImageBytes(imgPath);
        Texture2D tex = new Texture2D((int)width, (int)height);
        tex.LoadImage(bytes);
        Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        return sp;
    }

    public static void WriteImage(string imgPath, string toPath)
    {
        byte[] bytes = GetImageBytes(imgPath);
        File.WriteAllBytes(toPath, bytes);
    }

    public static IEnumerator EWaitSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public static GameObject RaycastUI(Vector2 pos)
    {
        EventSystem curEvent = EventSystem.current;
        PointerEventData eventData = new PointerEventData(curEvent);
        eventData.position = pos;
        List<RaycastResult> res = new List<RaycastResult>();
        curEvent.RaycastAll(eventData, res);
        if (res.Count > 0)
        {
            return res[0].gameObject;
        }
        else
        {
            return null;
        }
    }

    public static T String2Enum<T>(string str)
    {
        try
        {
            return (T)Enum.Parse(typeof(T),str);
        }
        catch (System.Exception)
        {

            return default(T);
        }
    }
}
