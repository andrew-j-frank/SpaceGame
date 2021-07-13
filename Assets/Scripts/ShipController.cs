using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region Fields

    public float velocity;
    public float angularVelocity;
    public float gravityConst;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public DrillTimerBar drill;
    private bool isGrounded = false;
    private Animator shipAnimator;
    private Animator drillAnimator;
    private Rigidbody2D rb;
    private bool gameStarted = false;
    private float highestShipDistance;
    private bool isMoving = false;
    private bool isMainShip;
    private float screenWidth;

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log($"ship spawned, position: {transform.position.x}, {transform.position.y}, {transform.position.z}");
        rb = GetComponent<Rigidbody2D>();
        rb.rotation = 0;
        transform.rotation = Quaternion.AngleAxis(0, new Vector3(0,0,1));
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        shipAnimator = GetComponent<Animator>();
        drillAnimator = transform.GetChild(0).GetComponent<Animator>();
        isMainShip = this.GetComponent<ScreenWrap>() != null;
        screenWidth = GameManager.Instance.GetScreenWidth();
        highestShipDistance = rb.position.y;
        drill.SetDrillAnimator(drillAnimator);
    }

    // Update is called once per frame
    void Update()
    {
        ControlAnimaions();
    }

    // FixedUpdate is called once per physics engine calculation
    void FixedUpdate()
    {
        PhysicsMovements();
        UpdateShipBounds();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "AstroidInfluence")
        {
            var distance = Mathf.Sqrt(Mathf.Pow(this.transform.position.x - collider.gameObject.transform.parent.position.x, 2) + Mathf.Pow(this.transform.position.y - collider.gameObject.transform.parent.position.y, 2));
            var magnitude = gravityConst*Mathf.Pow(collider.gameObject.transform.parent.localScale.x, 3)/Mathf.Pow(distance, 2);
            var force = new Vector2(collider.gameObject.transform.parent.position.x - this.transform.position.x, collider.gameObject.transform.parent.position.y - this.transform.position.y).normalized * magnitude;
            rb.AddForce(force);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(isMainShip && gameStarted && collider.gameObject.tag == "Currency")
        {
            Destroy(collider.gameObject);
            GameManager.Instance.IncreaseCurrency();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(!isMoving && isGrounded && collision.gameObject.tag == "Asteriod" && !drillAnimator.GetBool("ShipLandedOnAstroid") && !shipAnimator.GetBool("Flying"))
        {
            drillAnimator.SetBool("ShipLandedOnAstroid", true);
            //print("ShipLandedOnAstroid is true");
            drill.StartTimer(10, collision.transform.localScale.x, isMainShip, collision.gameObject);
            if(collision.transform.position.x < screenWidth/2 && collision.transform.position.x > -screenWidth/2)
            {
                GameManager.Instance.SetCurAstroid(collision.gameObject);
            }
        }
        else if(isMoving && !isGrounded && drillAnimator.GetBool("ShipLandedOnAstroid"))
        {
            drillAnimator.SetBool("ShipLandedOnAstroid", false);
            //print("ShipLandedOnAstroid is false");
            drill.StopTimer();
            GameManager.Instance.SetCurAstroid(null);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteriod")
        {
            drillAnimator.SetBool("ShipLandedOnAstroid", false);
            //print("ShipLandedOnAstroid is false");
            drill.StopTimer();
            GameManager.Instance.SetCurAstroid(null);
        }
    }
    
    #endregion
    
    #region Methods

    public Vector2 GetCOM()
    {
        return new Vector2(rb.centerOfMass.x + transform.position.x, rb.centerOfMass.y + transform.position.y);
    }

    private void PhysicsMovements()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if(!gameStarted)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                gameStarted = true;
            }
            var force = new Vector2(-Mathf.Sin(rb.rotation*Mathf.PI/180), Mathf.Cos(rb.rotation*Mathf.PI/180)) * velocity;
            rb.AddForce(force);
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(angularVelocity);
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-angularVelocity);
        }

        if(!isMoving && rb.velocity != new Vector2(0,0))
        {
            isMoving = true;
            //print("isMoving is true");
        }
        else if(isMoving && rb.velocity == new Vector2(0,0))
        {
            isMoving = false;
            //print("isMoving is false");
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void ControlAnimaions()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            shipAnimator.SetBool("Flying", true);
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            shipAnimator.SetBool("Flying", false);
        }
    }

    private void UpdateShipBounds()
    {
        if(rb.position.y > highestShipDistance)
        {
            highestShipDistance = rb.position.y;
        }

        if(rb.position.y < highestShipDistance - GameManager.Instance.GetBackTravelDistance())
        {
            rb.position = new Vector2(rb.position.x, highestShipDistance - GameManager.Instance.GetBackTravelDistance());
        }
    }

    #endregion
}
