using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Random = Unity.Mathematics.Random;

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

    private float CalculateMaxSeatNum()
    {
        float result = 0;
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
        Random random = new Random();

        int randomNum = random.NextInt(1, 4);
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
            if (table.customers.Length < table.maxCount)
            {
                emptyTables.Add(gameObject);
            }
        }

        if (emptyTables.Count == 0)
        {
            return null;
        }
        Random random = new Random();
        int randomIndex = random.NextInt(0, emptyTables.Count);
        return emptyTables[randomIndex];
    }

    public float[,] GETTableRoute()
    {
        Table table = GETRandomEmptyTable().GetComponent<Table>();
        if (table == null)
        {
            return null;
        }
        GameObject[] customers = table.customers;
        for (int i = 0; i < customers.Length; i++)
        {
            if (customers[i] == null)
            {
                string key = "Table" + table.tableNo + "Route" + i;
                return TableRouteMap.TableRouteDictionary[key];
            }
        }

        return null;
    }
}
