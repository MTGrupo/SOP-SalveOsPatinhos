using Assets.Scripts.Dialogos.Modal;
using UnityEngine;

namespace Dialog
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