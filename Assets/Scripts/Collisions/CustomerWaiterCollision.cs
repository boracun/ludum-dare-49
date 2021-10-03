using System;
using Movement;
using Objects;
using UnityEngine;

namespace Collisions
{
    public class CustomerWaiterCollision : MonoBehaviour
    {
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
