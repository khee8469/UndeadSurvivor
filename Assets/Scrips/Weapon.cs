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

    float timer;
    Player player;

    private void Awake()
    {
        player = GameManager.instance.player;
    }
    
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                //weapon0 ������Ʈ ȸ��
                transform.Rotate(Vector3.forward * speed * Time.deltaTime); 
                break;

            default:
                timer += Time.deltaTime;
                if(timer > speed) // �߻�
                {
                    timer = 0f; //�� �ʱ�ȭ
                    Fire();
                }
                break;
        }

        //test��
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 1);
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
        {
            Batch();
        }
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = $"Weaoon {data.itemId}";
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int index=0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = -150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }

        //Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for(int i = 0; i < count; i++)
        {
            Transform bullet;

            //���ڶ��Ÿ� ��������
            if(i< transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero; // �÷��̾��� ��ġ�� �ʱ�ȭ
            bullet.localRotation = Quaternion.identity; // ȸ������ �ʱ�ȭ

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); //-1 �� �������� ����
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)  // nearestTarget�� null�϶�
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position; // ���� ��ġ
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized; // ����

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
