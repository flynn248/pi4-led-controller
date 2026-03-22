using System;
using System.Collections.Generic;
using System.Text;
using Led.SharedKernal.DDD;

namespace Led.Domain.Users.Events;

public sealed record UserUsernameUpdatedDomainEvent(Guid Id) : IDomainEvent;
