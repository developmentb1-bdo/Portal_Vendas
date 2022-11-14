using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAPB1.WebForms.Foton
{
    public partial class DocumentoTecnico_Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("<table class=\"table table-bordered table-striped\">");
                stb.Append("<tr><th>序号</th><th>图号</th><th>名称</th><th>层级</th><th>版本</th><th>数量</th><th>配件属性</th><th>附加说明</th><th>备注</th></tr>");
                stb.Append("<tr>");
                stb.Append("<td></td>");
                stb.Append("<td>1S11293420110</td>");
                stb.Append("<td>转向传动装置</td>");
                stb.Append("<td>1</td>");
                stb.Append("<td>B</td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("</tr>");

                stb.Append("<tr>");
                stb.Append("<td>1</td>");
                stb.Append("<td>L0342020043A0</td>");
                stb.Append("<td>转向盘总成</td>");
                stb.Append("<td>2</td>");
                stb.Append("<td>A</td>");
                stb.Append("<td>1</td>");
                stb.Append("<td>Y</td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("</tr>");

                stb.Append("<tr>");
                stb.Append("<td>2</td>");
                stb.Append("<td>1104934200143</td>");
                stb.Append("<td>挡圈</td>");
                stb.Append("<td>2</td>");
                stb.Append("<td>A</td>");
                stb.Append("<td>2</td>");
                stb.Append("<td>Y</td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("</tr>");

                stb.Append("<tr>");
                stb.Append("<td>3</td>");
                stb.Append("<td>1104934200125</td>");
                stb.Append("<td>转向管柱支架焊合</td>");
                stb.Append("<td>2</td>");
                stb.Append("<td>C</td>");
                stb.Append("<td>1</td>");
                stb.Append("<td>Y</td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("</tr>");

                stb.Append("<tr>");
                stb.Append("<td>4</td>");
                stb.Append("<td>1104934200191</td>");
                stb.Append("<td>防尘套</td>");
                stb.Append("<td>2</td>");
                stb.Append("<td>A</td>");
                stb.Append("<td>1</td>");
                stb.Append("<td>Y</td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("</tr>");

                stb.Append("<tr>");
                stb.Append("<td>5</td>");
                stb.Append("<td>1104934200102</td>");
                stb.Append("<td>转向操纵机构总成</td>");
                stb.Append("<td>2</td>");
                stb.Append("<td>A</td>");
                stb.Append("<td>1</td>");
                stb.Append("<td>Y</td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("</tr>");

                stb.Append("<tr>");
                stb.Append("<td>6</td>");
                stb.Append("<td>1104934200150</td>");
                stb.Append("<td>转向管柱焊合</td>");
                stb.Append("<td>3</td>");
                stb.Append("<td>A</td>");
                stb.Append("<td>1</td>");
                stb.Append("<td>Y</td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("</tr>");

                stb.Append("<tr>");
                stb.Append("<td>7</td>");
                stb.Append("<td>1104934200160</td>");
                stb.Append("<td>转向传动轴总成</td>");
                stb.Append("<td>3</td>");
                stb.Append("<td>A</td>");
                stb.Append("<td>1</td>");
                stb.Append("<td>Y</td>");
                stb.Append("<td></td>");
                stb.Append("<td></td>");
                stb.Append("</tr>");

                stb.Append("<tr>");
                stb.Append("<td colspan='9'><img src=\"Imagens/imagemPeca2.png\" /></td>");
                stb.Append("</tr>");

                stb.Append("</table>");

                ltrCatalogos.Text = stb.ToString();
            }
        }
    }
}