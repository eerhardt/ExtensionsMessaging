// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Messaging.Abstractions;
using System;

namespace Microsoft.Extensions.Messaging.AzureStorageQueue
{
    public class AzureStorageMessageChannel : MessageChannel<Message>
    {
        public AzureStorageMessageChannel(CloudStorageAccount storageAccount, string queueName)
        {
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueName);

            Reader = new AzureStorageQueueReader(queue);
            Writer = new AzureStorageQueueWriter(queue);
        }
    }
}
