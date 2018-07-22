using UnityEngine;

namespace Game{
    public class GameController : MonoBehaviour{
        public static int SizeMatrix = 6;

        public static GameController Instance;

        private void Awake(){
            Instance = this;
        }

        public void LoadlMenu(){
            Application.LoadLevel("Menu");
            SaveScore();
        }

        public void ReplyLevel(){
            Application.LoadLevel("Game");
            SaveScore();
        }

        public void SaveScore(){
           PlayerPrefs.SetString("score",DotController.Instance.DotScore.ToString()); 
        }
        
        public string GetScore(){
            return PlayerPrefs.GetString("score"); 
        }
        
    }
}