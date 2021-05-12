using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Hallway> hallways = new List<Hallway>();

    private void Start()
    {
        GetHallways();
    }

    void GetHallways()
    {
        //loops through all children and add the ones that have a hallway script to hallway array
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Hallway>() != null)
                hallways.Add(child.gameObject.GetComponent<Hallway>());
        }
        print(hallways.Count);

        foreach (Hallway hallway in hallways)
        {
            //checks if there is a hallway on the top side
            if (hallway.transform.position.z > transform.position.z)
                hallway.direction = 1;

            //checks if there is a hallway on the right side
            if (hallway.transform.position.x > transform.position.x)
                hallway.direction = 2;

            //checks if there is a hallway on the bottom side
            if (hallway.transform.position.z < transform.position.z)
                hallway.direction = 3;

            //checks if there is a hallway on the left side
            if (hallway.transform.position.x < transform.position.x)
                hallway.direction = 4;
        }
    }
}
