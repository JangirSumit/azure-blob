using Azure.Storage.Blobs;

namespace azure_blob.Services;

public interface IBlobService
{
    void Init(BlobServiceClient blobServiceClient);
    Task<bool> UploadBlobAsync(string containerName, IFormFile formFile);
}
