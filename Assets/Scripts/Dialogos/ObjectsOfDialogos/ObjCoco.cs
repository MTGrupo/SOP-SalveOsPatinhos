using Dialogos.Services;

namespace Dialogos.ObjectsOfDialogos
{
    public class ObjCoco : ObjectBase
    {
        private static string CocoItemName = "coco";
        private static int _currentCocos;

        private void Start()
        {
            base.Start();
            _currentCocos = 0;
        }
        
        public override void OnPlayerInteraction()
        {
            base.OnPlayerInteraction();
            _currentCocos++;

            if (_currentCocos == 3)
            {
                DialogDuckAguaDeCoco.Instance.ShowMensagemCoco();
            }
        }
    }
}