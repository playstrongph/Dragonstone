using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowHeroHeroQCommand : Command
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

    float delay = 1f;

    

    public ShowHeroHeroQCommand(GameObject hero)
    {
        this.hero = hero;
    }

    public override void StartCommandExecution()
    {             
                
        Sequence s = DOTween.Sequence();

        s.AppendInterval(delay);  //Delay to show hero dies first
        
        s.AppendCallback(()=>{

           
            hero.GetComponent<CardManager>().ShowHero();

            hero.GetComponent<HeroLogic>().RestoreHeroBaseStats();  //this is where the skillpanel is hidden
           
            

             
            
        });        
        
        //s.Append( hero.transform.DOShakeRotation(doShakeDuration, doShakeStrength, doShakeVibrato, doShakeRandomness, doShakeSnapping) ); 
        
        //s.AppendInterval(doShakeDuration-0.5f);

        // s.AppendCallback(()=>{
        //     anim = GameObject.Instantiate(GlobalSettings.Instance.specialEffects[3], hero.transform.position, Quaternion.identity) as GameObject;

        // });

        //s.AppendInterval(skullDuration);        

        s.AppendCallback(()=>{

           
            //GameObject.Destroy(anim);

            hero.GetComponent<HeroQueue>().CommandExecutionComplete();

        });


        
    }
}
