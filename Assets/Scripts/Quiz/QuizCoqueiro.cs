using System.Collections.Generic;
using Dialogos.ObjectsOfDialogos;
using Quiz;
using UnityEngine;

namespace Dialogos.Services
{
    public class QuizCoqueiro : QuestionManager
    {
        [SerializeField] private List<ObjCoco> cocos = new(){null, null, null, null, null};
        private List<int> cocosAtivados = new();
        
        protected override void Acertou(int index)
        {
            base.Acertou(index);
            
            if (cocos != null)
            {
                if (cocosAtivados.Count == cocos.Count) return;

                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, cocos.Count);
                } while (cocosAtivados.Contains(randomIndex));
                
                cocos[randomIndex].obj.SetActive(true);
                
                cocosAtivados.Add(randomIndex);
            }
        }
    }
}