﻿using Harmony.Application.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmony.Application.Contracts.Messaging
{
    public interface INotificationsPublisher
    {
        void Publish<T>(T notification) where T : BaseNotification;
    }
}
