using Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.MediatHandler
{
    public class SendCompleteCommand: INotification
    {
        public string UserId { get; set; }
        public string AppKey { get; set; }
        public string Mobile { get; set; }
        public int Result { get; set; }
        public string Description { get; set; }
        public string UserNonce { get; set; }
        public string Nonce { get; set; }
        public string OperatorType { get; set; }
        public DateTime UserTimeStamp { get; set; }
        public DateTime ReceiveTime { get; set; }
        public DateTime SendSuccessTime { get; set; }
        public DateTime CreateDate { get; set; }
        public string SalesMan { get; set; }
        public string Signature { get; set; }
    }
}
