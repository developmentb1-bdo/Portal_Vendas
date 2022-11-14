using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;
using SAPB1.DALFactory.Servico;
using SAPB1.IDAL.Servico;

namespace SAPB1.BLL.Servicos
{
    public class ModeloVeiculoBLL
    {
        private readonly IModeloVeiculo _modeloVeiculo;

        public ModeloVeiculoBLL()
        {
            _modeloVeiculo = ModeloVeiculoFactory.ModeloVeiculoDAL();
        }

        public IList<ModeloVeiculoDTO> ListarTodosModelos()
        {
            return _modeloVeiculo.ListarTodosModelos();
        }

        public IList<ModeloVeiculoDTO> ListarANoModeloPorModelo(string modelo)
        {
            return _modeloVeiculo.ListarAnoModeloPorModelo(modelo);
        }

        public IList<ModeloVeiculoDTO> ListarEntreEixosPorAnoModelo(string modelo, string anoModelo)
        {
            return _modeloVeiculo.ListarEntreEixosPorAnoModelo(modelo, anoModelo);
        }
    }
}
