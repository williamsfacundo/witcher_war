using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Canvas _creditsCanvasOne;

    [SerializeField] private Canvas _creditsCanvasTwo;

    private void Awake()
    {
        ActiveCanvasOne();        
    }

    public void ActiveCanvasOne() 
    {
        _creditsCanvasOne.gameObject.SetActive(true);

        _creditsCanvasTwo.gameObject.SetActive(false);
    }

    public void ActiveCanvasTwo() 
    {
        _creditsCanvasOne.gameObject.SetActive(false);

        _creditsCanvasTwo.gameObject.SetActive(true);
    }     
}
