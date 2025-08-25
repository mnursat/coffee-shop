namespace CoffeeShop.Contracts.Jwt;

public record RefreshTokenRequestDto(Guid UserId, string RefreshToken);
