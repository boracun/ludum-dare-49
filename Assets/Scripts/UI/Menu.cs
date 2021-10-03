using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private List<MenuItem> menuItems;
        
        // Start is called before the first frame update
        void Start()
        {
            GameObject buttonTemplate = transform.GetChild(1).gameObject;
            GameObject buttonToCreate;

            for (int i = 0; i < menuItems.Count; i++)
            {
                buttonToCreate = Instantiate(buttonTemplate, transform);
                buttonToCreate.transform.GetChild(0).GetComponent<Text>().text =
                    menuItems[i].menuItemName + " (" + menuItems[i].preparationTime + "s)";
                buttonToCreate.transform.GetChild(1).GetComponent<Image>().sprite = menuItems[i].menuItemSprite;
                
                buttonToCreate.GetComponent<Button>().AddEventListener(menuItems[i], Kitchen.OrderItem);
            }
            
            Destroy(buttonTemplate);
        }
    }

    public static class ButtonExtension
    {
        public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
        {
            button.onClick.AddListener(delegate
            {
                OnClick(param);
            });
        }
    }
}
