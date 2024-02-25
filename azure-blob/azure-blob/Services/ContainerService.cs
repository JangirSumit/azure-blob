using Azure.Storage.Blobs;
using Azure;
using azure_blob.DTOs;
using Azure.Storage.Blobs.Models;

namespace azure_blob.Services;

public class ContainerService() : IContainerService
{
    private BlobServiceClient? _blobServiceClient;

    public void Init(BlobServiceClient? blobServiceClient)
    {
        _blobServiceClient = blobServiceClient ?? throw new NotImplementedException();
    }

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

    public async Task<List<string>> GetContainers(string prefix)
    {
        try
        {
            var containers = new List<string>();

            var resultSegment = _blobServiceClient.GetBlobContainersAsync(BlobContainerTraits.Metadata, prefix, default)
                .AsPages(default);

            await foreach (Page<BlobContainerItem> containerPage in resultSegment)
            {
                foreach (BlobContainerItem containerItem in containerPage.Values)
                {
                    containers.Add(containerItem.Name);
                }
            }

            return containers;
        }
        catch (RequestFailedException)
        {
            throw;
        }
    }
}
