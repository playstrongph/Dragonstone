using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchOfSeduction : SkillComponent
{


     int deadlyCharmCooldown = 1;


     public override IEnumerator UsePreAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
       
       var skillPanel = GetComponentInParent<SkillPanelManager>();
       var deadlyCharmSkill = skillPanel.GetComponentInChildren<DeadlyCharm>();

       deadlyCharmCooldown =  deadlyCharmSkill.currCoolDown;

      tree.CorQ.CoroutineCompleted();
      yield return null;
    }

    public override IEnumerator UsePostAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
      if(heroLogic.ChanceOK())     
      tree.AddCurrent(UsePassiveSkillCoroutine(Attacker, Target,tree));    

      tree.CorQ.CoroutineCompleted();
      yield return null;
    }

    IEnumerator UsePassiveSkillCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
       tree.AddCurrent(FloatingTextCoroutine(Attacker,tree));       

       tree.AddCurrent(RemoveBuffCoroutine(Attacker, Target,tree));        

       tree.AddCurrent(AddDebuffCoroutine(Attacker, Target,tree));   

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    
    IEnumerator FloatingTextCoroutine(GameObject Attacker, CoroutineTree tree)
    {
        string thisSkillName = this.skillAsset.skillBasicInfo.skillName.ToString();
        VisualSystem.Instance.CreateFloatingText(thisSkillName, Attacker, Color.yellow);       

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator RemoveBuffCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(RemoveRandomBuff(Attacker, Target,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator AddDebuffCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
              
        if(deadlyCharmCooldown == 0)
        {
            tree.AddCurrent(AddDebuffIgnoreImmunity(Attacker, Target, BuffName.ATTACK_DOWN, 2,tree));    
        }else
        tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.ATTACK_DOWN, 2,tree));
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

   
   

}
