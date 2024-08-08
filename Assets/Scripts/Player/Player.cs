using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayerMoveable
{
    private float inputX;
    private float inputY;
    private bool isFacingLeft;
    private Vector2 moveDir;

    [Header("이동속도")]
    [SerializeField] float moveSpeed;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
       rb= GetComponent<Rigidbody2D>(); 
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

    #region Player Movement
    public void GetInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    public void Move()
    {        
        Flip();
        moveDir = new Vector2(inputX, inputY).normalized;
        rb.velocity = moveDir * moveSpeed;
#if UNITY_EDITOR
        Debug.Log($"Current Velocity is : {rb.velocity}");
#endif

    }

    #endregion

    #region Animation

    // 음수 : 왼쪽, 양수 : 오른쪽 
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
