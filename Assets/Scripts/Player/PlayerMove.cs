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

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>(); 
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
    }

    public void Move()
    {
        if (player.isAlive == false)
            return;

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

    #endregion
}
