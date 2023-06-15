using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonManager<EnemyManager>
{
    public GameObject pPrefabMob = null; // 생성할 프리팹
    public Transform StartPos;

    private ObjectPool pPool;

    private Queue<GameObject> pMobQueue = new Queue<GameObject>();

    // 골드 포지션
    public ItemFx prefabItem;
    public Transform toTweenPos;
    public Transform goleParent;


    private void Start()
    {
        GameObject obj = new GameObject();
        obj.transform.SetParent(this.transform);
        obj.name = typeof(ObjectPool).Name;

        this.pPool = obj.AddComponent<ObjectPool>();
        this.pPool.Initialize(this.pPrefabMob, this.StartPos);

        // TODO : 몬스터 생성 및 삭제
        this.StartCoroutine(CreateEnemy());
    }

    public int Damaged (int att)
    {
        if(this.pMobQueue.Count != 0)
        {
            GameObject obj = this.pMobQueue.Peek();
            Monster mob = obj.GetComponent<Monster>();
            mob.HP -= att;

            if(mob.HP <= 0)
            {
                // 돈 올라가는 애니메이션
                this.SetMoney();

                GameManager.Instance.isScroll = true;
                this.StartCoroutine(CreateEnemy());
                // 몬스터 삭제
                this.pPool.InsertQueue(this.pMobQueue.Dequeue());
            }
            else
            {
                // 데미지 텍스트 처리
                DamageOn damageTxt = obj.GetComponent<DamageOn>();
                damageTxt.DamageTxt();
            }
            return mob.HP;
        }
        return 0;
    }



    IEnumerator CreateEnemy()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

        while(true)
        {
            if(GameManager.Instance.isScroll)
            {
                GameObject objMob = this.pPool.GetQueue();
                Monster mob = objMob.GetComponent<Monster>();
                mob.HP = 100;

                this.pMobQueue.Enqueue(objMob);
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            }
            else
                yield break;
        }
        yield return null;
    }

    void SetMoney ()
    {
        int randCount = Random.Range(5, 10);
        for(int i = 0; i < randCount; i++)
        {
            Vector3 screenPos = 
                Camera.main.WorldToScreenPoint(transform.position);
            ItemFx itemFx = Instantiate(prefabItem, screenPos, Quaternion.identity);
            itemFx.transform.SetParent(goleParent);
            itemFx.Explosion(screenPos, toTweenPos.position, 150f);
        }

        GameManager.Instance.SetMoney(Random.Range(50, 100));
    }
}
