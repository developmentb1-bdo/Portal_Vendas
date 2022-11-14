using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;

namespace SAPB1.IDAL.Servico
{
    public interface IModeloVeiculo
    {
        IList<ModeloVeiculoDTO> ListarTodosModelos();

        IList<ModeloVeiculoDTO> ListarAnoModeloPorModelo(string modelo);

        IList<ModeloVeiculoDTO> ListarEntreEixosPorAnoModelo(string modelo, string anoModelo);
    }
}
