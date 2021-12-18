using System;
using Controllers.Camera;
using UnityEngine;

namespace Character_Controller
{
    public class CharacterController : MonoBehaviour
    {
        [Header("Physics")] 
        [SerializeField] private Rigidbody _rigidbody;
        
        [Header("Momentum")]
        [SerializeField] private AnimationCurve _momentumCurve;
        private float _curveMask, _momentumMask;
        
        [Header("Movement")] 
        [SerializeField] private float _speed;
        [SerializeField] private bool _isMoving;

        [Header("Animations")] 
        [SerializeField] private Animator _animator;

        private void SetMomentum()
        {
            var curveTime = _momentumCurve[_momentumCurve.length - 1].time;
            var mask = _isMoving ? _curveMask + Time.deltaTime / curveTime : _curveMask - Time.deltaTime / curveTime;
            _curveMask = Mathf.Clamp(mask, 0, 1);
            _momentumMask = _momentumCurve.Evaluate(_curveMask * curveTime);
        }

        private float GetMovementSpeed()
        {
            return _speed * _momentumMask;
        }

        private void SetMovement()
        {
            SetMomentum();
            CameraController.Instance.MomentumMask = _momentumMask;
            _rigidbody.velocity = new Vector3(0, 0, GetMovementSpeed());
        }

        private void SetAnimations()
        {
            _animator.SetFloat("Momentum", _momentumMask);
        }

        private void Update()
        {
            SetMovement();
            SetAnimations();
        }
    }
}
