using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : SkillEffect
{
    public int cooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        GetComponent<CardManager>().HideTauntFrame();

        GetComponent<CardManager>().TauntActiveGlowImage.enabled = false;
         GetComponent<CardManager>().TauntTargetGlowImage.enabled = false;
         
    }

    void OnEnable()
    {
        GetComponent<CardManager>().ShowTauntFrame();
        GetComponent<CardManager>().ActiveGlowImage.enabled = false;
    }
}
