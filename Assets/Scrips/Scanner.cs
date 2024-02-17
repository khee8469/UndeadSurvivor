using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;  // ��ĵ����
    public LayerMask targetLayer;  //��� ���̾ ��ĵ����
    public RaycastHit2D[] targets;  //��ĵ �����
    public Transform nearestTarget;  //���� ����� Ÿ��

    private void FixedUpdate()
    {                                        //��ġ           ����      ĳ���ù���   �������  ����̾�                               
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest(); // ���� ����� Ÿ����ġ
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;  // �÷��̾� ��ġ
            Vector3 targetPos = target.transform.position;  // Ÿ����ġ
            float curDiff = Vector3.Distance(myPos, targetPos);

            if(curDiff < diff)  
            {
                diff = curDiff;  // Ÿ�ٰ��� �ּҰŸ� ����
                result = target.transform;  // �ּҰŸ����� ���� ��ġ ����
            }
        }

        return result;
    }
}
