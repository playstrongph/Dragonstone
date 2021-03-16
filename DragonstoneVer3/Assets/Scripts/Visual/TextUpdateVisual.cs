using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextUpdateVisual : MonoBehaviour
{
    public Text amountText;    
    public float animDuration = 0.12f;
    public float localScale = 1.5f;
    public int loopsCount = 4;

    

    // public int currValue;
    // public int maxValue;
    
    public void TextUpdateAnimation(int currValue, int maxValue, Vector3 origScale)
    {
        //set correct value
        amountText.text = currValue.ToString();
        

        var s = DOTween.Sequence();

        s.AppendCallback(()=>{
           transform.DOScale(this.transform.localScale*localScale, animDuration)
           .SetLoops(loopsCount, LoopType.Yoyo).SetEase(Ease.InOutQuad);

            //change color per value
            SetTextColor(currValue, maxValue, amountText);
        }).AppendInterval(2f*animDuration)
        
        .OnComplete(()=>{
            this.transform.localScale = origScale;
             Command.CommandExecutionComplete();
           
        });

        // var s1 = DOTween.Sequence();
        // s1.AppendInterval(2*animDuration).OnComplete(()=>{

        //     Command.CommandExecutionComplete();
        // });
        
            //Command.CommandExecutionComplete();
        
        

    }

    public void SetTextColor (int currValue, int maxValue, Text amountText)
    {
        if(currValue < maxValue)
        amountText.color = Color.red;
        else if(currValue > maxValue)
        amountText.color = Color.green;
        else if(currValue == maxValue)
        amountText.color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
}
