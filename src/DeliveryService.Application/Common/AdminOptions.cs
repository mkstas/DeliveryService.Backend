namespace DeliveryService.Application.Common
{
    /// <summary>
    /// Represents the default administrator account created on the first application startup.
    /// </summary>
    public class AdminOptions
    {
        /// <summary>
        /// The administrator's email address.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// The administrator's password.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// The administrator's name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
