using UnityEngine;
using System.Collections;

public class PhysicsObject : MonoBehaviour
{
    public bool IsPlanet;
    public bool IsMoon;
    public GameObject MoonsPlanet;
    public float Mass;
    public Vector3 Velocity;
    public Vector3 Acceleration;
    public Vector3 Force;
    public float DistanceToSun;
    public Vector3 DirectionOfForce;
    public Vector3 DirectionOfForceFromPlanet;
}
