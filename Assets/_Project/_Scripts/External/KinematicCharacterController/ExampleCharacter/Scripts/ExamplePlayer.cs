using Internal.Core.Inputs;
using Internal.Core.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace KinematicCharacterController.Examples
{
    public class ExamplePlayer : MonoBehaviour
    {
        [FormerlySerializedAs("Character")] [SerializeField]
        private ExampleCharacterController _character;

        [FormerlySerializedAs("CharacterCamera")] [SerializeField]
        private ExampleCharacterCamera _characterCamera;

        [Space] [SerializeField] private bool _allowBunnyHop = true;

        // Run-time values
        private Vector2 _movementDirection;
        private Vector2 _lookInput;
        private bool _isSprintPressed;
        private bool _isCrouchingPressed;
        private bool _isJumpPressed;

        private const string MouseScrollInput = "Mouse ScrollWheel";

        private InputReader _inputReader;

        [Inject]
        private void Construct(InputReader inputReader)
        {
            LogHandler.ThrowIfNull(this, inputReader, nameof(inputReader));
            _inputReader = inputReader;
        }

        private void OnEnable()
        {
            _inputReader.OnMoveInput += SetMovementDirection;
            _inputReader.OnLookInput += SetLookInput;

            _inputReader.OnSprintInput += SetIsSprinting;
            _inputReader.OnJumpInput += SetIsJumpPressed;
            _inputReader.OnCrouchInput += SetIsCrouchPressed;
        }

        private void OnDisable()
        {
            if (_inputReader != null)
            {
                _inputReader.OnMoveInput -= SetMovementDirection;
                _inputReader.OnLookInput -= SetLookInput;
                
                _inputReader.OnJumpInput -= SetIsJumpPressed;
                _inputReader.OnCrouchInput -= SetIsCrouchPressed;
            }
        }

        private void SetMovementDirection(Vector2 movementDirection) => _movementDirection = movementDirection;
        private void SetLookInput(Vector2 lookInput) => _lookInput = lookInput;
        private void SetIsSprinting(bool isPressed) => _isSprintPressed = isPressed;
        private void SetIsJumpPressed(bool isPressed) => _isJumpPressed = isPressed;
        private void SetIsCrouchPressed(bool isPressed) => _isCrouchingPressed = isPressed;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Tell the camera to follow transform
            _characterCamera.SetFollowTransform(_character.CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            _characterCamera.IgnoredColliders.Clear();
            _characterCamera.IgnoredColliders.AddRange(_character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _inputReader.IsPlayerMapEnabled())
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            HandleCharacterInput();
        }

        private void LateUpdate()
        {
            // Handle rotating the camera along with physics movers
            if (_characterCamera.RotateWithPhysicsMover && _character.Motor.AttachedRigidbody != null)
            {
                _characterCamera.PlanarDirection =
                    _character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation *
                    _characterCamera.PlanarDirection;
                _characterCamera.PlanarDirection = Vector3
                    .ProjectOnPlane(_characterCamera.PlanarDirection, _character.Motor.CharacterUp).normalized;
            }

            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            Vector3 lookInputVector = new Vector3(_lookInput.x, _lookInput.y, 0f);

            // Prevent moving the camera while the cursor isn't locked
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

            // Apply inputs to the camera
            _characterCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            // Handle toggling zoom level
            // if (Input.GetMouseButtonDown(1))
            // {
            //     _characterCamera.TargetDistance =
            //         (_characterCamera.TargetDistance == 0f) ? _characterCamera.DefaultDistance : 0f;
            // }
        }

        private void HandleCharacterInput()
        {
            var characterInputs = new PlayerCharacterInputs
            {
                MoveAxisForward = _movementDirection.y,
                MoveAxisRight = _movementDirection.x,
                CameraRotation = _characterCamera.Transform.rotation,
                JumpDown = _isJumpPressed,
                CrouchDown = _isCrouchingPressed == true,
                CrouchUp = _isCrouchingPressed == false,
                IsSprinting = _isSprintPressed
            };

            if (!_allowBunnyHop && _isJumpPressed)
            {
                _isJumpPressed = false;
            }

            // Apply inputs to character
            _character.SetInputs(ref characterInputs);
        }
    }
}