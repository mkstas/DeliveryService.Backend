using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

namespace DeliveryService.Infrastructure.RustFS
{
    /// <summary>
    /// Provides file management operations in the RustFS bucket.
    /// </summary>
    public class RustFSService(IAmazonS3 s3Client, IOptions<RustFSOptions> options) : IRustFSService
    {
        private readonly string _bucketName = options.Value.BucketName;

        /// <summary>
        /// Uploads a file to the RustFS bucket.
        /// </summary>
        /// <param name="objectName">The name of the object in the storage.</param>
        /// <param name="stream">The content stream to upload.</param>
        /// <param name="contentType">The content type of the file.</param>
        public async Task UploadFileAsync(string objectName, Stream stream, string contentType)
        {
            await EnsureBucketAsync();

            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = objectName,
                InputStream = stream,
                ContentType = contentType,
                AutoCloseStream = false
            };

            await s3Client.PutObjectAsync(putObjectRequest);
        }

        /// <summary>
        /// Downloads a file from the RustFS bucket.
        /// </summary>
        /// <param name="objectName">The name of the object in the storage.</param>
        /// <returns>
        /// The file content stream and its content type, determined from the object name
        /// extension, falling back to <c>application/octet-stream</c>.
        /// </returns>
        public async Task<(Stream Stream, string ContentType)> DownloadFileAsync(string objectName)
        {
            using var response = await s3Client.GetObjectAsync(_bucketName, objectName);

            var memoryStream = new MemoryStream();

            await response.ResponseStream.CopyToAsync(memoryStream);

            memoryStream.Position = 0;

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(objectName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return (memoryStream, contentType);
        }

        /// <summary>
        /// Deletes a file from the RustFS bucket.
        /// </summary>
        /// <param name="objectName">The name of the object in the storage.</param>
        public async Task DeleteFileAsync(string objectName)
        {
            await s3Client.DeleteObjectAsync(_bucketName, objectName);
        }

        private async Task EnsureBucketAsync()
        {
            if (!await AmazonS3Util.DoesS3BucketExistV2Async(s3Client, _bucketName))
            {
                await s3Client.PutBucketAsync(new PutBucketRequest { BucketName = _bucketName });
            }
        }
    }
}
