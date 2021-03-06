using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    //Attach this script to the parent object of the buttons
    public class PickUpMenu : MonoBehaviour
    {
        public GameObject buttonPrefab;
        
        // Start is called before the first frame update
        void Start()
        {
            foreach (MenuItem item in Kitchen.Instance.readyItems)
            {
                ItemPrepared(item);
            }
        }

        private void ItemPickedUp(MenuItem menuItem)
        {
            GameObject buttonToDelete = null;

            if (Waiter.Instance.HoldsItem())
            {
                ItemPrepared(Waiter.Instance.DropItem());
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                buttonToDelete = transform.GetChild(i).gameObject;
                string itemName = buttonToDelete.transform.GetChild(0)?.GetComponent<Text>().text;
                
                if (itemName.Equals(menuItem.menuItemName))
                {
                    buttonToDelete = transform.GetChild(i).gameObject;
                    break;
                }

                buttonToDelete = null;
            }

            if (buttonToDelete != null)
            {
                Destroy(buttonToDelete);
            }
            
            Waiter.Instance.PickUpItem(menuItem);
        }

        public void ItemPrepared(MenuItem menuItem)
        {
            GameObject buttonToCreate = Instantiate(buttonPrefab, transform);
            buttonToCreate.transform.GetChild(0).GetComponent<Text>().text = menuItem.menuItemName;
            buttonToCreate.transform.GetChild(1).GetComponent<Image>().sprite = menuItem.menuItemSprite;
                
            buttonToCreate.GetComponent<Button>().AddEventListener(menuItem, ItemPickedUp);
        }
    }
}
