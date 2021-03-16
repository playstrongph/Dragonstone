using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Serenity : SkillComponent
{

    

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

       tree.AddCurrent(AddCrippleDebuffCoroutine(Attacker, Target,tree));   

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

    IEnumerator AddCrippleDebuffCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.CRIPPLE, 1,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

   

}
