using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public string type;
    public float percentage;
    public float duration;
    public float currentTime;
    public Image icon;

    PlayerController player;

    private void Awake()
    {
        icon = GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="type">���� Ÿ��</param>
    /// <param name="per">���� ���� �ۼ�������</param>
    /// <param name="du">������ ����� duration </param>
    public void Init(string type,float per, float du)
    {
        this.type = type;
        this.percentage = per;
        this.duration = du;
        currentTime = duration;
        icon.fillAmount = 1;
        Execute();
    }

    public void Execute()
    {
        player.onBuff.Add(this);
        player.ChooseBuff(type);

        //�÷��̾ ����ȿ��

        StartCoroutine(Activation());
    }

    WaitForSeconds seconds = new WaitForSeconds(0.1f);
    //���� Ȱ��ȭ
    IEnumerator Activation()
    {
        while(currentTime>0)
        {
            currentTime -= 0.1f;
            icon.fillAmount = currentTime / duration;
            yield return seconds;
        }

        icon.fillAmount = 0;
        currentTime = 0;

        DestroyActivation();
        
    }

    public void DestroyActivation()
    {
        player.onBuff.Remove(this);
        player.minusBuff(type);

        Destroy(gameObject);
    }
}
