using UnityEngine;

namespace Infrastructure.Player
{
    public interface IPlayerTargetReceiver
    {
        void SetPlayerTarget(Transform target);
    }
}