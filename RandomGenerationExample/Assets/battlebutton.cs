using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class battlebutton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  public bool isPressing;

 public void OnPointerDown(PointerEventData eventData)
 {
 	isPressing = true;
 }
  public void OnPointerUp(PointerEventData eventData)
 {
 	isPressing = false;
 }

}
