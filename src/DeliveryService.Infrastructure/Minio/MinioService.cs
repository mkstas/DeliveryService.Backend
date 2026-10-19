using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace DeliveryService.Infrastructure.Minio
{
    /// <summary>
    /// Provides file management operations in the configured MinIO bucket.
    /// </summary>
    public class MinioService(IMinioClient minioClient, IOptions<MinioOptions> options) : IMinioService
    {
        private readonly string _bucketName = options.Value.BucketName;

        public async Task UploadFileAsync(string objectName, Stream stream, string contentType)
        {
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(contentType);

            await minioClient.PutObjectAsync(putObjectArgs);
        }
        public async Task<(Stream Stream, string ContentType)> DownloadFileAsync(string objectName)
        {
            var memoryStream = new MemoryStream();

            var getObjectArgs = new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream));

            await minioClient.GetObjectAsync(getObjectArgs);

            memoryStream.Position = 0;

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(objectName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return (memoryStream, contentType);
        }

        public async Task DeleteFileAsync(string objectName)
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName);

            await minioClient.RemoveObjectAsync(removeObjectArgs);
        }
    }
}
