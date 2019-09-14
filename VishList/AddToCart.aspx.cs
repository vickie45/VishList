using System;
using System.Diagnostics;
using System.Web.UI;
using Logic;

public partial class AddToCart : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		var rawId = Request.QueryString["ProductID"];
		if (!string.IsNullOrEmpty(rawId) && int.TryParse(rawId, out var productId))
		{
			using (var usersShoppingCart = new ShoppingCartActions())
			{
				usersShoppingCart.AddToCart(Convert.ToInt16(rawId));
			}

		}
		else
		{
			Debug.Fail("ERROR : We should never get to AddToCart.aspx without a ProductId.");
			throw new Exception("ERROR : It is illegal to load AddToCart.aspx without setting a ProductId.");
		}
		Response.Redirect("ShoppingCart.aspx");
	}
}