namespace azure_blob.DTOs;

public record CreateContainerRequestDto(string AccountName, string SASToken);
