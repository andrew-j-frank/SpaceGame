using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySpawner : MonoBehaviour
{
    #region Fields

	public GameObject currency;
    public float velocity = 2;

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject curAstroid = GameManager.Instance.GetCurAstroid();
        if(curAstroid != null && curAstroid.transform.position != this.transform.position)
        {
            this.transform.position = curAstroid.transform.position;
        }
    }
    
    #endregion
    
    #region Methods

	public void SpawnCurrency(float astroidSize)
    {
        int numCurrency = Mathf.CeilToInt(Random.Range(7f, 15f)*astroidSize);
        print($"money spawned: {numCurrency}");
        for(int x = 0; x < numCurrency; x++)
        {
            var currencyObj = Instantiate(currency, this.transform.position, Quaternion.AngleAxis(Random.Range(-180f, 180f), new Vector3(0,0,1)));
            float random = Random.Range(0, 2*Mathf.PI);
            currencyObj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(random), Mathf.Sin(random)) * velocity;
        }
    }

    #endregion
}
