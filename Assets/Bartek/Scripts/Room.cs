using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Hallway> hallways = new List<Hallway>();

    //might change to enum. 0 is normal, 1 is start, 2 is exit/boss.
    public int roomType;

    public GameObject Floor;

    //bools to check if there is a hallway in the corresponding position
    public bool topHallway, rightHallway, bottomHallway, leftHallway;

    //loops through children and assigns floor object if it fins an object named Floor
    public void GetFloor()
    {
        foreach (Transform t in transform)
        {
            if (t.gameObject.name == "Floor")
            {
                Floor = t.gameObject;
                return;
            }
        }
    }

    public void GetHallways()
    {
        hallways.Clear();

        //loops through all children and add the ones that have a hallway script to hallway array
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Hallway>() != null)
                hallways.Add(child.gameObject.GetComponent<Hallway>());
        }

        //print(gameObject.name + " " + hallways.Count);

        foreach (Hallway hallway in hallways)
        {
            //checks if there is a hallway on the top side
            if (hallway.transform.position.z > transform.position.z)
            {
                hallway.direction = 1;
                topHallway = true;
            }

            //checks if there is a hallway on the right side
            if (hallway.transform.position.x > transform.position.x)
            {
                hallway.direction = 2;
                rightHallway = true;
            }

            //checks if there is a hallway on the bottom side
            if (hallway.transform.position.z < transform.position.z)
            {
                hallway.direction = 3;
                bottomHallway = true;
            }

            //checks if there is a hallway on the left side
            if (hallway.transform.position.x < transform.position.x)
            {
                hallway.direction = 4;
                leftHallway = true;
            }
        }
    }
}
