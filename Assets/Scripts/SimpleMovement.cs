using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SimpleMovement : NetworkBehaviour
{

    [SerializeField] private NetworkVariable<float> forwardBackward = new NetworkVariable<float>();
    [SerializeField] private NetworkVariable<float> leftRight = new NetworkVariable<float>();
    private float rotation = 0;
    [SerializeField] float movementSpeed = 0.05f;
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] GameObject cam;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if(!IsOwner) return;
        cam.SetActive(true);
        transform.position = new Vector3(0, 0.5f, 0);
    }

    [ServerRpc]
    private void UpdatePositionServerRpc(float _position, string _type)
    {
        if(_type == "FB")
        {
            forwardBackward.Value += _position;
        }
        else
        {
            leftRight.Value += _position;
        }

        UpdatePositionClientRpc();


    }

    [ClientRpc]
    private void UpdatePositionClientRpc()
    {
        transform.position = new Vector3(leftRight.Value, 0.5f, forwardBackward.Value);
    }


    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKey(KeyCode.W))
        {

            UpdatePositionServerRpc(+movementSpeed, "FB");
        }
        else if (Input.GetKey(KeyCode.S))
        {

            UpdatePositionServerRpc(-movementSpeed, "FB");
        }

        if (Input.GetKey(KeyCode.D))
        {
            UpdatePositionServerRpc(+movementSpeed, "LF");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            UpdatePositionServerRpc(-movementSpeed, "LF");
        }

        


        /*float rotacionHorizontal = Input.GetAxis("Mouse X");
        if (rotacionHorizontal < 0)
        {
            rotation -= rotationSpeed;
        }
        else if (rotacionHorizontal > 0)
        {
            rotation += rotationSpeed;
        }
        Debug.Log(rotation);
        //transform.Rotate(new Vector3(0, rotation, 0));
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);*/

    }
}
