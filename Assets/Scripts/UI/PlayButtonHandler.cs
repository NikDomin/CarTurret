using Cysharp.Threading.Tasks;
using Infrastructure.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PlayButtonHandler : MonoBehaviour, IInitializable, IRespawnable
    {
        [SerializeField] private Button playButton;
        private UniTaskCompletionSource clickSource;
        private SignalBus signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void Initialize()
        {
            playButton.onClick.AddListener(OnClicked);
        }

        private void Awake()
        {
            clickSource = new UniTaskCompletionSource();
        }
        
        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            signalBus.Fire<PlayButtonClickedSignal>();
            clickSource.TrySetResult();
            gameObject.SetActive(false);
        }

        public void Respawn()
        {
            if(!gameObject.activeInHierarchy)
                gameObject.SetActive(true);
        }
    }
}