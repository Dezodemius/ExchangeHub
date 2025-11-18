using System;
using System.Collections.Generic;

namespace ExchangeHub.Shared.DTO;

public record UserDto(Guid Id, string Name, IEnumerable<string> Favorites);