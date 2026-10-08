using System;
using System.Collections.Generic;
using System.Linq;
using PizzApp.Models;
using Microsoft.EntityFrameworkCore;
using PizzApp.Data;

namespace PizzApp.Services;

public class CartService
{
    // The scoped cart belongs to the current Blazor circuit; checkout copies its values into a database order.
    private readonly List<CartItem> _items = new();

    private readonly IDbContextFactory<PizzAppDbContext> _dbFactory;

    public CartService(
        IDbContextFactory<PizzAppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public IReadOnlyList<CartItem> Items => _items;

    public decimal DeliveryFee { get; private set; } = 3.50m;

    public decimal TaxRate { get; private set; } = 0.06m;

    public decimal SalesTaxPercent =>
        TaxRate * 100m;

    public decimal Subtotal =>
        _items.Sum(item => item.TotalPrice);

    public decimal Tax =>
        Subtotal * TaxRate;

    public decimal Total =>
        Subtotal + DeliveryFee + Tax;

    public int ItemCount =>
        _items.Sum(item => item.Quantity);

    public event Action? OnChange;

    public async Task LoadStoreSettingsAsync()
    {
        await using var db =
            await _dbFactory.CreateDbContextAsync();

        var settings =
            await db.StoreSettings
                .AsNoTracking()
                .OrderBy(item => item.Id)
                .FirstOrDefaultAsync();

        if (settings is null)
        {
            // Keep the safe in-memory defaults when the store has not been initialized yet.
            return;
        }

        DeliveryFee = settings.DeliveryFee;
        TaxRate = settings.SalesTaxPercent / 100m;

        NotifyStateChanged();
    }

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
