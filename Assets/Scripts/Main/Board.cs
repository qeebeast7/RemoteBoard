using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    private RawImage board;

    public enum BoardState
    {
        Empty,
        Select,
        Draw,
        Shape,
        Text,
        Erase,
        Redo,
        Undo,
        Clear,
        DL,
        UL
    }

    public BoardState boardState;

    private Color color;
    private int size;
    private BasicShape.BShape shape;

    #region DrawProperty
    [SerializeField]
    private Material paintMat;
    [SerializeField]
    private Material eraseMat;
    [SerializeField]
    private Material clearMat;
    [SerializeField]
    private Texture brushTex;

    private RenderTexture renderTexture;
    private int canvasWidth;
    private int canvasHeight;

    private Vector2 lastPoint;
    private float lerpSize;
    #endregion DrawProperty


    private Stack<Shape> shapeStack;

    // Start is called before the first frame update
    void Start()
    {
        board = GetComponent<RawImage>();
        boardState = BoardState.Draw;

        color = Color.red;
        size = 100;
        shape = BasicShape.BShape.Circle;

        //DrawInit
        RectTransform rect = GetComponent<RectTransform>();
        canvasWidth = (int)rect.sizeDelta.x;
        canvasHeight = (int)rect.sizeDelta.y;
        canvasWidth = Screen.width;
        canvasHeight= Screen.height;

        lastPoint = Vector2.zero;
        lerpSize = (brushTex.width + brushTex.height) / 2f / size;

        shapeStack = new Stack<Shape>();

        //DrawInit
        InitCanvas();
        SetDrawBrush();

    }

    void InitCanvas()
    {

        renderTexture = RenderTexture.GetTemporary(canvasWidth, canvasHeight, 24);
        board.texture = renderTexture;
        Graphics.Blit(null, renderTexture, clearMat);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (true)
            {
                switch (boardState)
                {
                    case BoardState.Empty:
                        break;
                    case BoardState.Select:
                        break;
                    case BoardState.Draw:
                        LerpPaint(Input.mousePosition);
                        break;
                    case BoardState.Shape:
                        break;
                    case BoardState.Text:
                        break;
                    case BoardState.Erase:
                        break;
                    case BoardState.Redo:
                        break;
                    case BoardState.Undo:
                        break;
                    case BoardState.Clear:
                        break;
                    case BoardState.DL:
                        break;
                    case BoardState.UL:
                        break;
                    default:
                        break;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            switch (boardState)
            {
                case BoardState.Empty:
                    break;
                case BoardState.Select:
                    break;
                case BoardState.Draw:
                    lastPoint = Vector2.zero;
                    PenShape penShape = new PenShape();
                    shapeStack.Push(penShape);
                    break;
                case BoardState.Shape:
                    break;
                case BoardState.Text:
                    break;
                case BoardState.Erase:
                    break;
                case BoardState.Redo:
                    break;
                case BoardState.Undo:
                    break;
                case BoardState.Clear:
                    break;
                case BoardState.DL:
                    break;
                case BoardState.UL:
                    break;
                default:
                    break;
            }
            
        }
    }

    public void Redo()
    {

    }

    public void Undo()
    {

    }

    public void Clear()
    {

    }

    public void Download()
    {

    }

    public void Upload()
    {

    }

    public void SetSize(int size)
    {
        this.size = size;
        switch (boardState)
        {
            case BoardState.Draw:
                SetDrawBrush();
                break;
            case BoardState.Shape:
                break;
            case BoardState.Text:
                break;
            case BoardState.Erase:
                SetEraseBrush();
                break;
        }
    }

    public void SetColor(Color color)
    {
        this.color = color;
        switch (boardState)
        {
            case BoardState.Draw:
                SetDrawBrush();
                break;
            case BoardState.Shape:
                break;
            case BoardState.Text:
                break;
        }
    }

    public void SetShape(string shapeName)
    {
        this.shape = Utils.String2Enum<BasicShape.BShape>(shapeName);
    }

    public void SetProperty(int size)
    {
        this.size = size;
    }

    public void SetProperty(int size, Color color)
    {
        this.size = size;
        this.color = color;
    }

    public void SetProperty(int size, Color color, string shapeName)
    {
        this.size = size;
        this.color = color;
        this.shape = Utils.String2Enum<BasicShape.BShape>(shapeName);
    }

    //Draw
    void SetDrawBrush()
    {
        paintMat.SetColor("_Color", this.color);
        paintMat.SetFloat("_Size", this.size);
        paintMat.SetTexture("_BrushTex", brushTex);
    }

    void LerpPaint(Vector2 point)
    {
        PaintPoint(point);

        if (lastPoint == Vector2.zero)
        {
            lastPoint = point;
        }

        float dis = Vector2.Distance(lastPoint, point);
        if (dis > lerpSize)
        {
            Vector2 dir = (point - lastPoint).normalized;
            int count = (int)(dis / lerpSize);
            for (int i = 1; i <= count; i++)
            {
                Vector2 lerpPoint = lastPoint + i * lerpSize * dir;
                PaintPoint(lerpPoint);
            }
        }
        lastPoint = point;
    }

    void PaintPoint(Vector2 point)
    {
        Vector2 uv = new Vector2(point.x/(float)canvasWidth, point.y / (float)canvasHeight);
        paintMat.SetVector("_UV",uv);
        Graphics.Blit(renderTexture,renderTexture,paintMat);
    }

    //Erase
    void SetEraseBrush()
    {
        clearMat.SetFloat("_Size", this.size);
        clearMat.SetTexture("_BrushTex", brushTex);
    }
}
