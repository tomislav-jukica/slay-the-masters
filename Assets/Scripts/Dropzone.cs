using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    

    public void OnPointerEnter(PointerEventData eventData) {
    }
    
    public void OnDrop(PointerEventData eventData) {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if(draggable != null) {
            draggable.parentToReturnTo = this.transform;
        } else {
            Debug.LogError("Missing Draggable component!");
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        
    }
}
