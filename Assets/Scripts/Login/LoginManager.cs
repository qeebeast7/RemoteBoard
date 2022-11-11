using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public GameObject window;
    public GameObject joinPanel;
    public GameObject headPanel;

    public string tmpHeadImagePath;

    // Start is called before the first frame update
    void Awake()
    {
        window = Utils.FindObj(transform, "Window");
        joinPanel = Utils.FindObj(window.transform, "JoinPanel");
        headPanel = Utils.FindObj(window.transform, "HeadPanel");
    }

    void Start()
    {
        tmpHeadImagePath = "";

        joinPanel.SetActive(false);
        headPanel.SetActive(false);
    }

    //BindListener
    //Window
    void OnJoinBtnClick()
    {
        StartCoroutine(Tweens.EUIFade(joinPanel, true, 0.2f));
    }
    void OnQuickBtnClick()
    {
        BoardManager.Instance.boardId = Utils.CreateRandomId(10,false,false,true);
        BoardManager.Instance.user.isRoomer = true;
        SceneManager.LoadSceneAsync("Scenes/Main");
    }
    void OnSetBtnClick()
    {
        tmpHeadImagePath = "";
        StartCoroutine(Tweens.EUIFade(headPanel, true, 0.2f));
    }
    void OnExitBtnClick()
    {
        BoardManager.Instance.OnQuit();
        Application.Quit();
    }

    //JoinPanel
    void OnEnterBtnClick(string id)
    {
        //Check Valid
        if(Regex.IsMatch(id, @"^\d{10}$"))
        {
            BoardManager.Instance.boardId = id;
            BoardManager.Instance.user.isRoomer = false;
            SceneManager.LoadSceneAsync("Scenes/Main");
        }
        else
        {
            BoardManager.Instance.TipHint("请输入正确的白板ID！");
        }
    }
    void OnCloseJoinBtnClick()
    {
        StartCoroutine(Tweens.EUIFade(joinPanel, false, 0.2f));
    }

    //HeadPanel
    void OnHeadBtnClick()
    {
        //OpenImageDialog
        FolderBrowserHelper.SelectFile(filePath => {
            tmpHeadImagePath = filePath;
        }, FolderBrowserHelper.IMAGEFILTER);
    }
    void OnOkBtnClick(object[] o)
    {
        Sprite sp = o[0] as Sprite;
        string name = (string)o[1];

        //Save to head.png
        if (tmpHeadImagePath != "")
        {
            Debug.Log(tmpHeadImagePath);
            Utils.WriteImage(tmpHeadImagePath, Application.streamingAssetsPath + "\\" + "head.png");
        }

        BoardManager.Instance.user.name = name;
        BoardManager.Instance.TipHint("修改成功！");
    }
    void OnCloseHeadBtnClick()
    {
        StartCoroutine(Tweens.EUIFade(headPanel, false, 0.2f));
    }

}
