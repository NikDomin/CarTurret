using Infrastructure.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UIMenu : MonoBehaviour, IInitializable
    {
        [SerializeField] private Image winImage;
        [SerializeField] private Button winButton;
        [SerializeField] private Image loseImage;
        [SerializeField] private Button loseButton;
        
        private SignalBus signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void Initialize()
        {
            winButton.onClick.AddListener(OnClicked);
            loseButton.onClick.AddListener(OnClicked);
            signalBus.Subscribe<WinSignal>(OnWin);
            signalBus.Subscribe<PlayerDeathSignal>(OnLose);
        }

        private void OnLose()
        {
            loseImage.gameObject.SetActive(true);
        }

        private void OnWin()
        {
            winImage.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            winButton.onClick.RemoveListener(OnClicked);
            loseButton.onClick.RemoveListener(OnClicked);
            signalBus.Unsubscribe<WinSignal>(OnWin);
            signalBus.Unsubscribe<PlayerDeathSignal>(OnLose);
        }
        private void OnClicked()
        {
            signalBus.Fire<LevelRestartButtonPressedSignal>();
            winImage.gameObject.SetActive(false);
            loseImage.gameObject.SetActive(false);
        }
    }
}