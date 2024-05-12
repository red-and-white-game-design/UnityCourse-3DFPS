using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler instance;
    public float lookSensitivity = 1f;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public Vector3 GetMoveInput()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0 , Input.GetAxisRaw("Vertical"));
        move = Vector3.ClampMagnitude(move,1);
        return move;
    }

    public float GetMouseLookHorizontal() {
        return GetMouseLookAxis("Mouse X");
    }

    public float GetMouseLookVertical() {
        return GetMouseLookAxis("Mouse Y");
    }

    public float GetMouseLookAxis(string mouseInputName) {
        float i = Input.GetAxisRaw(mouseInputName);
        i *= lookSensitivity * 0.01f;

        return i;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    
}
