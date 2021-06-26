using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerRacer player;
    [HideInInspector]
    public PlanetPool _PlanetPool;
    [HideInInspector]
    public ObstaclePool _ObstaclePool;
    

    private void Awake()
    {
        

    }


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerRacer.instance;
        _PlanetPool = PlanetPool.Instance as PlanetPool;
        _ObstaclePool = ObstaclePool.Instance as ObstaclePool;
        _ObstaclePool.BatchPool(10);
        
        var planet = SpawnPlanet(new Vector3(0, -400, 0), 500f);
        SpawnPlayerInGLZone(planet);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Planet SpawnPlanet(Vector3 location, float scale)
    {
        var planet = PlanetPool.Instance.Get();
        planet.gameObject.transform.position = location;
        planet.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        planet.gameObject.SetActive(true);
        SpawnObstacleInGLZone(planet, Mathf.RoundToInt(scale/10f));
        return planet;
    }


    private void SpawnPlayerInGLZone(Planet planet)
    {
        player.transform.position = planet.GoldilocksZone();
        
        Vector3 gravDir = player.transform.position - planet.transform.position;
        Vector3 reAngle = Vector3.RotateTowards(transform.forward, -gravDir, 1, 0.0f);
        player.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(reAngle), 1);
    }

    private void SpawnEnemyInGLZone()
    {

    }

    private void SpawnObstacleInGLZone(Planet planet, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obstacle = ObstaclePool.Instance.Get();
            obstacle.gameObject.transform.position = planet.GoldilocksZone();
            obstacle.gameObject.SetActive(true);
        }
        
    }
}
