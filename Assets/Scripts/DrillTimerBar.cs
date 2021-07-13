using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillTimerBar : MonoBehaviour
{
    #region Fields

    public CurrencySpawner currencySpawner;
    private float maxTime = 10;
    private float astroidSize;
    private float currTime = 0;
    private bool startTimer = false;
	private Slider slider;
    private bool isMainDrillTimer;
    private Animator drillAnimator;
    private GameObject astroid;

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer)
        {
            if(currTime/maxTime < 1)
            {
                currTime += Time.deltaTime;
                slider.value = currTime/maxTime;
            }
            else
            {
                slider.value = 1;
                startTimer = false;
                if(isMainDrillTimer)
                {
                    //print("timer done");
                    currencySpawner.SpawnCurrency(astroidSize);
                }
                StopTimer();
                astroid.GetComponent<AstroidController>().Explode();
                //drillAnimator.SetBool("ShipLandedOnAstroid", false);
            }
        }
    }
    
    #endregion
    
    #region Methods

    public void StartTimer(float maxTime, float astroidSize, bool isMainDrillTimer, GameObject astroid)
    {
        this.maxTime = maxTime;
        this.astroidSize = astroidSize;
        this.isMainDrillTimer = isMainDrillTimer;
        this.astroid = astroid;
        this.gameObject.SetActive(true);
        startTimer = true;
    }

    public void StopTimer()
    {
        this.gameObject.SetActive(false);
        startTimer = false;
        currTime = 0;
    }

    public void SetDrillAnimator(Animator drillAnimator)
    {
        this.drillAnimator = drillAnimator;
    }

    #endregion
}
