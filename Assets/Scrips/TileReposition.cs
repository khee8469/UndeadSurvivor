using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileReposition : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {


        //�浹�� ������Ʈ�� �ױװ� Area�� �ƴҶ�
        if (!collision.CompareTag("Area"))
        {
            return;
        }
        
        //�浹�� ������Ʈ�� ��ũ�� Area�϶�

        //�÷��̾� ��ġ
        Vector2 playerPos = GameManager.instance.player.transform.position;
        // Ÿ�ϸ���ġ
        Vector2 tilePos = transform.position;

        // �÷��̾�� Ÿ�ϰ��� x�Ÿ�
        float diffx = Mathf.Abs(playerPos.x - tilePos.x);
        // �÷��̾�� Ÿ�ϰ��� y�Ÿ�
        float diffy = Mathf.Abs(playerPos.y - tilePos.y);

        //�÷��̾� ����
        Vector3 playerDir = GameManager.instance.player.inputVec;
        //Ÿ�ϸ� �̵�����
        float dirX = playerDir.x;
        float dirY = playerDir.y;


        
        switch (transform.tag)
        {
            case "Ground":
                if (Mathf.Abs(dirX) > Mathf.Abs(dirY))
                {
                    transform.Translate(Vector2.right * (dirX < 0? -1:1) * 60);
                }
                else if(Mathf.Abs(dirX) < Mathf.Abs(dirY))
                {
                    transform.Translate(Vector2.up * (dirY < 0 ? -1 : 1) * 50);
                }
                else if (Mathf.Abs(dirX) == Mathf.Abs(dirY)) //�밢��
                {
                    if(diffx > 30) // Ÿ�ϰ��� �Ÿ��� > 30 : Ÿ���� x ���� 
                    {
                        transform.Translate(Vector2.right * (dirX < 0 ? -1 : 1) * 60);
                    }
                    if(diffy > 25) // Ÿ�ϰ��� �Ÿ��� > 25 : Ÿ���� y ���� 
                    {
                        transform.Translate(Vector2.up * (dirY < 0 ? -1 : 1) * 50);
                    }
                    
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 30 + new Vector3(Random.Range(-5f,5f), Random.Range(-5f, 5f), 0f));
                }
                break;
        }
    }
}
