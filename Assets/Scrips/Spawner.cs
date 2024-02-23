
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;  // 스폰위치
    public SpawnDate[] spawnData;  //스폰몬스터 데이터

    int level; //스테이지레벨
    float timer; //리스폰용

    private void Awake()
    {
        // 오브젝트 자식을 전부 컴포넌트에 추가
        spawnPoint = GetComponentsInChildren<Transform>();  
    }
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        //둘중 최소값 출력 spawnData.Length -1 보다 레벨은 높아지지 않는다.
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length -1);


        timer += Time.deltaTime;
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}


[System.Serializable]
public class SpawnDate
{
    public int spriteType;
    public int health;
    public float spawnTime;
    public float speed;
}
