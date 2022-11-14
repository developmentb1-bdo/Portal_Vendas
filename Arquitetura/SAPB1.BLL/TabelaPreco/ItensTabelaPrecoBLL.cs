using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.ItensTabelaPreco;
using SAPB1.DTO.ItensTabelaPreco;
using SAPB1.IDAL.ItensTabelaPreco;
using SAPB1.DTO.Concessionario;
using SAPB1.DALFactory.Concessionario;
using SAPB1.IDAL.Concessionario;

namespace SAPB1.BLL.ItensTabelaPreco
{
    /// <summary>
    /// Regra de negócio da classe ItensTabelaPreco
    /// </summary>
    public class ItensTabelaPrecoBLL
    {
        IItensTabelaPreco itensTabelaPreco = ItensTabelaPrecoFactory.ItensTabelaPrecoDAL();

        /// <summary>
        /// Classe de regra de negócio para listar Itens da Tabela de Preço
        /// </summary>
        /// <param name="itensTabelaDTO">Classe ItenstabelaPrecoDTO</param>
        /// <returns>Lista genérica da classe ItensTabelaPrecoDTO</returns>
        public IList<ItensTabelaPrecoDTO> Listar(ItensTabelaPrecoDTO itensTabelaDTO)
        {
            return itensTabelaPreco.Listar(itensTabelaDTO);
        }

        public IList<ItensTabelaPrecoDTO> ListarComVariosPrecos(string cardCode)
        {
            IConcessionario concessinario = ConcessionarioFactory.ConcessionarioDAL();
            ConcessionarioDTO dadosConcessioanario = concessinario.ObterConcessionarioPorId(cardCode);

            if (!string.IsNullOrEmpty(dadosConcessioanario.CardCode))
            {
                List<string> codTabelas = new List<string>();
                
                if (dadosConcessioanario.ListNum > 0)
                    codTabelas.Add(dadosConcessioanario.ListNum.ToString());

                int tabelaGarantia = 0;

                if (int.TryParse(dadosConcessioanario.U_TabGarant, out tabelaGarantia))
                    codTabelas.Add(tabelaGarantia.ToString());

                int tabelaSugerida = 0;

                if (int.TryParse(dadosConcessioanario.U_TabSuger, out tabelaSugerida))
                    codTabelas.Add(tabelaSugerida.ToString());

                IList<ItensTabelaPrecoDTO> listGeral = itensTabelaPreco.ListarItensDeMaisDeUmaTabelapreco(codTabelas);

                IList<ItensTabelaPrecoDTO> itensNormalizados = new List<ItensTabelaPrecoDTO>();

                IList<ItensTabelaPrecoDTO> itensReposicao = new List<ItensTabelaPrecoDTO>();
                IList<ItensTabelaPrecoDTO> itensGarantia = new List<ItensTabelaPrecoDTO>();
                IList<ItensTabelaPrecoDTO> itensPrecoSugerido = new List<ItensTabelaPrecoDTO>();

                if (listGeral.Count > 0)
                {
                    if (dadosConcessioanario.ListNum > 0 || tabelaSugerida > 0 || tabelaGarantia > 0)
                    {
                        if (dadosConcessioanario.ListNum > 0)
                        {
                            itensReposicao = listGeral.Where(r => r.Lista == dadosConcessioanario.ListNum).ToList();

                            if (itensNormalizados.Count == 0)
                                itensNormalizados = itensReposicao;
                        }

                        if (tabelaGarantia > 0)
                        {
                            itensGarantia = listGeral.Where(r => r.Lista == tabelaGarantia).ToList();

                            if (itensNormalizados.Count == 0)
                                itensNormalizados = itensGarantia;
                        }

                        if (tabelaSugerida > 0)
                        {
                            itensPrecoSugerido = listGeral.Where(r => r.Lista == tabelaSugerida).ToList();

                            if (itensNormalizados.Count == 0)
                                itensNormalizados = itensPrecoSugerido;
                        }
                    }

                    if (itensReposicao.Count > 0)
                    {
                        foreach (ItensTabelaPrecoDTO item in itensNormalizados)
                        {
                            var tbR = itensReposicao.Where(r => r.CodigoItem == item.CodigoItem).ToList();

                            foreach (var i in tbR)
                                item.PrecoReposicao = Convert.ToDouble(i.Price);
                        }
                    }

                    if (itensGarantia.Count > 0)
                    {
                        foreach (ItensTabelaPrecoDTO item in itensNormalizados)
                        {
                            var tbG = itensGarantia.Where(r => r.CodigoItem == item.CodigoItem).ToList();

                            foreach (var i in tbG)
                                item.PrecoGarantia = Convert.ToDouble(i.Price);

                        }
                    }

                    if (itensPrecoSugerido.Count > 0)
                    {
                        foreach (ItensTabelaPrecoDTO item in itensNormalizados)
                        {
                            var tbS = itensPrecoSugerido.Where(r => r.CodigoItem == item.CodigoItem).ToList();

                            foreach (var i in tbS)
                                item.PrecoSugerido = Convert.ToDouble(i.Price);
                        }
                    }

                    return itensNormalizados;
                }
            }

            return new List<ItensTabelaPrecoDTO>();
        }

