using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.API.Exception
{
    /// <summary>
    /// 未知异常
    /// </summary>
    [Serializable]
    public class BaseException : System.Exception
    {
        public BaseException() { }
        public BaseException(string message) : base(message) { }
        public BaseException(string message, System.Exception inner) : base(message, inner) { }
        protected BaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    /// <summary>
    /// 权限不足
    /// </summary>
    [Serializable]
    public class PermissionTooLowException : System.Exception
    {
        public PermissionTooLowException() { }
        public PermissionTooLowException(string message) : base(message) { }
        public PermissionTooLowException(string message, System.Exception inner) : base(message, inner) { }
        protected PermissionTooLowException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    /// <summary>
    /// 登陆失败异常
    /// </summary>
    [Serializable]
    public class LoginFailureException : System.Exception
    {
        public LoginFailureException() { }
        public LoginFailureException(string message) : base(message) { }
        public LoginFailureException(string message, System.Exception inner) : base(message, inner) { }
        protected LoginFailureException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
