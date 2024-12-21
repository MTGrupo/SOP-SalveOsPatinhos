using Dialogos.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Dialogos.Services
{
    public class DialogCoqueiro : DialogWithQuiz
    {
        [FormerlySerializedAs("dialogDuckBaseAguaDeCoco")] [SerializeField] private DialogDuckAguaDeCoco dialogDuckAguaDeCoco;
        
        string textoCoqueiro = "Um pato me contou que você está procurando por cocos. Eu tenho um coco aqui, mas antes você terá que responder minhas perguntas";
        protected override void ShowDialogo()
        {
            if (dialogDuckAguaDeCoco.isDialogoCoco)
            {
                dialogoObject.UpdateDialogoForId(1, textoCoqueiro, "Coqueiro");
                dialogDuckAguaDeCoco.isDialogoCoco = false;
                base.ShowDialogo();
                
                zonaDeFechar.gameObject.SetActive(false);
                botaoProximo.gameObject.SetActive(true);
                
                return;
            }
            
            Random random = new Random();
            int index = random.Next(Speaks.AllSpeaks.Length);
        
            string frasesAleatorias = Speaks.AllSpeaks[index];
            
            dialogoObject.UpdateDialogoForId(1, frasesAleatorias, "Coqueiro");
            
            base.ShowDialogo();
        }
    }
}