using System;
using Dialogos.Enum;
using UnityEngine;

namespace Dialogos
{
    [Serializable]
    public class Dialogo
    {
        public int id;
        public string orador;
        [TextArea(3, 10)]
        public string texto;
        public TipoDialogoEnum TipoDialogoEnum;
    }
}