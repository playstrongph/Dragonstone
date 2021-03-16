using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpdateHeroTimerCommand : Command
{
    List<HeroTimer> htList;

    public UpdateHeroTimerCommand(List<HeroTimer> htList)
    {
        this.htList = htList;
    }


    public override void StartCommandExecution()
    {
        foreach(HeroTimer ht in htList)
        {
            ht.hero.gameObject.GetComponent<CardManager>().UpdateCardATB(ht.timerValuePercentage);
        }

         Command.CommandExecutionComplete();

    }
}
