using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    [CreateAssetMenu]
    public class PerguntaObj : ScriptableObject
    { 
        public List<Pergunta> pergunta;
    }
}