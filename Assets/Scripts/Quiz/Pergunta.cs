using System;
using System.Collections.Generic;

namespace Quiz
{
    [Serializable]
    public class Alternativa
    {
        public string alternativa;
    }

    [Serializable]
    public class Pergunta
    {
        public int index;
        public string pergunta;
        public string respostaCorreta;
        public List<Alternativa> alternativas = new() { null, null, null };
    }
}
