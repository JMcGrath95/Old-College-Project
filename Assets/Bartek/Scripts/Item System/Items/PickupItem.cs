using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PickupItem : InteractableArea
{
    ItemController controller;

    public BaseItem item;

    public bool testItem = false;

    Inventory inventory;

    public float rotationSpeed = 0.5f;
    public float moveSpeed = 0.5f;

    public float distanceTillFlip = 1;
    private float distanceTravelled;

    Vector3 origin;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        origin = transform.position;

        controller = FindObjectOfType<ItemController>();

        if (!testItem)
            item = controller.ReturnRandomItem();

    }

    public override void Interact()
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }

    private void Update()
    {
        distanceTravelled = Vector3.Distance(origin, transform.position);

        if (distanceTravelled >= distanceTillFlip)
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