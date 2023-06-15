using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject pPrefabMob = null; // ������ ������

    public Queue<GameObject> pQueue = new Queue<GameObject>(); // ��ü�� ������ ť

    private Transform startPos;

    public void Initialize(GameObject pPrefabMob, Transform StartPos)
    {
        this.pPrefabMob = pPrefabMob;
        this.startPos = StartPos;

        // 15���� ����
        for(int i = 0; i < 15; i++)
        {
            GameObject obj = Instantiate(pPrefabMob, this.transform);
            pQueue.Enqueue(obj);
            obj.transform.position = startPos.position;
            obj.SetActive(false);
        }
    }

    // ����� ��ü�� Pool(Queue)�� �ݳ�
    public void InsertQueue(GameObject obj)
    {
        pQueue.Enqueue(obj);
        obj.transform.position = this.startPos.position;
        obj.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject obj = pQueue.Dequeue();
        obj.SetActive(true);
        return obj;
    }
}
