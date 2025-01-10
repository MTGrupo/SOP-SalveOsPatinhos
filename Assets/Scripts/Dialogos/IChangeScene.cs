using Dialogos.Enum;

namespace Dialogos
{
    public interface IChangeScene
    {
        void HandleSceneChange(TipoDialogoEnum tipoDialogoEnum);
    }
}