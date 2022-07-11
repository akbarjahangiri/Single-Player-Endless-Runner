using UnityEngine;
using DG.Tweening;
using MyBox;


namespace VOID.GE
{
    public class MyTweenScale : MyTween
    {
        public Transform objectToTween;
        [MinMaxRange(0,30)]public MinMaxFloat targetScale;
        
        
        private Tween _myTween;
        
        public void Start()
        {
            if(onStart)
                PlayForward();
        }
        
        public override Tween BuildTween()
        {
            float randomScale = Random.Range(targetScale.Min, targetScale.Max);
            _myTween = objectToTween.DOScale(randomScale, duration).SetEase(easeType);
            
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
            // base.Pause();
            _myTween?.Pause();
        }
        public void KillTween()
        {
            if(_myTween != null)
                _myTween.Kill();
        }

        public override Tween GetMyTween()
        {
            return _myTween;
        }
    }
}