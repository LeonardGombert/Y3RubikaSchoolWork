using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public float min;
    public float max;

    public Drawer(float _min, float _max)
    {
        min = _min;
        max = _max;
    }
}
