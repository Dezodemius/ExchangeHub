using System;
using System.Collections.Generic;

namespace ExchangeHub.Shared.DTO;

public record CurrencyDto(string Code, string Name, decimal Rate);

public record UserDto(Guid Id, string Name, IEnumerable<string> Favorites);

public record AuthResponseDto(string Token);
