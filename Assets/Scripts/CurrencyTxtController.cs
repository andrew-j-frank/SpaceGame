using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyTxtController : MonoBehaviour
{
    #region Fields

	private int currencyCount = 0;
    private Text text;

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        this.text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    #endregion
    
    #region Methods

	public void IncreaseScore()
    {
        currencyCount++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        text.text = currencyCount.ToString();
    }

    #endregion
}
