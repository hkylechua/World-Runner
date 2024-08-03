using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Numerics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UnityEngine.Vector2 velocity;
    public float maxXVelocity = 100;
    public float gravity;
    public float groundHeight = -3;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float jumpVelocity = 50;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public float holdJumpTimer = 0.0f;
    public float maxHoldJumpTime = 0.04f;
    public float playerSpeed;
    public float jumpGroundThreshold = -2;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    void FixedUpdate()
    {
        UnityEngine.Vector2 pos = transform.position;
        if (!isGrounded)
        {

            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if(holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }
            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);
            
            velocity.x += acceleration * Time.fixedDeltaTime;
            if(velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }


        }
        transform.position = pos;
    }
}
