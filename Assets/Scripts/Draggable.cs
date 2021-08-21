using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private Vector2 clickPosition;
    public Transform parentToReturnTo = null;
    public Card card = null;
    //public bool reward = false;

    private void Start() {
        card = GetComponent<Card>();
    }
    public void OnBeginDrag(PointerEventData eventData) {   
        //if(reward) {
        //    reward = false;
        //    BattleManager.Instance().victoryScreen.GetComponent<RewardManager>().PickACardReward(this.gameObject.GetComponent<Card>());
        //}
        clickPosition = LocalPoint(eventData);
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData) {
        this.transform.position = eventData.position - clickPosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        this.transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        card.Action();
    }

    Vector3 LocalPoint(PointerEventData ped) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), ped.position, ped.pressEventCamera, out Vector2 localCursor);
        return localCursor;
    }
}
