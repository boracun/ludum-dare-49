using System;
using Objects;
using UnityEngine;

namespace Movement
{
    public class CustomerMovement : MonoBehaviour
    {
        public static int ENTER_MODE = 0;
        public static int WAIT_MODE = 1;
        public static int LEAVE_MODE = 2;
    
        public float TOLERANCE = 0.05f;
    
        public float horizontalMoveSpeed = 0f;

        public float verticalMoveSpeed = 0f;

        public int movementMode = ENTER_MODE;

        public SpriteRenderer sr;

        public float[,] selectedRoute = null;

        public int routeStep = 0;

        public Rigidbody2D rBody;
        
        public GameObject table;
    
    
        // Start is called before the first frame update
        void Start()
        {
            rBody = gameObject.GetComponent<Rigidbody2D>();
            rBody.velocity = new Vector2(0, 0);
            movementMode = ENTER_MODE;
        }

        // Update is called once per frame
        void Update()
        {
            if (selectedRoute == null)
            {
                RouteNode routeNode = GameObject.Find("RestaurantManager").GetComponent<CustomerManager>().GETTableRoute();
                float[,] route = routeNode.route;
                if (route != null)
                {
                    GameObject tableObj = routeNode.table;
                    selectedRoute = route;
                    tableObj.GetComponent<Table>().AddCustomer(routeNode.routeIndex, gameObject);
                    table = tableObj;
                    if (table.GetComponent<Table>().filledSeats == 3 ||
                        table.GetComponent<Table>().filledSeats == 4)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }
                }
            }
            if (selectedRoute != null)
            {
                if (movementMode == WAIT_MODE)
                {
                    WaitModeLogic();
                } 
                else if (movementMode == LEAVE_MODE)
                {
                    LeaveModeLogic();
                }
                else if (movementMode == ENTER_MODE)
                {
                    EnterModeLogic();
                }
            }

            if (rBody.velocity.x < 0)
                sr.flipX = true;
            else if (rBody.velocity.x > 0)
                sr.flipX = false;

            if (movementMode == CustomerMovement.WAIT_MODE) sr.flipX = false;
        }

        private void WaitModeLogic()
        {
            rBody.velocity = new Vector2(0, 0);
        }

        private void LeaveModeLogic()
        {
            if (Math.Abs(gameObject.transform.position.x - selectedRoute[0, 0]) < TOLERANCE &&
                Math.Abs(gameObject.transform.position.y - selectedRoute[0, 1]) < TOLERANCE)
            {
                LeaveModeDestinationReachedLogic();
            }
            PathNavigationLogic(-1);
        }

        private void EnterModeLogic()
        {
            if (Math.Abs(gameObject.transform.position.x - selectedRoute[selectedRoute.Length / 2 - 1, 0]) < TOLERANCE &&
                Math.Abs(gameObject.transform.position.y - selectedRoute[selectedRoute.Length / 2 - 1, 1]) < TOLERANCE)
            {
                EnterModeDestinationReachedLogic();
            }
            PathNavigationLogic(1);
        }

        private void EnterModeDestinationReachedLogic()
        {
            routeStep = selectedRoute.Length / 2 - 1;
            movementMode = WAIT_MODE;
            rBody.velocity = new Vector2(0, 0);
            Customer customer = gameObject.GetComponent<Customer>();
            customer.sitOnTable();
        }

        private void LeaveModeDestinationReachedLogic()
        {
            Destroy(gameObject);
        }

        private void PathNavigationLogic(int routeStepAddition)
        {
            float destinationPointX = selectedRoute[routeStep, 0];
            float destinationPointY = selectedRoute[routeStep, 1];

            Transform tForm = gameObject.transform;

            if (Math.Abs(destinationPointX - tForm.position.x) <= TOLERANCE &&
                Math.Abs(destinationPointY - tForm.position.y) <= TOLERANCE)
            {
                rBody.velocity = new Vector2(0, 0);
                if (routeStep + routeStepAddition >= 0 && routeStepAddition + routeStep < selectedRoute.Length / 2)
                {
                    routeStep += routeStepAddition;
                }
            }
            else if (destinationPointX > tForm.position.x)
            {
                rBody.velocity = new Vector2(horizontalMoveSpeed, 0);
            }
            else if (destinationPointX < tForm.position.x - TOLERANCE)
            {
                rBody.velocity = new Vector2(-horizontalMoveSpeed, 0);
            }
            else if (destinationPointY > tForm.position.y)
            {
                rBody.velocity = new Vector2(0, verticalMoveSpeed);
            }
            else if (destinationPointY < tForm.position.y)
            {
                rBody.velocity = new Vector2(0, -verticalMoveSpeed);
            }
        }
    }
}
