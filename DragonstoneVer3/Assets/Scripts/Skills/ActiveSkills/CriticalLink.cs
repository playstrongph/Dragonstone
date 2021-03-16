using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CriticalLink : SkillComponent
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

       tree.AddCurrent(ReplaceBuffsWithDebuffsCoroutine(Attacker, Target));
       
      
            
        //MANDATORY 
        tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
        tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      

        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

     
    
    //SKill Coroutines
    IEnumerator DealDamageCoroutine(GameObject Attacker, GameObject Target)
    {   
        int damage = UnityEngine.Random.Range(20,41);

        tree.AddCurrent(DealDamage(Attacker, Target, damage,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator ReplaceBuffsWithDebuffsCoroutine(GameObject Attacker, GameObject Target)
    {        
       
        var allbuffs = Target.GetComponents<BuffComponent>();
        var buffsList = BuffSystem.Instance.GetAllBuffsOfType(allbuffs, BuffType.BUFF);

        if(buffsList !=null)
        {
            foreach(var buff in buffsList)
            {
                int debuffCounter = buff.buffCounter;

                tree.AddCurrent(RemoveSpecificBuff(Target, buff.buffAsset.buffBasicInfo.buffName, tree));
                tree.AddCurrent(VisualDelay());
                tree.AddCurrent(AddRandomDebuff(Attacker, Target, debuffCounter, tree));
            }

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

    IEnumerator VisualDelay()
    {
        VisualSystem.Instance.Delay(0.25f);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }



   
}
