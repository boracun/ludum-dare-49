using System;
using UnityEngine;

namespace Movement
{
    public class CustomerMovement : MonoBehaviour

    {
        public static int ENTER_MODE = 0;
        public static int WAIT_MODE = 1;
        public static int LEAVE_MODE = 2;
    
        public static readonly double TOLERANCE = 0.0001;
    
        public float horizontalMoveSpeed = 0f;

        public float verticalMoveSpeed = 0f;

        public int movementMode = ENTER_MODE;

        public float[,] selectedRoute = null;

        public int routeStep = 0;

        public Rigidbody2D rBody;
    
    
        // Start is called before the first frame update
        void Start()
        {
            rBody = gameObject.GetComponent<Rigidbody2D>();
            rBody.velocity = new Vector2(0, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (selectedRoute == null)
            {
                float[,] route = GameObject.Find("RestaurantManager").GetComponent<CustomerManager>().GETTableRoute();
                if (route != null)
                {
                    selectedRoute = route;
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
            if (Math.Abs(gameObject.transform.position.x - selectedRoute[selectedRoute.Length - 1, 0]) < TOLERANCE &&
                Math.Abs(gameObject.transform.position.y - selectedRoute[selectedRoute.Length - 1, 1]) < TOLERANCE)
            {
                EnterModeDestinationReachedLogic();
            }
            PathNavigationLogic(1);
        }

        private void EnterModeDestinationReachedLogic()
        {
            routeStep = selectedRoute.Length - 1;
            movementMode = WAIT_MODE;
            rBody.velocity = new Vector2(0, 0);
        }

        private void LeaveModeDestinationReachedLogic()
        {
            Destroy(gameObject);
        }

        private void PathNavigationLogic(int routeStepAddition)
        {
            float destinationPointX = selectedRoute[routeStep, 0];
            float destinationPointY = selectedRoute[routeStep, 1];
        
            if (Math.Abs(destinationPointX - gameObject.transform.position.x) < TOLERANCE &&
                Math.Abs(destinationPointY - gameObject.transform.position.y) < TOLERANCE)
            {
                rBody.velocity = new Vector2(0, 0);
                if (routeStep + routeStepAddition > 0 && routeStepAddition + routeStep < selectedRoute.Length - 1)
                {
                    routeStep += routeStepAddition;
                }
            }
            else if (destinationPointX > gameObject.transform.position.x)
            {
                rBody.velocity = new Vector2(horizontalMoveSpeed, 0);
            }
            else if (destinationPointX < gameObject.transform.position.x)
            {
                rBody.velocity = new Vector2(-horizontalMoveSpeed, 0);
            }
            else if (destinationPointY > gameObject.transform.position.y)
            {
                rBody.velocity = new Vector2(0, verticalMoveSpeed);
            }
            else if (destinationPointY < gameObject.transform.position.y)
            {
                rBody.velocity = new Vector2(0, -verticalMoveSpeed);
            }
        }
    }
}
