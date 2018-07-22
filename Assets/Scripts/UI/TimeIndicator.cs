using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeIndicator : MonoBehaviour{

	private Text _timeIndicator;
	public int TimeGame = 60;
	public GameObject FinishWindow;

	private void Awake () {
		_timeIndicator = GetComponent<Text>();
		Time.timeScale = 1;
	}
	
	public void SetTime(){
		if (TimeGame == 0){
			Time.timeScale = 0;
			FinishWindow.SetActive(true);
			return;
		}
		TimeGame--;
		_timeIndicator.text = TimeGame.ToString();
	}
	
}
