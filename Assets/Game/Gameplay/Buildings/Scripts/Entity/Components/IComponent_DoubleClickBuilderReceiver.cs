
using Entities;
using System;

public interface IComponent_DoubleClickBuilderReceiver
{
    event Action<UnityEntity> OnClickBuilding;
}
