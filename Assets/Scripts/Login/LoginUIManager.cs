using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIManager : Singleton<LoginUIManager>
{
    private LoginManager lm;

    private Button joinBtn;
    private Button quickBtn;
    private Button setBtn;
    private Button exitBtn;

    private InputField idInput;
    private Button enterBtn;
    private Button closeJoinBtn;

    private Button headBtn;
    private Image headImage;
    private InputField nameInput;
    private Button okBtn;
    private Button closeHeadBtn;
    
    void Awake()
    {
        if (GetComponent<LoginManager>() == null)
        {
            lm = gameObject.AddComponent<LoginManager>();
        }
        else
        {
            lm = gameObject.GetComponent<LoginManager>();
        }
    }

    void Start()
    {
        //Find
        //Window
        joinBtn = Utils.FindObj(lm.window.transform, "Join").GetComponentInChildren<Button>();
        quickBtn = Utils.FindObj(lm.window.transform, "Quick").GetComponentInChildren<Button>();
        setBtn= Utils.FindObj(lm.window.transform, "SetBtn").GetComponent<Button>();
        exitBtn = Utils.FindObj(lm.window.transform, "ExitBtn").GetComponent<Button>();

        //JoinPanel
        idInput= Utils.FindObj(lm.joinPanel.transform, "IDInput").GetComponent<InputField>();
        enterBtn = Utils.FindObj(lm.joinPanel.transform, "EnterBtn").GetComponent<Button>();
        closeJoinBtn= Utils.FindObj(lm.joinPanel.transform, "CloseBtn").GetComponent<Button>();

        //HeadPanel
        headBtn= Utils.FindObj(lm.headPanel.transform, "HeadImage").GetComponentInChildren<Button>();
        headImage = headBtn.GetComponent<Image>();
        nameInput= Utils.FindObj(lm.headPanel.transform, "NameInput").GetComponent<InputField>();
        okBtn = Utils.FindObj(lm.headPanel.transform, "OkBtn").GetComponent<Button>();
        closeHeadBtn = Utils.FindObj(lm.headPanel.transform, "CloseBtn").GetComponent<Button>();

        //BindListener
        //Window
        joinBtn.onClick.AddListener(()=> {
            idInput.text = "";
            SendMessage("OnJoinBtnClick");
        });
        quickBtn.onClick.AddListener(() => {
            SendMessage("OnQuickBtnClick");
        });
        setBtn.onClick.AddListener(() => {
            nameInput.text = BoardManager.Instance.user.name;
            SendMessage("OnSetBtnClick");
        });
        exitBtn.onClick.AddListener(() => {
            SendMessage("OnExitBtnClick");
        });

        //JoinPanel
        enterBtn.onClick.AddListener(() => {
            SendMessage("OnEnterBtnClick",idInput.text);
        });
        closeJoinBtn.onClick.AddListener(() => {
            SendMessage("OnCloseJoinBtnClick");
        });

        //HeadPanel
        headBtn.onClick.AddListener(() => {
            SendMessage("OnHeadBtnClick");
            if (lm.tmpHeadImagePath != "")
            {
                headImage.sprite = Utils.GetSprite(lm.tmpHeadImagePath,
                    headImage.rectTransform.sizeDelta.x, headImage.rectTransform.sizeDelta.y);
            }
        });
        okBtn.onClick.AddListener(() => {
            SendMessage("OnOkBtnClick",new object[] {headImage.sprite,nameInput.text });
        });
        closeHeadBtn.onClick.AddListener(() => {
            SendMessage("OnCloseHeadBtnClick");
            BoardManager.Instance.SetHeadImage(headImage);
        });

        //InitUI
        InitUI();

        //Animation
        Tweens.UIFade(lm.window,true,1f);
    }
    void InitUI()
    {
        nameInput.text = BoardManager.Instance.user.name;
        BoardManager.Instance.SetHeadImage(headImage);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (lm.headPanel.activeInHierarchy)
            {
                //ok
                SendMessage("OnOkBtnClick", new object[] { headImage.sprite, nameInput.text });
            }
            if (lm.joinPanel.activeInHierarchy)
            {
                //enter
                SendMessage("OnEnterBtnClick", idInput.text);
            }
        }
    }
}
