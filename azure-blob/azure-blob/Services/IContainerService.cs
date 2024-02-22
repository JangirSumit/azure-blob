using Azure.Storage.Blobs;

namespace azure_blob.Services;

public interface IContainerService
{
    Task<BlobContainerClient?> CreateContainerAsync(string name);
}
