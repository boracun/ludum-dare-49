using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Table : MonoBehaviour
    {
        public int tableNo;
        
        public int maxCount;

        public List<GameObject> customers = new List<GameObject>();
    }
}
