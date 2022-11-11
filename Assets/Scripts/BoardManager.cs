using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class BoardManager : Singleton<BoardManager>
{
    public User user;
    public UserUI uui;
    public string boardId="";

    public GameObject tip;
    public Animation tipAnim;
    public Text tipText;

    public List<GameObject> sheets = new List<GameObject>();
    public List<GameObject> users = new List<GameObject>();


    void Awake()
    {
        user = JsonHelper.ReadJson<User>("user");
        if (user == null)
        {
            user = new User();
            user.SetInfo();
        }

        tip = GameObject.Find("Tip");
        tip.AddComponent<Stay>();
        tipAnim = tip.GetComponent<Animation>();
        tipText = tip.GetComponentInChildren<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        tip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TipHint(string hint)
    {
        tipText.text = hint;
        tip.SetActive(true);
        tipAnim.Play();
    }

    public void SetHeadImage(Image headImage)
    {
        headImage.sprite=Utils.GetSprite(Application.streamingAssetsPath + "\\" + "head.png",
    headImage.rectTransform.sizeDelta.x, headImage.rectTransform.sizeDelta.y);

    }

    public void OnQuit()
    {
        JsonHelper.WriteJson(user, "user");
    }

    private void OnApplicationQuit()
    {
        OnQuit();
    }
}
