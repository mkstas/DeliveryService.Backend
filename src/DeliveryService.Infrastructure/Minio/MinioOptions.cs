namespace DeliveryService.Infrastructure.Minio
{
    /// <summary>
    /// Represents the settings used to configure the MinIO client.
    /// </summary>
    public class MinioOptions
    {
        /// <summary>
        /// The MinIO server endpoint.
        /// </summary>
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// The access key used to authenticate with MinIO.
        /// </summary>
        public string AccessKey { get; set;  } = string.Empty;

        /// <summary>
        /// The secret key used to authenticate with MinIO.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// The name of the bucket used to store files.
        /// </summary>
        public string BucketName { get; set; } = string.Empty;
    }
}
