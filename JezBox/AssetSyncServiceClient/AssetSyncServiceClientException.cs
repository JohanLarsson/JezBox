using System;

namespace JezBox.AssetSyncServiceClient {
    /// <summary>
    /// Represents errors that occur during AssetSync service client execution.
    /// The Data dictionary will have an "AssetSyncServiceClientErrorType" entry indicating the type of exception error
    /// that occurred, whose value will be from an enumeration of error types (underlying type int) the first of which (value 0)
    /// will indicate 'Other'.  For exceptions intended to just be displayed to users, this entry will be set to Other, but for
    /// exceptions that calling code may want to handle, this may be set to another value in the enumeration.
    /// </summary>
    [Serializable]
    public class AssetSyncServiceClientException : Exception {
        #region Private vars
        private const string _defaultMsg = "There was a miscellaneous error with the AssetSync service client.";
        private const int _assetSyncServiceClientErrorTypeOther = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the AssetSyncServiceClientException class.
        /// </summary>
        public AssetSyncServiceClientException(): base(_defaultMsg) {
            this.Data["AssetSyncServiceClientErrorType"] = _assetSyncServiceClientErrorTypeOther;
        }

        /// <summary>
        /// Initializes a new instance of the AssetSyncServiceClientException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AssetSyncServiceClientException(string message): base(message) {
            this.Data["AssetSyncServiceClientErrorType"] = _assetSyncServiceClientErrorTypeOther;
        }

        /// <summary>
        /// Initializes a new instance of the AssetSyncServiceClientException class with an enum value specifying the error
        /// type of the exception.
        /// </summary>
        /// <param name="errorType">An enum value specifying the error type of the exception.</param>
        public AssetSyncServiceClientException(int errorType): base() {
            this.Data["AssetSyncServiceClientErrorType"] = errorType;
        }

        /// <summary>
        /// Initializes a new instance of the AssetSyncServiceClientException class with a specified error message and an
        /// enum value specifying the error type of the exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="errorType">An enum value specifying the error type of the exception.</param>
        public AssetSyncServiceClientException(string message, int errorType): base(message) {
            this.Data["AssetSyncServiceClientErrorType"] = errorType;
        }

        /// <summary>
        /// Initializes a new instance of the AssetSyncServiceClientException class with a specified error message and a
        /// reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public AssetSyncServiceClientException(string message, Exception innerException): base(message, innerException) {
            this.Data["AssetSyncServiceClientErrorType"] = _assetSyncServiceClientErrorTypeOther;
        }

        /// <summary>
        /// Initializes a new instance of the AssetSyncServiceClientException class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected AssetSyncServiceClientException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context): base(info, context) {
            this.Data["AssetSyncServiceClientErrorType"] = _assetSyncServiceClientErrorTypeOther;
        }
        #endregion
    }
}
