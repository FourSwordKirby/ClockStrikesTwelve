using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CameraControls : MonoBehaviour {
    /* A bunch of stuff that relates to how the camera shakes*/
    public enum ShakePresets
    {
        NONE,
        BOTH,
        HORIZONTAL,
        VERTICAL
    };
	private float shakeIntensity = 0.0f;
	private float shakeDuration = 0.0f;
	private ShakePresets shakeDirection = ShakePresets.NONE;
    private Action shakeComplete = null;
	private Vector2 shakeOffset = new Vector2();

    public float zoomSpeed = 20f;
    public float maxZoomFOV = 10f;

    public GameObject focalPointPrefab;

    private Camera cameraComponent;

    public GameObject target;
    public List<GameObject>visibleTargets; //accounts for all other targets that aren't just the main focus

    private float original_camera_size;
    private float min_camera_size;
    private float max_camera_size;
    private float target_camera_size;

    /* camera moving constants */
    private const float Z_OFFSET = -10;

    private const float TARGETING_LOWER_BOUND = 0.0f;
    private const float TARGETING_UPPER_BOUND = 1.0f;
    private const float ZOOM_IN_LOWER_BOUND = 0.3f;
    private const float ZOOM_IN_UPPER_BOUND = 0.7f;
    private const float ZOOM_OUT_LOWER_BOUND = 0.2f;
    private const float ZOOM_OUT_UPPER_BOUND = 0.8f;

    private const float ZOOM_RATE = 0.02f;

    private const float PAN_SPEED = 5.0f;

    public static CameraControls instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start () {
        cameraComponent = GetComponent<Camera>();

        target = Player.instance.gameObject;
        visibleTargets = new List<GameObject>();
        original_camera_size = cameraComponent.orthographicSize;
        min_camera_size = 0.75f * original_camera_size;
        max_camera_size = 2.0f * original_camera_size;
        target_camera_size = original_camera_size;

        transform.position = target.transform.position + new Vector3(0, 0, Z_OFFSET);

	}
	
	void FixedUpdate () {
        //Do shake calculations
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            if (shakeDuration <= 0)
                stopShaking();
            else
                applyShake();
        }

        //Now follow the target
        if (transform.position != target.transform.position + new Vector3(0, 0, Z_OFFSET))
        {
            float x = ((target.transform.position + new Vector3(0, 0, Z_OFFSET)) - transform.position).x;
            float y = ((target.transform.position + new Vector3(0, 0, Z_OFFSET)) - transform.position).y;
            GetComponent<Rigidbody2D>().velocity = new Vector2(x * PAN_SPEED, y * PAN_SPEED);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity.Set(0.0f, 0.0f);
        }

        //used to scale the camera's size in response to more targe
        List<GameObject> targetsToRemove = new List<GameObject>();

        foreach(GameObject targ in visibleTargets)
        {
            //check that the target is in bounds
            if (inCameraBounds(targ))
            {
                resizeToFitTarget(targ);
            }
            else
            {
                //crap need to do other things like changing player state, changing camera modes, etc.
                targetsToRemove.Add(targ);
                continue;
            }
                
            /*
            Vector3 targetCameraPosition = cameraComponent.WorldToViewportPoint(targ.transform.position);
            float cameraSize = cameraComponent.orthographicSize;

            if (!(0.1f < targetCameraPosition.x && targetCameraPosition.x < 0.9f && 0.1f < targetCameraPosition.y && targetCameraPosition.y < 0.9f))
            {
                if (cameraSize < max_camera_size)
                    cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 1.02f, zoomSpeed * Time.deltaTime);
            }

            if(0.3 < targetCameraPosition.x && targetCameraPosition.x < 0.7f && 0.3f < targetCameraPosition.y && targetCameraPosition.y < 0.7f)
            {
                if (cameraComponent.orthographicSize > min_camera_size)
                    cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 0.98f, zoomSpeed * Time.deltaTime);
            }
             */
        }

        foreach (GameObject targ in targetsToRemove)
        {
            visibleTargets.Remove(targ);
        }

        //If no targets are in range, go back to the original player
        if (visibleTargets.Count == 1)
        {
            Target(Player.instance.gameObject);
        }

        //external gradual resizing
        float cameraSize = cameraComponent.orthographicSize;
        //Current issue is that if the character moves too quickly, the opponent then leaves the FOV too quickly, resulting in an awkward camera
        if (cameraSize < target_camera_size)
            cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 1 + ZOOM_RATE, zoomSpeed * Time.deltaTime);
        if (cameraSize > target_camera_size)
            cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 1 - ZOOM_RATE, zoomSpeed * Time.deltaTime);
        
	}

    //Used to just follow a specific mobile
    public void Target(GameObject target)
    {
        this.target = target.gameObject;
    }

    //Used when the player needs to target some enemy etc. Creates a focus point game object
    public void Target(GameObject player, GameObject target)
    {
        GameObject new_target = Instantiate(focalPointPrefab);
        new_target.GetComponent<FocalPoint>().setTargets(player, target);
        this.target = new_target.GetComponent<FocalPoint>().gameObject;

        //hacky test case stuff
        this.visibleTargets.Add(player.gameObject);
        this.visibleTargets.Add(target.gameObject);
    }

    public void Shake(float Intensity = 0.05f, 
                        float Duration = 0.5f, 
                        Action OnComplete = null, 
                        bool Force = true, 
                        ShakePresets Direction = ShakePresets.NONE)
    {
        if(!Force && ((shakeOffset.x != 0) || (shakeOffset.y != 0)))
			return;
		shakeIntensity = Intensity;
		shakeDuration = Duration;
        shakeComplete = OnComplete;
		shakeDirection = Direction;
        shakeOffset.Set(0, 0);
    }

    private void stopShaking()
    {
        shakeOffset.Set(0, 0);
        if (shakeComplete != null)
            shakeComplete();
    }

    private void applyShake()
    {
        if (shakeDirection == ShakePresets.BOTH || shakeDirection == ShakePresets.HORIZONTAL)
                    shakeOffset.x = (UnityEngine.Random.Range(-1.0F, 1.0F) * shakeIntensity);
        if (shakeDirection == ShakePresets.BOTH || shakeDirection == ShakePresets.VERTICAL)
              shakeOffset.y = (UnityEngine.Random.Range(-1.0F, 1.0F) * shakeIntensity);

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        transform.position = new Vector3(x + shakeOffset.x, y + shakeOffset.y, z);
    }

    private bool inCameraBounds(GameObject target)
    {
        if (cameraComponent.orthographicSize != target_camera_size)
            return true;

        Vector3 targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);
        return (0.0f < targetCameraPosition.x && targetCameraPosition.x < 1.0f && 0.0f < targetCameraPosition.y && targetCameraPosition.y < 1.0f);
    }

    private void resizeToFitTarget(GameObject target)
    {
        float originalSize = cameraComponent.orthographicSize;
        float calculatedSize = originalSize;

        Vector3 targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);

        while (!(ZOOM_OUT_LOWER_BOUND < targetCameraPosition.x && targetCameraPosition.x < ZOOM_OUT_UPPER_BOUND
              && ZOOM_OUT_LOWER_BOUND < targetCameraPosition.y && targetCameraPosition.y < ZOOM_OUT_UPPER_BOUND))
        {
            if (calculatedSize < max_camera_size)
            {
                cameraComponent.orthographicSize *= 1+ZOOM_RATE;
                calculatedSize = cameraComponent.orthographicSize;
            }
            else
                break;

            targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);
        }

        while (ZOOM_IN_LOWER_BOUND < targetCameraPosition.x && targetCameraPosition.x < ZOOM_IN_UPPER_BOUND
            && ZOOM_IN_LOWER_BOUND < targetCameraPosition.y && targetCameraPosition.y < ZOOM_IN_UPPER_BOUND)
        {
            if (calculatedSize > min_camera_size)
            {
                cameraComponent.orthographicSize *= 1-ZOOM_RATE;
                calculatedSize = cameraComponent.orthographicSize;
            }
            else
                break;

            targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);
        }

        cameraComponent.orthographicSize = originalSize;
        target_camera_size = calculatedSize;
    }
}
