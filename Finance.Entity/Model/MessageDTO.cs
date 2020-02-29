using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Finance.Entity.Model
{
    [DataContract]
    public class MessageDTO<T>
    {
        public bool IsSuccess { get; set; }
        public string ReturnMessage { get; set; }
        public T Data { get; set; }
    }
}
