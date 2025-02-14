using Assets.Scripts.Dialogos.Modal;
using UnityEngine;

namespace Dialogos
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private DialogoBase dialog;

        void Start()
        {
            dialog.StartDialogo();
        }
    }
}