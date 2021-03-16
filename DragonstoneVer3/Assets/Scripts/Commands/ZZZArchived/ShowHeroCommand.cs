using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowHeroCommand : Command
{
    GameObject hero;

    // GameObject anim;

    
    // float skullDuration = 1.5f;

    // float animDuration = 1f;

    // //DoShake Variables
    // float doShakeDuration = 1f;
    // float doShakeStrength = 10f;
    // int doShakeVibrato = 10;
    // float doShakeRandomness = 5f;
    // bool doShakeSnapping = false;

    float delay = 3f;

    

    public ShowHeroCommand(GameObject hero)
    {
        this.hero = hero;
    }

    public override void StartCommandExecution()
    {             
                
        Sequence s = DOTween.Sequence();

        s.AppendInterval(delay);  
        
        s.AppendCallback(()=>{

           
            hero.GetComponent<CardManager>().ShowHero();
            hero.GetComponent<HeroLogic>().RestoreHeroBaseStats();  //this is where the skillpanel is hidden
            
        })                
           
        .OnComplete(()=>{
            Command.CommandExecutionComplete();
        });


        
    }
}
