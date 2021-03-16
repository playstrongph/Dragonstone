using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UseSkillPreviewCommand : Command  //Sequential By Default
{
    SkillComponent skill;
    GameObject useSkillPreview;
    float animTime = 1f;
    float displayTime = 1.5f;    

    public UseSkillPreviewCommand(SkillComponent skill)
    {
        this.skill = skill;
    }

    public override void StartCommandExecution()
    {
        Sequence s = DOTween.Sequence();

        Sequence s1 = DOTween.Sequence();

        s.AppendCallback(()=>{

           
           useSkillPreview = GameObject.Instantiate(skill.GetComponentInChildren<SkillHoverPreview>().previewGameObject, skill.transform.position, Quaternion.identity) as GameObject;
           
           useSkillPreview.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f );
           
           useSkillPreview.SetActive(true);
           useSkillPreview.transform.DOMove(GlobalSettings.Instance.skillPreviewLocation.transform.position, animTime);
           useSkillPreview.transform.DOScale(useSkillPreview.transform.localScale*30f, animTime);


        });

        s.AppendInterval(displayTime);   

        s.AppendCallback(()=>{ GameObject.Destroy(useSkillPreview);});       
                
        s1.AppendInterval(animTime).OnComplete(()=>{
            Command.CommandExecutionComplete();
        });

        

    }




    

}
