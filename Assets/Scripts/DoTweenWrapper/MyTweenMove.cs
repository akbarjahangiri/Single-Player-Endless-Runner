using UnityEngine;
using DG.Tweening;

namespace VOID.GE
{
    
    public class MyTweenMove : MyTween
    {
        public enum TweenMoveType
        {
            WorldRelative, ObjectRelative
        }

        public TweenMoveType tweenMoveType = TweenMoveType.WorldRelative;
        
        public Transform objectToTween = default;
        
        [Tooltip("Target position is relative to the object position as the center of transitions")]
        
        public Vector3 targetPosition;

        private Tween _myTween;

        private void Start()
        {
            if(onStart)
                PlayForward();
        }

        public override Tween BuildTween()
        {
            Vector3 endValue = new Vector3();
            if (tweenMoveType == TweenMoveType.ObjectRelative)
                endValue = targetPosition + objectToTween.position;
            else if(tweenMoveType == TweenMoveType.WorldRelative)
                endValue = targetPosition;

            _myTween = objectToTween.DOLocalMove(endValue, duration).SetEase(easeType).SetAutoKill(autoKill).OnStart(
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

        public override Tween GetMyTween()
        {
            return _myTween;
        }

        public void KillTween()
        {
            if (_myTween == null) return;
                
            _myTween.Kill();
            _myTween = null;

        }
       
    }
}