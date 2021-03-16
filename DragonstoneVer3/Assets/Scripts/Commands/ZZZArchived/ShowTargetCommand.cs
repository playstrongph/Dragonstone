using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTargetCommand : Command
{
    GameObject target;
    public ShowTargetCommand(GameObject target)
    {
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        target.GetComponent<HeroLogic>().isValidTarget = true;
        target.GetComponent<CardManager>().ShowTargetGlow();
        Command.CommandExecutionComplete();
    }
}
