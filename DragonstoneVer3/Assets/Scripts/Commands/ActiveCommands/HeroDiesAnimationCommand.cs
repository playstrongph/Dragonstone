using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeroDiesAnimationCommand : Command
{
    GameObject hero;
    GameObject anim;

    
    // float skullAnimDuration = 1.5f;

    // //DoShake Variables
    // float doShakeAnimDuration = 1f;
    float doShakeStrength = 10f;
    int doShakeVibrato = 10;
    float doShakeRandomness = 5f;
    bool doShakeSnapping = false;
  

    public HeroDiesAnimationCommand(GameObject hero)
    {
        this.hero = hero;
    }

    public override void StartCommandExecution()
    {             
        float skullAnimDuration = longAnimationDuration;

        //DoShake Variables
        float doShakeAnimDuration = shortAnimationDuration;       
        
        
        Sequence s = DOTween.Sequence();

        
        s.Append( hero.transform.DOShakeRotation(doShakeAnimDuration, doShakeStrength, doShakeVibrato, doShakeRandomness, doShakeSnapping) ); 
        

        s.AppendCallback(()=>{
            anim = GameObject.Instantiate(GlobalSettings.Instance.specialEffects[3], hero.transform.position, Quaternion.identity) as GameObject;
        });
        s.AppendInterval(skullAnimDuration);        
        s.AppendCallback(()=>{

            GameObject.Destroy(anim);
            hero.GetComponent<CardManager>().HideHero();           

        });

        Command.CommandExecutionComplete();
        
    }
}
