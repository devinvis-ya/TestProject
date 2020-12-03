using System;
using System.Collections.Generic;
using System.Text;

using TestWebApi.Infrastructure.Models.Enums;

namespace TestWebApi.Infrastructure.Models
{
    public class SomeModel
    {
        public SomeModel(StateStatus status) : this()
        {
            Status = status;
        }
        public SomeModel()
        {
            TimeStamp = DateTime.Now;
        }
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public StateStatus Status { get; set; }
    }
}
