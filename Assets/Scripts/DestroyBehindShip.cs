using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBehindShip : MonoBehaviour
{
    #region Fields

	public float extraDistance;

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetShipPosition().y - this.transform.position.y > GameManager.Instance.GetBackTravelDistance() + extraDistance)
        {
            Destroy(this.gameObject);
        }
    }
    
    #endregion
    
    #region Methods

	

    #endregion
}
