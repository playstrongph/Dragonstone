using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CreateHealEffectCommand : Command
{
    
    int amount;
   GameObject target;

   Command internalCommand;

    public CreateHealEffectCommand(int amount, GameObject target)
    {
        this.amount = amount;
        this.target = target;
    }


    public override void StartCommandExecution()
    {

        var healPrefab = SkillSystem.Instance.HealEffectPrefab;        

        VisualSystem.Instance.CreateFloatingText("HEAL", target, Color.yellow);

        VisualSystem.Instance.GenericSkillEffectAnimation(target, GenericSkillName.HEAL);       
      
        GameObject newHealEffect = (GameObject)GameObject.Instantiate(healPrefab, target.transform.position, Quaternion.identity);      
        newHealEffect.transform.SetParent(target.transform);

        HealEffect he = newHealEffect.GetComponent<HealEffect>();        

        he.AmountText.text = "+" + (amount).ToString();
        he.HealImage.color = Color.green;

        he.StartCoroutine(he.ShowHealEffect());  //has auto-destruct
       
        Command.CommandExecutionComplete();
        
    }


   



}
