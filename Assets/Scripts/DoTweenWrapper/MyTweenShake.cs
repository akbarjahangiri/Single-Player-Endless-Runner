using DG.Tweening;
using UnityEngine;

namespace VOID.GE.Scripts.Tweens
{
    public enum ShakeType
    {
        Position, Rotation, Scale
    }
    public class MyTweenShake : MyTween
    {
        public Transform objectToTween;
        public ShakeType shakeType;
        public float strength;
        
        private Tween _myTween;

        private void Start()
        {
            if(onStart)
                PlayForward();
        }
        
        public override Tween BuildTween()
        {
            switch (shakeType)
            {
                case ShakeType.Position:
                    _myTween = objectToTween.DOShakePosition(duration, strength);
                    break;
                case ShakeType.Rotation:
                    _myTween = objectToTween.DOShakeRotation(duration, strength);
                    break;
                case ShakeType.Scale:
                    _myTween = objectToTween.DOShakeScale(duration, strength);
                    break;
            }
            
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

            
            if (loop)
                _myTween.SetLoops(loopCounts, loopType);
            _myTween.SetAutoKill(autoKill);
            
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
            _myTween?.Pause();
        }
        
        public void KillTween()
        {
            _myTween?.Kill();
        }

        public override Tween GetMyTween()
        {
            return _myTween;
        }
    }
}