using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTargetCommand : Command
{
    GameObject target;
    public HideTargetCommand(GameObject target)
    {
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        target.GetComponent<HeroLogic>().isValidTarget = false;
        target.GetComponent<CardManager>().HideTargetGlow();
        Command.CommandExecutionComplete();
    }
}
