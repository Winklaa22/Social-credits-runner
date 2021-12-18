using System;
using UnityEngine;

namespace Controllers.Camera
{
    [ExecuteInEditMode]
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;
        
        [Header("Camera position")]
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        private Vector3 _backOfTarget = new Vector3(0, 1.2f, -2f);

        [Header("Camera bobbing")] 
        [SerializeField] private Transform _camera;
        [SerializeField, Range(0, 50)] private float _frenquency = 10;
        [SerializeField, Range(0, 0.00020f)] private float _amplitude = 0.00015f;
        
        private float _momentumMask;
        public float MomentumMask
        {
            set
            {
                _momentumMask = value;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        private Vector3 FootStepMotion()
        {
            Vector3 position = Vector3.zero;
            position.x += Mathf.Sin((Time.time * (_frenquency * _momentumMask))) * _amplitude;
            position.y += Mathf.Cos((Time.time * (_frenquency * _momentumMask)) / 2) * _amplitude * 2;
            return position;
        }
        
        private void CameraBobbing()
        {
            if(_momentumMask <= 0)
                return;
            
            _camera.localPosition += FootStepMotion();
        }


        private void LateUpdate()
        {
            transform.position = _target.position + _backOfTarget + _offset;
        }

        private void Update()
        {
            CameraBobbing();
        }
    }
}
