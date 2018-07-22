using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game{
    public class DotController : MonoBehaviour{
        [SerializeField] 
        private GameObject[] _dotPrefabs;        
        
        [SerializeField] 
        private Transform _bottomRight;

        public int DotScore { get; private set; }

        private Vector2 _bottomRightVector;
            
        public List<Dot> ChoosedDots { get; private set; }

        private Vector2 _pointGenerate;
        private Dots dots;
        private Vector2[] SpawnPositions;
        
        public static DotController Instance;

        private void Awake(){
            ChoosedDots = new List<Dot>();
            Instance = this;
        }

        private void Start(){
            _bottomRightVector =  new Vector2(_bottomRight.position.x, _bottomRight.position.y);
            _pointGenerate = _bottomRightVector;
            dots = new Dots();
            InitTypes();
            InitializeDots();
        }

        private void InstantiateDot(int row, int column, GameObject newPoint){
            GameObject point = Instantiate (newPoint,
                _pointGenerate + new Vector2(column * 0.9f, row *  0.9f),
                Quaternion.identity);
            point.transform.SetParent(gameObject.transform);
            point.transform.localScale = new Vector3(1,1,1);

            point.GetComponent<Dot>().Init(newPoint.GetComponent<Dot>().Type,row, column);
            dots.Add(row, column, point);
        }

        private void InitTypes(){
            foreach (var dot in _dotPrefabs){
                dot.GetComponent<Dot>().Type = dot.GetComponent<Image>().color;
            }
        }
        
        public void InitializeDots() {
            for(int row = 0; row < GameController.SizeMatrix; row++) {
                for(int column = 0; column < GameController.SizeMatrix; column++) {
                    GameObject newDot = GetRandomDot();
                    InstantiateDot(row, column, newDot);
                }
            }
            SpawnPositions = new Vector2[GameController.SizeMatrix];
            SetupSpawnPositions ();

        }

        public bool AddToChoosedDot (Dot dot){
            if (ChoosedDots.Contains(dot)){
                ChoosedDots.Remove(dot);
                Line.Instance.RemovePosition();
                return false;
            }

            if (ChoosedDots.Count != 0){
                if(!MatchChecker.CheckNeighbors(ChoosedDots.Last(),dot)
                 || !ChoosedDots.Last().Compare(dot)) return false;
            }

            ChoosedDots.Add(dot); 
            Line.Instance.DrawLineToDot(dot);
            return true;
        }
    
        
        public void Collapse(){
            if (ChoosedDots.Count <= 1){
                ChoosedDots.Clear();
                return;
            }
            
            var columns = new List<int>();

            foreach (var choosenDot in ChoosedDots){
                dots.Remove(choosenDot);
                DotScore++;
                Score.Instance.SetScore(DotScore);
                columns.Add(choosenDot.Column);

                choosenDot.gameObject.transform.DOScale(Vector3.zero, 0.3f).onComplete += DestroyChoosedObject;
            }
            var collapsedInfo = dots.Collapse(columns);
            var newDots = CreateNewDotForColumn(columns);
            
            MoveDots(newDots.AlteredDot);
            MoveDots(collapsedInfo.AlteredDot);
            ChoosedDots.Clear();
        }

        private void DestroyChoosedObject(){
          ChoosedDots.ForEach(x => Destroy(x.gameObject));
        }

        private GameObject GetRandomDot(){
           return  _dotPrefabs[Random.Range (0, _dotPrefabs.Length)];
        }
        
        private void MoveDots(IEnumerable<GameObject> movedGameObjects) {

            
            foreach (var item in movedGameObjects){
                var mainSequence = DOTween.Sequence();
                mainSequence.Pause();
                Vector2 endValue = _bottomRightVector
                                   + new Vector2(item.GetComponent<Dot>().Column * 0.9f,
                                       item.GetComponent<Dot>().Row * 0.9f);
                mainSequence.Append(item.transform.DOMove(endValue, 0.5f));
                mainSequence.Insert(0.0f, item.transform.DOJump(endValue, 0.2f, 1, 0.2f));
                mainSequence.Play();
            }
        }

        private CollapseInfo CreateNewDotForColumn(IEnumerable<int> columnsWithMissingCandies) {
            CollapseInfo dotCollapseInfo = new CollapseInfo ();

            foreach (int column in columnsWithMissingCandies) {
                var emptyItems = dots.GetEmptySlotsOnColumn(column);

                foreach(var item in emptyItems) {
                    var dot = GetRandomDot();
                    GameObject dotGameObject = Instantiate(dot, SpawnPositions[column], Quaternion.identity);
                    dotGameObject.transform.SetParent(gameObject.transform);
                    dotGameObject.transform.localScale = new Vector3(1,1,1);
                    dotGameObject.GetComponent<Dot>().Init(dot.GetComponent<Dot>().Type, item.Row, item.Column);
                    dots.Add(item.Row, item.Column,dotGameObject);
                    dotCollapseInfo.AddDot(dotGameObject);
                }
            }
            return dotCollapseInfo;
        }
        
        private void SetupSpawnPositions() {
            for(int column = 0; column < GameController.SizeMatrix; column++) {
                SpawnPositions[column] = _bottomRightVector +
                                         new Vector2(column * 0.9f, GameController.SizeMatrix * 0.9f);
            }
        }
    }
}