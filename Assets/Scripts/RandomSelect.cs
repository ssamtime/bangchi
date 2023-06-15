using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Skill> deck = new List<Skill>(); //��ų��
    public int total = 0; //ī����� ����ġ ����
    Coroutine sc;

    private void Start()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            //��ũ��Ʈ�� Ȱ��ȭ �Ǹ� ī�� ���� ��� ī���� �� ����ġ�� �����ݴϴ�.
            total += deck[i].weight;
        }
    }

    //�����ϰ� ���õ� ��ų�� ��� ����Ʈ
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
        //��ų�� 20�� ���� ����
        for(int i=0;i<20;i++)
        {
            //����ġ ���� ��� ����Ʈ�� �ֱ�
            result.Add(RandomCard());
            //����ִ� ī�带 ����
            GameObject skillUI = Instantiate(skillPrefab, parent);
            //������ ī�忡 ����Ʈ�� ������ �־��ش�.
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
