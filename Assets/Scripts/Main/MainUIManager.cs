using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    private MainManager mm;

    private Text idText;
    private Image boardImage;
    private Button setBtn;
    private Button exitBtn;

    //SetPanel
    private Toggle publicTog;
    private Toggle muteTog;
    private Button closeSetBtn;

    //ExitPanel
    private Button okBtn;
    private Button cancelBtn;

    //SheetPanel
    private Image[] sheetImages;
    private Toggle[] sheetTogs;
    private Button addSheetBtn;

    //UserPanel

    //UserAction
    private Toggle allowTog;
    private Button kickBtn;
    private Button passBtn;

    //ToolPanel
    private Button[] toolBtns;

    //DrawPanel
    private Toggle[] drawColorTogs;
    private Toggle[] drawSizeTogs;

    //ShapePanel
    private Toggle[] shapeShapeTogs;
    private Toggle[] shapeColorTogs;
    private Toggle[] shapeSizeTogs;

    //TextPanel
    private Toggle[] textColorTogs;
    private InputField textSizeInput;

    //ErasePanel
    private Toggle[] eraseSizeTogs;

    void Awake()
    {
        if (GetComponent<MainManager>() == null)
        {
            mm = gameObject.AddComponent<MainManager>();
        }
        else
        {
            mm = gameObject.GetComponent<MainManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Find
        idText = Utils.FindObj(transform, "ID").GetComponent<Text>();
        boardImage = Utils.FindObj(transform, "Board").GetComponent<Image>();
        setBtn = Utils.FindObj(transform, "SetBtn").GetComponent<Button>();
        exitBtn = Utils.FindObj(transform, "ExitBtn").GetComponent<Button>();

        //SetPanel
        publicTog = Utils.FindObj(mm.setPanel.transform, "PublicTog").GetComponent<Toggle>();
        muteTog = Utils.FindObj(mm.setPanel.transform, "MuteTog").GetComponent<Toggle>();
        closeSetBtn = Utils.FindObj(mm.setPanel.transform, "CloseBtn").GetComponent<Button>();

        //ExitPanel
        okBtn = Utils.FindObj(mm.exitPanel.transform, "OkBtn").GetComponent<Button>();
        cancelBtn = Utils.FindObj(mm.exitPanel.transform, "CancelBtn").GetComponent<Button>();

        //SheetPanel
        SetSheetUI();
        addSheetBtn = Utils.FindObj(mm.sheetPanel.transform, "AddBtn").GetComponent<Button>();

        //UserPanel

        //UserAction
        allowTog = Utils.FindObj(mm.userAction.transform, "AllowTog").GetComponent<Toggle>();
        kickBtn = Utils.FindObj(mm.userAction.transform, "KickBtn").GetComponent<Button>();
        passBtn = Utils.FindObj(mm.userAction.transform, "PassBtn").GetComponent<Button>();

        //ToolPanel
        toolBtns = mm.toolPanel.GetComponentsInChildren<Button>();

        //DrawPanel
        drawColorTogs= Utils.FindObj(mm.drawPanel.transform, "Colors").GetComponentsInChildren<Toggle>();
        drawSizeTogs = Utils.FindObj(mm.drawPanel.transform, "Sizes").GetComponentsInChildren<Toggle>();

        //ShapePanel
        shapeShapeTogs = Utils.FindObj(mm.shapePanel.transform, "Shapes").GetComponentsInChildren<Toggle>();
        shapeColorTogs = Utils.FindObj(mm.shapePanel.transform, "Colors").GetComponentsInChildren<Toggle>(); 
        shapeSizeTogs = Utils.FindObj(mm.shapePanel.transform, "Sizes").GetComponentsInChildren<Toggle>();

        //TextPanel
        textColorTogs = Utils.FindObj(mm.textPanel.transform, "Colors").GetComponentsInChildren<Toggle>();
        textSizeInput = mm.textPanel.GetComponentInChildren<InputField>();

        //ErasePanel
        eraseSizeTogs=Utils.FindObj(mm.erasePanel.transform, "Sizes").GetComponentsInChildren<Toggle>();


        //Bind
        setBtn.onClick.AddListener(() =>
        {
            SendMessage("OnSetBtnClick");
        });
        exitBtn.onClick.AddListener(() =>
        {
            SendMessage("OnExitBtnClick");
        });

        //SetPanel
        publicTog.onValueChanged.AddListener((bool value) =>
        {
            SendMessage("OnPublicTogChanged", value);
        });
        muteTog.onValueChanged.AddListener((bool value) =>
        {
            SendMessage("OnMuteTogChanged", value);
        });
        closeSetBtn.onClick.AddListener(() =>
        {
            SendMessage("OnCloseSetBtnClick");
        });

        //ExitPanel
        okBtn.onClick.AddListener(() =>
        {
            SendMessage("OnOkBtnClick");
        });
        cancelBtn.onClick.AddListener(() =>
        {
            SendMessage("OnCancelBtnClick");
        });

        //SheetPanel
        addSheetBtn.onClick.AddListener(() =>
        {
            SendMessage("OnAddSheetBtnClick");
            SetSheetUI();
        });

        //UserPanel

        //UserAction
        allowTog.onValueChanged.AddListener((bool value) =>
        {
            SendMessage("OnAllowTogChanged", value);
        });
        kickBtn.onClick.AddListener(() =>
        {
            SendMessage("OnKickBtnClick");
        });
        passBtn.onClick.AddListener(() =>
        {
            SendMessage("OnPassBtnClick");
        });

        //ToolPanel
        foreach (Button toolBtn in toolBtns)
        {
            toolBtn.onClick.AddListener(() =>
            {
                SendMessage("OnToolBtnClick", toolBtn.gameObject.name.Substring(0, toolBtn.gameObject.name.Length - 3));
            });
        }

        //DrawPanel
        foreach (Toggle tog in drawColorTogs)
        {
            Color color = tog.GetComponent<Image>().color;
            tog.onValueChanged.AddListener((bool value) =>
            {
                SendMessage("OnColorTogChanged", color);
                foreach (Toggle stog in drawSizeTogs)
                {
                    stog.GetComponent<Image>().color = color;
                }
            });
        }
        foreach (Toggle tog in drawSizeTogs)
        {
            tog.onValueChanged.AddListener((bool value) =>
            {
                SendMessage("OnSizeTogChanged",10*Mathf.Abs((int)tog.GetComponent<RectTransform>().sizeDelta.x));
            });
        }

        //ShapePanel
        foreach (Toggle tog in shapeShapeTogs)
        {
            tog.onValueChanged.AddListener((bool value) =>
            {
                SendMessage("OnShapeTogChanged", tog.gameObject.name);
            });
        }
        foreach (Toggle tog in shapeColorTogs)
        {
            Color color = tog.GetComponent<Image>().color;
            tog.onValueChanged.AddListener((bool value) =>
            {
                SendMessage("OnColorTogChanged", color);
                foreach (Toggle stog in shapeSizeTogs)
                {
                    stog.GetComponent<Image>().color = color;
                }
            });
        }
        foreach (Toggle tog in shapeSizeTogs)
        {
            tog.onValueChanged.AddListener((bool value) =>
            {
                SendMessage("OnSizeTogChanged", Mathf.Abs((int)tog.GetComponent<RectTransform>().sizeDelta.x));
            });
        }

        //TextPanel
        foreach (Toggle tog in textColorTogs)
        {
            Color color = tog.GetComponent<Image>().color;
            tog.onValueChanged.AddListener((bool value) =>
            {
                SendMessage("OnColorTogChanged", color);
            });
        }
        textSizeInput.onValueChanged.AddListener((string text) => {
            SendMessage("OnSizeTogChanged", int.Parse(text));
        });

        //ErasePanel
        foreach (Toggle tog in eraseSizeTogs)
        {
            tog.onValueChanged.AddListener((bool value) =>
            {
                //SendMessage("OnColorTogChanged", Color.white);
                SendMessage("OnSizeTogChanged", Mathf.Abs((int)tog.GetComponent<RectTransform>().sizeDelta.x));
            });
        }

        //InitUI
        InitUI();

        //Animation

    }

    void InitUI()
    {
        idText.text = BoardManager.Instance.boardId;
    }

    void SetSheetUI()
    {
        sheetImages = new Image[BoardManager.Instance.sheets.Count];
        for (int i = 0; i < sheetImages.Length; i++)
        {
            sheetImages[i] = Utils.FindObj(BoardManager.Instance.sheets[i].transform, "Image").GetComponent<Image>();
        }
        sheetTogs = mm.sheetPanel.GetComponentsInChildren<Toggle>();
        sheetTogs[sheetTogs.Length - 1].isOn = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateUserAction(bool value)
    {
        allowTog.isOn = value;
    }
}
