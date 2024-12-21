using Dialogos.Services;
using UnityEngine;

namespace Dialogos.ObjectsOfDialogos
{
    public class ObjCoco : ObjectBase
    {
        private static int _currentCocos;
        
        void Start()
        {
            base.Start();
            _currentCocos = 0;
        }
        
        public override void OnPlayerInteraction()
        {
            base.OnPlayerInteraction();
            _currentCocos++;
            Debug.Log("Cocos: " + _currentCocos);

            if (_currentCocos == 3)
            {
                DialogDuckAguaDeCoco.Instance.ShowMensagemCoco();
            }
        }
    }
}