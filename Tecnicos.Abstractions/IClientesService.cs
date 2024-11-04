using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tecnicos.Domain.DTO;

namespace Tecnicos.Abstractions;

public interface IClientesService
{
    Task<bool> Guardar(ClientesDto cliente);
    Task<bool> Eliminar(int clienteId);
    Task<ClientesDto> Buscar(int id);
    Task<List<ClientesDto>> Listar(Expression<Func<ClientesDto, bool>> criterio);
    Task<bool> ExisteCliente(int id, string nombres);
}
