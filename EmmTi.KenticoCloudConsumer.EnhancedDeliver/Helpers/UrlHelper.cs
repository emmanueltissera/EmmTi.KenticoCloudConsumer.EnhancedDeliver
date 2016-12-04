namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers
{
    /// <summary>
    /// Helps with Friendly URLs 
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Gets the friendly URL.
        /// </summary>
        /// <param name="system">The system.</param>
        /// <returns>An SEO friendly URL</returns>
        public static string GetFriendlyUrl(KenticoCloud.Deliver.System system)
        {
            var url = string.Empty;

            if (system == null)
            {
                return url;
            }

            if (system.SitemapLocations.Count > 0)
            {
                url = $"/{system.SitemapLocations[0]}";
            }

            if (url == "/root" & system.Codename == "home")
            {
                return "/";
            }

            if (url == "/root")
            {
                url = string.Empty;
            }

            var codeName = system.Codename.Replace("_", "-");

            if (codeName.StartsWith("n") && char.IsNumber(codeName, 1))
            {
                codeName = codeName.TrimStart('n');
            }

            url = $"{url}/{codeName}/";

            return url;
        }

        /// <summary>
        /// Gets the kentico identifier from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Gets a Kentico Content ID from the SEO friendly URL</returns>
        public static string GetKenticoIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            if (char.IsNumber(url, 0))
            {
                url = $"n{url}";
            }

            return url.Replace("-", "_");
        }
    }
}