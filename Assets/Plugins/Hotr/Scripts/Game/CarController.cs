using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyHerbagnole
{
    /// <summary>
    /// Represents a playable car in My Herbagnole
    /// </summary>
    public class CarController : MonoBehaviour
    {
        [Header("Infos")]
        [SerializeField] private float maxAccelleration = 500f;
        [SerializeField] private float maxSteerAngle = 30f;

        [Header("Components")]
        [SerializeField] private CarData data;
        [SerializeField] private CinemachineBrain cinemachineBrain;
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private Camera playerCamera;

        private float currentAccel = 0;
        private float currentTurnAngle = 0;
        private bool canMove;
        private bool flagNextLap;
        private TrackManager manager;
        private int id;


        private Vector2 currentInput;

        public void Init(int id, TrackManager manager)
        {
            OutputChannels channel = (OutputChannels)Mathf.Pow(2, id + 1);
            cinemachineBrain.ChannelMask = channel;
            cinemachineCamera.OutputChannel = channel;
            canMove = false;
            flagNextLap = false;
            this.manager = manager;
            this.id = id;
        }

        public void SetDriver(GameObject driverPrefab)
        {
            foreach (Transform child in data.driverSpot) Destroy(child.gameObject);
            Instantiate(driverPrefab, data.driverSpot).GetComponent<Animator>().runtimeAnimatorController = data.driverController;
        }

        public Camera GetCamera()
        {
            return playerCamera;
        }

        public void SetCanMove(bool value)
        {
            canMove = value;

            foreach (Wheel wheel in data.wheels)
            {
                wheel.collider.motorTorque = 0;
                wheel.collider.steerAngle = 0;
            }
        }

        public void FlagNextLap()
        {
            flagNextLap = true;
        }

        public void Move(Vector2 vector)
        {
            currentInput = vector;
        }

        void Start()
        {
            data.rb.centerOfMass = data.centerOfMass.localPosition;
        }

        void Update()
        {
            if (!canMove) return;

            if (flagNextLap)
            {
                flagNextLap = false;
                manager.DoLap(id);
            }
        }

        void FixedUpdate()
        {
            if (!canMove) return;

            currentAccel = maxAccelleration * currentInput.y;
            currentTurnAngle = currentInput.x * maxSteerAngle;

            foreach (Wheel wheel in data.wheels)
            {
                wheel.collider.motorTorque = currentAccel;

                if (wheel.axel == Axel.Front)
                {
                    wheel.collider.steerAngle = currentTurnAngle;
                    wheel.model.transform.localRotation = Quaternion.Euler(0f, currentInput.x * 30f, 0);
                }
            }
        }


        public enum Axel
        {
            Front,
            Back
        }

        [Serializable]
        public struct Wheel
        {
            public GameObject model;
            public WheelCollider collider;
            public Axel axel;
        }
    }
}
