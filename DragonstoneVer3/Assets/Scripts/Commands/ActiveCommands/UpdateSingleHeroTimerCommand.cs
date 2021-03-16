using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpdateSingleHeroTimerCommand : Command
{
    HeroLogic heroLogic;
    
    public UpdateSingleHeroTimerCommand(HeroLogic heroLogic)
    {
        this.heroLogic = heroLogic;
    }


    public override void StartCommandExecution()
    {
        
        heroLogic.GetComponent<CardManager>().UpdateCardATB(heroLogic.heroTimer.timerValuePercentage);
        
        Command.CommandExecutionComplete();

    }
}
