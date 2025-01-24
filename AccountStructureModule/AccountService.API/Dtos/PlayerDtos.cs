namespace AccountService.API.Dtos
{
    public record PlayerCreateDto(string Username, string FirstName, string LastName, string Password);
    public record PlayerUpdateDto(string Id, string Username, string FirstName, string LastName);
}
