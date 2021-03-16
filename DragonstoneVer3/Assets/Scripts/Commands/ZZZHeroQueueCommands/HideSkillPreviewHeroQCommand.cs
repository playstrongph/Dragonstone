using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSkillPreviewHeroQCommand : Command
{
    
    SkillHoverPreview skillHoverPreview;
    GameObject Target;

    public HideSkillPreviewHeroQCommand(GameObject Target, SkillHoverPreview skillHoverPreview)
    {
        this.skillHoverPreview = skillHoverPreview;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        skillHoverPreview.HidePreview();
        Target.GetComponent<HeroQueue>().CommandExecutionComplete();
    }


}