        public IList<ItensTabelaPrecoDTO> BuscarItensDeMaisDeUmaTabelapreco(string cardCode, ItensTabelaPrecoDTO itensDTO)
        {
            IConcessionario concessinario = ConcessionarioFactory.ConcessionarioDAL();
            ConcessionarioDTO dadosConcessioanario = concessinario.ObterConcessionarioPorId(cardCode);

            if (!string.IsNullOrEmpty(dadosConcessioanario.CardCode))
            {
                List<string> codTabelas = new List<string>();

                if (dadosConcessioanario.ListNum > 0)
                    codTabelas.Add(dadosConcessioanario.ListNum.ToString());

                int tabelaGarantia = 0;

                if (int.TryParse(dadosConcessioanario.U_TabGarant, out tabelaGarantia))
                    codTabelas.Add(tabelaGarantia.ToString());

                int tabelaSugerida = 0;

                if (int.TryParse(dadosConcessioanario.U_TabSuger, out tabelaSugerida))
                    codTabelas.Add(tabelaSugerida.ToString());

                IList<ItensTabelaPrecoDTO> listGeral = itensTabelaPreco.BuscarItensDeMaisDeUmaTabelapreco(codTabelas, itensDTO);

                IList<ItensTabelaPrecoDTO> itensNormalizados = new List<ItensTabelaPrecoDTO>();

                IList<ItensTabelaPrecoDTO> itensReposicao = new List<ItensTabelaPrecoDTO>();
                IList<ItensTabelaPrecoDTO> itensGarantia = new List<ItensTabelaPrecoDTO>();
                IList<ItensTabelaPrecoDTO> itensPrecoSugerido = new List<ItensTabelaPrecoDTO>();

                if (listGeral.Count > 0)
                {
                    if (dadosConcessioanario.ListNum > 0 || tabelaSugerida > 0 || tabelaGarantia > 0)
                    {
                        if (dadosConcessioanario.ListNum > 0)
                        {
                            itensReposicao = listGeral.Where(r => r.Lista == dadosConcessioanario.ListNum).ToList();

                            if (itensNormalizados.Count == 0)
                                itensNormalizados = itensReposicao;
                        }

                        if (tabelaGarantia > 0)
                        {
                            itensGarantia = listGeral.Where(r => r.Lista == tabelaGarantia).ToList();

                            if (itensNormalizados.Count == 0)
                                itensNormalizados = itensGarantia;
                        }

                        if (tabelaSugerida > 0)
                        {
                            itensPrecoSugerido = listGeral.Where(r => r.Lista == tabelaSugerida).ToList();

                            if (itensNormalizados.Count == 0)
                                itensNormalizados = itensPrecoSugerido;
                        }
                    }

                    if (itensReposicao.Count > 0)
                    {
                        foreach (ItensTabelaPrecoDTO item in itensNormalizados)
                        {
                            var tbR = itensReposicao.Where(r => r.CodigoItem == item.CodigoItem).ToList();

                            foreach (var i in tbR)
                                item.PrecoReposicao = Convert.ToDouble(i.Price);
                        }
                    }

                    if (itensGarantia.Count > 0)
                    {
                        foreach (ItensTabelaPrecoDTO item in itensNormalizados)
                        {
                            var tbG = itensGarantia.Where(r => r.CodigoItem == item.CodigoItem).ToList();

                            foreach (var i in tbG)
                                item.PrecoGarantia = Convert.ToDouble(i.Price);

                        }
                    }

                    if (itensPrecoSugerido.Count > 0)
                    {
                        foreach (ItensTabelaPrecoDTO item in itensNormalizados)
                        {
                            var tbS = itensPrecoSugerido.Where(r => r.CodigoItem == item.CodigoItem).ToList();

                            foreach (var i in tbS)
                                item.PrecoSugerido = Convert.ToDouble(i.Price);
                        }
                    }

                    return itensNormalizados;
                }
            }

            return new List<ItensTabelaPrecoDTO>();
        }

        public IList<ItensTabelaPrecoDTO> ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco(string codTabela)
        {
            return itensTabelaPreco.ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco(codTabela);
        }
    }
}
