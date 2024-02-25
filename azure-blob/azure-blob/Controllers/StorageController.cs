using Azure;
using Azure.Storage.Blobs;
using azure_blob.DTOs;
using azure_blob.Services;
using Microsoft.AspNetCore.Mvc;

namespace azure_blob.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController(IContainerService containerService,
    IBlobService blobService,
    ILogger<StorageController> logger) : ControllerBase
{
    private readonly IContainerService _containerService = containerService;
    private readonly IBlobService _blobService = blobService;
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
            await _containerService.DeleteContainerAsync(createContainerRequest.ContainerName);
            return true;
        }
        catch (RequestFailedException)
        {
            return false;
        }
    }

    [HttpPost("GetBlobContainers")]
    public async Task<List<string>> GetBlobContainers(DeleteContainerRequestDto createContainerRequest)
    {
        try
        {
            _containerService.Init(GetBlobServiceClientSAS(createContainerRequest.AccountName, createContainerRequest.SASToken));
            return await _containerService.GetContainers(createContainerRequest.ContainerName);
        }
        catch (RequestFailedException)
        {
            return [];
        }
    }

    [HttpPost("UploadBlob")]
    public async Task<bool> UploadBlob(UploadBlobRequestDto uploadBlobRequest)
    {
        try
        {
            _blobService.Init(GetBlobServiceClientSAS(uploadBlobRequest.AccountName, uploadBlobRequest.SASToken));
            await _blobService.UploadBlobAsync(uploadBlobRequest.ContainerName, uploadBlobRequest.FormFile);
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
