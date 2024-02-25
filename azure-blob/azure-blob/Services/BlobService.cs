using Azure.Storage.Blobs;
using Azure;

namespace azure_blob.Services;

public class BlobService : IBlobService
{
    private BlobServiceClient? _blobServiceClient;

    public void Init(BlobServiceClient? blobServiceClient)
    {
        _blobServiceClient = blobServiceClient ?? throw new NotImplementedException();
    }

    public async Task<bool> UploadBlobAsync(string containerName, IFormFile formFile)
    {
        try
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient?.GetBlobClient(formFile.FileName);

            await blobClient?.UploadAsync(formFile.OpenReadStream());
            return true;
        }
        catch (RequestFailedException)
        {
            throw;
        }
    }
}
