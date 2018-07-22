using UnityEngine;
using UnityEngine.UI;

namespace Game{
    public class InputController : MonoBehaviour{
     
        
        private void Update(){
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            if (Input.GetMouseButtonUp(0)){
                Line.Instance.EnabledLine(false);
               DotController.Instance.Collapse();
            } 
        }
    }
}