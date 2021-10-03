using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Random = UnityEngine.Random;

public class RouteNode
{
    public int routeIndex;
    public float[,] route;
    public GameObject table;
}
public class CustomerManager : MonoBehaviour
{
    public float customerSpawnTimer = 0f;
    
    public float customerSpawnTimeLimit = 20f;

    public GameObject customer1Prefab;
    
    public GameObject customer2Prefab;

    public GameObject customer3Prefab;

    public GameObject customer4Prefab;


    public Transform entranceTransform;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Customer").Length < CalculateMaxSeatNum())
        {
            customerSpawnTimer += Time.deltaTime;
            if (customerSpawnTimeLimit < customerSpawnTimer)
            {
                customerSpawnTimer = 0f;
                spawnCustomer(GETRandomCustomer() ,entranceTransform);
            }
        }
    }

    private int CalculateMaxSeatNum()
    {
        int result = 0;
        GameObject[] tables = GameObject.FindGameObjectsWithTag("Table");
        for (int i = 0; i < tables.Length; i++)
        {
            result += tables[i].GetComponent<Table>().maxCount;
        }
        return result;
    }

    public void spawnCustomer(GameObject customerPrefab, Transform spawnLocation)
    {
        var position = spawnLocation.position;
        GameObject.Instantiate(
            customerPrefab,
            new Vector3(position.x, position.y, position.z),
            Quaternion.identity
        );
    }

    public GameObject GETRandomCustomer()
    {

        int randomNum = (int)Math.Floor(Random.Range(1f, 4.99999f));
        switch (randomNum)
        {
            case 1:
                return customer1Prefab;
            case 2:
                return customer2Prefab;
            case 3:
                return customer3Prefab;
            case 4:
                return customer4Prefab;
        }

        return customer1Prefab;
    }

    private GameObject GETRandomEmptyTable()
    {
        GameObject[] allTables = GameObject.FindGameObjectsWithTag("Table");
        List<GameObject> emptyTables = new List<GameObject>();
        foreach (var gObject in allTables)
        {
            Table table = gObject.GetComponent<Table>();
            if (table.filledSeats < table.maxCount)
            {
                emptyTables.Add(gObject);
            }
        }
        if (emptyTables.Count == 0)
        {
            return null;
        }
        int randomIndex = (int)Math.Floor(Random.Range(0f, emptyTables.Count - 0.000001f));
        return emptyTables[randomIndex];
    }

    public RouteNode GETTableRoute()
    {
        GameObject tableObj = GETRandomEmptyTable();
        if (tableObj == null)
        {
            return null;
        }
        Table table = tableObj.GetComponent<Table>();
        RouteNode routeNode = new RouteNode();
        routeNode.table = tableObj;

        if (table.filledSeats >= table.maxCount)
        {
            return null;
        }
        string key = "Table" + table.tableNo + "Route" + (table.FindFirstEmptySpaceIndex() + 1);
        routeNode.route = TableRouteMap.TableRouteDictionary[key];
        routeNode.routeIndex = table.FindFirstEmptySpaceIndex();
        return routeNode;
    }
}
