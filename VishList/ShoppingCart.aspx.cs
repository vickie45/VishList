﻿using System;
using System.Collections.Generic;
using Logic;
using Models;
using System.Collections.Specialized;
using System.Collections;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCart : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		using (var usersShoppingCart = new ShoppingCartActions())
		{
			var cartTotal = usersShoppingCart.GetTotal();
			if (cartTotal > 0)
			{
				// Display Total.
				lblTotal.Text = $"{cartTotal:c}";
			}
			else
			{
				LabelTotalText.Text = "";
				lblTotal.Text = "";
				ShoppingCartTitle.InnerText = "Shopping Cart is Empty";
				UpdateBtn.Visible = false;
				CheckoutImageBtn.Visible = false;

			}
		}
	}

	public List<CartItem> GetShoppingCartItems()
	{
		var actions = new ShoppingCartActions();
		return actions.GetCartItems();
	}

	public List<CartItem> UpdateCartItems()
	{
		using (var usersShoppingCart = new ShoppingCartActions())
		{
			var cartId = usersShoppingCart.GetCartId();

			ShoppingCartActions.ShoppingCartUpdates[] cartUpdates = new ShoppingCartActions.ShoppingCartUpdates[CartList.Rows.Count];
			for (int i = 0; i < CartList.Rows.Count; i++)
			{
				IOrderedDictionary rowValues;
				rowValues = GetValues(CartList.Rows[i]);
				cartUpdates[i].ProductId = Convert.ToInt32(rowValues["ProductID"]);

				CheckBox cbRemove = new CheckBox();
				cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remove");
				cartUpdates[i].RemoveItem = cbRemove.Checked;

				TextBox quantityTextBox = new TextBox();
				quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");
				cartUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
			}
			usersShoppingCart.UpdateShoppingCartDatabase(cartId, cartUpdates);
			CartList.DataBind();
			lblTotal.Text = String.Format("{0:c}", usersShoppingCart.GetTotal());
			return usersShoppingCart.GetCartItems();
		}
	}

	public static IOrderedDictionary GetValues(GridViewRow row)
	{
		IOrderedDictionary values = new OrderedDictionary();
		foreach (DataControlFieldCell cell in row.Cells)
		{
			if (cell.Visible)
			{
				// Extract values from the cell.
				cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
			}
		}
		return values;
	}

	protected void UpdateBtn_Click(object sender, EventArgs e)
	{
		UpdateCartItems();
	}
	protected void CheckoutBtn_Click(object sender, ImageClickEventArgs e)
	{
		using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
		{
			Session["payment_amt"] = usersShoppingCart.GetTotal();
		}
		Response.Redirect("Checkout/CheckoutStart.aspx");
	}
}