using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HeroAttackVisual : MonoBehaviour 
{

    

    void Awake()
    {

    }

    public void AttackTarget(GameObject target, float animDuration)
    {

        //SFX before attacking      
        
        // bool punched = false;
        // animDuration = 0.7f;
        // int tweenLoops = 2;

        // Sequence s = DOTween.Sequence();
                
        // s.AppendCallback(()=>transform.DOMove(target.transform.position, animDuration).SetLoops(tweenLoops, LoopType.Yoyo).SetEase(Ease.InBack))
        
        // .OnStepComplete(() =>
        //     {
        //         //Event here is same as e_BeforeHeroTakesAtkDamage                
                
        //         if(!punched)
        //             { 
        //                 target.transform.DOPunchPosition (target.transform.position/7 - transform.position/7 , 1f, 5, 0.5f, false);
        //                 //target.transform.DOShakeRotation(1f, 45, 5, 2);
        //                 punched = true;
        //             }
                
                                                 
        //     });

        // s.AppendInterval(animDuration);

        // s.AppendCallback(()=>{ Command.CommandExecutionComplete();});  
        
    }

    public void AttackTargetCriticalStrike(GameObject target, float animDuration)
    {

        //SFX before attacking      
        
        bool punched = false;
        animDuration = 0.7f;
        int tweenLoops = 2;

        Sequence s = DOTween.Sequence();
                
        s.AppendCallback(()=>transform.DOMove(target.transform.position, animDuration).SetLoops(tweenLoops, LoopType.Yoyo).SetEase(Ease.InBack))
        
        .OnStepComplete(() =>
            {
                //Event here is same as e_BeforeHeroTakesAtkDamage                
                
                if(!punched)
                    { 
                        target.transform.DOPunchPosition (target.transform.position/2 - transform.position/2 , 1f, 10, 0.5f, false);
                        //target.transform.DOShakeRotation(1f, 45, 5, 2);
                        punched = true;
                    }                                 
            });

        s.AppendInterval(animDuration);

        s.AppendCallback(()=>{ Command.CommandExecutionComplete();});  
        
    }

    // public void AttackTargetCriticalStrikeHeroQ(GameObject target)
    // {

    //     //SFX before attacking      
        
    //     bool punched = false;
    //     animDuration = 0.7f;
    //     int tweenLoops = 2;

    //     Sequence s = DOTween.Sequence();
                
    //     s.AppendCallback(()=>transform.DOMove(target.transform.position, animDuration).SetLoops(tweenLoops, LoopType.Yoyo).SetEase(Ease.InBack))
        
    //     .OnStepComplete(() =>
    //         {
    //             //Event here is same as e_BeforeHeroTakesAtkDamage                
                
    //             if(!punched)
    //                 { 
    //                     target.transform.DOPunchPosition (target.transform.position/2 - transform.position/2 , 1f, 10, 0.5f, false);
    //                     //target.transform.DOShakeRotation(1f, 45, 5, 2);
    //                     punched = true;
    //                 }                                 
    //         });

    //     s.AppendInterval(animDuration);

    //     s.AppendCallback(()=>{ this.gameObject.GetComponent<HeroQueue>().CommandExecutionComplete();});  
        
    // }

    // public void AttackTargetHeroQ(GameObject target)
    // {

    //     //SFX before attacking      
        
    //     bool punched = false;
    //     animDuration = 0.7f;
    //     int tweenLoops = 2;

    //     Sequence s = DOTween.Sequence();
                
    //     s.AppendCallback(()=>transform.DOMove(target.transform.position, animDuration).SetLoops(tweenLoops, LoopType.Yoyo).SetEase(Ease.InBack))
        
    //     .OnStepComplete(() =>
    //         {
    //             //Event here is same as e_BeforeHeroTakesAtkDamage                
                
    //             if(!punched)
    //                 { 
    //                     target.transform.DOPunchPosition (target.transform.position/7 - transform.position/7 , 1f, 5, 0.5f, false);
    //                     //target.transform.DOShakeRotation(1f, 45, 5, 2);
    //                     punched = true;
    //                 }                                 
    //         });

    //     s.AppendInterval(animDuration);

    //     s.AppendCallback(()=>{ this.gameObject.GetComponent<HeroQueue>().CommandExecutionComplete();});  
        
    // }
        
}
