using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tecnicos.Abstractions;
using Tecnicos.Data.Context;
using Tecnicos.Data.Models;
using Tecnicos.Domain.DTO;

namespace Tecnicos.Services;

public class ClientesService(IDbContextFactory<TecnicosContext> DbFactory) : IClientesService
{

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(e => e.ClienteId == id);
    }

    private async Task<bool> Insertar(ClientesDto clienteDto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = new Clientes()
        {
            Nombres = clienteDto.Nombres,
            WhatsApp = clienteDto.WhatsApp
        };
        contexto.Clientes.Add(cliente);
        var guardo = await contexto.SaveChangesAsync() > 0;
        clienteDto.ClienteId = cliente.ClienteId;
        return guardo;
    }

    private async Task<bool> Modificar(ClientesDto clienteDto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = new Clientes()
        {
            ClienteId = clienteDto.ClienteId,
            Nombres = clienteDto.Nombres,
            WhatsApp = clienteDto.WhatsApp
        };
        contexto.Update(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(ClientesDto clienteDto)
    {
        if (!await Existe(clienteDto.ClienteId))
            return await Insertar(clienteDto);
        else
            return await Modificar(clienteDto);
    }

    public async Task<bool> Eliminar(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Where(e => e.ClienteId == clienteId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<ClientesDto> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = await contexto.Clientes
            .Where(e => e.ClienteId == id)
            .Select(p => new ClientesDto()
            {
                ClienteId = p.ClienteId,
                Nombres = p.Nombres,
                WhatsApp = p.WhatsApp
            }).FirstOrDefaultAsync();
        return cliente ?? new ClientesDto();
    }

    public async Task<List<ClientesDto>> Listar(Expression<Func<ClientesDto, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Select(p => new ClientesDto()
            {
                ClienteId = p.ClienteId,
                Nombres = p.Nombres,
                WhatsApp = p.WhatsApp
            })
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteCliente(int id, string nombres)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(e => e.ClienteId != id
            && e.Nombres.ToLower().Equals(nombres.ToLower()));
    }
}