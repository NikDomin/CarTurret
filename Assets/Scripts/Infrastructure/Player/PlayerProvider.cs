using UnityEngine;

namespace Infrastructure
{
    public class PlayerProvider
    {
        public GameObject Player { get; private set; }

        public void SetPlayer(GameObject player)
        {
            Player = player;
        }
    }
}