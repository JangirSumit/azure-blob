using Azure.Storage.Blobs;
using azure_blob.DTOs;

namespace azure_blob.Services;

public interface IContainerService
{
    void Init(BlobServiceClient blobServiceClient);
    Task<CreateContainerResponseDto?> CreateContainerAsync(string name);
    Task DeleteContainerAsync(string name);
    Task<List<string>> GetContainers(string prefix);
}
