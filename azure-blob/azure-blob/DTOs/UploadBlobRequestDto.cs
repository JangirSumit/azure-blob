namespace azure_blob.DTOs;

public record UploadBlobRequestDto(string AccountName, string SASToken, string ContainerName, IFormFile FormFile);
