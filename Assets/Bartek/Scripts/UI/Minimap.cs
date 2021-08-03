using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    List<Vector3> positions = new List<Vector3>();

    List<Vector2> roomPositions = new List<Vector2>();
    List<GameObject> roomGameObjects = new List<GameObject>();

    public GameObject roomPrefab;

    public Transform contentPosition;

    GameObject activeRoom;

    public void StartMinimap(List<Vector3> p)
    {
        positions = p;

        foreach (Vector3 pos in positions)
        {
            roomPositions.Add(new Vector2(pos.x, pos.z));
        }

        foreach (Vector3 roomPos in roomPositions)
        {
            CreateRoom(roomPos);
        }

        foreach (GameObject obj in roomGameObjects)
        {
            obj.GetComponent<Image>().enabled = false;
        }

        roomGameObjects[0].GetComponent<Image>().enabled = true;
    }

    void CreateRoom(Vector2 position)
    {
        print(position.x + " " + position.y);
        GameObject obj = Instantiate(roomPrefab, contentPosition);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(position.x, position.y, 0);

        roomGameObjects.Add(obj);
    }

    public void SetActiveRoom(Vector3 pos)
    {
        if (activeRoom != null)
        {
            activeRoom.GetComponent<Image>().color = Color.white;
        }

        Vector2 position = new Vector2(pos.x, pos.z);

        foreach (GameObject obj in roomGameObjects)
        {
            if(obj.GetComponent<RectTransform>().anchoredPosition == position)
            {
                obj.GetComponent<Image>().enabled = true;
                activeRoom = obj;
                activeRoom.GetComponent<Image>().color = Color.green;
            }
        }
    }
}
