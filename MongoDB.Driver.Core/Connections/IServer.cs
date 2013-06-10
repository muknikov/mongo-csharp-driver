﻿/* Copyright 2010-2013 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Threading;

namespace MongoDB.Driver.Core.Connections
{
    /// <summary>
    /// A logical connection to a remote server.
    /// </summary>
    public interface IServer : IDisposable
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        ServerDescription Description { get; }

        /// <summary>
        /// Gets a channel.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
        /// <returns>A channel.</returns>
        IServerChannel GetChannel(int millisecondsTimeout, CancellationToken cancellationToken);
    }

    public static class IServerExtensions
    {
        /// <summary>
        /// Gets a channel.
        /// </summary>
        /// <returns>A channel.</returns>
        public static IServerChannel GetChannel(this IServer @this)
        {
            return @this.GetChannel(Timeout.Infinite, CancellationToken.None);
        }

        /// <summary>
        /// Gets a channel.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
        /// <returns>A channel.</returns>
        public static IServerChannel GetChannel(this IServer @this, CancellationToken cancellationToken)
        {
            return @this.GetChannel(Timeout.Infinite, cancellationToken);
        }

        /// <summary>
        /// Gets a channel.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
        /// <returns>A channel.</returns>
        public static IServerChannel GetChannel(this IServer @this, int millisecondsTimeout)
        {
            return @this.GetChannel(millisecondsTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Gets a channel.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
        /// <returns>A channel.</returns>
        public static IServerChannel GetChannel(this IServer @this, TimeSpan timeout)
        {
            var millisecondsTimeout = (long)timeout.TotalMilliseconds;
            if (millisecondsTimeout < (long)-1 || millisecondsTimeout > (long)0x7fffffff)
            {
                throw new ArgumentOutOfRangeException("timeout");
            }

            return @this.GetChannel((int)millisecondsTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Gets a channel.
        /// </summary>
        /// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
        /// <returns>A channel.</returns>
        public static IServerChannel GetChannel(this IServer @this, TimeSpan timeout, CancellationToken cancellationToken)
        {
            var millisecondsTimeout = (long)timeout.TotalMilliseconds;
            if (millisecondsTimeout < (long)-1 || millisecondsTimeout > (long)0x7fffffff)
            {
                throw new ArgumentOutOfRangeException("timeout");
            }

            return @this.GetChannel((int)millisecondsTimeout, cancellationToken);
        }
    }
}