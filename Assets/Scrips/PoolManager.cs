using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //프리펩 보관 변수
    public GameObject[] prefabs;  // 몬스터, 아이템 프리펩

    public List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    // 비활성화 오브젝트 재활용
    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in pools[index])  
        {
            // 오브젝트가 비활성화 상태라면 다시 활성화하여 사용
            if (!item.activeSelf) 
            {
                select = item;  // 사용
                select.SetActive(true);
                break;
            }
        }

        if(select == null) //전부 활성화 중이라면 몬스터 소환
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select); //객체 생성 후 오브젝트 풀에 추가
        }


        return select;
    }
}
