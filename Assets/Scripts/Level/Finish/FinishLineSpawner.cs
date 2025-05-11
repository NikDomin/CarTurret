using UnityEngine;
using Zenject;

namespace Level.Finish
{
    public class FinishLineSpawner : IInitializable
    {
        private readonly DiContainer container;
        private readonly GameObject finishLinePrefab;
        private readonly float spawnDistance;
        private readonly IFinishLineReceiver targetReceiver;

        public FinishLineSpawner(DiContainer container, GameObject finishLinePrefab, float spawnDistance,
            IFinishLineReceiver finishLineReceiver)
        {
            this.container = container;
            this.finishLinePrefab = finishLinePrefab;
            this.spawnDistance = spawnDistance;
            targetReceiver = finishLineReceiver;
        }

        public void Initialize()
        {
            Vector3 spawnPosition = new Vector3(0, 0, spawnDistance);

            var finishLineInstance = container
                .InstantiatePrefabForComponent<FinishLine>(
                        finishLinePrefab,
                        spawnPosition,
                        Quaternion.identity,
                        null
                    );
            targetReceiver.SetFinishLine(finishLineInstance.transform);
        }
    }
}