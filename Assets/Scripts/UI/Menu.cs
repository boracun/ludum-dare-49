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
            GameObject buttonTemplate = transform.GetChild(0).gameObject;
            GameObject buttonToCreate;

            for (int i = 0; i < menuItems.Count; i++)
            {
                buttonToCreate = Instantiate(buttonTemplate, transform);
                buttonToCreate.transform.GetChild(0).GetComponent<Text>().text = menuItems[i].menuItemName;
                buttonToCreate.transform.GetChild(1).GetComponent<Image>().sprite = menuItems[i].menuItemSprite;
                
                buttonToCreate.GetComponent<Button>().AddEventListener(i, ItemClicked);
            }
            
            Destroy(buttonTemplate);
        }

        void ItemClicked(int i)
        {
            Debug.Log("New Order: " + menuItems[i].menuItemName);
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
