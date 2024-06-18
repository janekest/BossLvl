using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
   

  
  private void Update()
  {
      Flip();
  }

  void Flip()
  {
      if (Keyboard.current.sKey.wasPressedThisFrame )
      {
          transform.Rotate(0,180,0);
      }
  }
}
