using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Table : MonoBehaviour
    {
        public int tableNo;
        
        public int maxCount;

        public int filledSeats = 0;

        public GameObject[] customers;

        private void Start()
        {
            customers = new GameObject[maxCount];
        }

        public void AddCustomer(int index, GameObject customer)
        {
            customers[index] = customer;
            filledSeats++;
        }
        
        public void FillFirstEmptySpace(GameObject customer)
        {
            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i] == null)
                {
                    customers[i] = customer;
                }
            }
            filledSeats++;
        }

        public int FindFirstEmptySpaceIndex()
        {
            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool RemoveCustomer(int index)
        {
            if (index < 0 || index >= maxCount)
            {
                return false;
            }
            customers[index] = null;
            filledSeats--;
            return true;
        }
        
        public bool RemoveCustomer(GameObject customer)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (customers[i] == customer)
                {
                    customers[i] = null;
                    filledSeats--;
                    return true;
                }
            }
            return false;
        }
    }
}
