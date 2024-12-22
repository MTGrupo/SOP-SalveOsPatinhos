using Dialogos.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Dialogos.Services
{
    public class DialogCoqueiro : DialogWithQuiz
    {
        [SerializeField] private DialogDuckAguaDeCoco dialogDuckAguaDeCoco;
        
        string textoCoqueiro = "Que bom vê-lo vamos testar seu conhecimento para obter as recompensas.\n";
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