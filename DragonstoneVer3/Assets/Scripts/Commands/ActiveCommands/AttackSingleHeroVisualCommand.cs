using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackSingleHeroVisualCommand : Command
{
    GameObject Attacker;
    GameObject Target;

    

    bool punched = false;
    
    int tweenLoops = 2;

    float commandDelay = 0.7f;
    float animDuration = 0.7f;

    public AttackSingleHeroVisualCommand(GameObject Attacker, GameObject Target)
    {
        this.Attacker = Attacker;
        this.Target = Target;
        
    }

    
    public override void StartCommandExecution()
    {
        
        if (Attacker != null && Target != null)
        {              
            //Attacker.GetComponent<HeroAttackVisual>().AttackTarget(Target, 1.5f*shortAnimationDuration);   
        
        Sequence s = DOTween.Sequence();
                
        s.AppendCallback(()=>Attacker.transform.DOMove(Target.transform.position, animDuration).SetLoops(tweenLoops, LoopType.Yoyo).SetEase(Ease.InBack))
        
        .OnStepComplete(() =>
            {
                //Event here is same as e_BeforeHeroTakesAtkDamage                
                
                if(!punched)
                    { 
                        Target.transform.DOPunchPosition (Target.transform.position/7 - Attacker.transform.position/7 , 1f, 5, 0.5f, false);
                        //Target.transform.DOShakeRotation(1f, 45, 5, 2);
                        punched = true;
                    }
                
                                                 
            });

        s.AppendInterval(commandDelay);
        

        // .OnComplete(()=>{ Command.CommandExecutionComplete();});  

        Command.CommandExecutionComplete();        




        }             
      
    }

    
}
