using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetHeroInactiveCommand : Command
{
    HeroLogic hl;

   
    public SetHeroInactiveCommand(HeroLogic hl)
    {
        this.hl = hl;
    }

    public override void StartCommandExecution()
    {
        hl.GetComponent<CardManager>().HideActiveGlow();
        hl.GetComponent<CardManager>().SkillPanel.SetActive(false);
        hl.GetComponent<CardManager>().HeroPanel.SetActive(false);

        Command.CommandExecutionComplete();
       
    }
}
