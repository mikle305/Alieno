using Additional.Constants;
using UnityEngine;

namespace Services
{
    public abstract class InputServiceBase : IInputService
    {
        public Vector2 GetMoveDirection()
            => new Vector2(SimpleInput.GetAxisRaw(InputConstants.Horizontal), SimpleInput.GetAxisRaw(InputConstants.Vertical)).normalized;

        public bool IsDashInvoked()
            => SimpleInput.GetAxisRaw(InputConstants.Dash) != 0;
    }
}