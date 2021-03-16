using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHeroInactiveHeroQCommand : Command
{
    HeroLogic hl;
    public SetHeroInactiveHeroQCommand(HeroLogic hl)
    {
        this.hl = hl;
    }

    public override void StartCommandExecution()
    {
        hl.GetComponent<CardManager>().HideActiveGlow();
        hl.GetComponent<CardManager>().SkillPanel.SetActive(false);
        hl.GetComponent<CardManager>().HeroPanel.SetActive(false);
        hl.GetComponent<HeroQueue>().CommandExecutionComplete();
    }
}
