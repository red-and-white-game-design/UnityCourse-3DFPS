using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandlerT : MonoBehaviour
{
   public static PlayerInputHandlerT Instance;
   public float lookSensitivity = 1f;
   
   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }

   public Vector3 GetMoveInput()
   {
      Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
      move = Vector3.ClampMagnitude(move, 1);
      return move;
   }

   public float GetMouseLookHorizontal()
   {
      return GetMouseLookAxis("Mouse X");
   }

   public float GetMouseLookVertical()
   {
      return GetMouseLookAxis("Mouse Y");
   }

   public float GetMouseLookAxis(string mouseInputName)
   {
      float i = Input.GetAxisRaw(mouseInputName);
      i *= lookSensitivity * 0.01f;

      return i;
   }
}
