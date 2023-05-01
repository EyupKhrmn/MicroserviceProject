﻿using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.Model;

public class OrderItem : Entity
{
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string PictureUrl { get; private set; }
    public decimal Price { get; private set; }
    
    public OrderItem(string productId, string productName, string pictureUrl, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        PictureUrl = pictureUrl;
        Price = price;
    }

    public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
    {
        ProductName = productName;
        Price = price;
        PictureUrl = pictureUrl;
    }

}