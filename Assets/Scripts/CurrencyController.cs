using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    #region Fields

    public float gravityConst = 5;
	private Rigidbody2D rb;

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "ShipInfluence")
        {
            //print("Currency in ship influence");
            var distance = Mathf.Sqrt(Mathf.Pow(this.transform.position.x - collider.gameObject.transform.parent.position.x, 2) + Mathf.Pow(this.transform.position.y - collider.gameObject.transform.parent.position.y, 2));
            var magnitude = gravityConst*collider.gameObject.transform.parent.localScale.x/Mathf.Pow(distance, 2);
            var force = new Vector2(collider.gameObject.transform.parent.position.x - this.transform.position.x, collider.gameObject.transform.parent.position.y - this.transform.position.y).normalized * magnitude;
            rb.AddForce(force);
        }
    }
    
    #endregion
    
    #region Methods

	

    #endregion
}
