using System.Configuration;
using KenticoCloud.Deliver;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Factories
{
    /// <summary>
    /// Instance of the Kentico Deliver Client
    /// </summary>
    public class StaticDeliverClient
    {
        /// <summary>
        /// Gets the Kentico Deliver client.
        /// </summary>
        /// <value>
        /// The Kentico Deliver client.
        /// </value>
        protected static DeliverClient Client { get; } = new DeliverClient(ConfigurationManager.AppSettings["ProjectId"]);
    }
}
