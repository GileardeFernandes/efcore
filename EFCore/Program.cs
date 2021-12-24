using System;
using System.Linq;
using EFCore.Domain;
using EFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //InserirDadosEmMassa();
            BuscarDados();
        }

		private static void InserirDadosEmMassa()
		{
           var produto = new Produto{

               Descricao = "produto teste",
               CodigoBarras="99552556",
               valor= 10m,
               TipoProduto = TipoProduto.MercadoriaParaRevenda,
               Ativo = true
           };

           var cliente = new Cliente{

               Nome= "Mical Fernandes",
               CEP= "54300369",
               Estado= "PE",
               Telefone = "81999552266",
               Cidade= "Recife"
           };

           using ( var db  = new Data.ApplicationContext())
           {
              db.AddRange(produto, cliente);
              var registros = db.SaveChanges();
           }
		}

        public static void BuscarDados(){

			using (var db = new Data.ApplicationContext())
			{
               var dados = db.Clientes.Where(p => p.Id > 0 ).ToList();
               var pedidos = db.pedidos.Include(p => p.Itens) //primeiro nível
                                       .ThenInclude(p => p.Produto) //segundo nível
                                       .Where(p => p.Id > 0 ).ToList();
			}
        }
	}
}
