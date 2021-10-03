using System;
using UnityEngine;

namespace Objects
{
    public class Waiter : MonoBehaviour
    {
        public MenuItem heldItem = null;
        public GameObject heldItemBubble;
        public static Waiter Instance;

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if(heldItem != null)
                heldItemBubble.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = heldItem.menuItemSprite;
        }

        public void PickUpItem(MenuItem item)
        {
            heldItem = item;
            heldItemBubble.SetActive(true);
        }

        public MenuItem DropItem()
        {
            MenuItem droppedItem = heldItem;
            heldItem = null;
            heldItemBubble.SetActive(false);
            return droppedItem;
        }

        public bool HoldsItem()
        {
            return (heldItem != null);
        }
    }
}
