using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRAND : BuffComponent
{
    
    int reduceCounter = 1;
    int loseLife = 2;
    //bool enableBrand = true;
    int finalDamage;

    GameObject Target;

    public override void RegisterEventEffect()
    {
        ///<TODO>
        heroEvents.e_AfterHeroTakesDamage += BuffEventEffect;
    }

    public override void UnRegisterEventEffect()
    {
        ///<TODO>
        heroEvents.e_AfterHeroTakesDamage -= BuffEventEffect;
    }

    public void BuffEventEffect(GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Target,tree));
    }

    public override IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {       
        if(this != null)  // In case buff is destroyed
        {
           VisualSystem.Instance.Delay(0.5f); //to allow first damage to fade a bit

           VisualSystem.Instance.DebuffEffectAnimation(Target, this);        
           VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.magenta);  

           

           tree.AddCurrent(AttackSystem.Instance.LoseLife(Target, loseLife, tree));          

            tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    







    

    

     
}
