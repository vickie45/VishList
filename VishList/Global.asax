<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data.Entity" %>
<%@ Import Namespace="VishList" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="Models" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        // Initialize the product database.
        Database.SetInitializer(new ProductDatabaseInitializer());
    }

</script>
