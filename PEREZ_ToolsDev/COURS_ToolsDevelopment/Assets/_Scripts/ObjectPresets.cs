using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public enum Presets { Preset1, Preset2, Preset3, Preset4 }

[ExecuteInEditMode]
public class ObjectPresets : MonoBehaviour
{
    #region //VARIABLE DECLARATIONS
    [SerializeField] Presets currentPreset;
    Presets oldPreset;
    TestPlayerWithVar thisObject;
    GameObject objectToScan;

    //Base Integers, can be redefined by the player
    List<int> BaseInts;
    List<float> BaseFloats;
    List<GameObject> BaseGameObjects;

    //The Various lists present in the presets, new lists can be added
    List<int> Preset1Ints;
    List<float> Preset1Floats;
    List<GameObject> Preset1GameObjects;

    List<int> Preset2Ints;
    List<float> Preset2Floats;
    List<GameObject> Preset2GameObjects;

    List<int> Preset3Ints;
    List<float> Preset3Floats;
    List<GameObject> Preset3GameObjects;

    List<int> Preset4Ints;
    List<float> Preset4Floats;
    List<GameObject> Preset4GameObjects;
    #endregion

    #region //MONOBEHAVIOR CALLBACKS
    void Awake()
    {
        AssignBaseVariables();
    }

    void Update()
    {
        if (currentPreset != oldPreset)
        {
            oldPreset = currentPreset;
            LoadCurrentPresetValues();
        }
        GetTargetObjectValues();
    }
    #endregion

    #region //CUSTOM FUNCTIONS
    public void AssignBaseVariables()
    {
        objectToScan = this.gameObject;
        thisObject = objectToScan.gameObject.GetComponent<TestPlayerWithVar>();
    }

    public void LoadCurrentPresetValues()
    {
        switch (currentPreset)
        {
            case Presets.Preset1:
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().health = Preset1Ints[0];
                //objectToScan.gameObject.GetComponent<TestPlayerWithVar>().othervariable1 = Preset2Ints[1];
                //objectToScan.gameObject.GetComponent<TestPlayerWithVar>().othervariable2 = Preset2Ints[2];
                //objectToScan.gameObject.GetComponent<TestPlayerWithVar>().othervariable3 = Preset2Ints[3];                
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().speed = Preset1Floats[0];
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().weapon = Preset1GameObjects[0];
                break;

            case Presets.Preset2:
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().health = Preset2Ints[0];
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().speed = Preset2Floats[0];
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().weapon = Preset2GameObjects[0];
                break;

            case Presets.Preset3:
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().health = Preset3Ints[0];
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().speed = Preset3Floats[0];
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().weapon = Preset3GameObjects[0];
                break;

            case Presets.Preset4:
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().health = Preset4Ints[0];
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().speed = Preset4Floats[0];
                objectToScan.gameObject.GetComponent<TestPlayerWithVar>().weapon = Preset4GameObjects[0];
                break;
        }
    }

    public void GetTargetObjectValues()
    {
        switch (currentPreset)
        {
            case Presets.Preset1:
                Preset1Ints.Clear();
                Preset1Ints.Add(thisObject.health);

                Preset1Floats.Clear();
                Preset1Floats.Add(thisObject.speed);

                Preset1GameObjects.Clear();
                Preset1GameObjects.Add(thisObject.weapon);
                break;

            case Presets.Preset2:
                Preset2Ints.Clear();
                Preset2Ints.Add(thisObject.health);

                Preset2Floats.Clear();
                Preset2Floats.Add(thisObject.speed);

                Preset2GameObjects.Clear();
                Preset2GameObjects.Add(thisObject.weapon);
                break;

            case Presets.Preset3:
                Preset3Ints.Clear();
                Preset3Ints.Add(thisObject.health);

                Preset3Floats.Clear();
                Preset3Floats.Add(thisObject.speed);

                Preset3GameObjects.Clear();
                Preset3GameObjects.Add(thisObject.weapon);
                break;

            case Presets.Preset4:
                Preset4Ints.Clear();
                Preset4Ints.Add(thisObject.health);

                Preset4Floats.Clear();
                Preset4Floats.Add(thisObject.speed);

                Preset4GameObjects.Clear();
                Preset4GameObjects.Add(thisObject.weapon);
                break;
        }
    }
    #endregion

    #region //EDITOR DRAWER FUNCTIONS
    public void DefineAsBase()
    {
        BaseInts.Add(thisObject.health);
        BaseFloats.Add(thisObject.speed);
        BaseGameObjects.Add(thisObject.weapon);
    }

    public void ResetPresets()
    {
        objectToScan.gameObject.GetComponent<TestPlayerWithVar>().health = BaseInts[0];
        objectToScan.gameObject.GetComponent<TestPlayerWithVar>().speed = BaseFloats[0];
        objectToScan.gameObject.GetComponent<TestPlayerWithVar>().weapon = BaseGameObjects[0];
    }
    #endregion
}