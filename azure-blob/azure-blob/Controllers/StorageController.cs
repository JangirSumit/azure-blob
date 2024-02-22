using Azure.Storage.Blobs;
using azure_blob.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace azure_blob.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController(ILogger<StorageController> logger) : ControllerBase
{
    private readonly ILogger<StorageController> _logger = logger;

    [HttpPost("CreateBlobContainer")]
    public IEnumerable<string> CreateBlobContainer(CreateContainerRequestDto createContainerRequest)
    {
        return ["Sumit"];
    }

    public static BlobServiceClient GetBlobServiceClientSAS(string accountName, string sasToken)
    {
        string blobUri = "https://" + accountName + ".blob.core.windows.net";

        return new BlobServiceClient(new Uri($"{blobUri}?{sasToken}"), null);
    }
}
