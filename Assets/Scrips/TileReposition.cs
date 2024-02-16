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


        //충돌한 오브젝트의 테그가 Area가 아닐때
        if (!collision.CompareTag("Area"))
        {
            return;
        }
        
        //충돌한 오브젝트의 테크가 Area일때

        //플레이어 위치
        Vector2 playerPos = GameManager.instance.player.transform.position;
        // 타일맵위치
        Vector2 tilePos = transform.position;

        // 플레이어와 타일간의 x거리
        float diffx = Mathf.Abs(playerPos.x - tilePos.x);
        // 플레이어와 타일간의 y거리
        float diffy = Mathf.Abs(playerPos.y - tilePos.y);

        //플레이어 방향
        Vector3 playerDir = GameManager.instance.player.inputVec;
        //타일맵 이동방향
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
                else if (Mathf.Abs(dirX) == Mathf.Abs(dirY)) //대각선
                {
                    if(diffx > 30) // 타일과의 거리가 > 30 : 타일의 x 길이 
                    {
                        transform.Translate(Vector2.right * (dirX < 0 ? -1 : 1) * 60);
                    }
                    if(diffy > 25) // 타일과의 거리가 > 25 : 타일의 y 길이 
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
