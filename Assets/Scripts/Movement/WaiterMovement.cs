using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Movement
{
    public class WaiterMovement : MonoBehaviour
    {
        private const float DIAGONAL_PRESS_ALLOWANCE = 0.25f;
        private const int MAX_IMBALANCE_LEVEL = 6;
        
        public float playerSpeed;
        public Rigidbody2D rBody;
        public SpriteRenderer spriteRenderer;       //Move this to Animation Code when it is created
        private Vector2 _playerDirection;
        
        private int imbalanceLevel = 0;
        private KeyCode previousKey = KeyCode.B;
        private float previousTime = 0f;

        private void Start()
        {
            InvokeRepeating(nameof(ImbalanceCoolDown), 0f, 1.5f);
        }

        // Update is called once per frame
        void Update()
        {
            float directionX;
            if (Input.GetKey(KeyCode.A))
            {
                directionX = -1f;
                spriteRenderer.flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                directionX = 1f;
                spriteRenderer.flipX = false;
            }
            else directionX = 0f;

            float directionY;
            if (Input.GetKey(KeyCode.S)) directionY = -1f;
            else if (Input.GetKey(KeyCode.W)) directionY = 1f;
            else directionY = 0f;

            _playerDirection = new Vector2(directionX, directionY).normalized;
            
            HandleImbalanceLevel();
        }

        void FixedUpdate()
        {
            rBody.velocity = new Vector2(_playerDirection.x * playerSpeed, _playerDirection.y * playerSpeed);
        }

        private void HandleImbalanceLevel()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!((previousKey == KeyCode.W ||
                       previousKey == KeyCode.S) &&
                      previousTime > Time.time - DIAGONAL_PRESS_ALLOWANCE))
                {
                    imbalanceLevel++;
                }
                previousKey = KeyCode.A;
                previousTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (!((previousKey == KeyCode.W ||
                       previousKey == KeyCode.S) &&
                      previousTime > Time.time - DIAGONAL_PRESS_ALLOWANCE))
                {
                    imbalanceLevel++;
                }
                previousKey = KeyCode.D;
                previousTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (!((previousKey == KeyCode.A ||
                       previousKey == KeyCode.D) &&
                      previousTime > Time.time - DIAGONAL_PRESS_ALLOWANCE))
                {
                    imbalanceLevel++;
                }
                previousKey = KeyCode.W;
                previousTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (!((previousKey == KeyCode.A ||
                       previousKey == KeyCode.D) &&
                      previousTime > Time.time - DIAGONAL_PRESS_ALLOWANCE))
                {
                    imbalanceLevel++;
                }
                previousKey = KeyCode.S;
                previousTime = Time.time;
            }

            if (Waiter.Instance.heldItem == null)
                imbalanceLevel = 0;
            
            if (imbalanceLevel >= MAX_IMBALANCE_LEVEL)
            {
                Waiter.Instance.DropItem();
            }
            
            SetBubbleColor(imbalanceLevel, MAX_IMBALANCE_LEVEL);
        }

        private void ImbalanceCoolDown()
        {
            if (imbalanceLevel > 0)
            {
                imbalanceLevel--;
            }
        }

        private void SetBubbleColor(int imbLevel, int maxImbalanceLevel)
        {
            float coefficient = 1f / maxImbalanceLevel;
            float blueGreenValue = 1f - coefficient * imbLevel;
            transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color =
                new Color(1f, blueGreenValue, blueGreenValue);
        }
    }
}

