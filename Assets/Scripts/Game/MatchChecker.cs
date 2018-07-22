using UnityEngine;

namespace Game{
    public class MatchChecker{
        public static bool CheckNeighbors(Dot c1, Dot c2){
            return (c1.Column == c2.Column || c1.Row == c2.Row) && Mathf.Abs(c1.Column - c2.Column) <= 1 &&
                   Mathf.Abs(c1.Row - c2.Row) <= 1;
        }
    }
}