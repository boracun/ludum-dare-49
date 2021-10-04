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
            GameObject[] customers = gameObject.GetComponent<Table>().customers;
            for (int i = 0; i < customers.Length; i++)
            {
                if(customers[i] != null)
                {
                    Customer customer = customers[i].GetComponent<Customer>();
                    CustomerMovement customerMovement = customers[i].GetComponent<CustomerMovement>();
                    if (customerMovement.movementMode == CustomerMovement.WAIT_MODE && customer.order != null)
                    {
                        customer.hasOrdered = true;

                        customer.orderBubble.SetActive(true);

                        if (waiter.heldItem != null &&
                            customer.order.menuItemName.Equals(waiter.heldItem.menuItemName))
                        {
                            customer.hasOrderDelivered = true;
                            waiter.DropItem();
                        }
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Waiter"))
        {
            GameObject[] customers = gameObject.GetComponent<Table>().customers;
            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i] != null)
                {
                    Customer customer = customers[i].GetComponent<Customer>();
                    customer.orderBubble.SetActive(false);
                }
            }
        }
    }
}
