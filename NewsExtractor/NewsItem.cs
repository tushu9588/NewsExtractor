﻿using System;

namespace NewsExtractor
{
    /// <summary>
    /// Represents a single news article item with URL, title, and publication date.
    /// </summary>
    public class NewsItem
    {
        /// <summary>
        /// Gets or sets the URL of the news article.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the title of the news article.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the publication date of the news article.
        /// </summary>
        public DateTime PublicationDate { get; set; }
    }
}
