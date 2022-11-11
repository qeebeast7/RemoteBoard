using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Tweens : MonoBehaviour
{
    public static void UIFade(GameObject obj, bool isAppear, float time = 0.5f)
    {
        //Fade
        int value = isAppear ? 1 : 0;

        foreach (var item in obj.GetComponentsInChildren<Image>())
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, 1 - value);
        }
        foreach (var item in obj.GetComponentsInChildren<Text>())
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, 1 - value);
        }
        foreach (var item in obj.GetComponentsInChildren<Image>())
        {
            item.DOFade(value, time);
        }
        foreach (var item in obj.GetComponentsInChildren<Text>())
        {
            item.DOFade(value, time);
        }
    }

    public static IEnumerator EUIFade(GameObject obj, bool isAppear, float time = 0.5f)
    {
        UIFade(obj,isAppear,time);
        float waitSecs = isAppear ? 0 : time;
        yield return new WaitForSeconds(waitSecs);
        obj.SetActive(isAppear);
    }
}
