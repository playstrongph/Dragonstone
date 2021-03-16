using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHeroActiveHeroQCommand : Command
{
    HeroLogic hl;
    public SetHeroActiveHeroQCommand(HeroLogic hl)
    {
        this.hl = hl;
    }

    public override void StartCommandExecution()
    {
        hl.GetComponent<CardManager>().ShowActiveGlow();
        hl.GetComponent<CardManager>().SkillPanel.SetActive(true);
        hl.GetComponent<CardManager>().HeroPanel.SetActive(true);

        hl.GetComponent<HeroQueue>().CommandExecutionComplete();
    }
}
