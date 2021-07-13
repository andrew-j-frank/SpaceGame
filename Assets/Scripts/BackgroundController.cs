using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    #region Fields

	private float backgroundLength = 27;
    private float extraDistance = 3;

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetShipPosition().y - (this.transform.position.y + backgroundLength/2) > GameManager.Instance.GetBackTravelDistance() + extraDistance)
        {
            //print($"Ship y: {GameManager.Instance.GetShipPosition().y}, top of ground: {this.transform.position.y + backgroundLength/2}, distance: {GameManager.Instance.GetBackTravelDistance() + extraDistance}");
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + backgroundLength*2, this.transform.position.z);
        }
    }
    
    #endregion
    
    #region Methods

	

    #endregion
}
