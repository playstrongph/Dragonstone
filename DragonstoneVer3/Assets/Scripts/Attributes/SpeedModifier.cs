using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : MonoBehaviour
{

    public int amount;
    public BuffComponent buff;
    public SkillComponent skill;

    public SpeedModifier (int amount, BuffComponent buff = null, SkillComponent skill = null)
    {
        this.amount = amount;
        this.buff  = buff;
        this.skill = skill;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
