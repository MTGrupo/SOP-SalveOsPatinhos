using System;
using Dialogos.Enum;
using Mono.Cecil;
using UnityEngine;

namespace Dialogos
{
    [Serializable]
    public class Dialogo
    {
        public int id;
        public string orador;
        public TypeSpeaker typeSpeaker;
        [TextArea(3, 10)]
        public string texto;
        public TipoDialogoEnum TipoDialogoEnum;
    }
}