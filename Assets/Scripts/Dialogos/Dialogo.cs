using System;
using Dialog;
using Dialogos.Enum;
using UnityEngine;

namespace Dialogos
{
    [Serializable]
    public class Dialogo : IPage
    {
        public int id;
        public string orador;
        public TypeSpeaker typeSpeaker;
        [TextArea(3, 10)]
        public string texto;
        public TipoDialogoEnum TipoDialogoEnum;
        public string Speaker => orador;
        public string Text => texto;
    }
}