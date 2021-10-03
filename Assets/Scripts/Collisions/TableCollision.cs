using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using Objects;
using UnityEngine;

public class TableCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Waiter"))
        {
            Waiter waiter = Waiter.Instance;
            List<GameObject> customers = gameObject.GetComponent<Table>().customers;
            for (int i = 0; i < customers.Count; i++)
            {
                Customer customer = customers[i].GetComponent<Customer>();
                CustomerMovement customerMovement = customers[i].GetComponent<CustomerMovement>();
                if (customerMovement.movementMode == CustomerMovement.WAIT_MODE && customer.order != null)
                {
                    customer.hasOrdered = true;
                    if (waiter.heldItem != null &&
                        customer.order.menuItemName.Equals(waiter.heldItem.menuItemName) )
                    {
                        customer.hasOrderDelivered = true;
                        waiter.DropItem();
                    }
                }
            }
        }
    }
}
