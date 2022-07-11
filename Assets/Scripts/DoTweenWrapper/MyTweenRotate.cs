using DG.Tweening;
using UnityEngine;

namespace VOID.GE.Scripts.Tweens
{
    public class MyTweenRotate : MyTween
    {
        public Transform objectToTween;
        public Vector3 targetRotation;
        
        private Tween _myTween;

        private void Start()
        {
            if(onStart)
                PlayForward();
        }
        
        public override Tween BuildTween()
        {
            _myTween = objectToTween.DOLocalRotate(targetRotation, duration).SetEase(easeType);
            
            if (loop)
                _myTween.SetLoops(-1, loopType);
            _myTween.SetAutoKill(autoKill);
            
            _myTween.OnStart(
                () =>
                {
                    onStartEvent?.Invoke();
                }).OnComplete(
                () =>
                {
                    onCompleteEvent?.Invoke();
                }).OnStepComplete(
                () =>
                {
                    onStepEvent?.Invoke();
                });

            return _myTween;
        }

        public override void PlayForward()
        {
            if (_myTween == null)
                BuildTween();
            _myTween.PlayForward();
        }
        public void PlayBackward()
        {
            if (_myTween == null)
                BuildTween();
            _myTween.PlayBackwards();
        }

        public override void Pause()
        {
            // base.Pause();
            _myTween?.Pause();
        }
        public override Tween GetMyTween()
        {
            return _myTween;
        }
        public void KillTween()
        {
            if(_myTween != null)
                _myTween.Kill();
        }

    }
}