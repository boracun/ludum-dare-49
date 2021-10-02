using UnityEngine;

[CreateAssetMenu(fileName = "Menu Item", menuName = "MenuItem")]
public class MenuItem : ScriptableObject
{
    public string menuItemName;
    
    public Sprite menuItemSprite;
}
