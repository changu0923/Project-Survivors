using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Player player;

    private float inputX;
    private float inputY;
    private bool isFacingLeft;
    private Vector2 moveDir;
    private bool isMoving;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void GetInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if(inputX == 0f && inputY == 0f )
        {
            isMoving = false; 
            AnimationMove();
            return; 
        }
        isMoving = true;
        AnimationMove();
    }

    public void Move()
    {
        if (player.isAlive == false)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Flip();
        moveDir = new Vector2(inputX, inputY).normalized;
        rb.velocity = moveDir * player.MoveSpeed;
    }

    #region Animation
    private void Flip()
    {
        if(inputX > 0)
        {
            isFacingLeft = false;
        }
        else if(inputX < 0)
        {
            isFacingLeft = true;
        }
        spriteRenderer.flipX = isFacingLeft;
    }

    private void AnimationMove()
    {
        animator.SetBool("Idle", !isMoving);
    }

    #endregion
}
