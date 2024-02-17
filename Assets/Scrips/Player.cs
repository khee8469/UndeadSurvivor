using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float speed;
    public Vector2 inputVec;
    SpriteRenderer spriter;
    Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();

    }

    private void Move()
    {
        transform.Translate(inputVec.normalized * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rigid.velocity = Vector2.zero;
    }
}
