using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //������ ���� ����
    public GameObject[] prefabs;  // ���͵� ������

    public List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in pools[index])  // ���� ���� ��Ȱ����
        {
            if (!item.activeSelf) // ��Ȱ��ȭ ���� Ȯ��
            {
                select = item;  // ���
                select.SetActive(true);
                break;
            }
        }

        if(select == null) //���� Ȱ��ȭ ���̶�� ���� ��ȯ
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select); //��ü ���� �� ������Ʈ Ǯ�� �߰�
        }


        return select;
    }
}
