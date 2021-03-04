using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipMoveComponent))]
public class RacerMovement : Satellite
{
    
    private ShipMoveComponent shipMoveComponent;
    private float axisH, axisV;
    private Vector2 lookInput, screenCenter, mouseValue;

    //Animation components
    private Animator anim;


    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        shipMoveComponent = GetComponent<ShipMoveComponent>();
        anim = GetComponent<Animator>();
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        //circumvented /*
        mouseValue.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseValue.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseValue = Vector2.ClampMagnitude(mouseValue, 1f);

        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");
        //animation stuff

        anim.SetBool("InGrav", _inGravField);
    }
    
    // FixedUpdate is scaled for physics -- fires more consistantly per interval 
    protected override void FixedUpdate()
    {
        //apply the subjective GravForce if in field
        base.FixedUpdate();

        if (_inGravField == true)
        {
            shipMoveComponent.HoverMovement(axisH, axisV);
        }
        else
        {
            shipMoveComponent.StarshipMovement(axisH, axisV, mouseValue);
        }
        
        
    }
}
