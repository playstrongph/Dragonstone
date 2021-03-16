using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIFESTEAL : BuffComponent
{
        
    
    public int value;  
    public float lifeStealFactor = 0.5f;
    int reduceCounter = 1;

    GameObject Attacker, Target;

    public override void RegisterEventEffect()
    {     
        heroEvents.e_AfterHeroAttacks += BuffEventEffect;
    }

    public override void UnRegisterEventEffect ()
    {
        heroEvents.e_AfterHeroAttacks -= BuffEventEffect;
    }
    
    public void BuffEventEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Attacker, Target, tree));
    }


    public override IEnumerator CauseBuffEffectEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        

         if(this != null) // In case buff is destroyed
        {
            value = Mathf.FloorToInt(AttackSystem.Instance.finalDamage*lifeStealFactor);
            
            tree.AddCurrent(Heal(Attacker, value, tree)); 
            
            VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Attacker, Color.yellow);
           
           
            tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
         
        
    }    
     
}
