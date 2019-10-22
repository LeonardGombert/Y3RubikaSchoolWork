using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum IngredientUnits { Spoon, Cup, Bowl, Pieces}

[Serializable]
public class Ingredients
{
    public string name;
    public int amount;
    public IngredientUnits units;
}

public class Receipe : MonoBehaviour
{
    public Ingredients portionResult;
    public Ingredients[] portionIngredients;


}
