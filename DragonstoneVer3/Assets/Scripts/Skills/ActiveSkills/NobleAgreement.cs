using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NobleAgreement : SkillComponent
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
             
       //Main
       tree.AddCurrent(AddCriticalStrikeSkillEffect(Attacker, tree));
       tree.AddCurrent(Attacker.GetComponent<BasicAttack>().SkillUseSkillLogic(Attacker, Target, tree));       
       tree.AddCurrent(VisualDelay(0.2f));
       tree.AddCurrent(Attacker.GetComponent<BasicAttack>().SkillUseSkillLogic(Attacker, Target, tree));
       tree.AddCurrent(RemoveCriticalStrikeSkillEffect(Attacker, tree));
      
            
        //MANDATORY 
        tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
        tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      

        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator AddCriticalStrikeSkillEffect(GameObject Attacker, CoroutineTree tree)
    {
        Attacker.AddComponent<CriticalStrike>();

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator RemoveCriticalStrikeSkillEffect(GameObject Attacker, CoroutineTree tree)
    {
        Destroy(Attacker.GetComponent<CriticalStrike>());

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

     
    
    //SKill Coroutines
    


//Common Coroutines


   
 

    IEnumerator UseSkillPreviewCoroutine(GameObject Attacker, GameObject Target)
    {
        base.UseSkillPreview(Attacker, Target);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator VisualDelay(float delay)
    {
        VisualSystem.Instance.Delay(delay);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }



   
}
