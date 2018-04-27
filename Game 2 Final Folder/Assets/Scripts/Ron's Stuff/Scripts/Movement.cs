using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum Mode { Attack, Caster, Block, Utility }

    [Header("Movement Speed")]
    public float speed = 10f;
    public float normalSpeed = 10f;

    Vector3 forward, strafe;
    Vector3 vel = Vector3.zero;
    const float gravity = 9.8f;
    float vspeed;

    [Header("Player Modes & Settings")]
    public Mode mode;
    public float healingArea = 10f;
    public float utilitySpeed = 20f;

    [Header("Camera Settings")]
    public Camera cam;
    public Vector3 offset = new Vector3(8f, 3f, 2f);
    public Vector2 bounds = new Vector2(90f, 90f);
    Vector2 mouseDir = Vector2.zero;

    [Header("Camera Shake")]
    public float angle;
    float[] increment = new float[3];
    [Range(0.01f, 0.2f)] public float noiseSpeed = 0.01f;

    [Header("Launch Projectile")]
    public Transform cannon;
    public GameObject projectile;
    public float shootForce = 2000f;
    bool attack = false;

    [Header("Set in Inspector")]
    public Transform pivot;
    public CharacterController cc;
    public Animator anim;
    public GameObject shield;
    public Transform playerBody;
    public BaseVariables variables;

    int _casterMode = Animator.StringToHash("casterMode");


    private void Awake()
    {
        #region noise
        for (int i = 0; i < increment.Length; i++)
        {
            increment[i] = Random.Range(0f, 10f);
        }
        #endregion

        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        // print noise increments
        print(increment[0] + " " + increment[1] + " " + increment[2]);

        vspeed = -gravity * Time.deltaTime;
    }


    void Update ()
    {
        #region WASD movement
        if(mode != Mode.Caster)
        {
            forward = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
            strafe = transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        }
        else
        {
            forward = Vector3.zero;
            strafe = Vector3.zero;
        }
        vel = new Vector3(strafe.x + forward.x, vspeed, strafe.z + forward.z);
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
            cam.transform.position = pivot.transform.position
            + (pivot.forward * -offset.x)
            + (pivot.right * offset.y)
            + (pivot.up * offset.z);
        #endregion

        #region projectile
        if (attack)
        {
            GameObject proj = Instantiate(projectile, cannon.transform.position, cannon.transform.rotation);
            proj.GetComponent<Rigidbody>().AddForce(proj.transform.forward * shootForce);
            proj.GetComponent<ProjHit>().side = variables.minionSide;
            attack = false;
        }
        #endregion


        #region hit escape
        if (Input.GetKeyDown("escape"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }
        #endregion

        #region camera noise
        cam.transform.localEulerAngles = new Vector3(GenerateNoise(0), GenerateNoise(1), GenerateNoise(2));
        #endregion

        HandleRotationMovement();

    }

    #region functions & coroutines
    List<Collider> ScanForItems()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position,healingArea);
        return new List<Collider>(cols);
    }

    float GenerateNoise(int i)
    {
        float noise = Mathf.PerlinNoise(0f, increment[i] += noiseSpeed);
        noise = ((noise * angle) - (angle / 2)) * 2;
        return 0f + noise;
    }


    public void UtilityHeal()
    {
        print("Heal");
        foreach (BaseVariables minion in variables.nearbyAllyUnits)
        {
            if (minion.minionSide != MinionSide.White) continue;
            minion.health += 5f;
        }
    }


    private void HandleRotationMovement()
    {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDir += md;
        mouseDir.y = Mathf.Clamp(mouseDir.y, -bounds.x, bounds.y);
        pivot.transform.localRotation = Quaternion.Euler(-mouseDir.y, 0f, 0f);
        playerBody.transform.localRotation = Quaternion.Euler(0f, mouseDir.x, 0f);

        /// transform.localRotation = Quaternion.Euler(-mouseDir.y, mouseDir.x, 0f);
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

    public void Interpolate(float var, float from, float to, float seconds)
    {
        var -= (from - to) / (seconds / Time.deltaTime);
    }

    #endregion
}