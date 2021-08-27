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

    private SpawnBox spawnbox;

    private Planet primaryPlanet;
    

    private void Awake()
    {
        

    }


    // Start is called before the first frame update
    void Start()
    {
         spawnbox = GetComponent<SpawnBox>();

         player = PlayerRacer.instance;

        _PlanetPool = PlanetPool.Instance as PlanetPool;

        _ObstaclePool = ObstaclePool.Instance as ObstaclePool;
        _ObstaclePool.BatchPool(50);
        
        var planet = SpawnPlanet(new Vector3(0, -400, 0), 500f);
        MovePlayerToGLZone(planet);

        primaryPlanet = planet;
    }

    // Update is called once per frame
    void Update()
    {
        primaryPlanet.transform.localScale *= spawnbox.RadiusCheck(primaryPlanet.gameObject, player.gameObject);
        if (primaryPlanet.transform.localScale == Vector3.zero)
        {
            _PlanetPool.ReturnToPool(primaryPlanet);
            primaryPlanet = SpawnPlanet(player.transform.position + Random.insideUnitSphere * spawnbox.ObjectScalingDistance, 500f);          
        }
    }


    public Planet SpawnPlanet(Vector3 location, float scale)
    {
        var planet = PlanetPool.Instance.Get();
        planet.gameObject.transform.position = location;
        planet.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        //set here elsewise its set in the Awake() call only once instead of OnEnable -- seems hacky.
        planet.gravfield.SetCenterOfMass(planet.gameObject.transform);
        planet.gameObject.SetActive(true);
        SpawnObstacleInGLZone(planet, Mathf.RoundToInt(scale/10f));
        SpawnEnemyInGLZone(planet, Mathf.RoundToInt(scale / 50f));
        Debug.Log("planet and obstacles spawned");
        return planet;
    }


    private void MovePlayerToGLZone(Planet planet)
    {
        player.transform.position = planet.GoldilocksZone();
        Vector3 direction = player.GetPlanetCenterMass() - player.transform.position.normalized;
        player.transform.right = Vector3.Cross(player.transform.forward, direction); 
    }

    private void SpawnEnemyInGLZone(Planet planet, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int random = Mathf.RoundToInt(Random.Range(1f, 2f));

            if (random == 1)
            {
                var enemy = LeaperPool.Instance.Get();
                enemy.gameObject.transform.position = planet.GoldilocksZone();
                enemy.gameObject.SetActive(true);
            }

            if (random == 2)
            {
                var enemy = HopperPool.Instance.Get();
                enemy.gameObject.transform.position = planet.GoldilocksZone();
                enemy.gameObject.SetActive(true);
            }

        }
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
