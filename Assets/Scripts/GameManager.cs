using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields

	/// <summary>
    /// The singleton instance of GameManager
    /// </summary>
    /// <value></value>
    public static GameManager Instance { get; private set; }

    public ShipController ship;
    public GameObject asteroid;
    public CurrencyTxtController currencyTxt;
    public float backTravelDistance = 5;
    private float screenWidth;
    private GameObject curAstroid;

    #endregion
    
    #region Unity Methods
    
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        var cam = Camera.main;

        var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        
        screenWidth = screenTopRight.x - screenBottomLeft.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    #endregion
    
    #region Methods

	public Vector2 GetCameraFollowPos()
    {
        return ship.GetCOM();
    }

    public Vector2 GetShipPosition()
    {
        return ship.transform.position;
    }

    public Quaternion GetShipRotation()
    {
        return ship.transform.rotation;
    }

    public GameObject GetAstroidToSpawn()
    {
        return asteroid;
    }

    public float GetBackTravelDistance()
    {
        return backTravelDistance;
    }

    public float GetScreenWidth()
    {
        return screenWidth;
    }

    public GameObject GetCurAstroid()
    {
        return curAstroid;
    }

    public void SetCurAstroid(GameObject curAstroid)
    {
        this.curAstroid = curAstroid;
    }

    public void IncreaseCurrency()
    {
        currencyTxt.IncreaseScore();
    }

    #endregion
}
