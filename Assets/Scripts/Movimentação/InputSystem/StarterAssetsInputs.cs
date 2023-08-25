using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem; // Importa o namespace InputSystem se ENABLE_INPUT_SYSTEM estiver definido
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Valores de Entrada do Personagem")]
        public Vector2 move; // Entrada de movimento do jogador
        public Vector2 look; // Entrada de olhar da câmera do jogador
        public bool jump;    // Entrada de pulo do jogador
        public bool sprint;  // Entrada de corrida do jogador

        [Header("Configurações de Movimento")]
        public bool analogMovement; // Se deve usar movimento analógico (joystick analógico)

        [Header("Configurações do Cursor do Mouse")]
        public bool cursorLocked = true;       // Trava ou destrava o cursor
        public bool cursorInputForLook = true; // Usa a entrada do cursor para olhar

#if ENABLE_INPUT_SYSTEM
        // Métodos de entrada usando o InputSystem
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>()); // Obtém e processa a entrada de movimento
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>()); // Obtém e processa a entrada de olhar da câmera se cursorInputForLook for verdadeiro
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed); // Processa a entrada de pulo
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed); // Processa a entrada de corrida
        }
#endif

        // Métodos para lidar com a entrada
        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection; // Armazena a entrada de movimento
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection; // Armazena a entrada de olhar da câmera
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState; // Armazena a entrada de pulo
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState; // Armazena a entrada de corrida
        }

        // Método para lidar com o estado do cursor quando a aplicação perde ou ganha foco
        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked); // Define o estado do cursor com base no valor de cursorLocked
        }

        // Método para definir o estado do cursor
        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None; // Trava ou destrava o cursor com base no valor de newState
        }
    }
}
