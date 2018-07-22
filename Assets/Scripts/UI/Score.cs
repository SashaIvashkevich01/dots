using UnityEngine;
using UnityEngine.UI;

namespace UI{
    public class Score : MonoBehaviour{
        private Text _score;
        [SerializeField]
        private Text _finishScore;

        public static Score Instance;
        
        private void Start(){
            Instance = this;
            _score = GetComponent<Text>();
        }

        public void SetScore(int count){
            _score.text = count.ToString();
            _finishScore.text = count.ToString();
        }
    }
}