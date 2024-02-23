namespace azure_blob.DTOs;

public record DeleteContainerRequestDto(string AccountName, string SASToken, string ContainerName);
