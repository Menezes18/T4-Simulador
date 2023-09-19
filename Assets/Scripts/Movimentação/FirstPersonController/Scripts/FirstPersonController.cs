using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Jogador")]
        [Tooltip("Velocidade de movimento do personagem em m/s")]
        public float MoveSpeed = 4.0f;
        [Tooltip("Velocidade de corrida do personagem em m/s")]
        public float SprintSpeed = 6.0f;
        [Tooltip("Velocidade de rotação do personagem")]
        public float RotationSpeed = 1.0f;
        [Tooltip("Aceleração e desaceleração")]
        public float SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("A altura que o jogador pode pular")]
        public float JumpHeight = 1.2f;
        [Tooltip("O personagem usa o valor próprio de gravidade. O padrão do motor é -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Tempo necessário para passar antes de poder pular novamente. Defina como 0f para pular instantaneamente novamente")]
        public float JumpTimeout = 0.1f;
        [Tooltip("Tempo necessário para passar antes de entrar no estado de queda. Útil para descer escadas")]
        public float FallTimeout = 0.15f;

        [Header("Jogador no Chão")]
        [Tooltip("Se o personagem está no chão ou não. Não faz parte da verificação de chão embutida no CharacterController")]
        public bool Grounded = true;
        [Tooltip("Útil para terrenos irregulares")]
        public float GroundedOffset = -0.14f;
        [Tooltip("O raio da verificação de chão. Deve ser igual ao raio do CharacterController")]
        public float GroundedRadius = 0.5f;
        [Tooltip("Quais camadas o personagem considera como chão")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("O alvo de acompanhamento definido na Virtual Camera do Cinemachine que a câmera irá acompanhar")]
        public GameObject CinemachineCameraTarget;
        [Tooltip("Até que ponto em graus a câmera pode ser movida para cima")]
        public float TopClamp = 90.0f;
        [Tooltip("Até que ponto em graus a câmera pode ser movida para baixo")]
        public float BottomClamp = -90.0f;

        public float targetSpeed;
        // cinemachine
        private float _cinemachineTargetPitch;

        // jogador
        private float _speed;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;
        
        public GameObject characterWithAnimator; // Essa variável será atribuída no Editor Unity
        private Animator _animator;
#if ENABLE_INPUT_SYSTEM
        private PlayerInput _playerInput;
#endif
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;
        private ManagerPlayer _managerPlayer;

        public AudioSource passosAudioSource;
        public AudioClip[] passosAudioClip;
        private bool IsCurrentDeviceMouse
        {
            get
            {
                #if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
                #else
                return false;
                #endif
            }
        }

        private void Awake()
        {
            // obtenha uma referência para a nossa câmera principal
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else
    Debug.LogError("O pacote Starter Assets está faltando dependências. Use Ferramentas/Starter Assets/Reinstalar Dependências para corrigir");
#endif

            // Obtenha a referência ao Animator do GameObject separado
            _animator = characterWithAnimator.GetComponent<Animator>();
    
            // Redefina nossos timeouts no início
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {
            JumpAndGravity();
            GroundedCheck();
            Move();

        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void GroundedCheck()
        {
            // defina a posição da esfera, com compensação
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        }

        private void CameraRotation()
        {
            // se houver uma entrada
            if (_input.look.sqrMagnitude >= _threshold)
            {
                // Não multiplique a entrada do mouse por Time.deltaTime
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
                _rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

                // limite nossa rotação de inclinação
                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

                // Atualize a inclinação do alvo da câmera Cinemachine
                CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

                // gire o jogador para a esquerda e para a direita
                transform.Rotate(Vector3.up * _rotationVelocity);
            }
        }

        private void Move()
        {
            if(targetSpeed <= 0.1f)
                // defina a velocidade alvo com base na velocidade de movimento, velocidade de corrida e se a corrida está pressionada
             targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // uma aceleração e desaceleração simplista projetada para ser fácil de remover, substituir ou iterar

            // nota: o operador == de Vector2 usa aproximação, portanto, não é propenso a erros de ponto flutuante e é mais barato do que a magnitude
            // se não houver entrada, defina a velocidade alvo como 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // uma referência à velocidade horizontal atual do jogador
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // acelerar ou desacelerar até a velocidade alvo
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // cria um resultado curvo em vez de linear, resultando em uma mudança de velocidade mais orgânica
                // observe que o T em Lerp é limitado, então não precisamos limitar nossa velocidade
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // arredonde a velocidade para 3 casas decimais
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            // normalize a direção da entrada
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // nota: o operador != de Vector2 usa aproximação, portanto, não é propenso a erros de ponto flutuante e é mais barato do que a magnitude
            // se houver entrada de movimento, gire o jogador quando o jogador estiver se movendo
            if (_input.move != Vector2.zero)
            {
                // mova
                inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
            }
            if (_input.sprint)
            {
                targetSpeed = SprintSpeed;

                _animator.SetBool("Correr", true);
                
            }
            else
            {
                targetSpeed = MoveSpeed;

                // Verifique se o _animator não é nulo (certifique-se de que a referência foi atribuída no Unity Inspector)
                if (_animator != null)
                {
                    _animator.SetBool("Correr", false);
                }
            }
            // mova o jogador
            _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // redefina o temporizador de timeout de queda
                _fallTimeoutDelta = FallTimeout;

                // pare nossa velocidade de cair infinitamente quando estiver no chão
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Pular
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // a raiz quadrada de H * -2 * G = quanto velocidade é necessária para alcançar a altura desejada
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                }

                // timeout de pulo
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // redefina o temporizador de timeout de pulo
                _jumpTimeoutDelta = JumpTimeout;

                // timeout de queda
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }

                // se não estiver no chão, não pule
                _input.jump = false;
            }

            // aplique a gravidade ao longo do tempo se estiver abaixo da velocidade terminal (multiplique pelo delta time duas vezes para acelerar linearmente ao longo do tempo)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // quando selecionado, desenhe um gizmo na posição e raio correspondentes do colisor aterrado
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }

        private void Passos()
        {
            passosAudioSource.PlayOneShot(passosAudioClip[Random.Range(0, passosAudioClip.Length)]);
        }
    }       
}
