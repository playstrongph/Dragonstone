using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHeroPropertyCommand : Command
{
    GameObject Target;
    HeroProperties prop;
    int value;
    public SetHeroPropertyCommand(GameObject Target, HeroProperties prop, int value)
    {
        this.Target = Target;
        this.prop = prop;
        this.value = value;
    }

    public override void StartCommandExecution()
    {
        Debug.Log("Set Hero Property Command" +prop.ToString());
        
        //Target.GetComponent<HeroLogic>().SetHeroPropertyVisual(prop, value);
        Command.CommandExecutionComplete();
    }
}
