using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct PlayerStatus
{
    public float HP;
    public float MaxHP;
    public float dex;
    public float def;
    public float cri;
    public int att;

    public PlayerStatus(float HP, float MaxHP, float dex, float def, float cri, int att)
    {
        this.HP = HP;
        this.MaxHP = MaxHP;
        this.dex = dex;
        this.def = def;
        this.cri = cri; 
        this.att = att;
    }
}

public class PlayerController : MonoBehaviour
{
    public PlayerStatus curStatus;
    public PlayerStatus originStatus;

    

    public Image hp_bar;
    
    private Animator animator;

    private int mobHP = 0;

    // �˸�
    public Text noti;

    // ����
    public Text AttackTxt;
    public Text HpTxt;
    public Text DefTxt;
    public Text DexTxt;
    public Text CriTxt;

    public List<Buff> onBuff = new List<Buff>();
 
    private void Start()
    {
        this.originStatus = new PlayerStatus(100, 100, 1, 1, 1, 10);
        this.curStatus = this.originStatus;

        animator = GetComponent<Animator>();
    }

    int cntLoop = 0;

    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            float normalizeTime = 
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float normalizeTimeInCurrentLoop = 
                normalizeTime - Mathf.Floor(normalizeTime);

            if(normalizeTimeInCurrentLoop >= 0.9f && 
                normalizeTime > cntLoop)
            {
                cntLoop += 1;

                int criRan = Random.Range(1, 1000);
                if(criRan<(this.curStatus.cri*0.001f))
                {
                    // ũ��Ƽ�� damage �۾� �غ���
                    mobHP = EnemyManager.Instance.Damaged(this.curStatus.att);
                }
                else
                {
                    mobHP = EnemyManager.Instance.Damaged(this.curStatus.att);
                }
                

                if(mobHP <= 0)
                {
                    animator.SetBool("attack", false);
                    cntLoop = 0;
                }
            }
        }


        // TODO: ��ø ���� �غ���
        //animator.SetFloat("attackSpeed",this.curStatus.dex);
        this.animator.speed = this.curStatus.dex;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            GameManager.Instance.isScroll = false;

            animator.SetBool("attack", true);
        }
    }


    public float Damage(int att)
    {
        this.curStatus.HP -= att-(this.curStatus.def);
        this.hp_bar.fillAmount = this.curStatus.HP / this.curStatus.MaxHP;

        if (this.curStatus.HP <= 0)
        {
            noti.text = "ü���� 0�� �Ǹ� ���ݼӵ��� ������ ���µ˴ϴ�.";
            this.curStatus.dex = 0.5f;
            this.curStatus.HP = 0;

        }
        else
        {
            noti.text = "";

        }

        return this.curStatus.HP;
    }

    public void AttackUp()
    {
        if(GameManager.Instance.Money<1000)
        {
            print("�ݾ��� �����ϴ�");
        }
        else
        {
            GameManager.Instance.SetMoney(-1000);
            this.curStatus.att += 10;
            this.AttackTxt.text =  "���� ���ݷ� : " + this.curStatus.att;
        }
    }

    public void HPUp()
    {
        if (GameManager.Instance.Money < 100)
            print("�ݾ��� �����ϴ�.");
        else
        {
            GameManager.Instance.SetMoney(-100);
            this.curStatus.HP += 10;
            // ä���� �̻��ѵ�

            if (this.curStatus.HP >= this.curStatus.MaxHP)
                this.curStatus.MaxHP = this.curStatus.HP;

            this.HpTxt.text = "���� ä�� : " + this.curStatus.HP;
        }
    }
    
    public void DefUp()
    {
        if (GameManager.Instance.Money < 100)
            print("�ݾ��� �����ϴ�.");
        else
        {
            GameManager.Instance.SetMoney(-100);
            this.curStatus.def += 1;
            this.DefTxt.text = "���� ���� : " + this.curStatus.def;
        }
    }

    public void DexUp()
    {
        if (GameManager.Instance.Money < 100)
            print("�ݾ��� �����ϴ�.");
        else
        {
            GameManager.Instance.SetMoney(-100);
            this.curStatus.def += 1;
            this.DexTxt.text = "���� ��ø�� : " + this.curStatus.def;
        }
    }

    public void CriUp()
    {
        if(GameManager.Instance.Money<100)
            print ("�ݾ��� �����ϴ�.");
        else
        {
            GameManager.Instance.SetMoney(-100);
            this.curStatus.cri += 1;

            this.CriTxt.text = "���� ġ��Ÿ Ȯ�� : " + this.curStatus.cri +" %";
        }
    }

    // ���� ����
    public float BuffChange(string type, float origin)
    {
        if (onBuff.Count > 0)
        {
            float temp = 0;
            for (int i = 0; i < onBuff.Count; i++)
            {
                if (onBuff[i].type.Equals(type))
                {
                    temp += origin * onBuff[i].percentage;
                }
            }
            return origin + temp;
        }
        else
            return origin;
    }

    public void ChooseBuff(string type)
    {
        switch(type)
        {
            case "Atk":
                this.curStatus.att = (int)BuffChange(type, this.curStatus.att);
                break;
            case "dex":
                this.curStatus.dex = BuffChange(type, this.curStatus.dex);
                break;
        }
    }

    public void minusBuff(string type)
    {
        switch(type)
        {
            case "Atk":
                this.curStatus.att = (int)BuffChange(type, this.originStatus.att);
                break;
            case "dex":
                this.curStatus.dex = BuffChange(type, this.originStatus.dex);
                break;
        }
    }
}
