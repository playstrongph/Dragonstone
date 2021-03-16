using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AddBuffSymbolCommand : Command
{

   //Note:  NO Internal delay or animations required for this command.
   //Creates the BuffCard on top of the hero

   BuffComponent bc;
   int buffCounter;
   GameObject target;

  

   public AddBuffSymbolCommand(BuffComponent bc, int buffCounter, GameObject target)
   {
       this.bc = bc;
       this.buffCounter = buffCounter;
       this.target = target;       
   }

 

    public override void StartCommandExecution()
    {
        GameObject buffCard = GameObject.Instantiate(GlobalSettings.Instance.BuffCardPrefab, target.GetComponent<CardManager>().BuffPanel.transform) as GameObject;
        //Set a reference for the instantiated object to the buffComponent
        bc.buffVisualObject = buffCard;

        buffCard.transform.SetParent(target.GetComponent<CardManager>().BuffPanel.transform);        
        buffCard.GetComponent<BuffCardManager>().BuffText.text = buffCounter.ToString();
        buffCard.GetComponent<BuffCardManager>().BuffImage.sprite = bc.buffAsset.buffBasicInfo.thumbnail;
        buffCard.name = bc.buffAsset.buffBasicInfo.buffName.ToString();

        Command.CommandExecutionComplete();
    }




}
