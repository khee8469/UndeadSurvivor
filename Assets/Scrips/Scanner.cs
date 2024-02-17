using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;  // 스캔범위
    public LayerMask targetLayer;  //어느 레이어를 스캔할지
    public RaycastHit2D[] targets;  //스캔 결과들
    public Transform nearestTarget;  //가장 가까운 타겟

    private void FixedUpdate()
    {                                        //위치           범위      캐스팅방향   방향길이  대상레이어                               
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest(); // 제일 가까운 타겟위치
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;  // 플레이어 위치
            Vector3 targetPos = target.transform.position;  // 타겟위치
            float curDiff = Vector3.Distance(myPos, targetPos);

            if(curDiff < diff)  
            {
                diff = curDiff;  // 타겟과의 최소거리 저장
                result = target.transform;  // 최소거리엿던 몬스터 위치 저장
            }
        }

        return result;
    }
}
