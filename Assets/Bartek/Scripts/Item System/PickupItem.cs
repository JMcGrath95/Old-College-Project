using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PickupItem : InteractableArea
{
    ItemController controller;

    BaseItem item;

    public float rotationSpeed = 0.5f;
    public float moveSpeed = 0.5f;

    public float distanceTillFlip = 1;
    private float distanceTravelled;

    Vector3 origin;

    private void Start()
    {
        origin = transform.position;

        controller = FindObjectOfType<ItemController>();

        item = controller.ReturnRandomItem();
        UpdatePrefab();
    }

    private void UpdatePrefab()
    {
        if (item.ItemMesh != null)
            GetComponent<MeshFilter>().mesh = item.ItemMesh;
    }

    public override void Interact()
    {
        //add code calling player to add item to his inventory
    }

    private void Update()
    {
        distanceTravelled = Vector3.Distance(origin, transform.position);

        if(distanceTravelled >= distanceTillFlip)
        {
            distanceTravelled = 0;
            origin = transform.position;
            moveSpeed = -moveSpeed;
        }

        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + (moveSpeed * Time.deltaTime), pos.z);

        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
}