using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : SingletonManager<BackgroundManager>
{
    [SerializeField]
    private Background[] backgrounds;

    public float speed = 10;

    private float[] _leftPosX = new float[2];
    private float[] _rightPosX = new float[2];

    private void Start()
    {
        for(int i = 0; i < this.backgrounds.Length; i++)
        {
            Vector2 vect = this.backgrounds[i]._bg.sprite.rect.size /
                backgrounds[i]._bg.sprite.pixelsPerUnit;

            this._leftPosX[i] = (-vect.x);
            this._rightPosX[i] = vect.x;
        }
    }

    private void Update()
    {
        if(GameManager.Instance.isScroll)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                this.backgrounds[i].gameObject.transform.position +=
                    new Vector3(-speed, 0, 0) * Time.deltaTime;

                if (backgrounds[i].gameObject.transform.position.x <
                    _leftPosX[i] * 1.8f)
                {
                    int nIndex = (i == 0) ? 1 : 0;

                    Vector3 nextPos = this.backgrounds[nIndex].gameObject.transform.position;
                    nextPos = new Vector3(
                        nextPos.x + this._rightPosX[i] + (this._rightPosX[i] / 2),
                        nextPos.y, nextPos.z);
                    this.backgrounds[i].gameObject.transform.position = nextPos;
                }
            }
        }
    }
}
