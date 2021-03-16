using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DemoScript : MonoBehaviour
{
    public Transform demoObject;
    public Transform target;
    public float tweenDuration = 0.5f;
    public int loopDuration = 2;

    public Ease animEase;
    public LoopType loopType;
    
    

    

    void Start()
    {        
        Transform transform = demoObject;

                
        //Basic Attack Animation
        // bool punched = false;
        // transform.DOMove(target.transform.position, tweenDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InBack).OnStepComplete(() =>
        //     {
        //         if(!punched)
        //             { 
        //                 target.transform.DOPunchPosition (target.transform.position/2 - transform.position/2 , 1f, 10, 0.5f, false);
        //                 punched = true;
        //             }    
                                           
        //     });

        //Basic Attack Animation using enums in Inspector
        bool punched = false;
        //transform.DOMove(target.transform.position, tweenDuration).SetLoops(loopDuration, loopType).SetEase(animEase).OnStepComplete(() =>

        transform.DOScale(target.transform.localScale*2, tweenDuration).SetLoops(loopDuration, loopType).SetEase(animEase).OnStepComplete
        (() =>
       
            {
                if(!punched)
                    { 
                        target.transform.DOPunchPosition (target.transform.position/2 - transform.position/2 , 1f, 10, 0.5f, false);
                        punched = true;
                    }    

               
                                           
            }

            
            
        );



    }

    public void Reset()
    {
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
