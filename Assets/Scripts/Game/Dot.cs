using System.Collections;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dot : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler{

	public int Row { get; set; }
	public int Column { get; set; }

	public Color Type { get; set; }

	[SerializeField] 
	private Animator _selection;
	
	public bool Compare(Dot dot){
		return Type == dot.Type;
	}

	public void Init(Color type, int row, int column){
		Type = type;
		Column = column;
		Row = row;
	}

	public void OnPointerEnter(PointerEventData eventData){
		if (!Input.GetMouseButton(0)) return;
		if (DotController.Instance.AddToChoosedDot(this))
			_selection.Play("Appear");
	}


	public void OnPointerDown(PointerEventData eventData){
		if (!Input.GetMouseButton(0) ) return;
		if(DotController.Instance.AddToChoosedDot(this))
			_selection.Play("Appear");
	}
	

}
