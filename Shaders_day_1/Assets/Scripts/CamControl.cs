using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamControl : MonoBehaviour
{
    [SerializeField]
    float DistanceFromPlanet = 10.0f;
    int PreviousIndex = -1;
    int PlanetIndex = -1;

    GameObject[] Planets;
    Vector3 CamPosition;
    Vector3 StartPosition;

	// Use this for initialization
	void Start ()
    {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
        CamPosition = transform.position;
        StartPosition = CamPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlanetIndex--;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlanetIndex++;
        }

        if(PlanetIndex < -1)
        {
            PlanetIndex = Planets.Length - 1;
        }
        else if( PlanetIndex >= Planets.Length)
        {
            PlanetIndex = -1;
        }

        if(PlanetIndex >= 0 && PlanetIndex < Planets.Length)
        {
            Vector3 PlanetPos = Planets[PlanetIndex].transform.position;
            CamPosition = PlanetPos;
            CamPosition.z -= DistanceFromPlanet;
            transform.LookAt(PlanetPos);
        }
        else
        {
            CamPosition = StartPosition;
            transform.LookAt(Vector3.zero);
        }

        transform.position = CamPosition;
	}
}
