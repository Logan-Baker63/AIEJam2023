
using UnityEngine;
using UnityEngine.LowLevel;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

/// <summary>
/// Interpolates a GameObject's position and rotation while being updated in FixedUpdate.
/// </summary>
public class FixedUpdateInterpolation : MonoBehaviour
{
    #region Player Loop Setup
    private struct EarlyFixedUpdateLoop { }
    private struct LateFixedUpdateLoop { }

    private static readonly List<FixedUpdateInterpolation> allEnabledInstances = new List<FixedUpdateInterpolation>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Initialize()
    {
        var rootSystem = PlayerLoop.GetCurrentPlayerLoop();

        var early = new PlayerLoopSystem
        {
            updateDelegate = OnEarlyFixedUpdate,
            type = typeof(EarlyFixedUpdateLoop)
        };

        var late = new PlayerLoopSystem
        {
            updateDelegate = OnLateFixedUpdate,
            type = typeof(LateFixedUpdateLoop)
        };

        AttachPlayerLoopBefore<FixedUpdate, FixedUpdate.ScriptRunBehaviourFixedUpdate>(ref rootSystem, early);
        AttachPlayerLoopBefore<FixedUpdate, FixedUpdate.ScriptRunDelayedFixedFrameRate>(ref rootSystem, late);

        PlayerLoop.SetPlayerLoop(rootSystem);
    }

    private static void AttachPlayerLoopBefore<TSystem, TSubSystem>(ref PlayerLoopSystem rootSystem, PlayerLoopSystem newSystem)
    {
        for (var systemIndex = 0; systemIndex < rootSystem.subSystemList.Length; systemIndex++)
        {
            var system = rootSystem.subSystemList[systemIndex];

            if (system.type == typeof(TSystem))
            {
                for (var subSystemIndex = 0; subSystemIndex < system.subSystemList.Length; subSystemIndex++)
                {
                    var subSystem = system.subSystemList[subSystemIndex];

                    if (subSystem.type == typeof(TSubSystem))
                    {
                        InsertIntoArray(ref system.subSystemList, newSystem, subSystemIndex);
                        rootSystem.subSystemList[systemIndex] = system;
                        return;
                    }
                }
            }
        }
    }

    private static void InsertIntoArray<T>(ref T[] array, T element, int index = -1)
    {
        List<T> list;
        if (array == null)
        {
            list = new List<T>();
        }
        else
        {
            list = new List<T>(array);
        }

        if (index >= 0)
        {
            list.Insert(index, element);
        }
        else
        {
            list.Add(element);
        }

        array = list.ToArray();
    }

    private static void OnEarlyFixedUpdate()
    {
        foreach (var instance in allEnabledInstances)
        {
            instance.EarlyFixedUpdate();
        }
    }

    private static void OnLateFixedUpdate()
    {
        foreach (var instance in allEnabledInstances)
        {
            instance.LateFixedUpdate();
        }
    }
    #endregion

    new private Transform transform;

    private Vector3 pos0;
    private Vector3 pos1;
    private Quaternion rot0;
    private Quaternion rot1;

    private Vector3 lastUpdatePos;
    private Quaternion lastUpdateRot;

    private Transform lastUpdateParent;

    private void Awake()
    {
        transform = base.transform;
    }

    private void OnEnable()
    {
        lastUpdatePos = pos0 = pos1 = transform.localPosition;
        lastUpdateRot = rot0 = rot1 = transform.localRotation;
        lastUpdateParent = transform.parent;

        allEnabledInstances.Add(this);
    }

    private void OnDisable()
    {
        allEnabledInstances.Remove(this);
    }

    private void EarlyFixedUpdate()
    {
        var pos = transform.localPosition;
        if (pos == lastUpdatePos)
        {
            transform.localPosition = pos1;
            lastUpdatePos = pos1;
        }
        else
        {
            AcceptUpdatedPosition(pos);
        }

        var rot = transform.localRotation;
        if (rot == lastUpdateRot)
        {
            transform.localRotation = rot1;
            lastUpdateRot = rot1;
        }
        else
        {
            AcceptUpdatedRotation(rot);
        }
    }

    private void LateFixedUpdate()
    {
        if (transform.parent != lastUpdateParent)
        {
            TranslateLocalTransformToNewParent();

            lastUpdateParent = transform.parent;
        }

        pos0 = pos1;
        pos1 = transform.localPosition;
        lastUpdatePos = pos1;

        rot0 = rot1;
        rot1 = transform.localRotation;
        lastUpdateRot = rot1;
    }

    private void TranslateLocalTransformToNewParent()
    {
        if (lastUpdateParent != null)
        {
            pos1 = lastUpdateParent.TransformPoint(pos1);
            rot1 = lastUpdateParent.rotation * rot1;
        }

        if (transform.parent != null)
        {
            pos1 = transform.parent.InverseTransformPoint(pos1);
            rot1 = Quaternion.Inverse(transform.parent.rotation) * rot1;
        }
    }

    private void Update()
    {
        var t = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;

        var pos = transform.localPosition;
        if (pos == lastUpdatePos)
        {
            var newPosition = Vector3.Lerp(pos0, pos1, t);
            transform.localPosition = newPosition;
            lastUpdatePos = newPosition;
        }
        else
        {
            AcceptUpdatedPosition(pos);
        }

        var rot = transform.localRotation;
        if (rot == lastUpdateRot)
        {
            var newRotation = Quaternion.Lerp(rot0, rot1, t);
            transform.localRotation = newRotation;
            lastUpdateRot = newRotation;
        }
        else
        {
            AcceptUpdatedRotation(rot);
        }
    }

    private void AcceptUpdatedPosition(Vector3 pos)
    {
        pos0 = pos1 = pos;
        lastUpdatePos = pos;
    }

    private void AcceptUpdatedRotation(Quaternion rot)
    {
        rot0 = rot1 = rot;
        lastUpdateRot = rot;
    }
}