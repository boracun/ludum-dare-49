using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    public static Kitchen Instance;
    private Queue<MenuItem> orderedItems;
    private MenuItem currentlyPreparedItem;
    private List<MenuItem> readyItems;

    void Awake()
    {
        Instance = this;
        orderedItems = new Queue<MenuItem>();
        readyItems = new List<MenuItem>();
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
        currentlyPreparedItem = null;
    }
}
