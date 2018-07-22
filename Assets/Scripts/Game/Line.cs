using UnityEngine;

namespace Game{
    public class Line : MonoBehaviour{

        public static Line Instance;
        private LineRenderer _lineRenderer;

        private Vector3 _mousePosition;

        public void Awake(){
            Instance = this;
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            _lineRenderer.SetWidth(0.2f, 0.2f);
            _lineRenderer.enabled = false;
            _lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            _lineRenderer.startColor = Color.white;
            _lineRenderer.endColor = Color.white;
        }


        private void Update(){
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SetPosition(_mousePosition);
        }

        public void DrawLineToDot(Dot dot){
            _lineRenderer.startColor = dot.Type;
            _lineRenderer.endColor = dot.Type;
            
            Debug.Log(dot.Type);
            SetPosition(dot.gameObject.transform.position);
            DrawLineToMouse(_mousePosition);
            _lineRenderer.numCornerVertices = DotController.Instance.ChoosedDots.Count;
        }
        
        public void DrawLineToMouse(Vector3 mousePosition){
            ++_lineRenderer.positionCount;
            SetPosition(mousePosition);
        }

        public void SetPosition(Vector3 position){
            _lineRenderer.SetPosition(_lineRenderer.positionCount-1, position);
            _lineRenderer.enabled = true;
        }

        public void RemovePosition(){
            _lineRenderer.positionCount--;
        }
        
        
        public void EnabledLine(bool state){
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.enabled = state;
        }
    }
}