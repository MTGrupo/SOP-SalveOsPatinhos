using System.Collections.Generic;
using Dialogos.ObjectsOfDialogos;
using Quiz;
using UnityEngine;

namespace Dialogos.Services
{
    public class QuizCoqueiro : QuestionManager
    {
        [SerializeField] private List<ObjCoco> cocos = new(){null, null, null, null, null};
        private List<int> cocosActived = new();
        
        protected override void Right(int index)
        {
            base.Right(index);
            
            if (cocos != null)
            {
                if (cocosActived.Count == cocos.Count) return;

                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, cocos.Count);
                } while (cocosActived.Contains(randomIndex));
                
                cocos[randomIndex].obj.SetActive(true);
                
                cocosActived.Add(randomIndex);
            }
        }
    }
}