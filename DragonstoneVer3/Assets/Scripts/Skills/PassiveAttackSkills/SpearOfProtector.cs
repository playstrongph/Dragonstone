using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpearOfProtector : SkillComponent
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

       tree.AddCurrent(InflictSlowCoroutine(Attacker, Target,tree));        

       tree.AddCurrent(DealDamageCoroutine(Attacker, Target,tree));   

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

    IEnumerator InflictSlowCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        int debuffCounters = 2;

        tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.SLOW, debuffCounters , tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator DealDamageCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        int damage = UnityEngine.Random.Range(5,16);

        tree.AddCurrent(DealDamage(Attacker, Target, 12 ,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

   

}
