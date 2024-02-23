using Azure.Storage.Blobs;
using Azure;
using azure_blob.DTOs;

namespace azure_blob.Services;

public class ContainerService() : IContainerService
{
    private BlobServiceClient? _blobServiceClient;

    public async Task<CreateContainerResponseDto?> CreateContainerAsync(string name)
    {
        // Name the sample container based on new GUID to ensure uniqueness.
        // The container name must be lowercase.
        string containerName = "container-" + name.Replace(" ", "");
        try
        {
            // Create the container
            BlobContainerClient container = await _blobServiceClient.CreateBlobContainerAsync(containerName);

            if (await container.ExistsAsync())
            {
                Console.WriteLine("Created container {0}", container.Name);
                return new CreateContainerResponseDto(true, container.Name);
            }
        }
        catch (RequestFailedException)
        {
            throw;
        }

        return new CreateContainerResponseDto(false, "");
    }

    public async Task DeleteContainerAsync(string containerName)
    {
        BlobContainerClient container = _blobServiceClient.GetBlobContainerClient(containerName);

        try
        {
            await container.DeleteAsync();
        }
        catch (RequestFailedException)
        {
            throw;
        }
    }

    public void Init(BlobServiceClient? blobServiceClient)
    {
        _blobServiceClient = blobServiceClient ?? throw new NotImplementedException();
    }
}
