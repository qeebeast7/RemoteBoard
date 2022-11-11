using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBefore : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.AddComponent<BoardManager>();
    }
}
