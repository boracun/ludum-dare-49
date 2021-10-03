using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    public static Kitchen Instance;
    private Queue<MenuItem> orderedItems;
    private MenuItem currentlyPreparedItem;
    public List<MenuItem> readyItems;

    void Awake()
    {
        Instance = this;
        orderedItems = new Queue<MenuItem>();
        readyItems = new List<MenuItem>();        //TODO commented for testing, remove comments n the final product
    }

    private void Update()
    {
        if (currentlyPreparedItem == null && orderedItems.Count > 0)
        {
            currentlyPreparedItem = orderedItems.Dequeue();
            PrepareItem(currentlyPreparedItem);
        }
    }

    public static void OrderItem(MenuItem itemToOrder)
    {
        Instance.orderedItems.Enqueue(itemToOrder);
    }

    private void PrepareItem(MenuItem itemToPrepare)
    {
        StartCoroutine(PrepareItemCoroutine(itemToPrepare));
    }

    private IEnumerator PrepareItemCoroutine(MenuItem itemToPrepare)
    {
        yield return new WaitForSeconds(itemToPrepare.preparationTime);
        readyItems.Add(currentlyPreparedItem);
        PickUpMenu pickUpMenu = FindObjectOfType<PickUpMenu>();
        pickUpMenu.ItemPrepared(itemToPrepare);
        currentlyPreparedItem = null;
    }
}
