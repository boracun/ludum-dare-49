using System;
using Movement;
using Objects;
using UnityEngine;

namespace Collisions
{
    public class CustomerWaiterCollision : MonoBehaviour
    {
        /*private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Customer"))
            {
                if (other.collider.gameObject.GetComponent<CustomerMovement>().movementMode !=
                    CustomerMovement.WAIT_MODE)
                {
                    Waiter.Instance.DropItem();
                }
            }
        }*/

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Customer"))
            {
                if (other.gameObject.GetComponent<CustomerMovement>().movementMode !=
                    CustomerMovement.WAIT_MODE)
                {
                    Waiter.Instance.DropItem();
                }
            }
        }
    }
}
