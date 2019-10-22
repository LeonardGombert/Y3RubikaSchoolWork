using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ToolSet : MonoBehaviour
{
    [MenuItem("TestMenu/Print Log")] //create a new menu with a "function"
    static void PrintLog()
    {
        Debug.Log("Berdup");
    }

    [MenuItem("TestMenu/Print Freedom #%&L")] //allows you to use shift+ctrl+alt+L to use
    static void Freedom()
    {
        Debug.Log("Super Illegal Yeet Stick");
    }

    [MenuItem("TestMenu/Scene Info")]
    static void ReadScene()
    {
        GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject item in objects)
        {
            Debug.Log(item.name);
        }
    }

    [MenuItem("TestMenu/Log Selected Transform Name #%&T")]
    static void LogSelectedTransform()
    {
        if (Selection.activeTransform != null)
            Debug.Log("Selected transform is = " + Selection.activeTransform.gameObject.transform.position);
        else
            Debug.Log("There is no active selection");
    }

    [MenuItem("TestMenu/Toggle GameObject Status #&D")]
    static void ToggleGameObjectsStatus()
    {
        foreach (GameObject item in Selection.gameObjects)
        {
            if(item.active) item.SetActive(false);

           else item.SetActive(true);
        }
    }

    [MenuItem("CONTEXT/Rigidbody/Triple Mass")] //integrate a new functionality in rigibody components
    static void TripleRbMass(MenuCommand command)
    {
        Rigidbody body = (Rigidbody)command.context; //this command sends the context object to the function
        body.mass *= 3;
    }

    [MenuItem("GameObject/MyTestCategory/Custom GameObject", false, 10)]
    static void CustomGameObject(MenuCommand command)
    {
        GameObject gO = new GameObject("Custom GameObject");
        Undo.RegisterCreatedObjectUndo(gO, "Create" + gO); //stores information about object to allow you to undo changes
        Selection.activeGameObject = gO;
    }
}
