using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class User
{
    public string id;
    public string name;
    public bool canDraw;
    public bool isRoomer;

    public User()
    {

    }

    public void SetInfo()
    {
        this.id = Utils.CreateRandomId(6);
        name = id;
        canDraw = true;
        isRoomer = false;
    }
}
