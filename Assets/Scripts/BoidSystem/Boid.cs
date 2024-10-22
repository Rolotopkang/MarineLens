using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    BoidSettings settings;

    // State
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;
    [HideInInspector]
    public bool panic = false;

    // To update:
    Vector3 acceleration;
    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    // Cached
    Transform cachedTransform;
    Transform target;
    private Coroutine panicRecover;
    private Outline _outline;
    private Renderer fishRenderer;

    void Awake () {
        cachedTransform = transform;
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    private void Start()
    {
        fishRenderer = GetComponentInChildren<Renderer>(); 
         fishRenderer.material = new Material(fishRenderer.material);
    }

    public void Initialize (BoidSettings settings, Transform target , float size) {
        this.target = target;
        this.settings = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        transform.localScale = new Vector3(size,size,size);
        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
    }

    public void UpdateBoid () {
        Vector3 acceleration = Vector3.zero;

        if (target != null) {
            Vector3 offsetToTarget = (target.position - position);
            acceleration = SteerTowards (offsetToTarget) * (panic? settings.targetWeightInPanic : settings.targetWeight);
        }

        if (numPerceivedFlockmates != 0) {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards (avgFlockHeading) * settings.alignWeight;
            var cohesionForce = SteerTowards (offsetToFlockmatesCentre) * settings.cohesionWeight;
            var seperationForce = SteerTowards (avgAvoidanceHeading) * settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision ()) {
            Vector3 collisionAvoidDir = ObstacleRays ();
            Vector3 collisionAvoidForce = SteerTowards (collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        if (IsHeadingForMovingCollision())
        {
            panic = true;
            _outline.enabled = true;
            if (panicRecover != null)
            {
                StopCoroutine(panicRecover);
                panicRecover = null;
            }
            Vector3 collisionAvoidDir = ObstacleRays ();
            Vector3 collisionAvoidForce = SteerTowards (collisionAvoidDir) * settings.avoidMovingCollisionWeight;
            acceleration += collisionAvoidForce;
        }
        else
        {
            if (panicRecover == null && panic)
            {
                panicRecover = StartCoroutine(ReturnNonePanic());
            }
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp (speed, settings.minSpeed, panic? settings.panicMaxSpeed : settings.maxSpeed);
        velocity = dir * speed;

        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = Vector3.Slerp(cachedTransform.forward, dir, Time.deltaTime * (panic? settings.pancicRotationSpeed : settings.rotationSpeed));
        position = cachedTransform.position;
        forward = dir;
        

        float targetSpeed = settings.m_speedScale * speed;
        float targetFrequency = settings.m_frequencyScale * speed;
        float currentSpeed = fishRenderer.material.GetFloat("_Speed");
        float currentFrequency = fishRenderer.material.GetFloat("_Frequency");
        float smoothedSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * settings.smoothingSpeed);
        float smoothedFrequency = Mathf.Lerp(currentFrequency, targetFrequency, Time.deltaTime * settings.smoothingFrequency);
        fishRenderer.material.SetFloat("_Speed", smoothedSpeed);
        fishRenderer.material.SetFloat("_Frequency", smoothedFrequency);
    }

    bool IsHeadingForCollision () {
        RaycastHit hit;
        if (Physics.SphereCast (position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDst, settings.obstacleMask)) {
            return true;
        } else { }
        return false;
    }

    bool IsHeadingForMovingCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast (position, settings.boundsRadius, forward, out hit, settings.movingCollisionAvoidDst, settings.movingObstacleMask)) {
            return true;
        } else { }
        return false;
    }

    Vector3 ObstacleRays () {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++) {
            Vector3 dir = cachedTransform.TransformDirection (rayDirections[i]);
            Ray ray = new Ray (position, dir);
            if (!Physics.SphereCast (ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask)) {
                return dir;
            }
        }

        return forward;
    }

    IEnumerator ReturnNonePanic()
    {
        yield return new WaitForSeconds(settings.panicTime);
        panic = false;
        panicRecover = null;
        _outline.enabled = false;
    }

    Vector3 SteerTowards (Vector3 vector) {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude (v, settings.maxSteerForce);
    }

}