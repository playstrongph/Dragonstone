using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HideHeroHeroQCommand : Command
{
    GameObject hero;
    GameObject anim;

    
    float skullDuration = 1.5f;

    //DoShake Variables
    float doShakeDuration = 1f;
    float doShakeStrength = 10f;
    int doShakeVibrato = 10;
    float doShakeRandomness = 5f;
    bool doShakeSnapping = false;

    

    public HideHeroHeroQCommand(GameObject hero)
    {
        this.hero = hero;
    }

    public override void StartCommandExecution()
    {             
                
        Sequence s = DOTween.Sequence();

        
        s.Append( hero.transform.DOShakeRotation(doShakeDuration, doShakeStrength, doShakeVibrato, doShakeRandomness, doShakeSnapping) ); 
        
        s.AppendInterval(doShakeDuration-0.5f);
        s.AppendCallback(()=>{
            anim = GameObject.Instantiate(GlobalSettings.Instance.specialEffects[3], hero.transform.position, Quaternion.identity) as GameObject;
        });
        s.AppendInterval(skullDuration);        
        s.AppendCallback(()=>{

            GameObject.Destroy(anim);
            hero.GetComponent<CardManager>().HideHero();
            hero.GetComponent<HeroQueue>().CommandExecutionComplete();

        });


        
    }
}
