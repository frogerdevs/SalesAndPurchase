﻿using SalesAndPurchase.Server.Domain.Entities.Base;

namespace SalesAndPurchase.Server.Application.Entities
{
    public class BaseEntity : IBaseEntity
    {
    }
    public class BaseEntity<Id> : BaseEntity, IBaseEntity<Id>
    {
        Id IBaseEntity<Id>.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
