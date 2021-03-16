using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffAnimationManager : MonoBehaviour
{

    public BuffName buffName;

    public float buffAnimDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        buffAnimDuration = GetComponent<ParticleSystem>().main.duration;
    }

    // Update is called once per frame
    
}
