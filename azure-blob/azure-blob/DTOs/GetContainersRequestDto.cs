namespace azure_blob.DTOs;

public record GetContainersRequestDto(string AccountName, string SASToken, string ContainerName);
