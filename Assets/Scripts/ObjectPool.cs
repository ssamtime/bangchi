using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject pPrefabMob = null; // 생성할 프리팹

    public Queue<GameObject> pQueue = new Queue<GameObject>(); // 객체를 저장할 큐

    private Transform startPos;

    public void Initialize(GameObject pPrefabMob, Transform StartPos)
    {
        this.pPrefabMob = pPrefabMob;
        this.startPos = StartPos;

        // 15마리 생성
        for(int i = 0; i < 15; i++)
        {
            GameObject obj = Instantiate(pPrefabMob, this.transform);
            pQueue.Enqueue(obj);
            obj.transform.position = startPos.position;
            obj.SetActive(false);
        }
    }

    // 사용한 객체를 Pool(Queue)에 반납
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
