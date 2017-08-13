namespace StevenVolckaert.VrtNu
{
    using System;

    /// <summary>
    ///     Represents a live channel.
    /// </summary>
    public class LiveChannel
    {
        /// <summary>
        ///     Gets or sets the name of the live channel.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Uri of the live channel.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        ///     Returns a string that represents the live channel.
        /// </summary>
        /// <returns>A string that represents the live channel.</returns>
        public override string ToString()
        {
            return Name.IsNullOrWhiteSpace() ? base.ToString() : Name;
        }
    }
}
