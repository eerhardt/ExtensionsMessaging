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
    internal class AzureStorageQueueWriter : ChannelWriter<Message>
    {
        private CloudQueue _cloudQueue;

        public AzureStorageQueueWriter(CloudQueue cloudQueue)
        {
            _cloudQueue = cloudQueue;
        }

        public override bool TryWrite(Message item)
        {
            _cloudQueue.AddMessage(new CloudQueueMessage(item.Content));
            return true;
        }

        public override async ValueTask WriteAsync(Message item, CancellationToken cancellationToken = default)
        {
            await _cloudQueue
                .AddMessageAsync(new CloudQueueMessage(item.Content), cancellationToken)
                .ConfigureAwait(false);
        }

        public override ValueTask<bool> WaitToWriteAsync(CancellationToken cancellationToken = default)
        {
            return new ValueTask<bool>(true);
        }
    }
}