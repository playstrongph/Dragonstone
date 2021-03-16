using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSingleHeroTimerHeroQCommand : Command
{
    GameObject Target;
    
    public UpdateSingleHeroTimerHeroQCommand(GameObject Target)
    {
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        
        Target.GetComponent<CardManager>().UpdateCardATB(Target.GetComponent<HeroLogic>().heroTimer.timerValuePercentage);        
        
        Target.GetComponent<HeroQueue>().CommandExecutionComplete();
    }
}
