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
                orderPanel.gameObject.SetActive(true);
                pickUpPanel.gameObject.SetActive(true);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.tag.Equals("Waiter"))
            {
                orderPanel.gameObject.SetActive(false);
                pickUpPanel.gameObject.SetActive(false);
            }
        }
    }
}
