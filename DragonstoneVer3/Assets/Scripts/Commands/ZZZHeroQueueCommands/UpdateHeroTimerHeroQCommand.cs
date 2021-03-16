using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHeroTimerHeroQCommand : Command
{
    List<HeroTimer> htList;
    GameObject Target;
    
    public UpdateHeroTimerHeroQCommand(GameObject Target, List<HeroTimer> htList)
    {
        this.htList = htList;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        foreach(HeroTimer ht in htList)
        {
            ht.hero.gameObject.GetComponent<CardManager>().UpdateCardATB(ht.timerValuePercentage);
        }
        Target.GetComponent<HeroQueue>().CommandExecutionComplete();
    }
}
