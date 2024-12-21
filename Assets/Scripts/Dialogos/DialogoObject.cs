using System.Collections.Generic;
using UnityEngine;

namespace Dialogos
{
    [CreateAssetMenu]
    public class DialogoObject : ScriptableObject
    {
        public List<Dialogo> dialogos = new();

        public void UpdateDialogoForId(int id, string texto, string orador)
        {
            foreach (var dialogo in dialogos)
            {
                if (dialogo.id == id)
                {
                    dialogo.texto = texto;
                    dialogo.orador = orador;
                }
            }
        }
        
        public Dialogo GetDialogoAt(int index)
        {
            return dialogos[index];
        }
    }
}