using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PassiveSkillCD1 : SkillComponent
{
    // Start is called before the first frame update

    SkillComponent genericPassiveSkill;

     SkillComponent genericPassiveSkill2;

    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void RegisterEventEffect()
    {
        
        //heroEvents.e_AfterHeroAttacks += UsePassiveSkill;

        //genericPassiveSkill = heroLogic.gameObject.AddComponent<Taunt>();
        
    }

    public override void UnRegisterEventEffect()
    {
        //DelegateManager.Instance.e_StartOfGame -= UsePassiveSkill;

        
        

    }

    // public override void UsePostAttackPassiveSkill(GameObject Attacker, GameObject Target)
    // {
    //     if(currCoolDown == 0)
    //     {
    //         //Heal(heroLogic.gameObject, heroLogic.gameObject, 10);
    //         VisualSystem.Instance.CreateFloatingText("Passive Skill CD1", heroLogic.gameObject, Color.yellow);

    //         currCoolDown = baseCoolDown;
    //         VisualSystem.Instance.UpdateSkillCooldown(this, currCoolDown);
            
    //     }
        
        
    // }

    
    
}
