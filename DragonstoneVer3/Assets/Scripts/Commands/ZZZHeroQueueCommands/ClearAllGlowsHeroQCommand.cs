using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAllGlowsHeroQCommand : Command
{
    List<GameObject> targets;
    public ClearAllGlowsHeroQCommand(List<GameObject> targets)
    {
        this.targets = targets;
    }

    public override void StartCommandExecution()
    {
        foreach(GameObject go in targets)
        {
            go.GetComponent<HeroLogic>().isValidTarget = false;
            go.GetComponent<CardManager>().ClearAllGlows();
            go.GetComponent<HeroQueue>().CommandExecutionComplete();
        }
        
    }
}
