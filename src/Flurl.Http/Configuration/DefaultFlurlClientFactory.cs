﻿using System;
using System.Collections.Concurrent;

namespace Flurl.Http.Configuration
{
	/// <summary>
	/// Default implementation of IFlurlClientFactory used by Flurl.Http. Custom factories looking to extend
	/// Flurl's behavior should inherit from this class, rather than implementing IFlurlClientFactory directly.
	/// </summary>
	public class DefaultFlurlClientFactory : IFlurlClientFactory
	{
		private static readonly ConcurrentDictionary<string, IFlurlClient> _clients = new ConcurrentDictionary<string, IFlurlClient>();

		/// <summary>
		/// Uses a caching strategy of one FlurlClient per host. This maximizes reuse of underlying
		/// HttpClient/Handler while allowing things like cookies to be host-specific.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <returns>The FlurlClient instance.</returns>
		public virtual IFlurlClient Get(Url url) {
			var key = new Uri(url).Host;
			return _clients.GetOrAdd(key, _ => new FlurlClient());
		}
	}
}
