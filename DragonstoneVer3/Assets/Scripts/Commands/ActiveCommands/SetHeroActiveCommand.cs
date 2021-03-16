using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetHeroActiveCommand : Command
{
    HeroLogic hl;

    float commandDelay = 0f;
    public SetHeroActiveCommand(HeroLogic hl)
    {
        this.hl = hl;
    }

    public SetHeroActiveCommand(HeroLogic hl, float commandDelay)
    {
        this.hl = hl;
        this.commandDelay = commandDelay;
    }

    public override void StartCommandExecution()
    {
        
        //TargetSystem.Instance.FindValidTargets(hl); //Command - may be better to call outside

        hl.GetComponent<CardManager>().ShowActiveGlow();
        hl.GetComponent<CardManager>().SkillPanel.SetActive(true); 
        hl.GetComponent<CardManager>().HeroPanel.SetActive(true);  

        Command.CommandExecutionComplete();

        
    }
}
