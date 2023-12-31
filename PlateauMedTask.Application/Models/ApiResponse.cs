﻿using PlateauMedTask.Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Application.Models
{
    /// <summary>
    /// Class ApiResponse.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public ApiResponseCode Code { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public List<string> Errors { get; set; } = new List<string>();
        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value><c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
        public bool HasErrors => Errors.Any();
    }

    /// <summary>
    /// Class ApiResponse.
    /// Implements the <see cref="PlateauMedTask.Application.Models.ApiResponse" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="PlateauMedTask.Application.Models.ApiResponse" />
    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <param name="codes">The codes.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="errors">The errors.</param>
        public ApiResponse(T data = default, string message = "",
            ApiResponseCode codes = ApiResponseCode.OK, int? totalCount = 0, params string[] errors)
        {
            Payload = data;
            Errors = errors.ToList();
            Code = codes;
            Description = message;
            TotalCount = totalCount ?? 0;
        }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>The payload.</value>
        public T Payload { get; set; }
        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }
        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>The response code.</value>
        public string ResponseCode { get; set; }
    }
}
