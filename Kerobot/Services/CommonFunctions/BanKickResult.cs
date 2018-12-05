﻿using System;
using Discord.Net;

namespace Kerobot
{
    // Instances created by CommonFunctionService, but are meant to be sent to modules. Hence its namespace.

    /// <summary>
    /// Contains information on various success/failure outcomes for a ban or kick operation.
    /// </summary>
    public class BanKickResult
    {
        private readonly bool _userNotFound;

        internal BanKickResult(HttpException error, bool notifySuccess, bool errorNotFound)
        {
            OperationError = error;
            MessageSendSuccess = notifySuccess;
            _userNotFound = errorNotFound;
        }

        /// <summary>
        /// Gets a value indicating whether the kick or ban succeeded.
        /// </summary>
        public bool OperationSuccess { get => OperationError == null; }

        /// <summary>
        /// The exception thrown, if any, when attempting to kick or ban the target.
        /// </summary>
        public HttpException OperationError { get; }

        /// <summary>
        /// Indicates if the operation failed due to being unable to find the user.
        /// </summary>
        public bool ErrorNotFound
        {
            get
            {
                if (_userNotFound) return true; // TODO I don't like this.
                if (OperationSuccess) return false;
                return OperationError.HttpCode == System.Net.HttpStatusCode.NotFound;
            }
        }

        /// <summary>
        /// Indicates if the operation failed due to a permissions issue.
        /// </summary>
        public bool ErrorForbidden
        {
            get
            {
                if (OperationSuccess) return false;
                return OperationError.HttpCode == System.Net.HttpStatusCode.Forbidden;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the user was able to receive the ban or kick message.
        /// </summary>
        /// <value>
        /// <see langword="false"/> if an error was encountered when attempting to send the target a DM. Will always
        /// return <see langword="true"/> otherwise, including cases in which no message was sent.
        /// </value>
        public bool MessageSendSuccess { get; }
    }
}
