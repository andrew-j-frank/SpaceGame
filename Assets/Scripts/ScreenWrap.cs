using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    #region Fields

	private float screenWidth;
    private Transform[] ghosts = new Transform[2];

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        screenWidth = GameManager.Instance.GetScreenWidth();

        CreateGhostShips();
        PositionGhostShips();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDestroy();
        PositionGhostShips();
        SwapShips();
    }

    void OnDestroy()
    {
        for(int i = 0; i < 2; i++)
        {
            if(ghosts[i] != null)
            {
                Destroy(ghosts[i].gameObject);
            }
        }
    }
    
    #endregion
    
    #region Methods

	private void CreateGhostShips()
    {
        for(int i = 0; i < 2; i++)
        {
            ghosts[i] = Instantiate(transform, Vector3.zero, Quaternion.identity) as Transform;
            //print("ghost spawned");
            DestroyImmediate(ghosts[i].GetComponent<ScreenWrap>());
            //Destroy(ghosts[i].GetComponent<ScreenWrap>());
            //Destroy(ghosts[i].GetComponentInChildren<CurrencySpawner>());
            //print("parent set");
        }
    }

    private void PositionGhostShips()
    {
        try{
        // Ghosts are relative to this so start with that position
        var ghostPosition = transform.position;
    
        // Right ghost
        if(ghosts[0] != null)
        {
            ghostPosition.x = transform.position.x + screenWidth;
            ghostPosition.y = transform.position.y;
            ghosts[0].position = ghostPosition;
        }
    
        // Left ghost
        if(ghosts[1] != null)
        {
            ghostPosition.x = transform.position.x - screenWidth;
            ghostPosition.y = transform.position.y;
            ghosts[1].position = ghostPosition;
        }
    
        // All ghost ships should have the same rotation as the main ship
        for(int i = 0; i < 2; i++)
        {
            if(ghosts[i] != null)
            {
                ghosts[i].rotation = transform.rotation;
            }
        }
        }
        catch (MissingReferenceException e)
        {
            print(e.StackTrace);
        }
    }

    private void SwapShips()
    {
        foreach(var ghost in ghosts)
        {
            if(ghost != null)
            {
                if (ghost.position.x < screenWidth/2 && ghost.position.x > -screenWidth/2)
                {
                    transform.position = ghost.position;
        
                    break;
                }
            }
        }
    
        PositionGhostShips();
    }

    private void CheckForDestroy()
    {
        foreach(var ghost in ghosts)
        {
            if(ghost == null)
            {
                Destroy(this.gameObject);
                break;
            }
        }
    }

    public Transform[] GetGhosts()
    {
        return ghosts;
    }

    #endregion
}
