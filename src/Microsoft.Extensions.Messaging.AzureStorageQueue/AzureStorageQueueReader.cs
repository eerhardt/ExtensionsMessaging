// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Messaging.Abstractions;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Messaging.AzureStorageQueue
{
    internal class AzureStorageQueueReader : ChannelReader<Message>
    {
        private CloudQueue _cloudQueue;

        public AzureStorageQueueReader(CloudQueue cloudQueue)
        {
            _cloudQueue = cloudQueue;
        }

        public override bool TryRead(out Message item)
        {
            CloudQueueMessage cloudMessage = _cloudQueue.GetMessage();

            item = new Message()
            {
                Id = cloudMessage.Id,
                Content = cloudMessage.AsString
            };

            return true;
        }

        public override async ValueTask<Message> ReadAsync(CancellationToken cancellationToken = default)
        {
            CloudQueueMessage cloudMessage = await _cloudQueue.GetMessageAsync(cancellationToken)
                 .ConfigureAwait(false);

            return new Message()
            {
                Id = cloudMessage.Id,
                Content = cloudMessage.AsString
            };
        }

        public override async ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default)
        {
            do
            {
                await _cloudQueue.FetchAttributesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            while (_cloudQueue.ApproximateMessageCount == 0);

            return true;
        }
    }
}