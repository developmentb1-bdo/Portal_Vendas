using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.DeskPararelo.Estoque;
using SAPB1.DTO.DeskPararelo.Estoque;
using SAPB1.IDAL.DeskPararelo.Estoque;

namespace SAPB1.BLL.DeskPararelo.Estoque
{
    public class EstoqueDadosBLL
    {
        private readonly IEstoqueDados _estoqueDados;

        public EstoqueDadosBLL()
        {
            _estoqueDados = EstoqueDadosFactory.EstoqueDadosDAL();
        }

        public string RetonarTabelaPopuladaHtml()
        {
            IList<EstoqueDadosDTO> list = _estoqueDados.RetornarDadosEstoque();

            StringBuilder stb = new StringBuilder();

            if (list.Count == 0)
            {
                stb.Append("<table class=\"table table-striped table-bordered\" id=\"tblTab\">");
                stb.Append("</table>");
            }
            else
            {
                stb.Append("<table class=\"table table-striped table-bordered\" id=\"tblTab\">");

                stb.Append("<thead>");
                stb.Append("<tr>");
                stb.Append("<th>Código</th>");
                stb.Append("<th>Descrição</th>");
                stb.Append("<th>Estoque Trânsito</th>");
                stb.Append("<th>Saldo Estoque</th>");
                stb.Append("<th>NFs Emitidas</th>");
                stb.Append("<th>Estoque Real</th>");
                stb.Append("</tr>");
                stb.Append("<thead>");

                stb.Append("<tfoot>");
                stb.Append("<tr>");
                stb.Append("<th>Código</th>");
                stb.Append("<th>Descrição</th>");
                stb.Append("<th>Estoque Trânsito</th>");
                stb.Append("<th>Saldo Estoque</th>");
                stb.Append("<th>NFs Emitidas</th>");
                stb.Append("<th>Estoque Real</th>");
                stb.Append("</tr>");
                stb.Append("<tfoot>");

                stb.Append("<tbody>");

                foreach (var item in list)
                {
                    stb.Append("<tr>");
                    stb.Append("<td>" + item.ItemCode + "</td>");
                    stb.Append("<td>" + item.ItemName + "</td>");
                    stb.Append("<td>" + item.EstoqueTransito.ToString("n6") + "</td>");
                    stb.Append("<td>" + item.SaldoEstoque.ToString("n6") + "</td>");
                    stb.Append("<td>" + item.NfsEmitidas.ToString("n6") + "</td>");
                    stb.Append("<td>" + item.EstoqueReal.ToString("n6") + "</td>");
                    stb.Append("</tr>");
                }

                stb.Append("</tbody>");
                stb.Append("</table>");
            }

            return stb.ToString();
        }
    }
}
