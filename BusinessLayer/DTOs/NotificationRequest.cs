﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class NotificationRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Token { get; set; }
    }
}
