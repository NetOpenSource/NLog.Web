﻿using System.Text;
using NLog.LayoutRenderers;
#if !DNX
using System.Web;
#else
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
#endif

namespace NLog.Web.LayoutRenderers
{
    /// <summary>
    /// Base class for ASP.NET layout renderers.
    /// </summary>
    public abstract class AspNetLayoutRendererBase : LayoutRenderer
    {

#if DNX
        /// <summary>
        /// Initializes the <see cref="AspNetLayoutRendererBase"/> with the <see cref="IHttpContextAccessor"/>.
        /// </summary>
        protected AspNetLayoutRendererBase(IHttpContextAccessor accessor)
        {
            HttpContextAccessor = accessor;

        }
#else

        /// <summary>
        /// Initializes the <see cref="AspNetLayoutRendererBase"/>.
        /// </summary>
        protected AspNetLayoutRendererBase()
        {

            HttpContextAccessor = new DefaultHttpContextAccessor();

    }
#endif
        /// <summary>
        /// Provides access to the current request HttpContext.
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        /// <summary>
        /// Validates that the HttpContext is available and delegates append to subclasses.<see cref="StringBuilder" />.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append the rendered data to.</param>
        /// <param name="logEvent">Logging event.</param>
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (HttpContextAccessor.HttpContext == null)
                return;

            DoAppend(builder, logEvent);
        }

        /// <summary>
        /// Implemented by subclasses to render request information and append it to the specified <see cref="StringBuilder" />.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append the rendered data to.</param>
        /// <param name="logEvent">Logging event.</param>
        protected abstract void DoAppend(StringBuilder builder, LogEventInfo logEvent);
    }
}