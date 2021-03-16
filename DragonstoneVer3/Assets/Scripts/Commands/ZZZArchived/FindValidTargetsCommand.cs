using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindValidTargetsCommand : Command
{
    HeroLogic hero;

    public FindValidTargetsCommand(HeroLogic hero)
    {
        this.hero = hero;
    }

    public override void StartCommandExecution()
    {
        
    }

}
