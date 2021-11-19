using UnityEngine;

[SerializeField]
public class MachineCanvas : MonoBehaviour
{
    public MachineIgrendient machineType;

    public MachineCanvas(MachineIgrendient _machineType)
    {
        machineType = _machineType;
    }
}