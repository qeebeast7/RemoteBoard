using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainManager : MonoBehaviour
{
    public Board board;

    public GameObject setPanel;
    public GameObject exitPanel;

    public GameObject sheetPanel;
    public GameObject sheet;
    public GameObject addSheet;

    public GameObject userPanel;
    public GameObject user;

    public GameObject userAction;

    public GameObject toolPanel;

    public GameObject panels;
    public GameObject drawPanel;
    public GameObject shapePanel;
    public GameObject textPanel;
    public GameObject erasePanel;

    private UserUI tmpUser;

    void Awake()
    {
        board = FindObjectOfType<Board>();

        setPanel = Utils.FindObj(transform, "SetPanel");
        exitPanel = Utils.FindObj(transform, "ExitPanel");
        sheetPanel = Utils.FindObj(transform, "SheetPanel");
        sheet = Utils.FindObj(sheetPanel.transform, "Sheet");
        addSheet = Utils.FindObj(sheetPanel.transform, "AddBtn");
        userPanel = Utils.FindObj(transform, "UserPanel");
        user = Utils.FindObj(userPanel.transform, "User");
        userAction = Utils.FindObj(transform, "UserAction");
        toolPanel= Utils.FindObj(transform, "ToolPanel");

        panels = Utils.FindObj(transform, "Panels");
        drawPanel= Utils.FindObj(panels.transform, "DrawPanel");
        shapePanel = Utils.FindObj(panels.transform, "ShapePanel");
        textPanel = Utils.FindObj(panels.transform, "TextPanel");
        erasePanel = Utils.FindObj(panels.transform, "ErasePanel");

        if (sheet.GetComponent<Sheet>() == null)
        {
            sheet.AddComponent<Sheet>();
        }
        if (user.GetComponent<UserUI>() == null)
        {
            BoardManager.Instance.uui = user.AddComponent<UserUI>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BoardManager.Instance.sheets.Add(sheet);
        BoardManager.Instance.users.Add(user);

        int sheetCount = BoardManager.Instance.sheets.Count;
        int userCount = BoardManager.Instance.users.Count;

        InitSheets(sheetCount);
        InitUsers(userCount);

        setPanel.SetActive(false);
        exitPanel.SetActive(false);
        userAction.SetActive(false);

        foreach (Transform child in panels.transform)
        {
            child.gameObject.SetActive(false);
        }
        panels.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //If rightclick on User, pop UserAction
        if (Input.GetMouseButtonDown(1))
        {
            if (BoardManager.Instance.user.isRoomer)
            {
                if (Utils.RaycastUI(Input.mousePosition).GetComponentInParent<UserUI>() != null)
                {
                    tmpUser = Utils.RaycastUI(Input.mousePosition).GetComponentInParent<UserUI>();
                    if (tmpUser != BoardManager.Instance.uui)
                    {
                        userAction.GetComponent<RectTransform>().position = new Vector2(
                    tmpUser.GetComponent<RectTransform>().position.x -
                    userAction.GetComponent<RectTransform>().sizeDelta.x / 2,
                    tmpUser.GetComponent<RectTransform>().position.y -
                    userAction.GetComponent<RectTransform>().sizeDelta.y / 2);
                        SendMessage("UpdateUserAction", tmpUser.user.canDraw);
                        StartCoroutine(Tweens.EUIFade(userAction, true, 0.1f));
                        //userAction.SetActive(true);
                    }
                    else
                    {
                        tmpUser = null;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (userAction.activeInHierarchy)
            {
                if (Utils.RaycastUI(Input.mousePosition) != null)
                {
                    if (Utils.FindObj(userAction.transform, Utils.RaycastUI(Input.mousePosition).name) == null)
                    {
                        StartCoroutine(Tweens.EUIFade(userAction, false, 0.1f));
                        //userAction.SetActive(false);
                        tmpUser = null;
                    }
                }
            }

            if (panels.activeInHierarchy)
            {
                if (Utils.RaycastUI(Input.mousePosition).GetComponentInParent<Board>() != null)
                {
                    foreach (Transform child in panels.transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                    panels.SetActive(false);
                }
            }
        }
    }

    void InitSheets(int n)
    {
        for (int i = 0; i < n; i++)
        {
            if (i == 0)
            {

            }
            else
            {
                AddSheet(Application.streamingAssetsPath + "\\" + "head.png");
            }
        }
    }
    void InitUsers(int n)
    {
        for (int i = 0; i < n; i++)
        {
            if (i == 0)
            {
                BoardManager.Instance.uui.SetAll();
            }
            else
            {
                AddUser();
            }
        }
    }

    void OnSetBtnClick()
    {
        StartCoroutine(Tweens.EUIFade(setPanel, true, 0.1f));
    }

    void OnExitBtnClick()
    {
        StartCoroutine(Tweens.EUIFade(exitPanel, true, 0.1f));
    }

    void OnPublicTogChanged(bool value)
    {

    }
    void OnMuteTogChanged(bool value)
    {

    }
    void OnCloseSetBtnClick()
    {
        StartCoroutine(Tweens.EUIFade(setPanel, false, 0.1f));
    }

    void OnOkBtnClick()
    {
        BoardManager.Instance.sheets = new List<GameObject>();
        BoardManager.Instance.users = new List<GameObject>();
        SceneManager.LoadSceneAsync("Scenes/Login");
    }
    void OnCancelBtnClick()
    {
        StartCoroutine(Tweens.EUIFade(exitPanel, false, 0.1f));
    }

    void OnAddSheetBtnClick()
    {
        //create a sheet
        Transform parent = sheet.transform.parent;
        GameObject newSheet = Instantiate(sheet, parent);
        BoardManager.Instance.sheets.Add(newSheet);
        addSheet.transform.SetAsLastSibling();
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x,
            (BoardManager.Instance.sheets.Count + 1) * 100);
    }

    void AddSheet(string imgPath)
    {
        //create a sheet
        Transform parent = sheet.transform.parent;
        GameObject newSheet = Instantiate(sheet, parent);
        newSheet.GetComponent<Sheet>().SetImage(imgPath);
        BoardManager.Instance.sheets.Add(newSheet);
        addSheet.transform.SetAsLastSibling();
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x,
            (BoardManager.Instance.sheets.Count + 1) * 100);
    }

    void AddUser()
    {
        //create a user
        Transform parent = user.transform.parent;
        GameObject newUser = Instantiate(user, parent);
        newUser.GetComponent<UserUI>().SetAll();
        BoardManager.Instance.users.Add(newUser);
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x,
            (BoardManager.Instance.users.Count + 1) * 100);
    }

    void OnAllowTogChanged(bool value)
    {
        if (tmpUser != null)
        {
            BoardManager.Instance.uui.AllowDraw(tmpUser, value);
        }
        StartCoroutine(Tweens.EUIFade(userAction, false, 0.1f));
    }
    void OnKickBtnClick()
    {
        if (tmpUser != null)
        {

        }
        StartCoroutine(Tweens.EUIFade(userAction, false, 0.1f));
    }
    void OnPassBtnClick()
    {
        if (tmpUser != null)
        {
            BoardManager.Instance.uui.PassRoomer(tmpUser);
        }
        StartCoroutine(Tweens.EUIFade(userAction, false, 0.1f));
    }

    void OnToolBtnClick(string name)
    {
        switch (name)
        {
            case "Select":
                break;
            case "Draw":
            case "Shape":
            case "Text":
            case "Erase":
                panels.SetActive(true);
                foreach (Transform child in panels.transform)
                {
                    if (child.gameObject.name.Contains(name))
                    {
                        child.gameObject.SetActive(!child.gameObject.activeInHierarchy);
                    }
                    else
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                break;
            case "Redo":
                board.Redo();
                break;
            case "Undo":
                board.Undo();
                break;
            case "Clear":
                board.Clear();
                break;
            case "DL":
                board.Download();
                break;
            case "UL":
                board.Upload();
                break;
            default:
                break;
        }

        board.boardState = Utils.String2Enum<Board.BoardState>(name);
        Debug.Log(board.boardState);
    }

    void OnColorTogChanged(Color color)
    {
        board.SetColor(color);
    }

    void OnSizeTogChanged(int size)
    {
        board.SetSize(size);
    }

    void OnShapeTogChanged(string shape)
    {
        board.SetShape(shape);
    }
}
