using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidController : MonoBehaviour
{
    #region Fields

	private ParticleSystem ps;
    private Rigidbody2D rb;
    private CircleCollider2D cc2d;
    private SpriteRenderer sr;

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        this.ps = GetComponent<ParticleSystem>();
        this.rb = GetComponent<Rigidbody2D>();
        this.cc2d = GetComponent<CircleCollider2D>();
        this.sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    #endregion
    
    #region Methods

	public void Explode()
    {
        //Destroy(rb);
        //Destroy(cc2d);
        //Destroy(sr);
        cc2d.enabled = false;
        sr.enabled = false;
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        ps.Play();
    }

    #endregion
}
