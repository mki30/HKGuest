<%@ Application Language="C#" %>

<%@ Import Namespace="System.Web.Routing" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        
        InitializeRoutes(RouteTable.Routes);
        DBCheck.UpdateDB("HkGuest");
        //DBCheck.UpdateDB();
        Global.LoadGlobal();
    }

    void Application_BeginRequest(Object source, EventArgs e)
    {
        //HttpApplication app = (HttpApplication)source;
        //HttpContext context = app.Context;
        //host = FirstRequestInitialisation.Initialise(context);

        //if (host.Contains("mydoc.net.in") || host.Contains("localhost"))
        //    Global.IsDoctorDomain = true;
    }

    class FirstRequestInitialisation
    {
        private static string host = null;
        private static Object s_lock = new Object();

        // Initialise only on the first request
        public static string Initialise(HttpContext context)
        {
            if (string.IsNullOrEmpty(host))
            {
                lock (s_lock)
                {
                    if (string.IsNullOrEmpty(host))
                    {
                        Uri uri = HttpContext.Current.Request.Url;
                        host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                    }
                }
            }

            return host;
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
    
    }
    
    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }

    private void InitializeRoutes(RouteCollection routes)
    {
        //routes.Ignore("favicon.ico");
        //routes.Ignore("WebResource.axd");
        //routes.MapPageRoute(routeName: "default5", routeUrl: "by-{Data1}", physicalFile: "~/Default.aspx", checkPhysicalUrlAccess: true, defaults: new RouteValueDictionary() { { "Action", "catlistbyarea" }, { "Data1", "" }, { "Data2", "" } });
    }
       
</script>
