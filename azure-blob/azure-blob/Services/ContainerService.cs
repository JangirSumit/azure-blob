using Azure.Storage.Blobs;
using Azure;

namespace azure_blob.Services;

public class ContainerService : IContainerService
{
    private readonly BlobServiceClient? _blobServiceClient;

    public ContainerService(BlobServiceClient? blobServiceClient)
    {
        _blobServiceClient = blobServiceClient ?? throw new Exception("BlobServiceClient can not be null...");
    }

    public async Task<BlobContainerClient?> CreateContainerAsync(string name)
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
                return container;
            }
        }
        catch (RequestFailedException e)
        {
            Console.WriteLine("HTTP error code {0}: {1}",
                                e.Status, e.ErrorCode);
            Console.WriteLine(e.Message);
        }

        return null;
    }
}
