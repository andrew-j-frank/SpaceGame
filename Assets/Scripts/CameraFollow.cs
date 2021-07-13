using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Fields

	

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 followPosition = GameManager.Instance.GetCameraFollowPos();
        transform.position = new Vector3(transform.position.x, followPosition.y, transform.position.z);
    }
    
    #endregion
    
    #region Methods

	

    #endregion
}
