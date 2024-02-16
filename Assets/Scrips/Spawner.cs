
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnDate[] spawnData;

    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();  // 오브젝트 자식을 전부 컴포넌트에 추가
    }
    void Update()
    {
        timer += Time.deltaTime;
                  //둘중 최소값 출력
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length -1);


        // 소수점아래 버리기
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
