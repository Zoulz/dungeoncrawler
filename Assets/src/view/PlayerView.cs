using System;
using DG.Tweening;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using trailmarch.model.api;
using trailmarch.utils;
using trailmarch.actors;
using UnityEngine;

namespace trailmarch.view
{
    public class PlayerView : View, IDisposable
    {
        [Inject]
        public ILevelModel levelModel { get; set; }

        public Signal Complete { get { return _completeSignal; } }

        private GameObject _camObj = null;
        private Camera _cam = null;
        private Light _light = null;
        private PlayerActor _player = null;
        private Signal _completeSignal = new Signal();

        private Boolean _movingOrTurning = false;

        public void Init()
        {
            //  Get player actor.
            _player = levelModel.GetFirstActorOfType<PlayerActor>() as PlayerActor;

            //  Create camera.
            _camObj = new GameObject("PlayerCamera");
            _camObj.transform.localPosition = new Vector3(0.5f, 0.25f, 0f);
            _camObj.AddComponent<Camera>();
            _camObj.AddComponent<Light>();
	        _camObj.AddComponent<AudioListener>();
            _camObj.transform.parent = transform;

            //  Setup camera component.
            _cam = _camObj.GetComponent<Camera>();
            _cam.nearClipPlane = 0.1f;
            _cam.farClipPlane = 1000f;

            //  Player light.
            _light = _camObj.GetComponent<Light>();
            _light.renderMode = LightRenderMode.Auto;
            _light.type = LightType.Point;
            _light.range = 3f;

            //  Set camera to player Position.
            _cam.transform.position = new Vector3(_player.Position.x + 0.5f, 0.5f, _player.Position.y + 0.5f);
            _cam.transform.rotation = Quaternion.Euler(new Vector3(0, PositionUtil.GetDirectionDegree(_player.Facing), 0));
        }

        public void Dispose()
        {
            Destroy(_camObj);
        }

        public void Turn()
        {
            if (!_movingOrTurning)
            {
                _movingOrTurning = true;
                UpdateTurning();
            }
        }

        public void Move()
        {
            if (!_movingOrTurning)
            {
                _movingOrTurning = true;
                UpdateMovement();
            }
        }

        private void UpdateMovement()
        {
            Tweener tween = _cam.transform.DOMove(new Vector3(_player.Position.x + 0.5f, 0.5f, _player.Position.y + 0.5f), 1f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(OnMovementComplete);

            tween.PlayForward();
        }

        private void OnMovementComplete()
        {
            _movingOrTurning = false;
            _completeSignal.Dispatch();
        }

        private void UpdateTurning()
        {
            Tweener tween = _cam.transform.DORotate(new Vector3(0, PositionUtil.GetDirectionDegree(_player.Facing), 0), 1f)
                .OnComplete(OnMovementComplete)
                .SetEase(Ease.InOutQuad);

            tween.PlayForward();
        }
    }
}
