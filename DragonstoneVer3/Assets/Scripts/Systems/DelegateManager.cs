using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateManager : MonoBehaviour
{
    public static DelegateManager Instance;


     public delegate void NoInputDelegate();
     public event NoInputDelegate e_StartOfGame;


    private void Awake() {
        if (Instance == null) {
            Instance = this;            
        }else{
            Destroy(gameObject);            
        }
    }

    public void StartOfGame()
    {
      if (e_StartOfGame != null)
      e_StartOfGame();
    }
 
 
}
