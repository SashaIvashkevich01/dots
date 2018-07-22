using UniRx;
using UnityEngine;

namespace Game{
    public class Timer : MonoBehaviour{
        private CompositeDisposable _disposables;
        private TimeIndicator _timeIndicator;

        void Start(){
            _timeIndicator = GetComponent<TimeIndicator>();

            Observable.Timer(System.TimeSpan.FromSeconds(1))
                .Repeat()
                .Subscribe(_ => { _timeIndicator.SetTime(); }).AddTo(_disposables);
        }

        void OnEnable(){
            _disposables = new CompositeDisposable();
        }

        void OnDisable(){
            _disposables?.Dispose();
        }
    }
}