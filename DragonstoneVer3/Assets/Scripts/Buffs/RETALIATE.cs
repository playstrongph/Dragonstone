 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RETALIATE : BuffComponent
{
      
    int reduceCounter = 1;
    GameObject Attacker;
    GameObject Target;

    public override void RegisterEventEffect()
    {
        heroEvents.e_AfterHeroIsAttacked += BuffEventEffect;            
    }

    public override void UnRegisterEventEffect ()
    {
        heroEvents.e_AfterHeroIsAttacked -= BuffEventEffect;              
    }

    public void BuffEventEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Attacker, Target, tree));
    }

    public override IEnumerator CauseBuffEffectEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
         if(this != null)
        {
            int damage = heroLogic.Attack;    

            tree.AddCurrent(DealDamageNoAttacker(Attacker, damage,tree));

            BuffEffectAnimation(Attacker, Target);        

            tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     

        }  

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    }
   
    public void BuffEffectAnimation (GameObject Attacker, GameObject Target)
    {          
        VisualSystem.Instance.BuffEffectAnimation(Attacker, this);  
        VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.yellow);
    }

     
}
