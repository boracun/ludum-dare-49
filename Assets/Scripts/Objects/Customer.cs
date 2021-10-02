using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
    public class Customer : MonoBehaviour
    {
        public const float BASE_ORDER_WAIT_TIME_LIMIT = 20f;
        
        public const float BASE_FOOD_WAIT_TIME_LIMIT = 20f;

        public const float MAX_LIMIT_CHANGE_PRECENTAGE = 40f;

        public bool hasSat;

        public bool hasOrdered;

        public bool hasOrderDelivered;

        public float orderWaitTimeLimit;
        
        public float foodWaitTimeLimit;

        public GameObject order;

        public float levelOfAnger;
        
        public float timeOfWaitForOrder;

        private float timeOfWaitForOrderIntervalTimer;
        
        public float timeOfWaitForFood;
        
        private float timeOfWaitForFoodIntervalTimer;
        
        

        public void Start()
        {
            hasOrdered = false;
            hasSat = false;
            hasOrderDelivered = false;
            levelOfAnger = 0f;
            orderWaitTimeLimit = GETRandomOrderWaitTimeLimit();
            foodWaitTimeLimit = GETRandomFoodWaitTimeLimit();
            timeOfWaitForOrder = 0f;
            timeOfWaitForFood = 0f;
            timeOfWaitForOrderIntervalTimer = 0f;
            timeOfWaitForFoodIntervalTimer = 0f;
        }

        private void Update()
        {
            satButNotOrderedLogic();
            orderedButNotDeliveredLogic();
        }

        private GameObject GETRandomFood()
        {
            var foods = GameObject.FindGameObjectsWithTag("menuItem");
            Unity.Mathematics.Random random = new Unity.Mathematics.Random();
            int randomNum = random.NextInt(0, 8);
            return foods[randomNum];
        }

        private float GETRandomOrderWaitTimeLimit()
        {
            Unity.Mathematics.Random random = new Unity.Mathematics.Random();
            float randomNum = random.NextFloat(-MAX_LIMIT_CHANGE_PRECENTAGE, MAX_LIMIT_CHANGE_PRECENTAGE);
            return (randomNum) * (BASE_ORDER_WAIT_TIME_LIMIT) + BASE_ORDER_WAIT_TIME_LIMIT;
        }
        
        private float GETRandomFoodWaitTimeLimit()
        {
            Unity.Mathematics.Random random = new Unity.Mathematics.Random();
            float randomNum = random.NextFloat(-MAX_LIMIT_CHANGE_PRECENTAGE, MAX_LIMIT_CHANGE_PRECENTAGE);
            return (randomNum) * (BASE_FOOD_WAIT_TIME_LIMIT) + BASE_FOOD_WAIT_TIME_LIMIT;
        }

        private void satButNotOrderedLogic()
        {
            if (hasSat && !hasOrdered)
            {
                timeOfWaitForOrder += Time.deltaTime;
                if (order == null && timeOfWaitForOrder > orderWaitTimeLimit)
                {
                    order = GETRandomFood();
                }
                if (timeOfWaitForOrder > orderWaitTimeLimit + LOAController.orderWaitIncrementAngerOffsetTime)
                {
                    timeOfWaitForOrderIntervalTimer += Time.deltaTime;
                    if (timeOfWaitForOrderIntervalTimer > LOAController.orderWaitGetAngeryTimeInterval) 
                    {
                        timeOfWaitForOrderIntervalTimer = 0f;
                        incrementLOA();
                    }
                }
            }
        }
        
        private void orderedButNotDeliveredLogic()
        {
            if (hasOrdered && !hasOrderDelivered)
            {
                timeOfWaitForFood += Time.deltaTime;

                if (timeOfWaitForFood > foodWaitTimeLimit + LOAController.foodWaitIncrementAngerOffsetTime)
                {
                    timeOfWaitForFoodIntervalTimer += Time.deltaTime;
                    if (timeOfWaitForFoodIntervalTimer > LOAController.foodWaitGetAngeryTimeInterval) 
                    {
                        timeOfWaitForFoodIntervalTimer = 0f;
                        incrementLOA();
                    }
                }
            }
        }

        private void incrementLOA()
        {
            if (LOAController.LOALimit > levelOfAnger)
            {
                levelOfAnger++;
            }
        }
    }
}
