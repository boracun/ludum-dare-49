using System;
using Movement;
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

        private float timeOfWaitForOrderIntervalTimer = 0f;
        
        public float timeOfWaitForFood;
        
        private float timeOfWaitForFoodIntervalTimer = 0f;

        public float timeOfLeaveAfterFood;

        private float timeOfLeaveAfterFoodTimer = 0f;
        
        

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
            timeOfLeaveAfterFood = 0f;
            timeOfLeaveAfterFoodTimer = 0f;
        }

        private void Update()
        {
            SatButNotOrderedLogic();
            OrderedButNotDeliveredLogic();
            OrderDeliveredLogic();
        }

        private void OrderDeliveredLogic()
        {
            if (hasOrderDelivered)
            {
                timeOfLeaveAfterFoodTimer += Time.deltaTime;
                if (timeOfLeaveAfterFood <= timeOfLeaveAfterFoodTimer)
                {
                    gameObject.GetComponent<CustomerMovement>().movementMode = CustomerMovement.LEAVE_MODE;
                    timeOfLeaveAfterFoodTimer = 0f;
                }
            }
        }

        private GameObject GETRandomFood()
        {
            var foods = GameObject.FindGameObjectsWithTag("menuItem");
            int randomNum = (int)Math.Floor(Random.Range(0f, 8.99999f));
            return foods[randomNum];
        }

        private float GETRandomOrderWaitTimeLimit()
        {
            float randomNum = Random.Range(-MAX_LIMIT_CHANGE_PRECENTAGE, MAX_LIMIT_CHANGE_PRECENTAGE);
            return (randomNum) * (BASE_ORDER_WAIT_TIME_LIMIT) + BASE_ORDER_WAIT_TIME_LIMIT;
        }
        
        private float GETRandomFoodWaitTimeLimit()
        {
            float randomNum = Random.Range(-MAX_LIMIT_CHANGE_PRECENTAGE, MAX_LIMIT_CHANGE_PRECENTAGE);
            return (randomNum) * (BASE_FOOD_WAIT_TIME_LIMIT) + BASE_FOOD_WAIT_TIME_LIMIT;
        }

        private void SatButNotOrderedLogic()
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
                        IncrementLoa();
                    }
                }
            }
        }
        
        private void OrderedButNotDeliveredLogic()
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
                        IncrementLoa();
                    }
                }
            }
        }

        private void IncrementLoa()
        {
            if (LOAController.LOALimit > levelOfAnger && !hasOrderDelivered)
            {
                levelOfAnger++;
            }
        }

        public void sitOnTable()
        {
            if (!hasSat)
            {
                hasSat = true;
                hasOrdered = false;
                hasOrderDelivered = false;
            }
        }
    }
}
