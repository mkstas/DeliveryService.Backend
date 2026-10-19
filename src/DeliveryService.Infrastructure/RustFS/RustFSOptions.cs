namespace DeliveryService.Infrastructure.RustFS
{
    /// <summary>
    /// Represents the settings used to configure the RustFS client.
    /// </summary>
    public class RustFSOptions
    {
        /// <summary>
        /// The RustFS service URL.
        /// </summary>
        public string ServiceURL { get; set; } = string.Empty;

        /// <summary>
        /// The access key used to authenticate with RustFS.
        /// </summary>
        public string AccessKey { get; set;  } = string.Empty;

        /// <summary>
        /// The secret key used to authenticate with RustFS.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// The name of the bucket used to store files.
        /// </summary>
        public string BucketName { get; set; } = string.Empty;
    }
}
