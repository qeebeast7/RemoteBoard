using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour
{
    public User user;

    private Image headImage;
    private Text nameText;
    private GameObject roomer;

    private void Awake()
    {
        user = BoardManager.Instance.user;

        headImage = Utils.FindObj(transform, "Image").GetComponent<Image>();
        nameText= Utils.FindObj(transform, "Name").GetComponent<Text>();
        roomer= Utils.FindObj(transform, "Roomer");
    }

    private void Start()
    {
        SetRoomer();
    }

    public void AllowDraw(UserUI uui,bool value)
    {
        uui.user.canDraw = value;
    }

    public void KickOff(User user)
    {

    }

    public void PassRoomer(UserUI uui)
    {
        uui.user.isRoomer = true;
        this.user.isRoomer = false;
    }

    public void SetHeadImage(Sprite sprite)
    {
        headImage.sprite = sprite;
    }

    public void SetHeadImage()
    {
        BoardManager.Instance.SetHeadImage(headImage);
    }

    public void SetName()
    {
        nameText.text = user.name;
    }

    public void SetRoomer()
    {
        roomer.SetActive(user.isRoomer);
    }

    public void SetAll()
    {
        SetHeadImage();
        SetName();
        SetRoomer();
    }
}
