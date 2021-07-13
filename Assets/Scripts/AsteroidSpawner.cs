using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    #region Fields

	private float lastSpawnPosition;
    private bool spawnAsteroids = true;
    private float distanceToNewAstroid;

    #endregion
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        lastSpawnPosition = transform.position.y;
        GetNewAstroidDistance();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpawner();
        SpawnAsteroids();
    }
    
    #endregion
    
    #region Methods

	private void MoveSpawner()
    {
        Vector2 shipPos = GameManager.Instance.GetShipPosition();
        if(shipPos.y < transform.position.y + 2.5f)
        {
            transform.position = new Vector3(0, shipPos.y + 2.5f, 0);
        }
    }

    private void SpawnAsteroids()
    {
        if(spawnAsteroids && transform.position.y - lastSpawnPosition >= distanceToNewAstroid)
        {
            var newAsteroid = Instantiate(GameManager.Instance.GetAstroidToSpawn(), new Vector3(Random.Range(-1f, 1f), transform.position.y, 0), Quaternion.AngleAxis(Random.Range(-180f, 180f), new Vector3(0,0,1)));
            float scale = Random.Range(.4f, 1f);
            newAsteroid.transform.localScale = new Vector3(scale, scale, 1);
            lastSpawnPosition = transform.position.y;
            GetNewAstroidDistance();
        }
    }

    private void GetNewAstroidDistance()
    {
        distanceToNewAstroid = Random.Range(1.5f, 7f);
    }

    #endregion
}
