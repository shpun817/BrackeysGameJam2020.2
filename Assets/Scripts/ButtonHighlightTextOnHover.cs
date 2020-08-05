using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHighlightTextOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	Text buttonText;
	Color buttonTextColorOriginal;

	private void Start() {
		buttonText = GetComponentInChildren<Text>();
		if (!buttonText) {
			Debug.LogWarning("Text not attached to " + gameObject.name);
		}
		buttonTextColorOriginal = buttonText.color;
	}

	public void OnPointerEnter(PointerEventData eventData) {
		buttonText.color = Color.white;
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		buttonText.color = buttonTextColorOriginal;
	}

}
