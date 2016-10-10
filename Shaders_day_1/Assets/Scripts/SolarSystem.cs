using UnityEngine;
using System.Collections;

public class SolarSystem : MonoBehaviour
{
    // TODO(George): Make this code cleaner and reusable.
    // Should we have each planet calculate their forces instead??
    // Should all planets influence all other planets with their gravitational pull?

    public GameObject Sun;
    float SunMass;

    public GameObject[] Planets;
    
    /*
    public float SunMass = 100.0f;
    public float MercuryMass = 8.0f;
    public float VenusMass = 9.0f;
    public float EarthMass = 10.0f;
    public float EarthMoonMass = 3.0f;
    public float MarsMass = 9.0f;
    public float JupiterMass = 25.0f;
    public float SaturnMass = 20.0f;
    public float UranusMass = 18.0f;
    public float NeptuneMass = 18.0f;
    */

    public float GlobalGravity = 6.67f;

    // Use this for initialization
    void Start ()
    {
        PhysicsObject SunPhysics = Sun.GetComponent<PhysicsObject>();
        SunMass = SunPhysics.Mass;

        /*MercuryVelocity = new Vector3(6, 0, 0);
        VenusVelocity = new Vector3(5, 0, 0);
        EarthVelocity = new Vector3(4, 0, 0);
        EarthMoonVelocity = new Vector3(1, 0, 0);
        MarsVelocity = new Vector3(3, 0, 0);
        JupiterVelocity = new Vector3(3, 0, 0);
        SaturnVelocity = new Vector3(2.5f, 0, 0);
        UranusVelocity = new Vector3(2.25f, 0, 0);
        NeptuneVelocity = new Vector3(2.25f, 0, 0);*/
    }
	
    // Update is called once per frame
    void Update ()
    {
        for(int PlanetIndex = 0; PlanetIndex < Planets.Length; PlanetIndex++)
        {
            GameObject Planet = Planets[PlanetIndex];
            PhysicsObject PlanetPhysics = Planet.GetComponent<PhysicsObject>();
            
            PlanetPhysics.DistanceToSun = Vector3.Distance(Planet.transform.position, Sun.transform.position) + 1;
            
            if (PlanetPhysics.IsPlanet)
            {
                PlanetPhysics.DirectionOfForce = Sun.transform.position - Planet.transform.position;
                PlanetPhysics.DirectionOfForce = PlanetPhysics.DirectionOfForce.normalized;
                PlanetPhysics.Force = PlanetPhysics.DirectionOfForce * GlobalGravity * SunMass * PlanetPhysics.Mass / (PlanetPhysics.DistanceToSun * PlanetPhysics.DistanceToSun);
            }
            else if(PlanetPhysics.IsMoon)
            {
                PlanetPhysics.DirectionOfForceFromPlanet = PlanetPhysics.MoonsPlanet.transform.position - Planet.transform.position;
                PlanetPhysics.DirectionOfForceFromPlanet = PlanetPhysics.DirectionOfForceFromPlanet.normalized;
                PlanetPhysics.Force = PlanetPhysics.DirectionOfForceFromPlanet * GlobalGravity * SunMass * PlanetPhysics.MoonsPlanet.GetComponent<PhysicsObject>().Mass * PlanetPhysics.Mass / (PlanetPhysics.DistanceToSun * PlanetPhysics.DistanceToSun);
            }

            PlanetPhysics.Acceleration = PlanetPhysics.Force / PlanetPhysics.Mass;
            PlanetPhysics.Velocity += PlanetPhysics.Acceleration * Time.deltaTime;
            Planet.transform.position += PlanetPhysics.Velocity * Time.deltaTime;
        }
    }
}
