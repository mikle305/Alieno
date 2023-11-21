using Additional.Game;
using UnityEngine;

namespace Services
{
    public class InputService : MonoSingleton<InputService>
    {
        public Vector2 GetMoveDirection() 
            => new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        public bool IsDashInvoked()
            => Input.GetKey("space");
    }
}