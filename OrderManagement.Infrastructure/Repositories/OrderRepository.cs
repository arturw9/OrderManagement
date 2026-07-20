using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Common;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Infrastructure.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<PagedResult<Order>> GetAllAsync(
     OrderStatus? status,
     int page,
     int pageSize,
     CancellationToken cancellationToken)
    {
        IQueryable<Order> query = _context.Orders
            .Include(x => x.Items);

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status);
        }

        var totalItems = await query.CountAsync(cancellationToken);

        var orders = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Order>
        {
            Items = orders,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }
}