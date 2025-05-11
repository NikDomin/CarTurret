using System;
using System.Collections.Generic;
using Infrastructure.Player;
using Zenject;

namespace Level
{
    public class LevelRespawn : IInitializable, IDisposable
    {
        private readonly List<IRespawnable> respawnables;
        private readonly SignalBus signalBus;

        public LevelRespawn(List<IRespawnable> respawnables, SignalBus signalBus)
        {
            this.respawnables = respawnables;
            this.signalBus = signalBus;
        }

        public void Initialize()
        {
            signalBus.Subscribe<LevelRestartSignal>(OnRestart);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<LevelRestartSignal>(OnRestart);
        }

        private void OnRestart()
        {
            foreach (var r in respawnables)
                r.Respawn();
        }
    }
}