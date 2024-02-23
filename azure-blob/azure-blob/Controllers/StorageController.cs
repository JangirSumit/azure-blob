using Azure;
using Azure.Storage.Blobs;
using azure_blob.DTOs;
using azure_blob.Services;
using Microsoft.AspNetCore.Mvc;

namespace azure_blob.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController(IContainerService containerService, ILogger<StorageController> logger) : ControllerBase
{
    private readonly IContainerService _containerService = containerService;
    private readonly ILogger<StorageController> _logger = logger;

    [HttpPost("CreateBlobContainer")]
    public async Task<CreateContainerResponseDto?> CreateBlobContainer(CreateContainerRequestDto createContainerRequest)
    {
        try
        {
            _containerService.Init(GetBlobServiceClientSAS(createContainerRequest.AccountName, createContainerRequest.SASToken));
            return await _containerService.CreateContainerAsync(createContainerRequest.ContainerName);
        }
        catch (RequestFailedException)
        {
            return new CreateContainerResponseDto(false, "");
        }
    }

    [HttpPost("DeleteBlobContainer")]
    public async Task<bool> DeleteBlobContainer(DeleteContainerRequestDto createContainerRequest)
    {
        try
        {
            _containerService.Init(GetBlobServiceClientSAS(createContainerRequest.AccountName, createContainerRequest.SASToken));
            await _containerService.CreateContainerAsync(createContainerRequest.ContainerName);
            return true;
        }
        catch (RequestFailedException)
        {
            return false;
        }
    }

    public static BlobServiceClient GetBlobServiceClientSAS(string accountName, string sasToken)
    {
        string blobUri = "https://" + accountName + ".blob.core.windows.net";

        return new BlobServiceClient(new Uri($"{blobUri}?{sasToken}"), null);
    }
}
