using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu Item", menuName = "MenuItem")]
[Serializable]
public class MenuItem : ScriptableObject
{
    public string menuItemName;
    
    public Sprite menuItemSprite;

    public MenuItem Clone()
    {
        MenuItem clone = CreateInstance<MenuItem>();
        clone.menuItemName = menuItemName;
        clone.menuItemSprite = menuItemSprite;

        return clone;
    }
}
