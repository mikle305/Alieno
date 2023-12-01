using Additional.Game;
using UnityEngine;

namespace Services
{
    public class InputService : MonoSingleton<InputService>
    {
        public Vector2 GetMoveDirection() 
            => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        public bool IsDashInvoked()
            => Input.GetKey("space");
    }
}