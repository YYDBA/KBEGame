using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour {
    public Transform UIRoot;
    public Camera UICamera;
    public static RootManager Instance;
    void Awake()
    {
        Instance = this;
    }
}
