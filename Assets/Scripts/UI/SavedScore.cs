using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI{
    public class SavedScore : MonoBehaviour{
        private Text score;
            
        private void Start(){
            score = GetComponent<Text>();
            string savedScore = GameController.Instance.GetScore();
            if (string.IsNullOrEmpty(savedScore)) savedScore = "0";
            score.text = savedScore;
        }
    }
}