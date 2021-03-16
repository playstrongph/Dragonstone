using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shred : SkillComponent
{
    GameObject specialEffect;
    GameObject Attacker, Target;

    CoroutineTree tree = new CoroutineTree();

    public override void UseSkill(GameObject Attacker, GameObject Target)
    {
        this.Attacker = Attacker;
        this.Target = Target;

        tree.AddRoot(UseSkillLogic());
        tree.Start();
       
    }

    public IEnumerator UseSkillLogic()
    {
        tree.AddCurrent(UseSkillCoroutine(Attacker, Target));    

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    

    IEnumerator UseSkillCoroutine(GameObject Attacker, GameObject Target)
    {                  
       //Active Skills Mandatory
       tree.AddCurrent(UseSkillPreviewCoroutine(Attacker, Target));   
       tree.AddCurrent(UseSkillAnimationCoroutine(Attacker, Target));  
       
       //Main
       tree.AddCurrent(DealAttackDamageCoroutine(Attacker, Target));      
       tree.AddCurrent(InflictStunCoroutine(Attacker, Target));
      
            
        //MANDATORY 
        tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
        tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      

        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

     
    
    //SKill Coroutines
    IEnumerator DealAttackDamageCoroutine(GameObject Attacker, GameObject Target)
    {   
        int damage = Random.Range(40,61);

        tree.AddCurrent(DealDamage(Attacker, Target, damage,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

   

    IEnumerator InflictStunCoroutine(GameObject Attacker, GameObject Target)
    {
       int debuffCounters = 3;

        tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.DEFENSE_BREAK, debuffCounters,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    


//Common Coroutines


    IEnumerator UseSkillAnimationCoroutine(GameObject Attacker, GameObject Target)    
    {
        base.UseSkillAnimation(Attacker, Target, this);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
 

    IEnumerator UseSkillPreviewCoroutine(GameObject Attacker, GameObject Target)
    {
        base.UseSkillPreview(Attacker, Target);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

   
}
