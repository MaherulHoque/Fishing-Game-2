using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    private LineRenderer reel;
    public Transform rodTop, hook, busket;
    public bool isPressed, isRetured;
    [SerializeField] 
    private float navigationTime = 20f;

    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        reel = GetComponent<LineRenderer>();
        isPressed = false;
        isRetured = true;
    }

    // Update is called once per frame
    void Update()
    {
        DrawReel();

        if (isRetured)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isPressed = true;
                isRetured = false;
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            }
        }
       

        if (isPressed)
        {
            hook.position = Vector3.MoveTowards(hook.position, mousePos, navigationTime * Time.deltaTime);

        }
        else
        {
            hook.position = Vector3.MoveTowards(hook.position, rodTop.position, navigationTime * Time.deltaTime);
        }

        if (hook.position == mousePos)
        {
            
            isPressed = false;
        }

        if(hook.position == rodTop.position)
        {
            isRetured = true;

            if (transform.childCount > 0)
            {
                GameObject fish = hook.transform.GetChild(0).gameObject;
                fish.transform.position = Vector3.MoveTowards(fish.transform.position, busket.transform.position, navigationTime * Time.deltaTime);

                if(fish.transform.position == busket.transform.position)
                {
                    Destroy(fish);
                }
            }

            
        }


    }

    private void DrawReel()
    {
        reel.SetPosition(0, rodTop.position);
        reel.SetPosition(1, hook.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fish")
        {
            hook.position = Vector3.MoveTowards(hook.position, rodTop.position, navigationTime * Time.deltaTime);
            collision.transform.parent = hook.transform;
            isPressed = false;
        }
    }
}
