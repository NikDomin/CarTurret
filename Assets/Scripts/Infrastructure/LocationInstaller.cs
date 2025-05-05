using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform StartPoint;
        public GameObject CarPrefab;
        public override void InstallBindings()
        {
            
        }
    }
}