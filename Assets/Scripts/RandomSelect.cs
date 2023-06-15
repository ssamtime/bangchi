using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Skill> deck = new List<Skill>(); //스킬덱
    public int total = 0; //카드들의 가중치 총합
    Coroutine sc;

    private void Start()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            //스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구해줍니다.
            total += deck[i].weight;
        }
    }

    //랜덤하게 선택된 스킬을 담는 리스트
    public List<Skill> result = new List<Skill>();
    public List<GameObject> skillOb = new List<GameObject>();

    public Transform parent;
    public GameObject skillPrefab;

    public void RandomStart()
    {
        if (sc == null)
        {
            for(int i=0;i<skillOb.Count;i++)
            {
                Destroy(skillOb[i]);
            }
            result.Clear();
            skillOb.Clear();

            sc = StartCoroutine("ResultSelect");
        }

    }

    IEnumerator ResultSelect()
    {
        //스킬을 20개 뽑을 거임
        for(int i=0;i<20;i++)
        {
            //가중치 랜덤 결과 리스트에 넣기
            result.Add(RandomCard());
            //비어있는 카드를 생성
            GameObject skillUI = Instantiate(skillPrefab, parent);
            //생성된 카드에 리스트의 정보를 넣어준다.
            skillUI.GetComponent<SkillUI>().CardUISet(result[i]);

            skillOb.Add(skillUI);

            yield return new WaitForSeconds(0.2f);
        }
        sc = null;
                
    }

    public Skill RandomCard()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for(int i=0;i<deck.Count;i++)
        {
            weight += deck[i].weight;
            if(selectNum<=weight)
            {
                Skill temp = new Skill(deck[i]);
                return temp;
            }
        }
        return null;
    }
}
