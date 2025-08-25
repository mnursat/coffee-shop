namespace CoffeeShop.Contracts.Jwt;

public record TokenResponseDto(string AccessToken, string RefreshToken);