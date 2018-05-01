using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Candle's different modes
    public enum Mode { Attack, Caster, Block, Utility }

    [Header("Movement Speed")]
    // Candle's speed under normal circumstances
    public float normalSpeed = 10f;
    // Candle's current movement speed; will change upon different modes
    public float speed = 10f;
    
    // declaration of forward & side movemement vectors; velocity is calculated upon the two vectors
    Vector3 forward, strafe, vel = Vector3.zero;
    // downward force (gravity)
    float vspeed;

    [Header("Player Modes & Settings")]
    // Candle's current mode
    public Mode mode;
    // Healing range
    public float healingArea = 10f;
    // Utility movement speed
    public float utilitySpeed = 20f;

    [Header("Camera Settings")]
    // The camera attached to Candle
    public Camera cam;
    // The offset vector that pulls the camera back to a 3rd person perspective
    public Vector3 offset = new Vector3(8f, 3f, 2f);
    // The vertical bounds of the camera
    public Vector2 bounds = new Vector2(90f, 90f);
    // The vector that takes in the x & y mouse movement
    Vector2 mouseDir = Vector2.zero;

    [Header("Camera Shake")]
    // the angle offset of the screen shake
    public float angle;
    // the time variable that feeds into the noise function
    float[] increment = new float[3];
    // speed of the noise
    [Range(0.01f, 0.2f)] public float noiseSpeed = 0.01f;

    [Header("Launch Projectile")]
    // position of the cannon
    public Transform cannon;
    // the projectile to be spawned from the cannon
    public GameObject projectile;
    // the force behind the cannon
    public float shootForce = 2000f;
    // if true, the cannon shoots & is set back to false
    bool attack = false;

    [Header("Set in Inspector")]
    // pivot of the camera
    public Transform pivot;
    // character controller of Candle
    public CharacterController cc;
    // animator of Candle
    public Animator anim;
    // Shield gameobject
    public GameObject shield;
    // player's transform (not necessary; used for organization)
    public Transform playerBody;
    // Candle's variables
    public BaseVariables variables;

    // hash for caster mode offset position
    int _casterMode = Animator.StringToHash("casterMode");

    private void Awake()
    {
        #region noise
        for (int i = 0; i < increment.Length; i++)
        {
            increment[i] = Random.Range(0f, 10f);
        }
        #endregion


        // print noise increments
        print(increment[0] + " " + increment[1] + " " + increment[2]);

        // set gravity
        vspeed = -9.8f * Time.deltaTime;
    }


    void Update ()
    {
        #region WASD movement
        // don't have any movement in caster mode
        if(mode == Mode.Caster)
        {
            // both vectors are set to 0; no movement.
            forward = Vector3.zero;
            strafe = Vector3.zero;
        }
        // otherwise, Candle is able to move around
        else
        {
            // the forward vector takes Candle's forward vector & multiplies it by W/S, speed & delta time
            forward = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
            // the strafe vector takes Candle's forward vector & multiplies it by W/S, speed & delta time
            strafe = transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        }
        // calculate movement by adding the x and z of both vectors & setting the y as gravity
        vel = new Vector3(strafe.x + forward.x, vspeed, strafe.z + forward.z);
        // feed the velocity vector into the character controller function
        cc.Move(vel);
        #endregion
        #region modes
        if (Input.GetKey(KeyCode.Space))
        {
            mode = Mode.Caster;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            mode = Mode.Utility;
        }
        else if (Input.GetMouseButton(1))
        {
            mode = Mode.Block;
        }
        else
        {
            mode = Mode.Attack;
        }

        //print(mode.ToString());
        #endregion
        #region caster
        if (mode == Mode.Caster)
        {
            anim.SetBool(_casterMode, true);
            if (Input.GetMouseButtonDown(0))
            {
                print("click");
                attack = true;
            }
            bounds.x = 95f;
            bounds.y =  20f;
        }
        else
        {
            anim.SetBool(_casterMode, false);
            bounds.x = 50f;
            bounds.y = 15f;
        }
        #endregion
        #region block
        if (mode == Mode.Block)
        {
            shield.SetActive(true);
        }
        else shield.SetActive(false);
        #endregion
        #region utility
        if (mode == Mode.Utility)
        {
            speed = utilitySpeed;
            if (!IsInvoking("UtilityHeal")) InvokeRepeating("UtilityHeal", 0f, 1f);
        }
        else
        {
            speed = normalSpeed;
            if (IsInvoking("UtilityHeal")) CancelInvoke("UtilityHeal");
        }
            #endregion


        #region offset camera
        // set the offset of the camera from the pivot based on the offset vector
        cam.transform.position = pivot.transform.position
        + (pivot.forward    *  -offset.x)
        + (pivot.right      *   offset.y)
        + (pivot.up         *   offset.z);
        #endregion

        #region projectile attack
        // if the attack boolean is set to true, attack
        if (attack)
        {
            // instantiate a projectile to a variable
            GameObject proj = Instantiate(projectile, cannon.transform.position, cannon.transform.rotation);
            // add force to the projectile
            proj.GetComponent<Rigidbody>().AddForce(proj.transform.forward * shootForce);
            // set a variable in the projectile that tells it not to collide with minions from the same side as the caster
            proj.GetComponent<ProjHit>().side = variables.minionSide;
            // set the boolean back to false
            attack = false;
        }
        #endregion


        #region camera noise
        // apply noise to the x,y,z angles of the camera
        cam.transform.localEulerAngles = new Vector3(GenerateNoise(0), GenerateNoise(1), GenerateNoise(2));
        #endregion

        HandleRotationMovement();

    }

    #region functions & coroutines

    float GenerateNoise(int i)
    {
        float noise = Mathf.PerlinNoise(0f, increment[i] += noiseSpeed);
        noise = ((noise * angle) - (angle / 2)) * 2;
        return 0f + noise;
    }


    public void UtilityHeal()
    {
        print("Heal");
        // for each nearby minion
        foreach (BaseVariables minion in variables.nearbyAllyUnits)
        {
            // if the minion is not on our side, skip this iteration
            if (minion.minionSide != MinionSide.White) continue;
            // add 5 health every tick
            minion.health += 5f;
        }
    }


    private void HandleRotationMovement()
    {
        Vector2 md = Vector2.zero;
        // create a vector with mouse x & y directions
        if (!PauseMenu.pausetoggle)
        {
            md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }
        // add the vector values to the vector mouseDir
        mouseDir += md;
        // clamp the y direction of the camera
        mouseDir.y = Mathf.Clamp(mouseDir.y, -bounds.x, bounds.y);
        // rotate the vertical direction of the pivot
        pivot.transform.localRotation = Quaternion.Euler(-mouseDir.y, 0f, 0f);
        // rotate the horizontal direction of Candle
        playerBody.transform.localRotation = Quaternion.Euler(0f, mouseDir.x, 0f);

        /// transform.localRotation = Quaternion.Euler(-mouseDir.y, mouseDir.x, 0f);

        // have the cannon rotation be the same roation as the camera
        cannon.rotation = cam.transform.rotation;
    }

    public IEnumerator Shake(float seconds, float shakeSpeed, float shakeIntensity)
    {
        noiseSpeed = shakeSpeed;
        angle = shakeIntensity;
        float dest = 0f;
        float speedRate = noiseSpeed - dest;
        float intRate = shakeIntensity - dest;
        print("shake");
        while (noiseSpeed >= dest)
        {
            noiseSpeed -= speedRate / (seconds / Time.deltaTime);
            angle -= intRate / (seconds / Time.deltaTime);
            yield return null;
        }

        noiseSpeed = 0f;
        angle = 0f;

        /*
         *         shake.speed = shakeSpeed;
        shake.angle = shakeIntensity;
        float dest = 0f;
        float speedRate = speed - dest;
        float intRate = shakeIntensity - dest;
        print("shake");
        while(speed >= dest)
        {
            shake.speed -= speedRate / (seconds/Time.deltaTime);
            shake.angle -= intRate / (seconds / Time.deltaTime);
            yield return null;
        }
        */
    }
    #endregion
}