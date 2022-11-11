using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public int size;

    public Color color;

    public Vector2 leftPos;

    public Vector2 rightPos;

    public Shape()
    {

    }

    public Shape(int size,Color color,Vector2 leftPos,Vector2 rightPos)
    {
        this.size = size;
        this.color = color;
        this.leftPos = leftPos;
        this.rightPos = rightPos;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
