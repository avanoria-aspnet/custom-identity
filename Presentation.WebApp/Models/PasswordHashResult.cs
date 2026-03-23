namespace Presentation.WebApp.Models;

public record PasswordHashResult
(
    string HashedPassword,
    string Salt
);