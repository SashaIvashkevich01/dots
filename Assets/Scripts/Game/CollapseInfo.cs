using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game{
    public class CollapseInfo{
        private List<GameObject> dotShifts;

        public CollapseInfo() {
            dotShifts = new List<GameObject> ();
        }

        public IEnumerable<GameObject> AlteredDot {
            get {
                return dotShifts.Distinct ();
            }
        }

        public void AddDot(GameObject point) {
            if (!dotShifts.Contains (point)) {
                dotShifts.Add(point);
            }
        }
    }
}