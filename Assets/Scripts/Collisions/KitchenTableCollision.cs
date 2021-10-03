using System;
using UnityEngine;

namespace Collisions
{
    public class KitchenTableCollision : MonoBehaviour
    {
        public GameObject orderPanel;
        public GameObject pickUpPanel;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag.Equals("Waiter"))
            {
                orderPanel.gameObject.GetComponent<CanvasGroup>().alpha = 1;
                orderPanel.gameObject.GetComponent<CanvasGroup>().interactable = true;
                pickUpPanel.gameObject.GetComponent<CanvasGroup>().alpha = 1;
                pickUpPanel.gameObject.GetComponent<CanvasGroup>().interactable = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.tag.Equals("Waiter"))
            {
                orderPanel.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                orderPanel.gameObject.GetComponent<CanvasGroup>().interactable = false;
                pickUpPanel.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                pickUpPanel.gameObject.GetComponent<CanvasGroup>().interactable = false;
            }
        }
    }
}
