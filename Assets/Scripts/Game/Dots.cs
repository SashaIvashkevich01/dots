using System.Collections.Generic;
using UnityEngine;

namespace Game{
    public class Dots{
        private GameObject[,] _dots = new GameObject[GameController.SizeMatrix, GameController.SizeMatrix];
        
        public void Add(int row, int column, GameObject dot){
            _dots[row, column] = dot;
        }

        public GameObject GetDot(int row, int column){
            return _dots[row, column];
        }
        
        public void Remove(Dot dot) {
            _dots[dot.Row, dot.Column] = null;
        }

        public CollapseInfo Collapse(IEnumerable<int> columns){
           var collapseInfo = new CollapseInfo();
            foreach (var column in columns) {
                for(int row = 0; row < GameController.SizeMatrix - 1; row++) {
                    if(_dots[row, column] != null) continue;

                        for(int row2 = row + 1; row2 < GameController.SizeMatrix; row2++) {

                            if(_dots[row2, column] == null) continue;

                                _dots[row, column] = _dots[row2, column];
                                _dots[row2, column] = null;

                        
                                _dots[row, column].GetComponent<Dot>().Row = row;
                                _dots[row, column].GetComponent<Dot>().Column = column;

                                collapseInfo.AddDot(_dots[row, column]);
                                break;
                        }
                    }
                }

            return collapseInfo;
        }
        
        
        public IEnumerable<DotInfo> GetEmptySlotsOnColumn(int column) {
            List<DotInfo> emptySlot = new List<DotInfo> ();

            for(int row = 0; row < GameController.SizeMatrix; row++) {
                if(_dots[row, column] != null)  continue;
                emptySlot.Add(new DotInfo{Row = row, Column = column});
            }
            return emptySlot;
        }
    }
}