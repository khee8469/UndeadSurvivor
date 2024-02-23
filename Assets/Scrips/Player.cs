using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;
    public Scanner scanner;
    public Hand[] hands;

    public float speed;
    public Vector2 inputVec;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }


    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void Move()
    {
        transform.Translate(inputVec * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        Move();
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        //애니메이터 파라미터 Speed의 float값 입력
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0) // 좌우 방향키를 누르고있다면
        {
            //왼쪽입력 = true 오른쪽입력 = false
            spriter.flipX = inputVec.x < 0;
        }
    }

    //충돌후 밀림현상 종료
    private void OnTriggerExit2D(Collider2D collision)
    {
        rigid.velocity = Vector2.zero;
    }
}
