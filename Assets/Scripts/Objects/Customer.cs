using System;
using Collisions;
using Manager;
using Movement;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
    public class Customer : MonoBehaviour
    {
        public const float BASE_ORDER_WAIT_TIME_LIMIT = 5f;
        
        public const float BASE_FOOD_WAIT_TIME_LIMIT = 20f;

        public const float MAX_LIMIT_CHANGE_PRECENTAGE = 0.4f;

        public bool hasSat;

        public bool hasOrdered;

        public bool hasOrderDelivered;

        public float orderWaitTimeLimit;
        
        public float foodWaitTimeLimit;

        public MenuItem order;

        public float levelOfAnger;
        
        public float timeOfWaitForOrder;

        private float timeOfWaitForOrderIntervalTimer = 0f;
        
        public float timeOfWaitForFood;
        
        private float timeOfWaitForFoodIntervalTimer = 0f;

        public float timeOfLeaveAfterFood;

        private float timeOfLeaveAfterFoodTimer = 0f;

        public bool hasPointGiven;

        public GameObject orderBubble;

        public GameObject throwableObjectPrefab;

        public int customerType;

        public void Start()
        {
            hasOrdered = false;
            hasSat = false;
            hasOrderDelivered = false;
            hasPointGiven = false;
            levelOfAnger = 0;
            orderWaitTimeLimit = GETRandomOrderWaitTimeLimit();
            foodWaitTimeLimit = GETRandomFoodWaitTimeLimit();
            timeOfWaitForOrderIntervalTimer = 0f;
            timeOfWaitForFoodIntervalTimer = 0f;
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
                orderBubble.SetActive(false);
                if (!hasPointGiven) //This is useless since reputation is dynamic.
                {
                    hasPointGiven = true;
                }
                timeOfLeaveAfterFoodTimer += Time.deltaTime;
                if (timeOfLeaveAfterFood <= timeOfLeaveAfterFoodTimer)
                {
                    GameObject o = gameObject;
                    CustomerMovement customerMovement = (o).GetComponent<CustomerMovement>();
                    customerMovement.movementMode = CustomerMovement.LEAVE_MODE;
                    customerMovement.table.GetComponent<Table>().RemoveCustomer(o);
                    
                    timeOfLeaveAfterFoodTimer = 10f;
                }
            }
        }

        private MenuItem GETRandomFood()
        {
            return GameObject.Find("ItemCatalog").GetComponent<FoodCatalog>().GETRandomMenuItem();
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
                    orderBubble.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
                        order.menuItemSprite;
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
            if (!hasOrderDelivered)
            {
                levelOfAnger++;
                GameObject.Find("RestaurantManager").GetComponent<ReputationManager>().reputation--;
                SetRedTint();
                if (levelOfAnger >= LOAController.FIRST_THROW_LEVEL)
                {
                    Instantiate(throwableObjectPrefab, transform.localPosition, Quaternion.identity);
                }
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

        private void SetRedTint()
        {
            if (levelOfAnger == 0 || levelOfAnger >= LOAController.FIRST_THROW_LEVEL)
                return;
            float angerPercentage = levelOfAnger / LOAController.FIRST_THROW_LEVEL;
            transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f - angerPercentage, 1f - angerPercentage);
        }
    }
}
