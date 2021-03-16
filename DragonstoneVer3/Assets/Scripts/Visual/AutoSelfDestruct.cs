using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoSelfDestruct : MonoBehaviour
{

    void OnEnable()
    {

    }
    
    public float timer;  //timer is for animations you want to specify the specific duration
    public bool specificTime = false;    
    
    void Start()  //convert this to switch
    {
        if(specificTime)
        SelfDestructGivenTime();
        
        else 
        SelfDestructParticle();
    }

    void SelfDestructGivenTime()
    {
        
        Sequence s = DOTween.Sequence();

        s.AppendInterval(timer);
        s.AppendCallback(()=>{        

                
                GameObject.Destroy(this.gameObject);

        });

    }

    void SelfDestructParticle()
    {
        
        if(GetComponent<ParticleSystem>() != null)
        {
            float duration = GetComponent<ParticleSystem>().main.duration;
        
            Sequence s = DOTween.Sequence();

            s.AppendInterval(duration);
            s.AppendCallback(()=>{

            
            GameObject.Destroy(this.gameObject);

        });
        
        }else
        Debug.Log("No Particle System Component Attached");
        
        

    }

    
}
