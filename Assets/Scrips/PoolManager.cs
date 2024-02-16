using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //프리펩 보관 변수
    public GameObject[] prefabs;  // 몬스터들 프리팹

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

        foreach(GameObject item in pools[index])  // 죽은 몬스터 재활성용
        {
            if (!item.activeSelf) // 비활성화 여부 확인
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
