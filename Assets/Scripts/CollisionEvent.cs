using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    [SerializeField]
    private Color color;

    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// �浹�� �Ͼ�� ���� 1ȸ ȣ��
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        spriteRenderer.color = color;
    }

    /// <summary>
    /// �浹�� �����Ǵ� ���� �� ������ ȣ��
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + 
            " : OnCollisionStay2D() �޼ҵ� ����");
    }

    /// <summary>
    /// �浹�� ����Ǵ� ���� 1ȸ ȣ��
    /// </summary>
    private void OnCollisionExit2D(Collision2D collision)
    {
        spriteRenderer.color = Color.white;
    }
}
