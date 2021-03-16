using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpdateSkillCooldownCommand : Command
{
    
    SkillComponent skill;
    int value;    

    public UpdateSkillCooldownCommand(SkillComponent skill, int value)
    {
        this.value = value;
        this.skill = skill;

    }

    public override void StartCommandExecution()
    {
        skill.UpdateSkillCooldown(value);

        Command.CommandExecutionComplete();

    }

}
