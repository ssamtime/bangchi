using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillUI : MonoBehaviour
{
    public Image imgSkill;
    public Text skillName;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CardUISet(Skill skill)
    {
        imgSkill.sprite = skill.skillImage;
        skillName.text = skill.skillName;
    }

}
