using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour {
    public Transform UIRoot;
    public Transform ModelRoot;
    public Camera MainCamera;
    public GameObject kbeMain;
    public EasyTouch easyTouch;
    public static RootManager Instance;
    void Awake()
    {
        Instance = this;
    }
}
