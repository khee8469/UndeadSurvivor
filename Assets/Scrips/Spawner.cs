
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;  // ������ġ
    public SpawnDate[] spawnData;  //�������� ������

    int level; //������������
    float timer; //��������

    private void Awake()
    {
        // ������Ʈ �ڽ��� ���� ������Ʈ�� �߰�
        spawnPoint = GetComponentsInChildren<Transform>();  
    }
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        //���� �ּҰ� ��� spawnData.Length -1 ���� ������ �������� �ʴ´�.
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
