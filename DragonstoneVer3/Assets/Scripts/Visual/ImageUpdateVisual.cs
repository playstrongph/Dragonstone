using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageUpdateVisual : MonoBehaviour
{
    //public Text amountText;    
    
    public Image iconImage;
    public float animDuration = 0.12f;
    public float localScale = 1.5f;
    public int loopsCount = 4;

    // public int currValue;
    // public int referenceValue;
    
    public void ImageUpdateAnimation(int currValue, int referenceValue)
    {
        //set correct value
        //amountText.text = currValue.ToString();
        
        transform.DOScale(this.transform.localScale*localScale, animDuration)
        .SetLoops(loopsCount, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        //change color per value
        SetImageColor(currValue, referenceValue);

        

    }

    public void SetImageColor (int currValue, int referenceValue)
    {
        if(currValue < referenceValue)
        iconImage.color = Color.red;
        else if(currValue > referenceValue)
        iconImage.color = Color.green;
        else if(currValue == referenceValue)
        iconImage.color = Color.white;
    }


    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
