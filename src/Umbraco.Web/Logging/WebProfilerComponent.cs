﻿using System;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;

namespace Umbraco.Web.Logging
{
    internal sealed class WebProfilerComponent : IComponent
    {
        private readonly WebProfiler _profiler;
        private readonly bool _profile;
        private readonly List<Action> _terminate = new List<Action>();

        public WebProfilerComponent(IProfiler profiler, ILogger logger)
        {
            _profile = true;

            // although registered in WebRuntime.Compose, ensure that we have not
            // been replaced by another component, and we are still "the" profiler
            _profiler = profiler as WebProfiler;
            if (_profiler != null) return;

            // if VoidProfiler was registered, let it be known
            if (profiler is VoidProfiler)
                logger.Info<WebProfilerComponent>("Profiler is VoidProfiler, not profiling (must run debug mode to profile).");
            _profile = false;
        }

        public void Initialize()
        {
            if (!_profile) return;

            // bind to ApplicationInit - ie execute the application initialization for *each* application
            // it would be a mistake to try and bind to the current application events
            UmbracoApplicationBase.ApplicationInit += InitializeApplication;
        }

        public void Terminate()
        {
            UmbracoApplicationBase.ApplicationInit -= InitializeApplication;
            foreach (var t in _terminate) t();
        }

        private void InitializeApplication(object sender, EventArgs args)
        {
            if (!(sender is HttpApplication app)) return;

            // for *each* application (this will run more than once)
            void beginRequest(object s, EventArgs a) => _profiler.UmbracoApplicationBeginRequest(s, a);
            app.BeginRequest += beginRequest;
            _terminate.Add(() => app.BeginRequest -= beginRequest);

            void endRequest(object s, EventArgs a) => _profiler.UmbracoApplicationEndRequest(s, a);
            app.EndRequest += endRequest;
            _terminate.Add(() => app.EndRequest -= endRequest);
        }
    }
}
