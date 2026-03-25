using System;
using System.Collections.Generic;
using System.Text;
using Led.SharedKernal.DDD;

namespace Led.Domain.Tenants.Events;

public sealed record UserEmailUpdatedDomainEvent(Guid UserId, string NewEmail, string OldEmail) : IDomainEvent;
