using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sheet : MonoBehaviour
{
    private Image board;
    private Toggle tog;

    // Start is called before the first frame update
    void Awake()
    {
        board = Utils.FindObj(transform, "Image").GetComponent<Image>();
        tog = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetImage(Sprite sprite)
    {
        board.sprite = sprite;
    }
    public void SetImage(string imgPath)
    {
        if (board == null)
        {
            board = Utils.FindObj(transform, "Image").GetComponent<Image>();
        }
        board.sprite = Utils.GetSprite(imgPath, board.rectTransform.sizeDelta.x, board.rectTransform.sizeDelta.y);
    }

    public bool Selected()
    {
        return tog.isOn;
    }
}
