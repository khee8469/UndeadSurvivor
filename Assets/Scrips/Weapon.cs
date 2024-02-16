using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                //weapon0 오브젝트 회전
                transform.Rotate(Vector3.forward * speed * Time.deltaTime); 
                break;
            default:

                break;
        }

        /*test용
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }*/
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
        {
            Batch();
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                Batch();
                break;
            default:

                break;
        }
    }

    void Batch()
    {
        for(int i = 0; i < count; i++)
        {
            Transform bullet;

            //모자란거만 가져오기
            if(i< transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero; // 플레이어의 위치로 초기화
            bullet.localRotation = Quaternion.identity; // 회전값도 초기화

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1); //-1 은 무한으로 관통
        }
    }
}
