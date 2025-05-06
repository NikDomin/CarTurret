using Cysharp.Threading.Tasks;

namespace Infrastructure.FSM
{
    public interface IGameState
    {
        UniTask Enter();
        UniTask Exit();   
    }
}