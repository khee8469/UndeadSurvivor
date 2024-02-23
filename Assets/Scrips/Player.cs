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
        //�ִϸ����� �Ķ���� Speed�� float�� �Է�
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0) // �¿� ����Ű�� �������ִٸ�
        {
            //�����Է� = true �������Է� = false
            spriter.flipX = inputVec.x < 0;
        }
    }

    //�浹�� �и����� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        rigid.velocity = Vector2.zero;
    }
}
