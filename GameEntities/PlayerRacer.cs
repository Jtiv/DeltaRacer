using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ShipMoveComponent))]
public class PlayerRacer : Satellite
{
    [HideInInspector]
    public static PlayerRacer instance;

    //Ship Components
    private ShipMoveComponent shipMoveComponent;

    //Input Fields >> change to inputcomponent ? <<
    private float axisH, axisV;
    private Vector2 lookInput, screenCenter, mouseValue;
    private bool Boost;

    //Animation components
    private Animator anim;

    //Camera Controls
    [SerializeField]
    private float fovThresh, scalingLogBase, vel;

    //Exposed elements for HUD
    //[HideInInspector]
    public float Health, Fuel, AirCap;

    //Events
    public event Action OnPlayerDeath;

    [HideInInspector]
    public Vector3 OutGravDir;

    public DisplayBarValue speedbar, fuelbar, airbar, boostbar, healthbar;
 

    // Start is called before the first frame update. Awake is called even before that (O_o)
    //Override awake, called into Satellite awake first

    protected override void Awake()
    {
        instance = this;
        base.Awake();
        Fuel = 1000f;
        Health = 100f;
        AirCap = 500f;
        shipMoveComponent = GetComponent<ShipMoveComponent>();
        anim = GetComponent<Animator>();
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        //game conditions
        if (Health <= 0 || AirCap <= 0 || Fuel <= 0)
        {
            StartCoroutine(Die());
        }

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        //circumvented /*
        mouseValue.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseValue.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseValue = Vector2.ClampMagnitude(mouseValue, 1f);

        Boost = Input.GetButton("Jump");
        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");
        //animation stuff

        anim.SetBool("InGrav", _inGravField);

        //Camera Effects n stuff

        SpeedWarpCamera();

        //UI Updates and Such
        //Debug.Log(vel);
        speedbar.SetValue(vel / 50f);
        fuelbar.SetValue(Fuel / 1000f);
        airbar.SetValue(AirCap / 500f);
        healthbar.SetValue(Health / 100f);
    }
    
    // FixedUpdate is scaled for physics -- fires more consistantly per interval 
    protected override void FixedUpdate()
    {
        //apply the subjective GravForce if in field
        base.FixedUpdate();

        if (_inGravField == true)
        {
            OutGravDir = (rb.position - GetPlanetCenterMass());
            
            if (Fuel > 0)
            {
                shipMoveComponent.HoverMovement(axisH, axisV);

                if (Boost)
                {
                    shipMoveComponent.ZoomBoost();
                    Fuel -= 25f;
                }

                Fuel += (vel * Time.fixedDeltaTime);

            } 
           
        }
        else
        {
            shipMoveComponent.StarshipMovement(axisH, axisV, mouseValue);
            OutGravDir = new Vector3(0,0,0);
            AirCap -= .1f;
        }
        
        
    }

    private void SpeedWarpCamera()
    {
        
        vel = Mathf.Abs(rb.velocity.magnitude);
        if (vel > fovThresh)
        {
            Camera.main.fieldOfView = Mathf.Clamp(Mathf.Lerp(Camera.main.fieldOfView, Camera.main.fieldOfView *= ((1 + Mathf.Log(scalingLogBase,vel)/10)) , Time.deltaTime/4), 0 , 80);
        }

        else Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 45, Time.deltaTime);
          
    }

    public IEnumerator Die()
    {
        
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        //Instantiate(ExplosionMaker.Explosion()); -- static explosion maker to make making explosions ~~simple~~
        //gameManager.reloadscene as triggered by on death event
        yield return new WaitForSeconds(3);
        OnPlayerDeath?.Invoke();
    }
}
