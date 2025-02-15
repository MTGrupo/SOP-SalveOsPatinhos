using System.Collections;
using Assets.Scripts.Dialogos.Modal;
using UnityEngine;

namespace Dialogos.Services.especificos
{
    public class DialogCreditos : DialogoBase
    {
        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            StartCoroutine(CloseDialogo());
        }

        private IEnumerator CloseDialogo()
        {
            yield return new WaitForSeconds(3f);
            FinishedDialogo();
        }
    }
}