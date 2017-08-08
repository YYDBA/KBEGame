using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class ToolsEditor
{
    [MenuItem("IceDog/Break Prefab Instance",false,1)]
    static void BreakPrefabInstance()
    {
        GameObject obj = Selection.activeGameObject;
        GameObject go = (GameObject)GameObject.Instantiate(obj, obj.transform.position, obj.transform.rotation);
        go.transform.parent = obj.transform.parent;
        go.name = obj.name;
        GameObject.DestroyImmediate(obj);
    }
}
