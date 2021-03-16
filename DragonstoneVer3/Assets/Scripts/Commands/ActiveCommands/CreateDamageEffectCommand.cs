using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CreateDamageEffectCommand : Command
{
    
    int amount;
    GameObject target;
 
    public CreateDamageEffectCommand(int amount, GameObject target)
    {
        this.amount = amount;       
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        
        GameObject newDamageEffect = GameObject.Instantiate(GlobalSettings.Instance.DamageEffectPrefab, target.transform.position, Quaternion.identity) as GameObject;        
        
        newDamageEffect.transform.SetParent(target.transform);

        DamageEffect de = newDamageEffect.GetComponent<DamageEffect>();

        if (amount < 0)
        amount = 0;
        
        de.AmountText.text = "-"+amount.ToString();
  
        // start a coroutine to fade away and delete this effect after a certain time
        de.StartCoroutine(de.ShowDamageEffect());        

        Command.CommandExecutionComplete();




        
    }

}
