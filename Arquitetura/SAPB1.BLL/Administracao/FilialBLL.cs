/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using SAPB1.DALFactory.Administracao;
using SAPB1.DTO.Administracao;
using SAPB1.IDAL.Administracao;

namespace SAPB1.BLL.Administracao
{
    public class FilialBLL
    {
        public FilialBLL() { }

        public IList<FilialDTO> Listar()
        {
            try
            {
                IFilial filialDAL = AdministracaoFactory.FilialDAL();

                return filialDAL.Listar();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}