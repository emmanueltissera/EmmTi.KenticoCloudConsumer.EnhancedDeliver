namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers
{
    /// <summary>
    /// Helps with Friendly URLs
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Gets the friendly parent path.
        /// </summary>
        /// <param name="system">The system.</param>
        /// <returns></returns>
        public static string GetFriendlyParentPath(KenticoCloud.Deliver.System system)
        {
            var url = string.Empty;

            if (system == null)
            {
                return url;
            }

            if (system.SitemapLocations.Count > 0)
            {
                url = $"/{TransformPath(system.SitemapLocations[0])}";
            }

            if (url == "/root")
            {
                url = string.Empty;
            }

            return url;
        }

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

            url = GetFriendlyParentPath(system);

            if (url == "/" & system.Codename == "home")
            {
                return "/";
            }

            var codeName = TransformPath(system.Codename);

            return string.IsNullOrEmpty(url) ? $"{codeName}/" : $"{url}/{codeName}/";
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

        /// <summary>
        /// Transforms the path.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        /// <returns>An SEO friendly path</returns>
        private static string TransformPath(string codeName)
        {
            codeName = codeName.Replace("_", "-");

            if (codeName.StartsWith("n") && char.IsNumber(codeName, 1))
            {
                codeName = codeName.TrimStart('n');
            }
            return codeName;
        }
    }
}