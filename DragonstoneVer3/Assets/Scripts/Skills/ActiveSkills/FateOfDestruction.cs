using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FateOfDestruction : SkillComponent
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
       
       tree.AddCurrent(DealAttackDamageCoroutine(Attacker, Target));
       tree.AddCurrent(InflictPoisonDebuffCoroutine(Attacker, Target));

       tree.AddCurrent(InflictStunDebuffCoroutine(Attacker, Target));
       tree.AddCurrent(TakeExtraTurnCoroutine(Attacker));      
            
        //MANDATORY 
        tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
        tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      

        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

     
    
    //SKill Coroutines
    IEnumerator DealAttackDamageCoroutine(GameObject Attacker, GameObject Target)
    {   
        int damage = Attacker.GetComponent<HeroLogic>().Attack;

        tree.AddCurrent(DealDamage(Attacker, Target, damage,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator InflictPoisonDebuffCoroutine(GameObject Attacker, GameObject Target)
    {
        int poisonCounters = 2;

        tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.POISON, poisonCounters,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator InflictStunDebuffCoroutine(GameObject Attacker, GameObject Target)
    {
       int stunCounter = 1;

        tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.STUN, stunCounter,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator TakeExtraTurnCoroutine(GameObject Attacker)
    {
        tree.AddCurrent(ExtraTurn(Attacker,tree));

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
