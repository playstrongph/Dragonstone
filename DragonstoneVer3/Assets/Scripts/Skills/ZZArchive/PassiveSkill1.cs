using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PassiveSkill1 : SkillComponent
{
    // Start is called before the first frame update

    SkillEffect genericPassiveSkill;

     SkillEffect genericPassiveSkill2;

    

    public override void RegisterEventEffect()
    {
        UsePassiveSkill();

        //genericPassiveSkill = heroLogic.gameObject.AddComponent<Taunt>();
        
    }

    public override void UnRegisterEventEffect()
    {
        //DelegateManager.Instance.e_StartOfGame -= UsePassiveSkill;

        DisablePassiveSkill();
        

    }

    public override void UsePassiveSkill()
    {
        genericPassiveSkill = heroLogic.gameObject.AddComponent<Taunt>();
        genericPassiveSkill2 = heroLogic.gameObject.AddComponent<DebuffImmunity>();
        
    }

    public void DisablePassiveSkill()
    {
        if(genericPassiveSkill != null)
        Destroy(genericPassiveSkill);

        if(genericPassiveSkill2 != null)
        Destroy(genericPassiveSkill2);
    }

    
}
