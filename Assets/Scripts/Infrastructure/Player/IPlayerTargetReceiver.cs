using UnityEngine;

namespace Infrastructure.Player
{
    public interface IPlayerTargetReceiver
    {
        public void SetPlayerTarget(Transform target);
    }
}