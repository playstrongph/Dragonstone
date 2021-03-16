using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SilverPlatedBullet : SkillComponent
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
       tree.AddCurrent(DealDamageCoroutine(Attacker, Target));
       tree.AddCurrent(Remove2BuffsCoroutine(Attacker, Target));
       
      
            
        //MANDATORY 
        tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
        tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      

        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

     
    
    //SKill Coroutines
    IEnumerator DealDamageCoroutine(GameObject Attacker, GameObject Target)
    {   
        int damage = UnityEngine.Random.Range(20,31);

        tree.AddCurrent(DealDamage(Attacker, Target, damage,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator Remove2BuffsCoroutine(GameObject Attacker, GameObject Target)
    {        
       List<BuffComponent> buffs = BuffSystem.Instance.GetAllBuffsOfType(Target.GetComponents<BuffComponent>(), BuffType.BUFF);

       int buffCount = buffs.Count;
       if(buffCount < 0) buffCount = 0;

       switch(buffCount)
       {
           case(0):
           break;

           case(1):
           tree.AddCurrent(RemoveRandomBuff(Attacker, Target, tree));
           break;

           default:
           tree.AddCurrent(RemoveRandomBuff(Attacker, Target, tree));
           tree.AddCurrent(RemoveRandomBuff(Attacker, Target, tree));
           break;

       }

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
