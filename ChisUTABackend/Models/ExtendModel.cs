using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChisUTABackend.Models
{
    public class ExtendModel
    {
    }

    public class ChisUtaResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }

    #region Users
    public partial class Users
    {
        public string ConfirmPassword { get; set; }
    }

    #endregion
}