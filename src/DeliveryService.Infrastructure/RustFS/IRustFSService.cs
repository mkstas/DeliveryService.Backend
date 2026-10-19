namespace DeliveryService.Infrastructure.RustFS
{
    /// <summary>
    /// Provides operations for managing files in the RustFS object storage.
    /// </summary>
    public interface IRustFSService
    {
        /// <summary>
        /// Uploads a file to the storage.
        /// </summary>
        /// <param name="objectName">The name of the object in the storage.</param>
        /// <param name="stream">The content stream to upload.</param>
        /// <param name="contentType">The content type of the file.</param>
        Task UploadFileAsync(string objectName, Stream stream, string contentType);

        /// <summary>
        /// Downloads a file from the storage.
        /// </summary>
        /// <param name="objectName">The name of the object in the storage.</param>
        /// <returns>The file content stream and its content type.</returns>
        Task<(Stream Stream, string ContentType)> DownloadFileAsync(string objectName);

        /// <summary>
        /// Deletes a file from the storage.
        /// </summary>
        /// <param name="objectName">The name of the object in the storage.</param>
        Task DeleteFileAsync(string objectName);
    }
}
