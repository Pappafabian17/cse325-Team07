using System;
using System.Collections.Generic;
using System.Linq;
using PizzApp.Models;

namespace PizzApp.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();

    public IReadOnlyList<CartItem> Items => _items;

    public const decimal DeliveryFee = 3.50m;

    public const decimal TaxRate = 0.06m;

    public decimal Subtotal =>
        _items.Sum(item => item.TotalPrice);

    public decimal Tax =>
        Subtotal * TaxRate;

    public decimal Total =>
        Subtotal + DeliveryFee + Tax;

    public int ItemCount =>
        _items.Sum(item => item.Quantity);

    public event Action? OnChange;

    public void AddItem(CartItem item)
    {
        _items.Add(item);
        NotifyStateChanged();
    }

    public CartItem? GetItemCopy(Guid cartItemId)
    {
        var item = _items.FirstOrDefault(
            item => item.CartItemId == cartItemId);

        return item?.Copy();
    }

    public bool ReplaceItem(Guid cartItemId, CartItem replacement)
    {
        var index = _items.FindIndex(
            item => item.CartItemId == cartItemId);

        if (index < 0)
        {
            return false;
        }

        var storedItem = replacement.Copy();
        storedItem.CartItemId = cartItemId;

        _items[index] = storedItem;

        NotifyStateChanged();

        return true;
    }

    public void RemoveItem(CartItem item)
    {
        _items.Remove(item);
        NotifyStateChanged();
    }

    public void UpdateQuantity(CartItem item, int quantity)
    {
        if (!_items.Contains(item))
        {
            return;
        }

        if (quantity <= 0)
        {
            RemoveItem(item);
            return;
        }

        item.Quantity = quantity;
        NotifyStateChanged();
    }

    public void Clear()
    {
        _items.Clear();
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}