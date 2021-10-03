using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class FoodCatalog : MonoBehaviour
{
    public List<MenuItem> MenuItems;

    public MenuItem GETRandomMenuItem()
    {
        int randomIndex = (int)Math.Floor(UnityEngine.Random.Range(0, MenuItems.Count - 0.00001f));
        return MenuItems[randomIndex];
    }
}
